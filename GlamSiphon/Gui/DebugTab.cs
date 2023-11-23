using System;
using Dalamud.Plugin.Services;
using GlamSiphon.Exporters;
using GlamSiphon.Services;
using ImGuiNET;
using OtterGui.Raii;
using OtterGui.Widgets;


namespace GlamSiphon.Gui;

public class DebugTab : ITab
{
    public ReadOnlySpan<byte> Label
        => "Debug"u8;

    private readonly IChatGui      _chatGui;
    private readonly ExportService _exportService;

    public DebugTab(IChatGui chatGui, ExportService exportService )
    {
        _chatGui       = chatGui;
        _exportService = exportService;
    }

    public void DrawContent()
    {
        using var child = ImRaii.Child("MainWindowChild");
        if (!child)
            return;
        
        ImGui.TextUnformatted( $"This is the Debugging tab." );

        if ( ImGui.Button( "##Export Yourself" ) )
        {
            var          exporters     = ExportService.RegisteredExporters;
            FbxExporter? debugExporter = null;
            foreach ( var exporter in exporters )
            {
                if( exporter is FbxExporter fbxExporter)
                {
                    debugExporter = fbxExporter;
                    break;
                }
            }

            if ( debugExporter == null )
            {
                _chatGui.PrintError( "Could not retrieve the FBX exporter!" );
            }
            else
            {
                debugExporter.Debug_ExportSelf(_chatGui);
            }
        }
    }
}
