using BepInEx;
using HarmonyLib;
using MTM101BaldAPI;
using MTM101BaldAPI.ObjectCreation;
using MTM101BaldAPI.PlusExtensions;
using MTM101BaldAPI.Registers;
using EditorCustomRooms;
using MTM101BaldAPI.Components;

using UnityEngine;
using MTM101BaldAPI.AssetTools;
using System.Collections;
using TheHardestMod.Extensions;
// made by @PixelGuy need to find a way to credit him in the mod
// cuz pre-release of the mod wont be on gb :P
namespace TheHardestMod.ObjectExtensions
{
    public static class ObjectCreationExtensions
    {
	   public static AudioManager CreateAudioManager(this GameObject target, float minDistance = 25f, float maxDistance = 50f)
        {
            var audio = target.AddComponent<AudioManager>();
            audio.audioDevice = target.CreateAudioSource(minDistance, maxDistance);

            return audio;
        }
        public static AudioSource CreateAudioSource(this GameObject target, float minDistance = 25f, float maxDistance = 50f)
        {
            var audio = target.AddComponent<AudioSource>();
            audio.minDistance = minDistance;
            audio.maxDistance = maxDistance;
            return audio;
        }


        public static ItemObject DuplicateItem(this ItemObject target, string newName, bool Enabled) {
            var target2IO = UnityEngine.GameObject.Instantiate(target);
            var target2 = UnityEngine.GameObject.Instantiate(target.item);
            target2IO.AddMeta(target.GetMeta());
            target2.gameObject.ConvertToPrefab(Enabled);
            target2IO.item = target2;
            target2IO.name = newName;
            target2IO.nameKey = newName;
            return target2IO;
        }
        [Obsolete("just use Mathf.lerp()")]
        public static float lerp(this float a,float b,float t) {
            return Mathf.Lerp(a,b,t);
        }

        public static SceneObject DuplicateAndNewLevel(this SceneObject SO,string NewLvName, int LevelNo, bool Isfinal = false)
        {
            
            SceneObject OldLev = SO;
            if (OldLev != null)
            {
                SceneObject Newlev = UnityEngine.GameObject.Instantiate(OldLev);
                LevelObject NewOlev = UnityEngine.GameObject.Instantiate(OldLev.levelObject);
                Newlev.levelTitle = NewLvName;
                Newlev.levelNo = LevelNo;
                Newlev.levelObject = NewOlev;
                OldLev.nextLevel = Newlev;
                if (OldLev.levelObject.finalLevel == true) OldLev.levelObject.finalLevel = false ;
                Debug.Log(NewLvName + "Has been created with his previous level being " + OldLev.name);
                return Newlev;
            } else
            {
                Debug.LogError("Level " + NewLvName + " Could not have been created bc the Previous Level Has not be assigned");
                return null;
            }

        }



        
    }
}