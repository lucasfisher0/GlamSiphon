using System;
using Dalamud.Interface;
using Dalamud.Interface.Windowing;
using GlamSiphon.Gui;

namespace GlamSiphon.Services;

public class GlamourerWindowSystem : IDisposable
{
    private readonly WindowSystem _windowSystem = new(GlamSiphon.Name);
    private readonly UiBuilder    _uiBuilder;
    private readonly MainWindow   _ui;

    public GlamourerWindowSystem(
        UiBuilder     uiBuilder,
        MainWindow    ui,
        Configuration config )
    {
        _uiBuilder = uiBuilder;
        _ui        = ui;

        _uiBuilder.Draw         += _windowSystem.Draw;
        _uiBuilder.OpenConfigUi += _ui.Toggle;
    }

    public void Dispose()
    {
        _uiBuilder.Draw         -= _windowSystem.Draw;
        _uiBuilder.OpenConfigUi -= _ui.Toggle;
    }
}
