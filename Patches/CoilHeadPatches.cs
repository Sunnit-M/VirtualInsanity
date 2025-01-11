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
        __instance.loseAggroTimer = 0;
    }
}