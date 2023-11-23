using System;
using System.Numerics;
using Dalamud.Interface.Utility;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin;
using GlamSiphon.Events;
using ImGuiNET;
using OtterGui.Custom;
using OtterGui.Widgets;

namespace GlamSiphon.Gui;

public class MainWindow : Window, IDisposable
{
    public enum TabType
    {
        None     = -1,
        Settings = 0,
        Debug    = 1,
        Actors   = 2
    }

    private readonly Configuration   _config;
    private readonly TransientConfig _transientConfig;
    private readonly TabSelected     _event;
    private readonly ITab[]          _tabs;

    public readonly SettingsTab Settings;
    public readonly DebugTab    Debug;
    public readonly ActorTab    Actors;

    public TabType SelectTab = TabType.None;

    public MainWindow(
        DalamudPluginInterface pi,
        Configuration          config,
        TransientConfig        transientConfig,
        TabSelected            @event,
        SettingsTab            settingsTab,
        DebugTab               debugTab,
        ActorTab               actorTab ) : base( "My Amazing Window" )
    {
        // Window Setup
        pi.UiBuilder.DisableGposeUiHide = true;
        this.SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2( 375,            330 ),
            MaximumSize = new Vector2( float.MaxValue, float.MaxValue )
        };


        // Get tab singletons
        Settings = settingsTab;
        Debug    = debugTab;
        Actors   = actorTab;
        _tabs = new ITab[]
        {
            Settings,
            Debug,
            Actors
        };

        // Add services/events
        _config          = config;
        _transientConfig = transientConfig;
        _event           = @event;
        _event.Subscribe( OnTabSelected, TabSelected.Priority.MainWindow );
    }

    public void Dispose()
    {
        _event.Unsubscribe( OnTabSelected );
    }
    
    public override void Draw()
    {
        var yPos = ImGui.GetCursorPosY();
        if ( TabBar.Draw( "##tabs", ImGuiTabBarFlags.None, ToLabel( SelectTab ), out var currentTab, () => { },
                          _tabs ) )
        {
            SelectTab                    = TabType.None;
            _transientConfig.SelectedTab = FromLabel( currentTab );
        }
    }

    private void OnTabSelected( TabType type )
    {
        SelectTab = type;
        IsOpen    = true;
    }
    
    private ReadOnlySpan<byte> ToLabel( TabType type )
        => type switch
        {
            TabType.Settings => Settings.Label,
            TabType.Debug    => Debug.Label,
            TabType.Actors   => Actors.Label,
            _                => ReadOnlySpan<byte>.Empty,
        };

    private TabType FromLabel( ReadOnlySpan<byte> label )
    {
        // @formatter:off
        if (label == Settings.Label)   return TabType.Settings;
        if (label == Debug.Label)      return TabType.Debug;
        if (label == Actors.Label)     return TabType.Actors;
        // @formatter:on
        return TabType.None;
    }
}
