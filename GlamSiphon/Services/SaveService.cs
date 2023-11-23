using Dalamud.Configuration;
using Dalamud.Plugin;
using System;
using System.IO;
using Newtonsoft.Json;
using OtterGui.Classes;
using OtterGui.Log;


namespace GlamSiphon.Services;

public interface ISavable : ISavable<FilenameService>
{ }

public sealed class SaveService : SaveServiceBase<FilenameService>
{
    public SaveService(Logger log, FrameworkManager framework, FilenameService filenameNames)
        : base(log, framework, filenameNames)
    { }
}
