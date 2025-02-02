using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using VirtualInsanity.Patches;

namespace VirtualInsanity;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class VirtualInsanity : BaseUnityPlugin
{
    public static VirtualInsanity Instance { get; private set; } = null!;
    internal new static ManualLogSource Logger { get; private set; } = null!;
    internal static Harmony? Harmony { get; set; }

    private void Awake()
    {
        Logger = base.Logger;
        Instance = this;

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
        
        Logger.LogDebug("Finished patching!");
    }

    internal static void Unpatch()
    {
        Logger.LogDebug("Unpatching...");

        Harmony?.UnpatchSelf();

        Logger.LogDebug("Finished unpatching!");
    }
}