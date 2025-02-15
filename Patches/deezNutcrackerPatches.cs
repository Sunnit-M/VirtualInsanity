using HarmonyLib;

namespace VirtualInsanity.Patches;

[HarmonyPatch(typeof(NutcrackerEnemyAI),nameof(NutcrackerEnemyAI.Start))]
public class deezNutcrackerPatches
{
    [HarmonyPostfix]
    public static void NutcrakerPatch(NutcrackerEnemyAI __instance)
    {
        if(VirtualInsanity.Config.NutcrackerChangesEnabled.Value) {__instance.agent.speed = 10f; __instance.torsoTurnSpeed = 2500f;}
    }
}