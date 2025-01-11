using HarmonyLib;

namespace VirtualInsanity.Patches;

public class SnareFleaPatches
{
    [HarmonyPatch(typeof(CentipedeAI), nameof(CentipedeAI.Update))]
    [HarmonyPostfix]
    private static void CentipedePatch(CentipedeAI __instance)
    {
        __instance.damagePlayerInterval = 0f;
    }
}