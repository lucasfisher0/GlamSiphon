using Dalamud.Configuration;
using Dalamud.Plugin;
using System;
using System.IO;
using Dalamud.Game.Command;
using Dalamud.Plugin.Services;
using Newtonsoft.Json;
using OtterGui.Classes;
using OtterGui.Log;
using GlamSiphon.Gui;


namespace GlamSiphon.Services;

public class CommandService : IDisposable
{
    private const string WindowCommand = "/glamout";
    private const string ExportCommand = "/glamexport";

    private readonly ICommandManager _commands;
    private readonly MainWindow      _mainWindow;
    private readonly IChatGui        _chat;
    private readonly Configuration   _config;

    public CommandService(
        ICommandManager commands, MainWindow mainWindow, IChatGui chat, Configuration config )
    {
        _commands          = commands;
        _mainWindow        = mainWindow;
        _chat              = chat;
        _config            = config;

        _commands.AddHandler( WindowCommand,
                              new CommandInfo( ToggleWindow ) { HelpMessage = "Toggle visibility of the main window." } );
        _commands.AddHandler( ExportCommand,
                              new CommandInfo( OnExport ) { HelpMessage = "Instantly export your character's model with current settings." } );
    }
    
    private void ToggleWindow(string command, string arguments)
    {
        _chat.Print( "Toggling window visibility." );
        _mainWindow.Toggle();
    }
    
    private void OnExport(string command, string arguments)
    {
        _chat.Print("Not implemented.");
    }

    public void Dispose()
    {
        _commands.RemoveHandler( WindowCommand );
        _commands.RemoveHandler( ExportCommand );
    }
}
