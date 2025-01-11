using BepInEx;
using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace VirtualInsanity.Patches;
[HarmonyPatch(typeof(ClaySurgeonAI), nameof(ClaySurgeonAI.HourChanged))]
public class BarberPatches
{
    [HarmonyPostfix]
    private static void BarberPatch(ClaySurgeonAI __instance)
    {
        __instance.currentInterval = 1f;
    }
}