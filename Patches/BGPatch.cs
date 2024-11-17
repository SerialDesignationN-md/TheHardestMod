using HarmonyLib;

namespace TheHardestMod
{
    public class BGPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(MainGameManager), "AllNotebooks")]
        public bool AllNBPatch(MainGameManager __instance)  {
            if (TheHardestMod.MainClass.Instance.laps <= 1 &&  Singleton<CoreGameManager>.Instance.sceneObject.levelTitle == "F4") {
                TheHardestMod.MainClass.Instance.laps++;
                foreach (Notebook nb in Singleton<BaseGameManager>.Instance.Ec.notebooks) {
                    nb.activity.ReInit();
                    Singleton<BaseGameManager>.Instance.AddNotebookTotal(1);
                }
                Singleton<BaseGameManager>.Instance.AngerBaldi(-15);
                Singleton<CoreGameManager>.Instance.AddPoints(200,0,false);
                Singleton<BaseGameManager>.Instance.Ec.AddTimeScale(new TimeScaleModifier(1.25f,1.25f,1.25f));
                return false;

            } else {
                return true;
            }

        }
	
    }
}

