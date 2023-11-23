using Dalamud.Configuration;
using Dalamud.Plugin;
using System;
using Newtonsoft.Json;
using GlamSiphon.Services;

namespace GlamSiphon;

public partial class Configuration : IPluginConfiguration, ISavable
{
    public void MigrateToCurrentVersion()
    {
        // NOOP
    }
}
