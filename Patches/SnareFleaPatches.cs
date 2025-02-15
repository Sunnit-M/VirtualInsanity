using HarmonyLib;

namespace VirtualInsanity.Patches;

public class SnareFleaPatches
{
    [HarmonyPatch(typeof(CentipedeAI), nameof(CentipedeAI.Update))]
    [HarmonyPostfix]
    private static void CentipedePatch(CentipedeAI __instance)
    {
        if(VirtualInsanity.Config.SnareChangesEnabled.Value) {__instance.damagePlayerInterval = 0f;}
    }
}