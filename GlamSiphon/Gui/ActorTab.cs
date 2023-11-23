using System;
using ImGuiNET;
using OtterGui.Raii;
using OtterGui.Widgets;


namespace GlamSiphon.Gui;

public class ActorTab : ITab
{
    public ReadOnlySpan<byte> Label
        => "Actors"u8;

    public void DrawContent()
    {
        using var child = ImRaii.Child("MainWindowChild");
        if (!child)
            return;
        
        ImGui.TextUnformatted( $"This is the actors tab." );
    }
}
