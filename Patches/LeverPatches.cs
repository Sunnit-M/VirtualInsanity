using HarmonyLib;

namespace VirtualInsanity.Patches;

[HarmonyPatch(typeof(StartMatchLever), nameof(StartMatchLever.Update))]
public class LeverPatches
{
    [HarmonyPrefix]
    public static void LeverPatch(StartMatchLever __instance)
    {
        if (__instance.playersManager.shipHasLanded && VirtualInsanity.Config.ConfigurationEnabled.Value && VirtualInsanity.Config.CanPullLever.Value)
        {
            if (RoundPatches.currentlevel.planetHasTime)
            {
                __instance.triggerScript.interactable = false;
            }
            else
            {
                __instance.triggerScript.interactable = true;
            }
        }
    }
}