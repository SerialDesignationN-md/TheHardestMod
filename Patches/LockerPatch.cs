using HarmonyLib;
using TheHardestMod;
using MTM101BaldAPI.Reflection;
using MTM101BaldAPI.AssetTools;
using UnityEngine;
using System;
using System.Reflection;
namespace TheHardestMod
{
    public class LockerPatch
    {
        PlayerManager PM;
        static FieldInfo _PlayerInLocker = AccessTools.Field(typeof(HideableLocker), "playerInLocker");

        [HarmonyPostfix]
        [HarmonyPatch(typeof(HideableLocker), "Clicked")]
        private bool ClickedPatch(HideableLocker __instance) {
            PM = (PlayerManager)_PlayerInLocker.GetValue(__instance);
            MainClass.Instance.PlayerInLocker = PM;


            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(HideableLocker), "CameraReset")]
        private bool CRPatch(HideableLocker __instance) {
            PM = (PlayerManager)_PlayerInLocker.GetValue(__instance);
            if (PM == null && !PM.plm.Entity.Frozen) {
                MainClass.Instance.PlayerInLocker = null;
            }


            return true;
        }

	
    }
}