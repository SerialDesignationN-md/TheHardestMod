using HarmonyLib;
using MTM101BaldAPI;
using MTM101BaldAPI.Reflection;
namespace TheHardestMod {
	public class EcPatch  {
		
		
		[HarmonyPrefix]
		[HarmonyPatch(typeof(BaseGameManager), "CollectNotebooks")]
		public static bool StartPatch(BaseGameManager __instance){
			__instance.AngerBaldi(-0.8f);
			
			 

			return true;
		}
		

	}




}