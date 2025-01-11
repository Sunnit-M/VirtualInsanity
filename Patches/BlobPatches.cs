using BepInEx;
using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;
namespace VirtualInsanity.Patches;

[HarmonyPatch(typeof(BlobAI), nameof(BlobAI.Update))]
public class BlobPatches
{
    [HarmonyPostfix]
    private static void BlobPatch(BlobAI __instance)
    {
        __instance.tamedTimer = 0;
    }
}