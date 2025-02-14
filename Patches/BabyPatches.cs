using HarmonyLib;

namespace VirtualInsanity.Patches;
[HarmonyPatch(typeof(CaveDwellerAI),nameof(CaveDwellerAI.Start))]
public class BabyPatches
{
    [HarmonyPostfix]
    public static void BabyPathes(CaveDwellerAI __instance)
    {
        if(VirtualInsanity.Config.ConfigurationEnabled.Value && VirtualInsanity.Config.BabyChangesEnabled.Value) { __instance.TransformIntoAdult();}
    }
}