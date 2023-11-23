using System;
using ImGuiNET;
using OtterGui.Widgets;


namespace GlamSiphon.Gui;

public class ActorTab : ITab
{
    public ReadOnlySpan<byte> Label
        => "Actors"u8;

    public void DrawContent()
    {
        ImGui.TextUnformatted( $"This is the actors tab." );
    }
}
