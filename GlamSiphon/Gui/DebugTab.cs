using System;
using ImGuiNET;
using OtterGui.Raii;
using OtterGui.Widgets;


namespace GlamSiphon.Gui;

public class DebugTab : ITab
{
    public ReadOnlySpan<byte> Label
        => "Debug"u8;

    public void DrawContent()
    {
        using var child = ImRaii.Child("MainWindowChild");
        if (!child)
            return;
        
        ImGui.TextUnformatted( $"This is the Debugging tab." );
    }
}
