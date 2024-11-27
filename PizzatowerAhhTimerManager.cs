using HarmonyLib;
using MTM101BaldAPI.UI;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TheHardestMod
{

    public class PizzatowerAhhTimerManager : MonoBehaviour
    {
        float baldiAngered = 0f;
        float timeToAdd = 0f;
        float timeBeforeUpdate = 1.5f;
	   float time = 10;
       float timeelapes = 0f;
       public float TimeBetweenPoint = 1f;
       bool isredded = false;
       List<Cell> toUpdate = new List<Cell>();
       TextMeshProUGUI Timer;
       void Start() {
            Timer = MTM101BaldAPI.UI.UIHelpers.CreateText<TextMeshProUGUI>(MTM101BaldAPI.UI.BaldiFonts.ComicSans24, (Mathf.Round(time*10)/10).ToString() + " - RUN",Singleton<CoreGameManager>.Instance.GetHud(0).Canvas().transform,Vector3.zero);
            MainClass.Instance.CurrentTimerText = Timer;
            Timer.color = Color.red;
            Timer.horizontalAlignment = HorizontalAlignmentOptions.Center;
            Timer.rectTransform.anchorMax = new Vector2(0.5f,0.15f);
            Timer.rectTransform.anchorMin = new Vector2(0.5f,0.15f);
       }
       void Update() {
            time -= Time.deltaTime;
            timeelapes += Time.deltaTime;
            timeBeforeUpdate -= Time.deltaTime;
            Timer.text = (Mathf.Round(time*10)/10).ToString() + " - RUN";
            if (time <= 0) {
                Singleton<BaseGameManager>.Instance.AngerBaldi(0.05f);
                baldiAngered += 0.05f;
                var cells = Singleton<BaseGameManager>.Instance.Ec.AllTilesNoGarbage(false,false);
                
                Singleton<BaseGameManager>.Instance.Ec.standardDarkLevel = new Color(1f,0f,0f);
                
                
                
            } else {
                Singleton<BaseGameManager>.Instance.AngerBaldi(-baldiAngered);
                baldiAngered = 0;

            }
            if (timeelapes >= TimeBetweenPoint) {
                
                Singleton<CoreGameManager>.Instance.GetComponent<ScoreManager>().AddScore(-2,true);
                timeelapes = 0;
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
       public void SetTime(float SetTime) {
            time = SetTime;
       }

    }
}