﻿using BepInEx;
using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace VirtualInsanity.Patches;

public class ThumperPatches
{
    [HarmonyPatch(typeof(CrawlerAI), nameof(CrawlerAI.Update))]
    private static void ThumperPatch(CrawlerAI __instance)
    {
        if (VirtualInsanity.Config.ThumperChangesEnabled.Value)
        {
            __instance.agent.acceleration = 10f;
        }
    }
}