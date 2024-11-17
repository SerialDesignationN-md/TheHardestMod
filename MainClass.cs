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
using TheHardestMod.ObjectExtensions;
using TheHardestMod.Extensions;
using TheHardestMod.Events;
using TheHardestMod.Npcs;
namespace TheHardestMod
{
    [BepInPlugin("Wenda.HardestMod","The hardest mod", "0.0.0.0")]

    internal class MainClass : BaseUnityPlugin
    {

        internal static MainClass Instance {get; private set;}
        internal List<RoomAsset> Classes;
        internal string modpath;
        internal ItemObject RandomEffect;
        internal ItemObject MetalPipe;
        internal ItemObject Hammer;
        internal AudioClip randomEffect_PickUp;
        internal Sprite MysteryPotionImage;
        internal Sprite MetalPipeImage;
        internal Sprite HammerImage;
        internal Sprite PandemoniumImage;
        internal Sprite CursorImage;
        internal Sprite GoalImage;
        internal PandemoniumNPC Pandemonium;
        internal SoundObject MetalPipe_Sound;
        internal AudioClip MetalPipe_Sfx;
        internal AudioClip Sfx_RandomEffect_Drink;
        internal AudioClip Sfx_Pandemonium_Scream;
        internal int laps = 0;
        internal SoundObject Snd_Sfx_RandomEffect_Drink;
        public SoundObject Snd_Sfx_Pandemonium_Scream;
        internal SoundObject Snd_Event_Baldi_Pdm;
        internal SoundObject Snd_Bang_Locker;
        internal AudioClip Event_Baldi_Pdm;
        internal LocalizationAsset Eng;
        internal SceneObject CurrSO;
        internal AudioClip MinigameMusic;
        internal SoundObject Snd_mus_Minigame_Pand;
        internal RandomEvent PandemoniumEvent;
        internal PlayerManager PlayerInLocker;
        SceneObject Yay;
        void Awake()
        {

            Harmony harmony = new Harmony("Wenda.HardestMod");
            MTM101BaldiDevAPI.AddWarningScreen("You are currently playing the 2nd pre-release of the mod! \n Please do not give the mods to others \n\n and also it's not finished YET so there is still a <color=green>LOT</color> to come", false);
            
            MTM101BaldiDevAPI.AddWarningScreen("oh yeah credits (it's their discord username) thanks to: \n @cheemzit_kiri for some sprites/sounds \n @_pixelguy for helping me for some script and also baldi voice for events\n @missingtextureman101 for helping me too, and also their api!", false);
            MTM101BaldiDevAPI.AddWarningScreen("thanks to @duckieundefined and @test_dithered99 for playing the pre-release 1 and 2", false);
            modpath = MTM101BaldAPI.AssetTools.AssetLoader.GetModPath(this);

            MTM101BaldAPI.Registers.LoadingEvents.RegisterOnAssetsLoaded(this.Info,OnLoaded,false);
        
            AssetLoader.LocalizationFromFile(Path.Combine(modpath,"Localization","Eng.json"), Language.English);
            


            // Items








            MTM101BaldAPI.Registers.GeneratorManagement.Register(this, MTM101BaldAPI.Registers.GenerationModType.Override, (LevelName, LevelNo,CustomLO) => {
                RoomGroup[] aaa = [CustomLO.levelObject.roomGroup.First(x => x.name == "Class"), CustomLO.levelObject.roomGroup.First(x => x.name == "Faculty"), CustomLO.levelObject.roomGroup.First(x => x.name == "Office")];
                
                CustomLO.levelObject.minEventGap = 5;
                CustomLO.levelObject.maxEventGap = 120;
                aaa[0].potentialRooms = [
                        new WeightedRoomAsset
                        {
                            selection = Classes[0],
                            weight = 100
                        },
                        new WeightedRoomAsset
                        {
                            selection = Classes[1],
                            weight = 100
                        },
                        new WeightedRoomAsset
                        {
                            selection = Classes[2],
                            weight = 20
                        },
                        new WeightedRoomAsset
                        {
                            selection = Classes[3],
                            weight = 100
                        },
                        new WeightedRoomAsset
                        {
                            selection = Classes[4],
                            weight = 100
                        },
                        new WeightedRoomAsset
                        {
                            selection = Classes[5],
                            weight = 100
                        },
                        new WeightedRoomAsset
                        {
                            selection = Classes[6],
                            weight = 100
                        },
                        new WeightedRoomAsset
                        {
                            selection = Classes[7],
                            weight = 100
                        },
                        new WeightedRoomAsset
                        {
                            selection = Classes[8],
                            weight = 100
                        }



                    ];
                    CustomLO.levelObject.potentialItems = [
                    new WeightedItemObject{
                        weight = 100,
                        selection = RandomEffect
                    },
                    new WeightedItemObject{
                        weight = 10,
                        selection = MetalPipe
                    }
                    ];
                    CustomLO.totalShopItems = 1;
                    CustomLO.shopItems = [new WeightedItemObject{
                        weight = 100,
                        selection = MetalPipe
                    }];
                    CustomLO.levelObject.randomEvents = [
                        new WeightedRandomEvent{
                            weight = 100,
                            selection = PandemoniumEvent
                        }
                    ];

                if (LevelName == "F1") {
                    aaa[0].maxRooms = 3;
                    aaa[0].minRooms = 3;
                    CustomLO.levelObject.maxSize = new IntVector2(14,8);
                    CustomLO.levelObject.minSize = new IntVector2(8,8);
                   
                    CustomLO.mapPrice = Int32.MaxValue;
                    
                    CustomLO.usesMap = true;
                    CustomLO.levelObject.additionTurnChance = -20;
                    CustomLO.levelObject.deadEndBuffer = 1;
                    CustomLO.levelObject.forcedItems = [];
                    
                }
                if (LevelName == "F2") {
                    aaa[0].maxRooms = 12;
                    aaa[0].minRooms = 12;
                    aaa[1].maxRooms = 30;
                    aaa[1].minRooms = 20;
                    CustomLO.levelObject.exitCount = 10;
                    CustomLO.levelObject.maxSize = new IntVector2(50,50);
                    CustomLO.levelObject.minSize = new IntVector2(30,30);
                   
                    CustomLO.mapPrice = Int32.MaxValue;
                    
                    CustomLO.usesMap = true;
                    CustomLO.levelObject.additionTurnChance = -20;
                    CustomLO.levelObject.deadEndBuffer = 1;
                    CustomLO.levelObject.forcedItems = [];
                    
                    
                }
                if (LevelName == "F3") {
                    aaa[0].maxRooms = 30;
                    aaa[0].minRooms = 30;
                    aaa[1].maxRooms = 50;
                    aaa[1].minRooms = 40;
                    CustomLO.levelObject.exitCount = 50;
                    CustomLO.levelObject.maxSize = new IntVector2(60,60);
                    CustomLO.levelObject.minSize = new IntVector2(50,50);
                   
                    CustomLO.mapPrice = Int32.MaxValue;
                    
                    CustomLO.usesMap = true;
                    CustomLO.levelObject.additionTurnChance = 500;
                    CustomLO.levelObject.deadEndBuffer = 1;
                    CustomLO.levelObject.forcedItems = [];
                    
                    
                }






            });


            var AllSmth = Resources.FindObjectsOfTypeAll<MathMachine>();

            


            MTM101BaldAPI.SaveSystem.ModdedSaveGame.AddSaveHandler(this.Info);
            Instance = this;
            harmony.PatchAll()
        }
        IEnumerator LoadAssets() {
            
            randomEffect_PickUp = MTM101BaldAPI.AssetTools.AssetLoader.AudioClipFromFile(Path.Combine(modpath,"Sounds","pickup_randomglasseffect.wav"));
            MetalPipe_Sfx = MTM101BaldAPI.AssetTools.AssetLoader.AudioClipFromFile(Path.Combine(modpath,"Sounds","MetalPipe_SFX.wav"));
            
            
            Classes = EditorCustomRooms.RoomFactory.CreateAssetsFromPath(Path.Combine(modpath,"Rooms","ClassRooms.cbld"),40,true,null,false,false,null,true,false);
            
            MysteryPotionImage = AssetLoader.SpriteFromFile(Path.Combine(modpath,"Items","MysteryPotion.png"),new Vector2(0.5f,0.5f),30f);
            MetalPipeImage = AssetLoader.SpriteFromFile(Path.Combine(modpath,"Items","MetalPipe.png"),new Vector2(0.5f,0.5f),40f);
           


            yield break;
        }
        // Example of a method to override behavior in the game
        private void Update()
        {
            // Place code here that runs every frame (if needed)
        }

