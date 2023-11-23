
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dalamud.Game.ClientState.Objects.Types;
using Lumina.Data;
using Xande;
using Xande.Files;
using Xande.Havok;
using Xande.Models;

namespace GlamSiphon.Exporters;

public interface IModelExporter
{
    public string DisplayName { get; }
}

public class FbxExporter : IModelExporter
{
    public string DisplayName => "FBX";
    
    private readonly LuminaManager _luminaManager;

    public FbxExporter( LuminaManager luminaManager )
    {
        _luminaManager = luminaManager;
    }

    internal void ResolvePathTest()
    {
        // Penumbra.Api.Ipc.ResolveCharacterPath
        GameObject
        /*
         Penumbra API
         
         Penumbra.Api.Enums.ResourceType
         
         
        public (string, bool) GetCharacterCollection(string characterName);
        
        
        
        */
        
        
    }
    
    



    internal void TestExport()
    {
        var havokConverter = new HavokConverter();
        var modelConverter = new ModelConverter(_luminaManager);

        // outputDir can be any directory that exists and is writable, temp paths are used for demonstration
        var outputDir = Path.Combine(Path.GetTempPath(), "XandeModelExport");
        Directory.CreateDirectory(outputDir);

        // This is Grebuloff
        var mdlPaths  = new string[] { "chara/monster/m0405/obj/body/b0002/model/m0405b0002.mdl" };
        var sklbPaths = new string[] { "chara/monster/m0405/skeleton/base/b0001/skl_m0405b0001.sklb" };

        var skeletons = sklbPaths.Select(path => {
            var file   = luminaManager.GetFile<FileResource>(path);
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

    public BlenderExporter( LuminaManager luminaManager ) : base( luminaManager ) { }

}

public class VrChatExporter : FbxExporter
{
    public string DisplayName => "VR Chat Avatar";
    
    public VrChatExporter( LuminaManager luminaManager ) : base( luminaManager ) { }
    
}

