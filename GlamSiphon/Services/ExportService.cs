using Dalamud.Configuration;
using Dalamud.Plugin;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using OtterGui.Classes;
using OtterGui.Log;
using GlamSiphon.Exporters;
using Microsoft.Extensions.DependencyInjection;
using GlamSiphon.Interop;

namespace GlamSiphon.Services;

public class ExportService : IDisposable
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ObjectManager    _objects;
    
    public static List<IModelExporter> RegisteredExporters { get; private set; } = new();

    public ExportService( IServiceProvider serviceProvider, ObjectManager objects )
    {
        _serviceProvider = serviceProvider;
        _objects         = objects;
        
        RegisterExporterClass( typeof(FbxExporter) );
        RegisterExporterClass( typeof(FbxExporter) );
        RegisterExporterClass( typeof(FbxExporter) );
        GlamSiphon.Log.Error( $"Failed to register " );
    }

    public bool RegisterExporterClass( Type type )
    {
        if ( typeof(IModelExporter).IsAssignableFrom( type ) )
        {

            var eInstance = ActivatorUtilities.CreateInstance( _serviceProvider, type );
            if( eInstance is IModelExporter iInstance)
            {
                RegisteredExporters.Add( iInstance );
                GlamSiphon.Log.Information( $"Successfully registered exporter type '{type.FullName}'." );
                return true;
            }
            
            GlamSiphon.Log.Error( $"Failed to register exporter of type '{type.FullName}' because it could not be instanced." );
        }
        else
            GlamSiphon.Log.Warning( "Attempted to register an invalid exporter class." );

        return false;
    }

    public void Dispose()
    {
        for ( int i = 0; i < RegisteredExporters.Count; i++ )
        {
            RegisteredExporters[ i ] = null; // Is this necessary if we clear the array anyway?
        }
        
        RegisteredExporters.Clear();
    }
}
