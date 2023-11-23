using Dalamud.Game.ClientState.Objects;
using Dalamud.Interface.DragDrop;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GlamSiphon.Services;

public class DalamudServices
{
    public DalamudServices( DalamudPluginInterface pi )
    {
        pi.Inject( this );
    }

    public void AddServices( IServiceCollection services )
    {
        services.AddSingleton( PluginInterface );
        services.AddSingleton( Commands );
        services.AddSingleton( GameData );
        services.AddSingleton( ClientState );
        services.AddSingleton( Condition );
        services.AddSingleton( GameGui );
        services.AddSingleton( Chat );
        services.AddSingleton( Framework );
        services.AddSingleton( Targets );
        services.AddSingleton( Objects );
        services.AddSingleton( KeyState );
        services.AddSingleton( this );
        services.AddSingleton( PluginInterface.UiBuilder );
        services.AddSingleton( DragDropManager );
        services.AddSingleton( TextureProvider );
        services.AddSingleton( Log );
        services.AddSingleton( Interop );
    }

    // @formatter:off
    [PluginService][RequiredVersion("1.0")] public DalamudPluginInterface PluginInterface { get; private set; } = null!;
    [PluginService][RequiredVersion("1.0")] public ICommandManager        Commands        { get; private set; } = null!;
    [PluginService][RequiredVersion("1.0")] public IDataManager           GameData        { get; private set; } = null!;
    [PluginService][RequiredVersion("1.0")] public IClientState           ClientState     { get; private set; } = null!;
    [PluginService][RequiredVersion("1.0")] public ICondition             Condition       { get; private set; } = null!;
    [PluginService][RequiredVersion("1.0")] public IGameGui               GameGui         { get; private set; } = null!;
    [PluginService][RequiredVersion("1.0")] public IChatGui               Chat            { get; private set; } = null!;
    [PluginService][RequiredVersion("1.0")] public IFramework             Framework       { get; private set; } = null!;
    [PluginService][RequiredVersion("1.0")] public ITargetManager         Targets         { get; private set; } = null!;
    [PluginService][RequiredVersion("1.0")] public IObjectTable           Objects         { get; private set; } = null!;
    [PluginService][RequiredVersion("1.0")] public IKeyState              KeyState        { get; private set; } = null!;
    [PluginService][RequiredVersion("1.0")] public IDragDropManager       DragDropManager { get; private set; } = null!;
    [PluginService][RequiredVersion("1.0")] public ITextureProvider       TextureProvider { get; private set; } = null!;
    [PluginService][RequiredVersion("1.0")] public IPluginLog             Log             { get; private set; } = null!;
    [PluginService][RequiredVersion("1.0")] public IGameInteropProvider   Interop         { get; private set; } = null!;
    // @formatter:on
}
