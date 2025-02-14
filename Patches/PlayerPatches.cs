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
            if (__instance.isExhausted && VirtualInsanity.Config.ConfigurationEnabled && VirtualInsanity.Config.DieOnExhaustion.Value)
            {
                { __instance.KillPlayer(new Vector3(0, 0, 0));}
            }
            {
                 { __instance.KillPlayer(new Vector3(0, 0, 0));}
            }

            if (__instance.insanityLevel >= __instance.maxInsanityLevel && VirtualInsanity.Config.ConfigurationEnabled && VirtualInsanity.Config.DieOnInsanity.Value)
            {
                { __instance.KillPlayer(new Vector3(0, 0, 0));}
            }

            if (__instance.isUnderwater && VirtualInsanity.Config.ConfigurationEnabled && VirtualInsanity.Config.DieOnWater.Value)
            {
                __instance.KillPlayer(new Vector3(0, 0, 0));
            }
            
            if(VirtualInsanity.Config.ConfigurationEnabled) { __instance.drunkness = VirtualInsanity.Config.Drunkness.Value;}
            else
            {
                __instance.drunkness = 0.2f;
            }

            playerInFacilty = __instance.isInsideFactory;
            
        }
        else if (RoundPatches.ShipPhase)
        {
            __instance.drunkness = 0f;
        }
    }
}
