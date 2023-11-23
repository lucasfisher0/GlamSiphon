using Dalamud.Configuration;
using Dalamud.Plugin;
using System;
using Newtonsoft.Json;


namespace GlamSiphon;

[Serializable]
public class Configuration : IPluginConfiguration
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
}
