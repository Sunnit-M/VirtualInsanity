using HarmonyLib;
using UnityEngine;

namespace VirtualInsanity.Patches;

public class TimeODay
{
    [HarmonyPatch(typeof(TimeOfDay), nameof(TimeOfDay.DecideRandomDayEvents))]
    [HarmonyPostfix]
    public static void MeteorPatch(TimeOfDay __instance)
    {
        __instance.meteorShowerAtTime = 0.1f;
    }

    [HarmonyPatch(typeof(TimeOfDay), nameof(TimeOfDay.SetNewProfitQuota))]
    [HarmonyPrefix]
    public static void SetNewProfitQuota(TimeOfDay __instance)
    {
        if (!__instance.IsServer)
            return;
        ++__instance.timesFulfilledQuota;
        int num1 = __instance.quotaFulfilled - __instance.profitQuota;
        float num2 = Mathf.Clamp((float) (1.0 + (double) __instance.timesFulfilledQuota * ((double) __instance.timesFulfilledQuota / (double) __instance.quotaVariables.increaseSteepness)), 0.0f, 10000f);
        __instance.CalculateLuckValue();
        float num3 = UnityEngine.Random.Range(0.0f, 1f);
        Debug.Log((object) string.Format("Randomizer amount before: {0}", (object) num3));
        float time = num3 * Mathf.Abs(__instance.luckValue - 1f);
        Debug.Log((object) string.Format("Randomizer amount after: {0}", (object) time));
        float num4 = (float) ((double) __instance.quotaVariables.baseIncrease * (double) num2 * ((double) __instance.quotaVariables.randomizerCurve.Evaluate(time) * (double) __instance.quotaVariables.randomizerMultiplier + 1.0));
        Debug.Log((object) string.Format("Amount to increase quota:{0}", (object) num4));
        __instance.profitQuota = (int) Mathf.Clamp((float) __instance.profitQuota + num4, 0.0f, 1E+09f) * VirtualInsanity.Config.QuotaMultiplier.Value;
        __instance.quotaFulfilled = 0;
        __instance.timeUntilDeadline = __instance.totalTime * 4f;
        int overtimeBonus = num1 / 5 + 15 * __instance.daysUntilDeadline;
        __instance.furniturePlacedAtQuotaStart.Clear();
        AutoParentToShip[] objectsByType = UnityEngine.Object.FindObjectsByType<AutoParentToShip>(FindObjectsSortMode.None);
        for (int index = 0; index < objectsByType.Length; ++index)
        {
            if (objectsByType[index].unlockableID != -1)
                __instance.furniturePlacedAtQuotaStart.Add(objectsByType[index].unlockableID);
        }
        __instance.SyncNewProfitQuotaClientRpc(__instance.profitQuota, overtimeBonus, __instance.timesFulfilledQuota);
        return;
    }
}