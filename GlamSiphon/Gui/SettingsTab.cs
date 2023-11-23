using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Dalamud.Interface;
using Dalamud.Interface.Components;
using Dalamud.Interface.Internal.Windows.Settings.Widgets;
using Dalamud.Interface.Utility;
using GlamSiphon.Exporters;
using GlamSiphon.Services;
using OtterGui.Raii;
using OtterGui.Widgets;
using ImGuiNET;
using OtterGui;


namespace GlamSiphon.Gui;

public class SettingsTab : ITab
{
    public ReadOnlySpan<byte> Label
        => "Settings"u8;

    private readonly Configuration _config;

    private List<string> GlamourApplyType = new() { "Disable", "Applied", "Use Selected" };

    private int TestOption = 0;

    private string temporaryExportDirectory = string.Empty;

    private string exportDirectoryError = string.Empty;

    private readonly ClippedSelectableCombo<IModelExporter> _exportTypeCombo;
    private          int                                    _exportTypeSelection;

    public SettingsTab( Configuration config )
    {
        _config                  = config ?? throw new ArgumentNullException( nameof(config) );
        temporaryExportDirectory = _config.ExportDirectory;
        
        _exportTypeCombo = new ClippedSelectableCombo<IModelExporter>( "id", "Export Type", ImGui.GetTextLineHeight(),
                                                                       ExportService.RegisteredExporters, // TODO: Update if new exporter is registered
                                                                       ( IModelExporter m ) => m.DisplayName );
    }

    public void DrawContent()
    {
        using var child = ImRaii.Child( "MainWindowChild" );
        if ( !child )
            return;


        if ( ImGui.CollapsingHeader( "Model Options" ) )
        {
            ImGuiUtil.LabeledHelpMarker( "Labeled Help Marker",
                                         "Choose the sort mode for the mod selector in the designs tab." );

            ImGui.Separator();
            ImGui.Dummy( new Vector2( ImGui.GetTextLineHeight() / 2 ) );
            Checkbox( "Include Penumbra?", "Should current Penumbra modifications be included?",
                      _config.IncludePenumbra,
                      v => _config.IncludePenumbra = v );
            Checkbox( "Include Glamourer?", "Should Glamourer modifications be included?", _config.IncludeGlamourer,
                      v => _config.IncludeGlamourer = v );
            ImGui.Dummy( new Vector2( ImGui.GetTextLineHeight() / 2 ) );
            ImGui.Combo( "Glamour Apply Type", ref TestOption, GlamourApplyType.ToArray(), GlamourApplyType.Count );
            ImGui.Dummy( Vector2.One );
            ImGui.TextUnformatted( GlamourApplyType[ Math.Clamp( TestOption, 0, GlamourApplyType.Count - 1 ) ] );
        }

        if ( ImGui.CollapsingHeader( "Export Options" ) )
        {
            // Export Directory
            ImGui.TextUnformatted( "Export Directory" );
            ImGui.Columns( 2 );
            ImGui.SetColumnWidth(
                0,
                (float)( (double)ImGui.GetWindowContentRegionMax().X - (double)ImGui.GetWindowContentRegionMin().X -
                         48.0                                        - 76.0 * (double)ImGuiHelpers.GlobalScale ) );
            ImGui.SetColumnWidth( 1, (float)( 14.0 + 26.0 * (double)ImGuiHelpers.GlobalScale ) );

            ImGui.SetNextItemWidth( -1f );
            ImGui.InputText( "##devPluginLocationInput", ref temporaryExportDirectory, 300U );
            ImGui.NextColumn();
            if ( ImGuiComponents.IconButton( FontAwesomeIcon.Save ) )
            {
                if ( string.IsNullOrWhiteSpace( temporaryExportDirectory ) )
                {
                    exportDirectoryError = "An export directory is required.";
                    Task.Delay( 3000 )
                        .ContinueWith<string>( (Func<Task, string>)( t => exportDirectoryError = string.Empty ) );
                }
                else if ( IsExportDirectoryValid( temporaryExportDirectory ) )
                {
                    _config.ExportDirectory = temporaryExportDirectory;
                    exportDirectoryError    = "Export directory updated.";
                    Task.Delay( 3000 )
                        .ContinueWith<string>( (Func<Task, string>)( t => exportDirectoryError = string.Empty ) );
                }
                else
                {
                    exportDirectoryError = "Directory is not valid.";
                    Task.Delay( 3000 )
                        .ContinueWith<string>( (Func<Task, string>)( t => exportDirectoryError = string.Empty ) );
                }
            }

            ImGui.Columns( 1 );
            if ( ImGui.Button( "Reset" ) )
            {
                temporaryExportDirectory = _config._saveService.FileNames.DefaultExportDirectory;
                exportDirectoryError     = "Export directory reset to default.";
                Task.Delay( 3000 )
                    .ContinueWith<string>( (Func<Task, string>)( t => exportDirectoryError = string.Empty ) );
            }

            ImGuiHelpers.SafeTextColoredWrapped( new Vector4( 1f, 0.0f, 0.0f, 1f ), exportDirectoryError );
            ImGuiHelpers.ScaledDummy( 5f );


            // Export Type
            _exportTypeCombo.Draw("Export Type", out _exportTypeSelection, ImGuiComboFlags.HeightRegular);
            ImGui.TextUnformatted( _exportTypeSelection <= ExportService.RegisteredExporters.Count
                                       ? ExportService.RegisteredExporters[_exportTypeSelection].DisplayName
                                       : "UNKNOWN EXPORTER" );

        }

        if ( ImGui.CollapsingHeader( "Other" ) )
        {
            Checkbox( "Debug Mode",
                      "Show the debug tab. Only useful for debugging or advanced use. Not recommended in general.",
                      _config.DebugMode,
                      v => _config.DebugMode = v );
        }

        if ( ImGui.Button( "Apply##CustomizeCharacter" ) )
            GlamSiphon.Log.Information( "Congratulations, you have clicked the apply settings button." );
    }

    [MethodImpl( MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization )]
    private void Checkbox( string label, string tooltip, bool current, Action<bool> setter )
    {
        using var id  = ImRaii.PushId( label );
        var       tmp = current;
        if ( ImGui.Checkbox( string.Empty, ref tmp ) && tmp != current )
        {
            setter( tmp );
            _config.Save();
        }

        ImGui.SameLine();
        ImGuiUtil.LabeledHelpMarker( label, tooltip );
    }

    private bool IsExportDirectoryValid( string newPath )
    {
        return Path.Exists( newPath );
    }
}
