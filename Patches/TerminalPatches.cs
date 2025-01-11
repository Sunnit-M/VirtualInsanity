using BepInEx;
using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace VirtualInsanity.Patches;

[HarmonyPatch(typeof(Terminal), nameof(Terminal.Update))]
public class TerminalPatches
{
    [HarmonyPostfix]
    private static void ApplyMoonWeather(ref Terminal __instance)
    {
        foreach (SelectableLevel level in __instance.moonsCatalogueList)
        {
            level.currentWeather = LevelWeatherType.Eclipsed;
            level.maxEnemyPowerCount = 40;
            level.maxOutsideEnemyPowerCount = 60;
        }
    }
}