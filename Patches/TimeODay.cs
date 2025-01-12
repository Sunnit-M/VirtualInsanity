using HarmonyLib;

namespace VirtualInsanity.Patches;

public class TimeODay
{
    [HarmonyPatch(typeof(TimeOfDay), nameof(TimeOfDay.DecideRandomDayEvents))]
    [HarmonyPostfix]
    public static void MeteorPatch(TimeOfDay __instance)
    {
        __instance.meteorShowerAtTime = 0.1f;
    }
}