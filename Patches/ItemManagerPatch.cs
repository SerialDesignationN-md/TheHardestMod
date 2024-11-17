using HarmonyLib;


namespace TheHardestMod.Patches
{   
    public class ItemManagerPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(ItemManager),"Awake")]
        private bool AwakePatch(ItemManager __instance) {
            
            Singleton<CoreGameManager>.Instance.GetHud(0).UpdateInventorySize(10);
            return true;
        }

	
    }
}