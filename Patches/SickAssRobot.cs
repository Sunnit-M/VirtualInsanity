using HarmonyLib;

namespace VirtualInsanity.Patches;

public class SickAssRobot
{
    [HarmonyPatch(typeof(RadMechAI), nameof(RadMechAI.Update))]
    [HarmonyPostfix]
    private static void Postfix(RadMechAI __instance)
    {
        __instance.fireRate = 0.1f;
    }
}