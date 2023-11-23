using Dalamud.Configuration;
using Dalamud.Plugin;
using System;
using GlamSiphon.Services;
using Newtonsoft.Json.Serialization;
using System.IO;
using Dalamud.Interface.Internal.Notifications;
using GlamSiphon.Services;
using Newtonsoft.Json;
using OtterGui.Classes;
using ErrorEventArgs = Newtonsoft.Json.Serialization.ErrorEventArgs;


namespace GlamSiphon;

[Serializable]
public partial class Configuration : IPluginConfiguration, ISavable
{
    public static class Constants
    {
        public const byte CurrentVersion = 0;
    }

    public int  Version          { get; set; } = Constants.CurrentVersion;
    public bool IncludePenumbra  { get; set; } = true;
    public bool IncludeGlamourer { get; set; } = true;

#if DEBUG
    public bool DebugMode { get; set; } = true;
#else
    public bool DebugMode { get; set; } = false;
#endif

    [JsonIgnore]
    private readonly SaveService _saveService;

    public Configuration( SaveService saveService )
    {
        _saveService = saveService;
        Load();
    }

    public void Save()
        => _saveService.DelaySave( this );

    public void Load()
    {
        static void HandleDeserializationError( object? sender, ErrorEventArgs errorArgs )
        {
            GlamSiphon.Log.Error(
                $"Error parsing Configuration at {errorArgs.ErrorContext.Path}, using default or migrating:\n{errorArgs.ErrorContext.Error}" );
            errorArgs.ErrorContext.Handled = true;
        }

        if ( !File.Exists( _saveService.FilenameNames.ConfigFile ) )
            return;

        if ( File.Exists( _saveService.FilenameNames.ConfigFile ) )
            try
            {
                GlamSiphon.Log.Debug( $"Attempting to read existing configuration file." );
                var text = File.ReadAllText(_saveService.FilenameNames.ConfigFile);
                JsonConvert.PopulateObject(text, this, new JsonSerializerSettings
                {
                    Error = HandleDeserializationError,
                });
            }
            catch ( Exception ex )
            {
                GlamSiphon.Messager.NotificationMessage( ex,
                                                         "Error reading Configuration, reverting to default.\nYou may be able to restore your configuration using the rolling backups in the XIVLauncher/backups/Glamourer directory.",
                                                         "Error reading Configuration", NotificationType.Error );
            }

        MigrateToCurrentVersion();
    }

    public string ToFilename( FilenameService filenameNames )
        => filenameNames.ConfigFile;

    public void Save( StreamWriter writer )
    {
        using var jWriter    = new JsonTextWriter(writer) { Formatting = Formatting.Indented };
        var       serializer = new JsonSerializer { Formatting         = Formatting.Indented };
        serializer.Serialize(jWriter, this);
    }
}
