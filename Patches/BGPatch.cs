using HarmonyLib;
using UnityEngine;
namespace TheHardestMod
{
    [HarmonyPatch]
    public class BGPatch
    {
        
        
        [HarmonyPrefix]
        [HarmonyPatch(typeof(MainGameManager), "AllNotebooks")]
        static public bool AllNBPatch(MainGameManager __instance)  {

            // 
            if (TheHardestMod.MainClass.Instance.laps == 0 &&  Singleton<CoreGameManager>.Instance.sceneObject.levelTitle == "F4") {
                TheHardestMod.MainClass.Instance.laps += 1;
                Singleton<BaseGameManager>.Instance.gameObject.AddComponent<PizzatowerAhhTimerManager>();
                foreach (Notebook nb in Singleton<BaseGameManager>.Instance.Ec.notebooks) {
                    if (nb != MainClass.Instance.lastNotebookCollected) {
                        nb.activity.ReInit();
                    Singleton<BaseGameManager>.Instance.AddNotebookTotal(1);
                    }
                    
                }
                Singleton<BaseGameManager>.Instance.Ec.GetBaldi().SetAnger(-9f);
                
                Singleton<CoreGameManager>.Instance.AddPoints(200,0,false);
                Singleton<BaseGameManager>.Instance.Ec.AddTimeScale(new TimeScaleModifier(1.025f,1.25f,1.5f));
                Singleton<CoreGameManager>.Instance.audMan.FlushQueue(true);
                Singleton<CoreGameManager>.Instance.audMan.QueueAudio(TheHardestMod.MainClass.Instance.Snd_FINALE);
                Singleton<CoreGameManager>.Instance.audMan.SetLoop(true);
                return false;

            } else {
                return true;
            }

        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(MainGameManager), "BeginPlay")]

        static public bool beginPlayPatch(MainGameManager __instance)  {
            TheHardestMod.MainClass.Instance.laps = 0;
            Singleton<BaseGameManager>.Instance.Ec.AddTimeScale(new TimeScaleModifier(1f,1.3f,1.75f));
            if (MainClass.Instance.CurrentTimerText != null) UnityEngine.Object.Destroy(MainClass.Instance.CurrentTimerText);
            if (Singleton<CoreGameManager>.Instance.currentMode != Mode.Main) {
                // the one who is in explorer mode IS COOKED 💀
                // (spawns 200 baldis if not in hide and seek mode)
                for (int i=1; i < 200; i++) {
                    Singleton<CoreGameManager>.Instance.SetLives(-1);
                    Singleton<BaseGameManager>.Instance.Ec.SpawnNPC(Singleton<BaseGameManager>.Instance.levelObject.potentialBaldis[0].selection,__instance.Ec.AllTilesNoGarbage(false,false)[UnityEngine.Random.Range(0,__instance.Ec.AllTilesNoGarbage(false,false).Count())].position);
                    UnityEngine.Debug.LogError(new Exception("YOU CANNOT CHEAT YOUR WAY USING EXPLORER MODE, NOW SUFFER"));

                }

            } else {
            
            }

            return true;
        }
        [HarmonyPrefix]
        [HarmonyPatch(typeof(MainGameManager), "CollectNotebook")]

        static public bool CollePatch(MainGameManager __instance, ref Notebook notebook)  {
            TheHardestMod.MainClass.Instance.lastNotebookCollected = notebook;
            if (Singleton<BaseGameManager>.Instance.GetComponent<PizzatowerAhhTimerManager>()  != null) {
                Singleton<BaseGameManager>.Instance.GetComponent<PizzatowerAhhTimerManager>().AddTime(6f);
            }
            if (Singleton<BaseGameManager>.Instance.Ec.GetBaldi() != null) {
                Singleton<BaseGameManager>.Instance.Ec.GetBaldi().Praise(3f);
            }
            return true;
        }

        
	
    }
}