        private void CustomLevelGenerator(SceneObject SO) {
            RoomGroup[] aaa = [SO.levelObject.roomGroup.First(x => x.name == "Class"), SO.levelObject.roomGroup.First(x => x.name == "Faculty"), SO.levelObject.roomGroup.First(x => x.name == "Office")];
                
                SO.levelObject.minEventGap = 5;
                SO.levelObject.maxEventGap = 120;
                aaa[0].potentialRooms = [
                        new WeightedRoomAsset
                        {
                            selection = Classes[0],
                            weight = 100
                        },
                        new WeightedRoomAsset
                        {
                            selection = Classes[1],
                            weight = 100
                        },
                        new WeightedRoomAsset
                        {
                            selection = Classes[2],
                            weight = 20
                        },
                        new WeightedRoomAsset
                        {
                            selection = Classes[3],
                            weight = 100
                        },
                        new WeightedRoomAsset
                        {
                            selection = Classes[4],
                            weight = 100
                        },
                        new WeightedRoomAsset
                        {
                            selection = Classes[5],
                            weight = 100
                        },
                        new WeightedRoomAsset
                        {
                            selection = Classes[6],
                            weight = 100
                        },
                        new WeightedRoomAsset
                        {
                            selection = Classes[7],
                            weight = 100
                        },
                        new WeightedRoomAsset
                        {
                            selection = Classes[8],
                            weight = 100
                        }



                    ];
                    SO.levelObject.potentialItems = [
                    new WeightedItemObject{
                        weight = 100,
                        selection = RandomEffect
                    },
                    new WeightedItemObject{
                        weight = 10,
                        selection = MetalPipe
                    }
                    ];
                    SO.totalShopItems = 1;
                    SO.shopItems = [new WeightedItemObject{
                        weight = 100,
                        selection = MetalPipe
                    }];
                    SO.levelObject.randomEvents = [
                        new WeightedRandomEvent{
                            weight = 100,
                            selection = PandemoniumEvent
                        }
                    ];


                    if (SO.levelTitle == "F4") {
                        aaa[0].maxRooms = 2;
                        aaa[0].minRooms = 2;
                        SO.levelObject.maxSize = new IntVector2(20,20);
                        SO.levelObject.minSize = new IntVector2(20,20);
                        SO.levelObject.exitCount = 8;
                    }








                    Debug.Log(SO.levelTitle + " has been modified sucessfully");

        }





