using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using System.IO;
using System.Reflection;
using System.Text.Json;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin.Services;
using Microsoft.Extensions.DependencyInjection;
using OtterGui.Classes;
using OtterGui.Log;
using GlamSiphon.Gui;
using Dalamud.Interface;
using GlamSiphon.Services;

namespace GlamSiphon;

public class GlamSiphon : IDalamudPlugin
{
    public static string Name => "GlamSiphon";
    public static readonly string Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? string.Empty;
    public static readonly string CommitHash =
        Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "Unknown";

    public static readonly Logger Log = new();
    public static MessageService Messager { get; private set; } = null!;

    public static JsonSerializerOptions SerializerOptions { get; private set; } =
        new JsonSerializerOptions( JsonSerializerDefaults.General )
        {
            AllowTrailingCommas = true,
            WriteIndented = true
        };
    
    private readonly ServiceProvider _services;

    public GlamSiphon( DalamudPluginInterface pluginInterface, ICommandManager commandManager )
    {
        try
        {
            Log.Information($"{Name}: Starting services.");
            _services = ServiceManager.CreateProvider(pluginInterface, Log);
            Messager  = _services.GetRequiredService<MessageService>();
            // _services.GetRequiredService<StateListener>();         // Initialize State Listener.
            _services.GetRequiredService<GSWindowService>(); // initialize ui.
            _services.GetRequiredService<CommandService>();        // initialize commands.
            // _services.GetRequiredService<VisorService>();
            // _services.GetRequiredService<ScalingService>();
            Log.Information($"GlamSiphon v{Version} loaded successfully.");
            
            
        }
        catch
        {
            IPluginLog? logger = _services?.GetService<IPluginLog>();
            logger?.Fatal( $"{Name}: unable to start!" );

            Dispose();
            throw;
        }
    }

    public void Dispose()
    {
        _services?.Dispose();
    }
}
