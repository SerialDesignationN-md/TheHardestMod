using UnityEngine;
using HarmonyLib;

namespace TheHardestMod
{
    [HarmonyPatch]
    public class BaldiPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(Baldi), "Start")]
        static public void StartPatch(Baldi __instance) {
            __instance.speedMultiplier = 0.2f;
        }
	
    }
}