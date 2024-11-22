using UnityEngine;
using TMPro;

namespace TheHardestMod
{

    public class PizzatowerAhhTimerManager : MonoBehaviour
    {
        float baldiAngered = 0f;
        float timeToAdd = 0f;
        float timeBeforeUpdate = 1.5f;
	   float time = 10;
       List<Cell> toUpdate = new List<Cell>();
       TextMeshProUGUI Timer;
       void Start() {
            Timer = MTM101BaldAPI.UI.UIHelpers.CreateText<TextMeshProUGUI>(MTM101BaldAPI.UI.BaldiFonts.ComicSans24, (Mathf.Round(time*10)/10).ToString() + " - RUN",Singleton<CoreGameManager>.Instance.GetHud(0).Canvas().transform,Vector3.zero);
            MainClass.Instance.CurrentTimerText = Timer;
            Timer.color = Color.red;

       }
       void Update() {
            time -= Time.deltaTime;
            timeBeforeUpdate -= Time.deltaTime;
            Timer.text = (Mathf.Round(time*10)/10).ToString() + " - RUN";
            if (time <= 0) {
                Singleton<BaseGameManager>.Instance.AngerBaldi(0.05f);
                baldiAngered += 0.05f;
                var cells = Singleton<BaseGameManager>.Instance.Ec.AllTilesNoGarbage(false,false);
                var cell = cells[UnityEngine.Random.Range(0, cells.Count)];
                if (cell.hasLight) {
                    cell.lightColor = Color.red;
                    cell.SetLight(true);
                    cell.lightStrength = 7;
                }
                Singleton<BaseGameManager>.Instance.Ec.standardDarkLevel = new Color(1f,0f,0f);
                Singleton<BaseGameManager>.Instance.Ec.QueueLightSourceForRegenerate(cell);
                
                
            } else {
                Singleton<BaseGameManager>.Instance.AngerBaldi(-baldiAngered);
                baldiAngered = 0;

            }
            if (timeBeforeUpdate <= 0) {
                
                    

            }
            if (timeToAdd >= 0 ) {
                timeToAdd -= 0.2f;
                time += 0.2f;
            }
       }
       public void AddTime(float timeAdd) {
            timeToAdd += timeAdd;
       }
       

    }
}