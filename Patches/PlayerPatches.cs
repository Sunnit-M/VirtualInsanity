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
                HudPatches.Instance.DisplayTip("Death Reason", "You've Tripped on oxygen", true, false,
                    "LC_EclipseTip");
            }

            if (__instance.insanityLevel >= __instance.maxInsanityLevel)
            {
                __instance.KillPlayer(new Vector3(0, 0, 0));
                HudPatches.Instance.DisplayTip("Death Reason", "Went Crazy", true, false,
                    "LC_EclipseTip");
            }

            if (__instance.isUnderwater)
            {
                __instance.KillPlayer(new Vector3(0, 0, 0));
                HudPatches.Instance.DisplayTip("Death Reason", "Your a terrible swimmer", true, false,
                    "LC_EclipseTip");
            }

            __instance.drunkness = 0.5f;

            playerInFacilty = __instance.isInsideFactory;

            if (__instance.isInHangarShipRoom && Counting)
            {
                timeInShip -= Time.deltaTime;
            }

            if (timeInShip <= 0)
            {
                __instance.KillPlayer(new Vector3(0, 0, 0));
                HudPatches.Instance.DisplayTip("Death Reason", "NO LOITERING", true, false,
                    "LC_EclipseTip");
            }
        }
        else if (RoundPatches.ShipPhase)
        {
            __instance.drunkness = 0f;
            Counting = false;
        }
    }
}