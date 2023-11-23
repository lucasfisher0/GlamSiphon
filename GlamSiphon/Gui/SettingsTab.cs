using System;
using System.Runtime.CompilerServices;
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

    public SettingsTab( Configuration config )
    {
        _config = config ?? throw new ArgumentNullException( nameof(config) );
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

            Checkbox( "Include Penumbra?", "Should Penumbra modifications be included?", _config.IncludePenumbra,
                      v => _config.IncludePenumbra = v );
            Checkbox( "Include Glamourer?", "Should Glamourer modifications be included?", _config.IncludeGlamourer,
                      v => _config.IncludeGlamourer = v );
        }

        if ( ImGui.CollapsingHeader( "Export Options" ) ) { }

        if ( ImGui.CollapsingHeader( "Other" ) )
        {
            Checkbox( "Debug Mode",
                      "Show the debug tab. Only useful for debugging or advanced use. Not recommended in general.",
                      _config.DebugMode,
                      v => _config.DebugMode = v );
        }
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
}
