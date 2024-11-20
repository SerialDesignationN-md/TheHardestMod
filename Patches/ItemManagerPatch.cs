using HarmonyLib;


namespace TheHardestMod.Patches
{   [HarmonyPatch]
    public class ItemManagerPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(ItemManager),"Awake")]
        static private void AwakePatch(ItemManager __instance) {
            
            Singleton<CoreGameManager>.Instance.GetHud(0).UpdateInventorySize(3);
            __instance.maxItem = 2;
            
        }

	
    }
}