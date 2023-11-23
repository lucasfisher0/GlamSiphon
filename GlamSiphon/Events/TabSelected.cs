using System;
using OtterGui.Widgets;
using OtterGui.Classes;
using GlamSiphon.Gui;


namespace GlamSiphon.Events;

public sealed class TabSelected : EventWrapper<Action<MainWindow.TabType>, TabSelected.Priority>
{
    public enum Priority
    {
        MainWindow = 0,
    }

    public TabSelected() : base( nameof(TabSelected) ) { }

    public void Invoke( MainWindow.TabType type )
        => Invoke( this, type );
}
