using HarmonyLib;


namespace TheHardestMod.Patches
{   
    public class ItemManagerPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(ItemManager),"Awake")]
        private bool AwakePatch(ItemManager __instance) {
            __instance.maxItem = 2;
            return true;
        }

	
    }
}