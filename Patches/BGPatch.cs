using HarmonyLib;

namespace TheHardestMod
{
    [HarmonyPatch]
    public class BGPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(MainGameManager), "AllNotebooks")]

        static public bool AllNBPatch(MainGameManager __instance)  {

            // 
            if (TheHardestMod.MainClass.Instance.laps != 1 &&  Singleton<CoreGameManager>.Instance.sceneObject.levelTitle == "F4") {
                TheHardestMod.MainClass.Instance.laps += 1;
                foreach (Notebook nb in Singleton<BaseGameManager>.Instance.Ec.notebooks) {
                    nb.activity.ReInit();
                    Singleton<BaseGameManager>.Instance.AddNotebookTotal(1);
                }
                Singleton<BaseGameManager>.Instance.Ec.GetBaldi().SetAnger(3f);
                Singleton<CoreGameManager>.Instance.AddPoints(200,0,false);
                Singleton<BaseGameManager>.Instance.Ec.AddTimeScale(new TimeScaleModifier(1.025f,1.05f,1.05f));
                Singleton<CoreGameManager>.Instance.audMan.loop = true;
                Singleton<CoreGameManager>.Instance.audMan.PlaySingle(TheHardestMod.MainClass.Instance.Snd_FINALE);
                return false;

            } else {
                return true;
            }

        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(MainGameManager), "BeginPlay")]

        static public bool beginPlayPatch(MainGameManager __instance)  {
            TheHardestMod.MainClass.Instance.laps = 0;
            if (Singleton<CoreGameManager>.Instance.currentMode != Mode.Main) {
                // the one who is in explorer mode IS COOKED 💀
                // (spawns 100 baldis if not in hide and seek mode)
                for (int i=1; i < 100; i++) {
                    Singleton<CoreGameManager>.Instance.SetLives(-1);
                    Singleton<BaseGameManager>.Instance.Ec.SpawnNPC(Singleton<BaseGameManager>.Instance.levelObject.potentialBaldis[0].selection,__instance.Ec.AllTilesNoGarbage(false,false)[UnityEngine.Random.Range(0,__instance.Ec.AllTilesNoGarbage(false,false).Count())].position);
                    UnityEngine.Debug.LogError(new Exception("YOU CANNOT CHEAT YOUR WAY USING EXPLORER MODE, NOW SUFFER"));
                }

            }

            return true;
        }
	
    }
}

