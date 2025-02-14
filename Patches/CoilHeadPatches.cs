using BepInEx;
using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace VirtualInsanity.Patches;
[HarmonyPatch(typeof(SpringManAI), nameof(SpringManAI.Update))]
public class CoilHeadPatches
{
    [HarmonyPostfix]
    private static void CoilHeadPatch(SpringManAI __instance)
    {
        if(VirtualInsanity.Config.ConfigurationEnabled.Value && VirtualInsanity.Config.CoilChangesEnabled.Value) {__instance.loseAggroTimer = 0;}
    }
}