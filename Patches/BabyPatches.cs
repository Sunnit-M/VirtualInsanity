using HarmonyLib;

namespace VirtualInsanity.Patches;
[HarmonyPatch(typeof(CaveDwellerAI),nameof(CaveDwellerAI.Start))]
public class BabyPatches
{
    [HarmonyPostfix]
    public static void BabyPathes(CaveDwellerAI __instance)
    {
        __instance.TransformIntoAdult();
    }
}