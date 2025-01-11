using GameNetcodeStuff;
using HarmonyLib;
using Newtonsoft.Json;
using UnityEngine;

namespace VirtualInsanity.Patches;

public class RoundPatches
{
    public static int time = 5;
    public static bool tryDeath;
    public static bool elevatorRunning;
    
    [HarmonyPatch(typeof(StartOfRound), nameof(StartOfRound.Update))]
    [HarmonyPostfix]
    
    private static void StartOfRoundPatch(StartOfRound __instance)
    {
        if ((__instance.elevatorTransform.position + __instance.localPlayerController.transform.position).magnitude >=
            5 && __instance.elevatorTransform.position != new Vector3(0,0,0) && tryDeath == false && elevatorRunning && PlayerPatches.playerInFacilty)
        {
            if (Random.RandomRangeInt(1, 11) == 1)
            {
                __instance.localPlayerController.KillPlayer(new Vector3(0, 0, 0));
                tryDeath = true;
            }
            else
            {
                tryDeath = true;
            }
        }
        
        
    }

    [HarmonyPatch(typeof(MineshaftElevatorController), nameof(MineshaftElevatorController.Update))]
    [HarmonyPostfix]
    public static void ElevatorControllerPatch(MineshaftElevatorController __instance)
    {
        elevatorRunning = __instance.elevatorJingleMusic.isPlaying;

        if (__instance.elevatorJingleMusic.isPlaying == false)
        {
            tryDeath = false;
        }
    }
}