                private void OnLoaded() {
            // assets
           
            randomEffect_PickUp = MTM101BaldAPI.AssetTools.AssetLoader.AudioClipFromFile(Path.Combine(modpath,"Sounds","pickup_randomglasseffect.wav"));
           
            MetalPipe_Sfx = MTM101BaldAPI.AssetTools.AssetLoader.AudioClipFromFile(Path.Combine(modpath,"Sounds","MetalPipe_SFX.wav"));

            MinigameMusic = MTM101BaldAPI.AssetTools.AssetLoader.AudioClipFromFile(Path.Combine(modpath,"Sounds","Knock Knock.wav"));
           
            Sfx_RandomEffect_Drink = MTM101BaldAPI.AssetTools.AssetLoader.AudioClipFromFile(Path.Combine(modpath,"Sounds","Drinking.wav"));

            Sfx_Pandemonium_Scream = MTM101BaldAPI.AssetTools.AssetLoader.AudioClipFromFile(Path.Combine(modpath,"Sounds","Pand_SCREAM.wav"));

            var Sfx_Locker_Bang = MTM101BaldAPI.AssetTools.AssetLoader.AudioClipFromFile(Path.Combine(modpath,"Sounds","LockerBang.wav"));

            Event_Baldi_Pdm = MTM101BaldAPI.AssetTools.AssetLoader.AudioClipFromFile(Path.Combine(modpath,"Voices","baldiPdEvent.wav"));
            
            Classes = EditorCustomRooms.RoomFactory.CreateAssetsFromPath(Path.Combine(modpath,"Rooms","ClassRooms.cbld"),40,true,null,false,false,null,true,false);

            var PendoRoom = EditorCustomRooms.RoomFactory.CreateAssetsFromPath(Path.Combine(modpath,"Rooms","PendoRoom.cbld"),40,true,null,false,false,null,true,false);
            
            MysteryPotionImage = AssetLoader.SpriteFromFile(Path.Combine(modpath,"Items","MysteryPotion.png"),new Vector2(0.5f,0.5f),30f);
            
            MetalPipeImage = AssetLoader.SpriteFromFile(Path.Combine(modpath,"Items","MetalPipe.png"),new Vector2(0.5f,0.5f),40f);

            HammerImage = AssetLoader.SpriteFromFile(Path.Combine(modpath,"Items","hammer.png"),new Vector2(0.5f,0.5f),1f);

            PandemoniumImage = AssetLoader.SpriteFromFile(Path.Combine(modpath,"Npcs","Pandemonium.png"),new Vector2(0.5f,0.5f),40f);
            
            CursorImage = AssetLoader.SpriteFromFile(Path.Combine(modpath,"Image","Cursor.png"),new Vector2(0.5f,0.5f),2f);

            GoalImage = AssetLoader.SpriteFromFile(Path.Combine(modpath,"Image","Goal.png"),new Vector2(0.5f,0.5f),2f);

            
            
            // soundObjects
            SoundObject PickUpSoundRndEffect = ScriptableObject.CreateInstance<SoundObject>();
            PickUpSoundRndEffect.color = Color.white;
            PickUpSoundRndEffect.soundKey = "sfx_PickUp_ITM_RandomEffect";
            PickUpSoundRndEffect.soundType = SoundType.Effect;
            PickUpSoundRndEffect.soundClip = randomEffect_PickUp;

            MetalPipe_Sound = MTM101BaldAPI.ObjectCreators.CreateSoundObject(MetalPipe_Sfx, "Sfx_MetalPipe",SoundType.Effect,Color.gray);
            MetalPipe_Sound.volumeMultiplier = 20;

            Snd_Sfx_RandomEffect_Drink = MTM101BaldAPI.ObjectCreators.CreateSoundObject(Sfx_RandomEffect_Drink, "Sfx_RandomEffect_Drink",SoundType.Effect,Color.magenta);

            Snd_Event_Baldi_Pdm = MTM101BaldAPI.ObjectCreators.CreateSoundObject(Event_Baldi_Pdm,"event_pdm",SoundType.Voice,Color.green);

            Snd_Sfx_Pandemonium_Scream = MTM101BaldAPI.ObjectCreators.CreateSoundObject(Sfx_Pandemonium_Scream,"Pandemonium_SCREAM",SoundType.Voice,Color.black,1200);

            Snd_mus_Minigame_Pand = MTM101BaldAPI.ObjectCreators.CreateSoundObject(MinigameMusic,"*music*",SoundType.Music,Color.black);

            Snd_Bang_Locker = MTM101BaldAPI.ObjectCreators.CreateSoundObject(Sfx_Locker_Bang,"*bang*",SoundType.Effect,Color.gray,0.6f);

            Snd_Sfx_Pandemonium_Scream.volumeMultiplier = 0.25f;

            // items

            RandomEffect = new ItemBuilder(this.Info)
            .SetGeneratorCost(1)
            .SetEnum("RandomEffect")
            .SetShopPrice(100)
            .SetItemComponent<ITM_RandomEffect>()
            .SetSprites(MysteryPotionImage,MysteryPotionImage)
            .SetPickupSound(PickUpSoundRndEffect)
            .SetNameAndDescription("Itm_MysteryPotion", "Desc_MysteryPotion")
            .Build();

            MetalPipe = new ItemBuilder(this.Info)
            .SetGeneratorCost(60)
            .SetEnum("MetalPipe")
            .SetShopPrice(350)
            .SetItemComponent<ITM_MetalPipe>()
            .SetSprites(MetalPipeImage,MetalPipeImage)
            .SetNameAndDescription("Itm_MetalPipe_3", "Desc_MetalPipe")
            
            .Build();

            Hammer = new ItemBuilder(this.Info)
            .SetGeneratorCost(120)
            .SetEnum("Hammer")
            .SetShopPrice(500)
            .SetItemComponent<ITM_MetalPipe>()
            .SetSprites(HammerImage,HammerImage)
            .SetNameAndDescription("Itm_Hammer", "Desc_Hammer")
            
            .Build();

            // npcs
            Pandemonium = new NPCBuilder<PandemoniumNPC>(this.Info)
            .AddTrigger()
            .IgnoreBelts()
            .SetMinMaxAudioDistance(10,10*200)

            .Build();

            Pandemonium.spriteRenderer[0].sprite = PandemoniumImage;

            Pandemonium.AudMan =  Pandemonium.GetComponent<PropagatedAudioManager>();

            // events

            PandemoniumEvent = new RandomEventBuilder<pandemoniumEvent>(this.Info)
            .SetSound(Snd_Event_Baldi_Pdm)
            .SetMinMaxTime(120,180)
            .SetEnum("PandeEvent")
            .AddRoomAsset(PendoRoom[0])
            .Build();



            
            var AudMan = MetalPipe.item.gameObject.CreateAudioManager(99999,99999);
            AudMan.audioDevice = MetalPipe.item.gameObject.CreateAudioSource(99999,99999);

            AudMan = RandomEffect.item.gameObject.CreateAudioManager(99999,99999);
            AudMan.audioDevice = RandomEffect.item.gameObject.CreateAudioSource(99999,99999);



            // new floors
            var SceneObjects = Resources.FindObjectsOfTypeAll<SceneObject>();

            SceneObject F1 = SceneObjects.Where(x => x.levelTitle == "F1").First();
            SceneObject F2 = SceneObjects.Where(x => x.levelTitle == "F2").First();
            SceneObject F3 = SceneObjects.Where(x => x.levelTitle == "F3").First();
            
            var F4 = F1.DuplicateAndNewLevel("F4",4,true);
            F3.nextLevel = F4;

            F1.nextLevel = F4;






            CustomLevelGenerator(F4);
        }
    }
}
