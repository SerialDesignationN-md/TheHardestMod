using HarmonyLib;
using UnityEngine;


namespace TheHardestMod
{
    public class MMPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(MathMachine), "Start")]
        private bool CorruptMath(MathMachine __instance) {
            if (UnityEngine.Random.Range(1f,3f)==1) __instance.Corrupt(true); 


            return true;
        }	
    }
}