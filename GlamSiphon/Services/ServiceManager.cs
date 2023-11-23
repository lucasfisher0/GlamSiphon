using Dalamud.Plugin;
using GlamSiphon.Events;
using Microsoft.Extensions.DependencyInjection;
using OtterGui.Classes;
using OtterGui.Log;
using Penumbra.GameData.Data;
using GlamSiphon.Gui;
using GlamSiphon.Interop;
using Xande;

namespace GlamSiphon.Services;

public static class ServiceManager
{
    public static ServiceProvider CreateProvider( DalamudPluginInterface pi, Logger log )
    {
        EventWrapper.ChangeLogger( log );
        var services = new ServiceCollection()
                      .AddSingleton( log )
                      .AddTransient<TransientConfig>()
                      .AddDalamud( pi )
                      .AddMeta()
                      .AddInterop()
                      .AddEvents()
                      .AddData()
                      .AddUi()
                      .AddApi();

        return services.BuildServiceProvider( new ServiceProviderOptions { ValidateOnBuild = true } );
    }

    private static IServiceCollection AddDalamud( this IServiceCollection services, DalamudPluginInterface pi )
    {
        new DalamudServices( pi ).AddServices( services );
        return services;
    }

    private static IServiceCollection AddMeta( this IServiceCollection services )
        => services.AddSingleton<MessageService>()
                   .AddSingleton<FilenameService>()
                   .AddSingleton<FrameworkManager>()
                   .AddSingleton<SaveService>()
                   .AddSingleton<Configuration>();

    private static IServiceCollection AddEvents( this IServiceCollection services )
        => services.AddSingleton<TabSelected>()
                   .AddSingleton<PenumbraReloaded>();

    private static IServiceCollection AddData( this IServiceCollection services )
        => services.AddSingleton<ExportService>()
                   .AddSingleton<ActorService>()
                   .AddSingleton<LuminaManager>(); /* TODO: Data
                   .AddSingleton<IdentifierService>()
                  .AddSingleton<ItemService>()
                  .AddSingleton<CustomizationService>()
                  .AddSingleton<ItemManager>()
                  .AddSingleton<HumanModelList>(); */

    private static IServiceCollection AddInterop( this IServiceCollection services )
        => services.AddSingleton<ObjectManager>()
                   .AddSingleton<PenumbraService>(); /* TODO: Interop
                   services.AddSingleton<VisorService>()
                       .AddSingleton<ChangeCustomizeService>()
                       .AddSingleton<MetaService>()
                       .AddSingleton<UpdateSlotService>()
                       .AddSingleton<WeaponService>()
                       .AddSingleton<ObjectManager>()
                       .AddSingleton<PenumbraAutoRedraw>()
                       .AddSingleton<JobService>()
                       .AddSingleton<CustomizeUnlockManager>()
                       .AddSingleton<ItemUnlockManager>()
                       .AddSingleton<ImportService>()
                       .AddSingleton<InventoryService>()
                       .AddSingleton<ContextMenuService>()
                       .AddSingleton<ScalingService>(); */

    private static IServiceCollection AddUi( this IServiceCollection services )
        => services.AddSingleton<GSWindowService>()
                   .AddSingleton<SettingsTab>()
                   .AddSingleton<DebugTab>()
                   .AddSingleton<ActorTab>()
                   .AddSingleton<MainWindow>();

    private static IServiceCollection AddApi( this IServiceCollection services )
        => services.AddSingleton<CommandService>();
    // .AddSingleton<GlamourerIpc>();
}
