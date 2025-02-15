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
    public static SelectableLevel currentlevel = null!;


    public static bool ShipPhase;
    
    [HarmonyPatch(typeof(StartOfRound), nameof(StartOfRound.Update))]
    [HarmonyPostfix]
    
    private static void StartOfRoundPatch(StartOfRound __instance)
    {
        currentlevel = __instance.currentLevel;
        
        if ((__instance.elevatorTransform.position + __instance.localPlayerController.transform.position).magnitude >=
            5 && __instance.elevatorTransform.position != new Vector3(0,0,0) && tryDeath == false && elevatorRunning && PlayerPatches.playerInFacilty)
        {
            if (Random.RandomRangeInt(1, 11) == 1 && VirtualInsanity.Config.ElevatorBreakdownEnabled.Value)
            {
                __instance.localPlayerController.KillPlayer(new Vector3(0, 0, 0));
                tryDeath = true;
                HudPatches.Instance.DisplayTip("Death Reason", "Elevator Broke Down", true, false,
                    "LC_EclipseTip");
            }
            else
            {
                tryDeath = true;
            }
        }

        ShipPhase = __instance.inShipPhase;
    }

    [HarmonyPatch(typeof(StartOfRound), nameof(StartOfRound.OnShipLandedMiscEvents))]
    [HarmonyPrefix]
    public static bool StartOfRoundOnShipLandedMiscEventsPatch(StartOfRound __instance)
    {
        if (TimeOfDay.Instance.currentLevelWeather == LevelWeatherType.Eclipsed)
        {
            int random = Random.RandomRangeInt(1,4);
            if(random == 1){HudPatches.Instance.DisplayTip("Caution", "Loitering On The Ship Will Not be Tolerated >:(", true, false,
                "LC_EclipseTip");} else if(random == 2){HudPatches.Instance.DisplayTip("Caution", "Ship Lever Is Broken Sowwy :c", true, false,
                "LC_EclipseTip");} else if(random == 3){HudPatches.Instance.DisplayTip("Caution", "GO BE PRODUCTIVE, FOR THE COMPANY!!!", true, false,
                "LC_EclipseTip");}
        }
        else
        {
            HudPatches.Instance.DisplayTip("71-Gordion", "Time to be Financially responsible", true, false,
                "LC_EclipseTip");
        }
        return true;
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