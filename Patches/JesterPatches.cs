using BepInEx;
using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;
namespace VirtualInsanity.Patches;

[HarmonyPatch(typeof(JesterAI), nameof(JesterAI.SetJesterInitialValues))]
public class JesterPatches
{
    [HarmonyPostfix]
    private static void JesterPatch(JesterAI __instance)
    {
        if(VirtualInsanity.Config.ConfigurationEnabled.Value && VirtualInsanity.Config.JesterChangesEnabled.Value) {__instance.popUpTimer = UnityEngine.Random.Range(1f, 5f);}
    }
}