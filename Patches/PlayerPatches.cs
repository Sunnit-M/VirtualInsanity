using BepInEx;
using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace VirtualInsanity.Patches;
[HarmonyPatch(typeof(PlayerControllerB), nameof(PlayerControllerB.Update))]
public class PlayerPatches
{
    public static bool playerInFacilty;
    
    [HarmonyPostfix]
    public static void PlayerPatch(PlayerControllerB __instance)
    {
        if (__instance.isExhausted)
        {
            __instance.KillPlayer(new Vector3(0, 0, 0));
        }

        if (__instance.insanityLevel >= __instance.maxInsanityLevel)
        {
            __instance.KillPlayer(new Vector3(0,0,0));
        }

        if (__instance.isUnderwater)
        {
            __instance.KillPlayer(new Vector3(0,0,0));
        }
        
        //__instance.drunkness = 0.25f;-

        playerInFacilty = __instance.isInsideFactory;
    }
}