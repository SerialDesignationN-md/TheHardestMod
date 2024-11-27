using HarmonyLib;
using MTM101BaldAPI.UI;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace TheHardestMod.Patches
{
    [HarmonyPatch]
    internal class MainMenuPatch
    {
        [HarmonyPatch(typeof(MainMenu),"Start")]
        [HarmonyPostfix]
	   static void OnMainMenu(MainMenu __instance) {
            // var text = UIHelpers.CreateText<TextMeshProUGUI>(BaldiFonts.ComicSans18,"Modifiers",__instance.transform,Vector3.zero,true);
            // text.rectTransform.anchorMax = new Vector2(0.5f,0.8f);
            // text.rectTransform.anchorMin = new Vector2(0.5f,0.8f);
            // text.rectTransform.anchoredPosition = new Vector2(0,0);
            // text.color = Color.black;
            // text.raycastTarget  = true;
            // var ModifiersScreen = UIHelpers.CreateBlankUIScreen("Modifiers");
            // CursorController.Instance.transform.SetAsLastSibling();
            // __instance.transform.Find("Bottom").SetAsLastSibling();
            // __instance.transform.Find("BlackCover").SetAsLastSibling();
            // StandardMenuButton Modifiers = text.gameObject.ConvertToButton<StandardMenuButton>(true);

            // var menu = GameObject.Find("Menu");
            // Modifiers.OnPress.AddListener(() => {
            //     __instance.ActivateTransition(0.05f);
            //     menu.SetActive(false);
            //     ModifiersScreen.gameObject.AddComponent<CursorInitiator>();
            //     Debug.Log("pressed");

            // });

        }
    }
}

