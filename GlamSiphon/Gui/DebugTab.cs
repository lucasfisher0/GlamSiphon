using System;
using ImGuiNET;
using OtterGui.Widgets;


namespace GlamSiphon.Gui;

public class DebugTab : ITab
{
    public ReadOnlySpan<byte> Label
        => "Debug"u8;

    public void DrawContent()
    {
        ImGui.TextUnformatted( $"This is the Debugging tab." );
    }
}
