using HarmonyLib;

namespace VirtualInsanity.Patches;

[HarmonyPatch(typeof(HUDManager),nameof(HUDManager.Update))]
public class HudPatches
{
    public static HUDManager Instance;
    
    [HarmonyPostfix]
    public static void Postfix(HUDManager __instance)
    {
        Instance = __instance;
    }
}