
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
// using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Plugin;
using Dalamud.Plugin.Ipc;
using Dalamud.Plugin.Services;
using FFXIVClientStructs.FFXIV.Client.Game.Character;
using GlamSiphon.Interop;
using GlamSiphon.Interop.Structs;
using Lumina.Data;
using Xande;
using Xande.Files;
using Xande.Havok;
using Xande.Models;
using FFXIVClientStructs.FFXIV.Client.Game.Object;
using GlamSiphon.Services;
using Penumbra.Api.Helpers;
using Penumbra.GameData.Actors;

namespace GlamSiphon.Exporters;

public interface IModelExporter
{
    public string DisplayName { get; }
}

public class FbxExporter : IModelExporter
{
    public string DisplayName => "FBX";
    
    private readonly LuminaManager          _luminaManager;
    private readonly DalamudPluginInterface _dalamudPluginInterface;
    private readonly IServiceProvider       _serviceProvider;
    private readonly ActorService           _actors;
    private readonly ObjectManager          _objects;
    private readonly PenumbraService        _penumbraService;
    private readonly IFramework             _framework;
    

    private readonly ParamsFuncSubscriber<ushort, IReadOnlyDictionary<string, string[]>?[]> _penumbraResourcePaths;
    private readonly ICallGateSubscriber<string, string, string> _penumbraResolveCharacterPath;
    // private readonly EventSubscriber<nint, string, string>       _penumbraGameObjectResourcePathResolved;
    private readonly ICallGateSubscriber<IReadOnlyDictionary<ushort, IReadOnlyDictionary<string, string[]>>> _penumbraGetPlayerResourcePaths;
    
   

    public FbxExporter( LuminaManager luminaManager, DalamudPluginInterface pi, IServiceProvider serviceProvider, ActorService actors, ObjectManager objects, PenumbraService penumbraService, IFramework framework )
    {
        _luminaManager          = luminaManager;
        _dalamudPluginInterface = pi;
            _serviceProvider    = serviceProvider;
            _objects            = objects;
            _actors             = actors;
            
            _penumbraService = penumbraService;
            _framework       = framework;
            
            _penumbraResourcePaths = Penumbra.Api.Ipc.GetGameObjectResourcePaths.Subscriber(pi);
            _penumbraResolveCharacterPath           = _dalamudPluginInterface.GetIpcSubscriber<string, string, string>( "Penumbra.ResolveCharacterPath" );
            // _penumbraGameObjectResourcePathResolved = Penumbra.Api.Ipc.GameObjectResourcePathResolved.Subscriber(pi);

            _penumbraGetPlayerResourcePaths = _dalamudPluginInterface
               .GetIpcSubscriber<IReadOnlyDictionary<ushort, IReadOnlyDictionary<string, string[]>>>(
                    "Penumbra.GetPlayerResourcePaths");
    }

    internal unsafe void Debug_ExportSelf(IChatGui chatGui)
    {
        var penumbraAvailable = _penumbraService.Available;
        chatGui.Print( $"Is Penumbra available? {penumbraAvailable}" );
        
        var localPlayer = _actors.AwaitedService.GetCurrentPlayer();
        if ( localPlayer.IsValid )
        {
            chatGui.Print( $"Retrieved local player! Is player type? {localPlayer.Type == IdentifierType.Player}" );
            chatGui.Print( $"Name? {localPlayer.PlayerName}" );

            if ( !penumbraAvailable )
                return;

            var actorPtr = _objects.Player;
            if ( !actorPtr.IsCharacter )
            {
                chatGui.Print( $"Actor Pointer was not a character." );
                return;
            }

            var objPtr = actorPtr.AsObject;
            if ( objPtr is null )
            {
                chatGui.Print( $"Object Pointer was null." );
                return;
            }

            var pathsDic = _penumbraGetPlayerResourcePaths.InvokeFunc();
            foreach ( var kvp in pathsDic )
            {
                chatGui.Print( $"Object ID: {kvp.Key}" );
                foreach ( var val in kvp.Value )
                {
                    chatGui.Print( $"{val.Key}: {string.Join( " | ", val.Value)}"  );
                }
            }
            
            chatGui.Print( "That's it! Go home!" );
        }
    }

    internal void ResolvePathTest()
    {
        
        
        // Penumbra.Api.Ipc.ResolveCharacterPath
        // GameObject
        /*
         Penumbra API
         
         Penumbra.Api.Enums.ResourceType
         
         
        public (string, bool) GetCharacterCollection(string characterName);
        
         /// <summary> Obtain the name of the collection currently assigned to the player. </summary>
            public string GetCurrentPlayerCollection()
        
        
        
        */
        
        
    }
    
    internal void TestExport()
    {
        var havokConverter = new HavokConverter(_dalamudPluginInterface);
        var modelConverter = new ModelConverter(_luminaManager);

        // outputDir can be any directory that exists and is writable, temp paths are used for demonstration
        var outputDir = Path.Combine(Path.GetTempPath(), "XandeModelExport");
        Directory.CreateDirectory(outputDir);

        // This is Grebuloff
        var mdlPaths  = new string[] { "chara/monster/m0405/obj/body/b0002/model/m0405b0002.mdl" };
        var sklbPaths = new string[] { "chara/monster/m0405/skeleton/base/b0001/skl_m0405b0001.sklb" };

        var skeletons = sklbPaths.Select(path => {
            var file   = _luminaManager.GetFile<FileResource>(path);
            var sklb   = SklbFile.FromStream(file.Reader.BaseStream);
            var xmlStr = havokConverter.HkxToXml(sklb.HkxData);
            return new HavokXml(xmlStr);
        }).ToArray();

        modelConverter.ExportModel(outputDir, mdlPaths, skeletons);
    }
}

public class BlenderExporter : FbxExporter
{
    public string DisplayName => "Blender";

    public BlenderExporter
    ( LuminaManager luminaManager, DalamudPluginInterface pi, IServiceProvider serviceProvider, ActorService actors,
        ObjectManager objects, PenumbraService penumbraService, IFramework framework ) : base( luminaManager, pi, serviceProvider, actors, objects, penumbraService, framework ) { }

}

public class VrChatExporter : FbxExporter
{
    public string DisplayName => "VR Chat Avatar";
    
    public VrChatExporter( LuminaManager luminaManager, DalamudPluginInterface pi, IServiceProvider serviceProvider, ActorService actors,
                           ObjectManager objects, PenumbraService penumbraService, IFramework framework ) : base( luminaManager, pi, serviceProvider, actors, objects, penumbraService, framework ) { }
    
}

