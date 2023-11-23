﻿using Dalamud.Plugin;
using Microsoft.Extensions.DependencyInjection;
using OtterGui.Classes;
using OtterGui.Log;
using Penumbra.GameData.Data;
using GlamSiphon.Gui;

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
                   .AddSingleton<FrameworkManager>()
                   .AddSingleton<Configuration>();

    private static IServiceCollection AddEvents( this IServiceCollection services )
        => services; /* TODO: Events
        services.AddSingleton<VisorStateChanged>()
            .AddSingleton<SlotUpdating>()
            .AddSingleton<DesignChanged>()
            .AddSingleton<AutomationChanged>()
            .AddSingleton<StateChanged>()
            .AddSingleton<WeaponLoading>()
            .AddSingleton<HeadGearVisibilityChanged>()
            .AddSingleton<WeaponVisibilityChanged>()
            .AddSingleton<ObjectUnlocked>()
            .AddSingleton<TabSelected>()
            .AddSingleton<MovedEquipment>()
            .AddSingleton<GPoseService>()
            .AddSingleton<PenumbraReloaded>(); */

    private static IServiceCollection AddData( this IServiceCollection services )
        => services; /* TODO: Data
        services.AddSingleton<IdentifierService>()
            .AddSingleton<ItemService>()
            .AddSingleton<ActorService>()
            .AddSingleton<CustomizationService>()
            .AddSingleton<ItemManager>()
            .AddSingleton<HumanModelList>(); */

    private static IServiceCollection AddInterop( this IServiceCollection services )
        => services; /* TODO: Interop
        services.AddSingleton<VisorService>()
            .AddSingleton<ChangeCustomizeService>()
            .AddSingleton<MetaService>()
            .AddSingleton<UpdateSlotService>()
            .AddSingleton<WeaponService>()
            .AddSingleton<PenumbraService>()
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
        => services.AddSingleton<SettingsTab>()
                   .AddSingleton<DebugTab>()
                   .AddSingleton<ActorTab>()
                   .AddSingleton<MainWindow>();

    private static IServiceCollection AddApi( this IServiceCollection services )
        => services; /* TODO: API
        services.AddSingleton<CommandService>()
            .AddSingleton<GlamourerIpc>();*/
}