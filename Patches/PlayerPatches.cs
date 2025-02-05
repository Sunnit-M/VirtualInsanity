using BepInEx;
using BepInEx.Logging;
using Dissonance.Config;
using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace VirtualInsanity.Patches;
[HarmonyPatch(typeof(PlayerControllerB), nameof(PlayerControllerB.Update))]
public class PlayerPatches
{
    public static bool playerInFacilty;
    public static float timeInShip = 10;
    public static bool Counting;
    
    [HarmonyPostfix]
    public static void PlayerPatch(PlayerControllerB __instance)
    {
        if (!RoundPatches.ShipPhase)
        {

            if (__instance.isExhausted)
            {
                __instance.KillPlayer(new Vector3(0, 0, 0));
            }

            if (__instance.insanityLevel >= __instance.maxInsanityLevel)
            {
                __instance.KillPlayer(new Vector3(0, 0, 0));
            }

            if (__instance.isUnderwater)
            {
                __instance.KillPlayer(new Vector3(0, 0, 0));
            }

            __instance.drunkness = 0.1f;

            playerInFacilty = __instance.isInsideFactory;

            if (__instance.isInHangarShipRoom && Counting)
            {
                timeInShip -= Time.deltaTime;
            }

            if (timeInShip <= 0)
            {
                __instance.KillPlayer(new Vector3(0, 0, 0));
            }
        }
        else if (RoundPatches.ShipPhase)
        {
            __instance.drunkness = 0f;
            Counting = false;
        }
    }
}
