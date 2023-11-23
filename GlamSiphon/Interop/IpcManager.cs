using System;
using System.Collections.Generic;
using Dalamud.Plugin;
using Dalamud.Plugin.Ipc;

namespace GlamSiphon.Interop;

public partial class IpcManager : IDisposable
{
    private readonly ICallGateSubscriber<int, int> _penumbraApiVersions;
    private readonly ICallGateSubscriber<bool>     _penumbraEnabled;
    private readonly ICallGateSubscriber<bool>     _penumbraEnabledChange;

    IpcManager( DalamudPluginInterface pi )
    {
        _penumbraApiVersions   = pi.GetIpcSubscriber<int, int>( "Penumbra.ApiVersions" );
        _penumbraEnabled       = pi.GetIpcSubscriber<bool>( "Penumbra.GetEnabledState" );
        _penumbraEnabledChange = pi.GetIpcSubscriber<bool>( "Penumbra.EnabledChange" );
    }

    public void Dispose()
    {
        // TODO release managed resources here
    }
}
