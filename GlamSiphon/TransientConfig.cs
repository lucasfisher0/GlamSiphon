using Dalamud.Configuration;
using Dalamud.Plugin;
using System;
using System.IO;
using GlamSiphon.Gui;
using GlamSiphon.Services;
using Newtonsoft.Json;
using OtterGui.Classes;
using OtterGui.Log;


namespace GlamSiphon;

public class TransientConfig
{
    public MainWindow.TabType SelectedTab { get; set; } = MainWindow.TabType.None;
}
