using System;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using LobbyCompatibility.Attributes;
using LobbyCompatibility.Enums;
using VirtualInsanity.Patches;
using BepInEx.Configuration;
using CSync.Extensions;
using CSync.Lib;
using LethalConfig.ConfigItems;

namespace VirtualInsanity;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency("BMX.LobbyCompatibility", BepInDependency.DependencyFlags.HardDependency)] // LobbyCompatibility
[BepInDependency("ainavt.lc.lethalconfig")] // LethalConfig
[BepInDependency("com.sigurd.csync", "5.0.0")] // CSync
[LobbyCompatibility(CompatibilityLevel.Everyone, VersionStrictness.Patch)]

public class VirtualInsanity : BaseUnityPlugin
{
    public static VirtualInsanity Instance { get; private set; } = null!;
    internal new static ManualLogSource Logger { get; private set; } = null!;
    internal static Harmony? Harmony { get; set; }

    internal static new VirtualInsanityConfig Config;
    
    

    private void Awake()
    {
        Logger = base.Logger;
        Instance = this;
        
        Config = new VirtualInsanityConfig(base.Config);
        
        Patch();

        Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
    }

    internal static void Patch()
    {
        Harmony ??= new Harmony(MyPluginInfo.PLUGIN_GUID);

        Logger.LogDebug("Patching...");

        Harmony.PatchAll(typeof(PlayerPatches));
        Harmony.PatchAll(typeof(JesterPatches));
        Harmony.PatchAll(typeof(BlobPatches));
        Harmony.PatchAll(typeof(BarberPatches));
        Harmony.PatchAll(typeof(TerminalPatches));
        Harmony.PatchAll(typeof(CoilHeadPatches));
        Harmony.PatchAll(typeof(ThumperPatches));
        Harmony.PatchAll(typeof(RoundPatches));
        Harmony.PatchAll(typeof(SickAssRobot));
        Harmony.PatchAll(typeof(SnareFleaPatches));
        Harmony.PatchAll(typeof(BabyPatches));
        Harmony.PatchAll(typeof(TimeODay));
        Harmony.PatchAll(typeof(deezNutcrackerPatches));
        Harmony.PatchAll(typeof(HudPatches));
        Harmony.PatchAll(typeof(LeverPatches));
        
        Logger.LogDebug("Finished patching!");
    }

    internal static void Unpatch()
    {
        Logger.LogDebug("Unpatching...");

        Harmony?.UnpatchSelf();

        Logger.LogDebug("Finished unpatching!");
    }
}

public class VirtualInsanityConfig : SyncedConfig2<VirtualInsanityConfig>
{
    // Config Entries
    [SyncedEntryField] public SyncedEntry<bool> ConfigurationEnabled;
    [SyncedEntryField] public SyncedEntry<bool> DieOnExhaustion;
    [SyncedEntryField] public SyncedEntry<bool> DieOnWater;
    [SyncedEntryField] public SyncedEntry<bool> DieOnInsanity;
    [SyncedEntryField] public SyncedEntry<float> Drunkness;
    [SyncedEntryField] public SyncedEntry<bool> CanPullLever;
    [SyncedEntryField] public SyncedEntry<bool> ThumperChangesEnabled;
    [SyncedEntryField] public SyncedEntry<bool> JesterChangesEnabled;
    [SyncedEntryField] public SyncedEntry<bool> BabyChangesEnabled;
    [SyncedEntryField] public SyncedEntry<bool> BarberChangesEnabled;
    [SyncedEntryField] public SyncedEntry<bool> NutcrackerChangesEnabled;
    [SyncedEntryField] public SyncedEntry<bool> SnareChangesEnabled;
    [SyncedEntryField] public SyncedEntry<bool> RobotChangesEnabled;
    [SyncedEntryField] public SyncedEntry<bool> BlobChangesEnabled;
    [SyncedEntryField] public SyncedEntry<bool> CoilChangesEnabled;
    [SyncedEntryField] public SyncedEntry<bool> MeteorEnabled;
    [SyncedEntryField] public SyncedEntry<bool> EclipseEnabled;
    [SyncedEntryField] public SyncedEntry<bool> ElevatorBreakdownEnabled;
    [SyncedEntryField] public SyncedEntry<int> QuotaMultiplier;

    public VirtualInsanityConfig(ConfigFile cfg) : base(MyPluginInfo.PLUGIN_GUID)
    {
        ConfigurationEnabled =
            cfg.BindSyncedEntry("Misc", "Configuration Enabled", true, "Enable or disable the configuration.");
        DieOnExhaustion = cfg.BindSyncedEntry("Player", "Die On Exhaustion", true, "Player dies upon exhaustion.");
        DieOnWater = cfg.BindSyncedEntry("Player", "Die On Water Contact", true, "Player dies upon water contact.");
        DieOnInsanity = cfg.BindSyncedEntry("Player", "Die On Max Insanity", true,
            "Player dies upon reaching maximum insanity.");
        Drunkness = cfg.BindSyncedEntry("Player", "Drunkness", 0.2f,
            new ConfigDescription("Player's initial level of drunkness.", new AcceptableValueRange<float>(0f, 1f)));
        CanPullLever = cfg.BindSyncedEntry("Misc", "Can Pull Lever", true, "Allow players to pull the lever.");
        ThumperChangesEnabled = cfg.BindSyncedEntry("Enemies", "Thumper Changes Enabled", true,
            "Enable changes for Thumper enemy.");
        JesterChangesEnabled =
            cfg.BindSyncedEntry("Enemies", "Jester Changes Enabled", true, "Enable changes for Jester enemy.");
        BabyChangesEnabled =
            cfg.BindSyncedEntry("Enemies", "Baby Changes Enabled", true, "Enable changes for Baby enemy.");
        BarberChangesEnabled =
            cfg.BindSyncedEntry("Enemies", "Barber Changes Enabled", true, "Enable changes for Barber enemy.");
        NutcrackerChangesEnabled = cfg.BindSyncedEntry("Enemies", "Nutcracker Changes Enabled", true,
            "Enable changes for Nutcracker enemy.");
        SnareChangesEnabled =
            cfg.BindSyncedEntry("Enemies", "Snare Changes Enabled", true, "Enable changes for Snare enemy.");
        RobotChangesEnabled =
            cfg.BindSyncedEntry("Enemies", "Robot Changes Enabled", true, "Enable changes for Robot enemy.");
        BlobChangesEnabled =
            cfg.BindSyncedEntry("Enemies", "Blob Changes Enabled", true, "Enable changes for Blob enemy.");
        CoilChangesEnabled =
            cfg.BindSyncedEntry("Enemies", "Coil Changes Enabled", true, "Enable changes for Coil enemy.");
        MeteorEnabled = cfg.BindSyncedEntry("Environment", "Meteors Enabled", true, "Enable meteor events.");
        EclipseEnabled = cfg.BindSyncedEntry("Environment", "Eclipse All Planets Enabled", true,
            "Enable eclipses for all planets.");
        ElevatorBreakdownEnabled = cfg.BindSyncedEntry("Environment", "Elevator Breakdown Enabled", true,
            "Enable elevator breakdowns.");
        QuotaMultiplier = cfg.BindSyncedEntry("Money", "Quota Multiplier", 2,
            new ConfigDescription("Multiplier for quotas.", new AcceptableValueRange<int>(1, 10)));

        ConfigManager.Register(this);
        Console.WriteLine("Config registered");
    }
}
