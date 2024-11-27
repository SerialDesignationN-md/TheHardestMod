using HarmonyLib;
using MTM101BaldAPI.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TheHardestMod
{
    public class ScoreManager : MonoBehaviour
    {
	   public float multiplier = 1f;
       public float CurrentScore = 100f;
       public float OldScore = 100f;
       private float TimeElapsed = 0f;
       public TextMeshProUGUI CurrentScoreText;
       public void AddScore(float score, bool unaffectedMultiplier = false, bool Playsound = false, string reason = "") {
            var CalculatedScore = 0f;
            if (!unaffectedMultiplier) CurrentScore += score * multiplier;
            else CurrentScore += score;
            if (!unaffectedMultiplier) CalculatedScore += score * multiplier;
            else CalculatedScore += score;
            if (Playsound) {
                Singleton<CoreGameManager>.Instance.audMan.PlaySingle(CalculatedScore >= 0 ? MainClass.Instance.Snd_PlusPoint : MainClass.Instance.Snd_MinusPoint);
            }
            CalculatedScore = Mathf.Round(CalculatedScore);
            var addedScoreText = UIHelpers.CreateText<TextMeshProUGUI>(BaldiFonts.ComicSans36, CalculatedScore > 0 ? "<color=green>+" + CalculatedScore.ToString() + "</color>" : CalculatedScore < 0 ? "<color=red>" + CalculatedScore.ToString() + "</color>" : "<color=gray>" + CalculatedScore.ToString() + "</color>",Singleton<CoreGameManager>.Instance.GetHud(0).Canvas().transform,Vector3.zero);
            addedScoreText.rectTransform.anchorMax = new Vector2(UnityEngine.Random.Range(0.25f,0.35f),0.75f);
            addedScoreText.rectTransform.anchorMin = new Vector2(UnityEngine.Random.Range(0.25f,0.35f),0.75f);
            addedScoreText.enableWordWrapping = false;
            var addedScorereason = UIHelpers.CreateText<TextMeshProUGUI>(BaldiFonts.ComicSans18, CalculatedScore > 0 ? "<color=green>+" + reason + "</color>" : CalculatedScore < 0 ? "<color=red>" + reason + "</color>" : "<color=gray>" + reason + "</color>",Singleton<CoreGameManager>.Instance.GetHud(0).Canvas().transform,Vector3.zero);
            addedScorereason.rectTransform.anchorMax = new Vector2(UnityEngine.Random.Range(0.25f,0.35f),0.6f);
            addedScorereason.rectTransform.anchorMin = new Vector2(UnityEngine.Random.Range(0.25f,0.35f),0.6f);
            addedScorereason.enableWordWrapping = false;
            StartCoroutine(AnimText(addedScoreText, addedScorereason));

       }
       IEnumerator AnimText(TextMeshProUGUI text, TextMeshProUGUI reason)
        {
            var speed = 1.5f;
            
            
            for (float i = 0; i < 70; i+=1) {
                
                text.rectTransform.anchoredPosition += new Vector2(0,speed);
                reason.rectTransform.anchoredPosition += new Vector2(0,speed);
                speed -= 0.1f;
                if (i>50) { 
                text.alpha -= 0.05f;
                reason.alpha -= 0.05f;
            }
                yield return new WaitForSeconds(0.001f);
            }
            Destroy(text);
            Destroy(reason);

        }
        
       void Awake() {
            CurrentScoreText = UIHelpers.CreateText<TextMeshProUGUI>(BaldiFonts.ComicSans24,"score: null",Singleton<CoreGameManager>.Instance.GetHud(0).Canvas().transform,Vector3.zero) ;
            CurrentScoreText.rectTransform.anchorMax = new Vector2(0.15f,0.8f);
            CurrentScoreText.rectTransform.anchorMin = new Vector2(0.15f,0.8f);
            CurrentScoreText.color = Color.magenta;
            CurrentScoreText.horizontalAlignment = HorizontalAlignmentOptions.Center;
            CurrentScoreText.enableWordWrapping = false;

       }

       void Update() {
            TimeElapsed += Time.deltaTime;
            CurrentScoreText.text = "Score: " +Mathf.Round(CurrentScore).ToString() + "(X" +  multiplier.ToString() + ")";
            CurrentScoreText.rectTransform.anchoredPosition = new Vector2(CurrentScoreText.rectTransform.anchoredPosition.x,Mathf.Sin(TimeElapsed / 3) *20);
       }
    }
}