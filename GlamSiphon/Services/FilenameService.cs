using Dalamud.Configuration;
using Dalamud.Plugin;
using System;
using System.IO;
using Newtonsoft.Json;
using OtterGui.Classes;
using OtterGui.Log;


namespace GlamSiphon.Services;

public class FilenameService
{
    public readonly string ConfigDirectory;
    public readonly string ConfigFile;
    
    
    
    
    public readonly string LastExportDirectory;
    
    public FilenameService(DalamudPluginInterface pi)
    {
        ConfigDirectory     = pi.ConfigDirectory.FullName;
        ConfigFile          = pi.ConfigFile.FullName;
        LastExportDirectory = Path.Combine( ConfigDirectory, "Export" );
    }
}
