using BepInEx;
using BepInEx.Logging;
using Dissonance.Config;
using GameNetcodeStuff;
using HarmonyLib;
using Unity.Netcode;
using UnityEngine;

namespace VirtualInsanity.Patches;
[HarmonyPatch(typeof(PlayerControllerB), nameof(PlayerControllerB.Update))]
public class PlayerPatches
{
    public static bool playerInFacilty;
    
    [HarmonyPostfix]
    public static void PlayerPatch(PlayerControllerB __instance)
    {
        if (!RoundPatches.ShipPhase)
        {
            if (VirtualInsanity.Config.DieOnExhaustion.Value && __instance.isExhausted)
            {
                { __instance.KillPlayer(new Vector3(0, 0, 0));}
            }

            if (__instance.insanityLevel >= __instance.maxInsanityLevel && VirtualInsanity.Config.DieOnInsanity.Value)
            {
                { __instance.KillPlayer(new Vector3(0, 0, 0));}
            }

            if (__instance.isUnderwater && VirtualInsanity.Config.DieOnWater.Value)
            {
                __instance.KillPlayer(new Vector3(0, 0, 0));
            }
            
            __instance.drunkness = VirtualInsanity.Config.Drunkness.Value;

            playerInFacilty = __instance.isInsideFactory;
            
        }
        else if (RoundPatches.ShipPhase)
        {
            __instance.drunkness = 0f;
        }
    }
}
