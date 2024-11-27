using BepInEx;
using HarmonyLib;
using BepInEx.Configuration;
using MTM101BaldAPI;
using MTM101BaldAPI.ObjectCreation;
using MTM101BaldAPI.PlusExtensions;
using MTM101BaldAPI.Registers;
using EditorCustomRooms;
using MTM101BaldAPI.Components;
using TMPro;
using UnityEngine;
using MTM101BaldAPI.OptionsAPI;
using MTM101BaldAPI.AssetTools;
using System.Collections;
using TheHardestMod.ObjectExtensions;
using TheHardestMod.Extensions;
using TheHardestMod.Events;
using PlusLevelLoader;
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
        internal TextMeshProUGUI CurrentTimerText;
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
        internal AudioClip Sfx_Pandemonium_Moving;
        internal int laps = 0;
        internal SoundObject Snd_Sfx_RandomEffect_Drink;
        public SoundObject Snd_Sfx_Pandemonium_Scream;
        internal SoundObject Snd_Event_Baldi_Pdm;
        internal SoundObject Snd_Bang_Locker;
        internal AudioClip Event_Baldi_Pdm;
        internal LocalizationAsset Eng;
        internal SceneObject CurrSO;
        internal AudioClip MinigameMusic;
        internal AudioClip FINALE;
        internal AudioClip SpeedrunMusic;
        internal AudioClip Lap1;
        internal AudioClip Lap2;
        internal AudioClip Lap3;
        internal AudioClip Lap4;
        internal AudioClip nextLap;
        internal AudioClip PlusPoint;
        internal AudioClip MinusPoint;
        internal AudioClip Bal_Lap1;
        internal SoundObject Snd_mus_Minigame_Pand;
        internal SoundObject Snd_FINALE;
        internal SoundObject Snd_speedrunMusic;
        internal SoundObject Snd_Lap1;
        internal SoundObject Snd_Lap2;
        internal SoundObject Snd_Lap3;
        internal SoundObject Snd_Lap4;
        internal SoundObject Snd_NextLap;
        internal SoundObject Snd_Sfx_Pandemonium_Moving;
        internal SoundObject Snd_Bal_Lap1;
        internal SoundObject Snd_PlusPoint;
        internal SoundObject Snd_MinusPoint;
        internal RandomEvent PandemoniumEvent;
        internal PlayerManager PlayerInLocker;
        internal Notebook lastNotebookCollected;
        ConfigEntry<bool> InsaneMode;
        SceneObject Yay;
        void Awake()
        {
            var deeznuts = new GameObject("Mc");
            deeznuts.AddComponent<ModifiersCategorySettings>();
            Harmony harmony = new Harmony("Wenda.HardestMod");
            // Things to put
            MTM101BaldiDevAPI.AddWarningScreen("There are now modifiers (currently only 7) , to activate or deactivate a modifier just go in the settings ", false);
            // pre-release thing?
            MTM101BaldiDevAPI.AddWarningScreen("You are currently playing the last (4th) pre-release of the mod! \n Please do not give the mods to others \n\n and also it's not finished YET so there is still a <color=green>LOT</color> to come", false);
            
            // credits
            MTM101BaldiDevAPI.AddWarningScreen("oh yeah credits (purple = discord, red=youtube) thanks to: \n <color=purple>@cheemzit_kiri</color> for some sprites/sounds \n <color=purple>@_pixelguy</color> for helping me for some script and also baldi voice for events</color>\n <color=purple>@missingtextureman101</color> for helping me too, and also their api!", false);
            MTM101BaldiDevAPI.AddWarningScreen("thanks also to everyone down here who made some of the musics used in the mod:\n <color=red>@bartuscus</color> and <color=red>@NoLongerNullMUSIC</color>", false);
            MTM101BaldiDevAPI.AddWarningScreen("and finally thanks to <color=purple>@duckieundefined</color> and <color=purple>@test_dithered99</color> for playing the pre-release 1 and 2", false);
            modpath = MTM101BaldAPI.AssetTools.AssetLoader.GetModPath(this);

            MTM101BaldAPI.Registers.LoadingEvents.RegisterOnAssetsLoaded(this.Info,OnLoaded(),false);
        
            AssetLoader.LocalizationFromFile(Path.Combine(modpath,"Localization","Eng.json"), Language.English);
            InsaneMode = Config.Bind<bool>("Modifiers", "Insane Mode", false, "Removes everything that makes the game easier (Speed boost, baldi pausing (except pandemonium), and on lap 2 baldi will only slow a little bit instead of a lot) and every floor is bigger too Good luck!");
                CustomOptionsCore.OnMenuInitialize += AddCategory;



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
                    CustomLO.levelObject.standardDarkLevel = Color.black;
                    CustomLO.levelObject.standardLightStrength = 6;
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
                if (!InsaneMode.Value) {
                if (LevelName == "F1") {
                    aaa[0].maxRooms = 5;
                    aaa[0].minRooms = 5;
                    CustomLO.levelObject.maxSize = new IntVector2(28,28);
                    CustomLO.levelObject.minSize = new IntVector2(12,12);
                   
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
                   
                    
                    
                    CustomLO.usesMap = true;
                    CustomLO.levelObject.additionTurnChance = 500;
                    CustomLO.levelObject.deadEndBuffer = 1;
                    CustomLO.levelObject.forcedItems = [];
                    
                    
                }
            } else {
                if (LevelName == "F1") {
                    aaa[0].maxRooms = 6;
                    aaa[0].minRooms = 6;
                    CustomLO.levelObject.maxSize = new IntVector2(24,24);
                    CustomLO.levelObject.minSize = new IntVector2(16,16);
                   
                    CustomLO.mapPrice = Int32.MaxValue;
                    
                    CustomLO.usesMap = true;
                    CustomLO.levelObject.additionTurnChance = 16;
                    CustomLO.levelObject.deadEndBuffer = 1;
                    CustomLO.levelObject.forcedItems = [];
                    
                }
                if (LevelName == "F2") {
                    aaa[0].maxRooms = 20;
                    aaa[0].minRooms = 20;
                    aaa[1].maxRooms = 30;
                    aaa[1].minRooms = 20;
                    CustomLO.levelObject.exitCount = 12;
                    CustomLO.levelObject.maxSize = new IntVector2(50,50);
                    CustomLO.levelObject.minSize = new IntVector2(30,30);
                   
                    CustomLO.mapPrice = Int32.MaxValue;
                    
                    CustomLO.usesMap = true;
                    CustomLO.levelObject.additionTurnChance = -20;
                    CustomLO.levelObject.deadEndBuffer = 1;
                    CustomLO.levelObject.forcedItems = [];
                    
                    
                }
                if (LevelName == "F3") {
                    aaa[0].maxRooms = 40;
                    aaa[0].minRooms = 40;
                    aaa[1].maxRooms = 50;
                    aaa[1].minRooms = 40;
                    CustomLO.levelObject.exitCount = 50;
                    CustomLO.levelObject.maxSize = new IntVector2(90,90);
                    CustomLO.levelObject.minSize = new IntVector2(70,70);
                   
                    
                    
                    CustomLO.usesMap = true;
                    CustomLO.levelObject.additionTurnChance = 500;
                    CustomLO.levelObject.deadEndBuffer = 1;
                    CustomLO.levelObject.forcedItems = [];
                    
                    
                }


            }





            });


            var AllSmth = Resources.FindObjectsOfTypeAll<MathMachine>();

            


            MTM101BaldAPI.SaveSystem.ModdedSaveGame.AddSaveHandler(this.Info);
            Instance = this;
            harmony.PatchAll();
        }
        void OnMen(OptionsMenu instance, CustomOptionsHandler Ins) {
            if (Singleton<CoreGameManager>.Instance != null) return;
            var modifiers = Ins.AddCategory<CustomOptionsCategory>("Modifiers");


            

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

        void AddCategory(OptionsMenu __instance, CustomOptionsHandler handler)
        {
            if (Singleton<CoreGameManager>.Instance != null) return;
            handler.AddCategory<ModifiersCategory>("Modifiers");
        }

        private void CustomLevelGenerator(SceneObject SO) {
            RoomGroup[] aaa = [SO.levelObject.roomGroup.First(x => x.name == "Class"), SO.levelObject.roomGroup.First(x => x.name == "Faculty"), SO.levelObject.roomGroup.First(x => x.name == "Office")];
                SO.mapPrice = Int32.MaxValue;
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
                        
                    ];


                    if (SO.levelTitle == "F4") {
                        aaa[0].maxRooms = 40;
                        aaa[0].minRooms = 40;
                        aaa[1].maxRooms = 20;
                        aaa[1].minRooms = 10;
                        SO.levelObject.maxSize = new IntVector2(25,25);
                        SO.levelObject.minSize = new IntVector2(25,25);
                        SO.levelObject.exitCount = 8;
                        aaa[0].potentialRooms = [
                        new WeightedRoomAsset
                        {
                            selection = Classes[3],
                            weight = 100
                        }



                    ];
                    }








                    Debug.Log(SO.levelTitle + " has been modified sucessfully");

        }





            IEnumerator OnLoaded() {
            // assets
                yield return 8;
                yield return "Loading sounds...";
           
            randomEffect_PickUp = MTM101BaldAPI.AssetTools.AssetLoader.AudioClipFromFile(Path.Combine(modpath,"Sounds","pickup_randomglasseffect.wav"));
           
            MetalPipe_Sfx = MTM101BaldAPI.AssetTools.AssetLoader.AudioClipFromFile(Path.Combine(modpath,"Sounds","MetalPipe_SFX.wav"));

            MinigameMusic = MTM101BaldAPI.AssetTools.AssetLoader.AudioClipFromFile(Path.Combine(modpath,"Sounds","Knock Knock.wav"));

            FINALE = MTM101BaldAPI.AssetTools.AssetLoader.AudioClipFromFile(Path.Combine(modpath,"Sounds","THEFINALE.wav"));

            SpeedrunMusic = MTM101BaldAPI.AssetTools.AssetLoader.AudioClipFromFile(Path.Combine(modpath,"Sounds","dremdrama.mp3"));

            Lap1 = MTM101BaldAPI.AssetTools.AssetLoader.AudioClipFromFile(Path.Combine(modpath,"Sounds","Lap1.mp3"));

            Lap2= MTM101BaldAPI.AssetTools.AssetLoader.AudioClipFromFile(Path.Combine(modpath,"Sounds","Lap2.mp3"));

            Lap3= MTM101BaldAPI.AssetTools.AssetLoader.AudioClipFromFile(Path.Combine(modpath,"Sounds","youaresoDEAD.wav"));

            Lap4= MTM101BaldAPI.AssetTools.AssetLoader.AudioClipFromFile(Path.Combine(modpath,"Sounds","Lap4.wav"));
           
            Sfx_RandomEffect_Drink = MTM101BaldAPI.AssetTools.AssetLoader.AudioClipFromFile(Path.Combine(modpath,"Sounds","Drinking.wav"));

            nextLap = MTM101BaldAPI.AssetTools.AssetLoader.AudioClipFromFile(Path.Combine(modpath,"Sounds","nextLap.wav"));

            Sfx_Pandemonium_Scream = MTM101BaldAPI.AssetTools.AssetLoader.AudioClipFromFile(Path.Combine(modpath,"Sounds","Pand_SCREAM.wav"));

            Sfx_Pandemonium_Moving = MTM101BaldAPI.AssetTools.AssetLoader.AudioClipFromFile(Path.Combine(modpath,"Sounds","Pand_MOVING.wav"));


            var Sfx_Locker_Bang = MTM101BaldAPI.AssetTools.AssetLoader.AudioClipFromFile(Path.Combine(modpath,"Sounds","LockerBang.wav"));

            PlusPoint = MTM101BaldAPI.AssetTools.AssetLoader.AudioClipFromFile(Path.Combine(modpath,"Sounds","PlusPoint.wav"));

            MinusPoint = MTM101BaldAPI.AssetTools.AssetLoader.AudioClipFromFile(Path.Combine(modpath,"Sounds","LostPoint.wav"));

            Event_Baldi_Pdm = MTM101BaldAPI.AssetTools.AssetLoader.AudioClipFromFile(Path.Combine(modpath,"Voices","AnEntity.wav"));

            Bal_Lap1 = MTM101BaldAPI.AssetTools.AssetLoader.AudioClipFromFile(Path.Combine(modpath,"Voices","CollectingWow.wav"));
            yield return "Loading rooms...";
            
            Classes = EditorCustomRooms.RoomFactory.CreateAssetsFromPath(Path.Combine(modpath,"Rooms","ClassRooms.cbld"),40,true,null,false,false,null,true,false);

            var PendoRoom = EditorCustomRooms.RoomFactory.CreateAssetsFromPath(Path.Combine(modpath,"Rooms","PendoRoom.cbld"),40,true,null,false,false,null,true,false);
            
            yield return "Loading images...";

            MysteryPotionImage = AssetLoader.SpriteFromFile(Path.Combine(modpath,"Items","MysteryPotion.png"),new Vector2(0.5f,0.5f),30f);
            
            MetalPipeImage = AssetLoader.SpriteFromFile(Path.Combine(modpath,"Items","MetalPipe.png"),new Vector2(0.5f,0.5f),40f);

            HammerImage = AssetLoader.SpriteFromFile(Path.Combine(modpath,"Items","hammer.png"),new Vector2(0.5f,0.5f),1f);

            PandemoniumImage = AssetLoader.SpriteFromFile(Path.Combine(modpath,"Npcs","Pandemonium.png"),new Vector2(0.5f,0.5f),40f);
            
            CursorImage = AssetLoader.SpriteFromFile(Path.Combine(modpath,"Image","Cursor.png"),new Vector2(0.5f,0.5f),2f);

            GoalImage = AssetLoader.SpriteFromFile(Path.Combine(modpath,"Image","Goal.png"),new Vector2(0.5f,0.5f),2f);

            yield return "Creating sound objects...";
            
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

            Snd_Bal_Lap1 = MTM101BaldAPI.ObjectCreators.CreateSoundObject(Bal_Lap1,"bal_troll",SoundType.Voice,Color.green);

            Snd_Sfx_Pandemonium_Scream = MTM101BaldAPI.ObjectCreators.CreateSoundObject(Sfx_Pandemonium_Scream,"Pandemonium_SCREAM",SoundType.Voice,Color.black,1200);

            Snd_Sfx_Pandemonium_Moving = MTM101BaldAPI.ObjectCreators.CreateSoundObject(Sfx_Pandemonium_Moving,"Pandemonium_MOVING",SoundType.Voice,Color.black,1200);

            Snd_mus_Minigame_Pand = MTM101BaldAPI.ObjectCreators.CreateSoundObject(MinigameMusic,"*music*",SoundType.Music,Color.black);

            Snd_FINALE = MTM101BaldAPI.ObjectCreators.CreateSoundObject(FINALE,"*music*",SoundType.Music,Color.black,0.1f);

            Snd_speedrunMusic = MTM101BaldAPI.ObjectCreators.CreateSoundObject(SpeedrunMusic,"*music*",SoundType.Music,Color.black,0.1f);

            Snd_Lap1 = MTM101BaldAPI.ObjectCreators.CreateSoundObject(Lap1,"*music*",SoundType.Music,Color.black,0.1f);

            Snd_Lap2 = MTM101BaldAPI.ObjectCreators.CreateSoundObject(Lap2,"*music*",SoundType.Music,Color.black,0.1f);

            Snd_Lap3 = MTM101BaldAPI.ObjectCreators.CreateSoundObject(Lap3,"*music*",SoundType.Music,Color.black,0.1f);

            Snd_Lap4 = MTM101BaldAPI.ObjectCreators.CreateSoundObject(Lap4,"*music*",SoundType.Music,Color.black,0.1f);

            Snd_NextLap = MTM101BaldAPI.ObjectCreators.CreateSoundObject(nextLap,"*music*",SoundType.Music,Color.black,0.1f);

            Snd_Bang_Locker = MTM101BaldAPI.ObjectCreators.CreateSoundObject(Sfx_Locker_Bang,"*bang*",SoundType.Effect,Color.gray,0.6f);

            Snd_PlusPoint = MTM101BaldAPI.ObjectCreators.CreateSoundObject(PlusPoint,"UnusedSubtitle",SoundType.Effect,Color.gray,0.6f);

            Snd_MinusPoint = MTM101BaldAPI.ObjectCreators.CreateSoundObject(MinusPoint,"UnusedSubtitle",SoundType.Effect,Color.gray,0.6f);

            Snd_Sfx_Pandemonium_Scream.volumeMultiplier = 0.25f;
            Snd_Sfx_Pandemonium_Moving.volumeMultiplier = 0.5f;

            Snd_Lap1.subtitle = false;
            Snd_Lap2.subtitle = false;
            Snd_Lap3.subtitle = false;
            Snd_Lap4.subtitle = false;
            Snd_NextLap.subtitle = false;
            Snd_FINALE.subtitle = false;
            Snd_PlusPoint.subtitle = false;
            Snd_MinusPoint.subtitle = false;
            Snd_speedrunMusic.subtitle = false;
            // items
            yield return "Creating items...";
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
            yield return "Creating npcs...";
            // npcs
            Pandemonium = new NPCBuilder<PandemoniumNPC>(this.Info)
            .AddTrigger()
            .IgnoreBelts()
            .AddLooker()
            .SetMinMaxAudioDistance(10,10*100)

            .Build();

            Pandemonium.spriteRenderer[0].sprite = PandemoniumImage;

            Pandemonium.AudMan =  Pandemonium.GetComponent<PropagatedAudioManager>();

            // events
            yield return "Creating events...";
            PandemoniumEvent = new RandomEventBuilder<pandemoniumEvent>(this.Info)
            .SetSound(Snd_Event_Baldi_Pdm)
            .SetMinMaxTime(5,5)
            .SetEnum("PandeEvent")
            .AddRoomAsset(PendoRoom[0])
            .Build();



            
            var AudMan = MetalPipe.item.gameObject.CreateAudioManager(99999,99999);
            AudMan.audioDevice = MetalPipe.item.gameObject.CreateAudioSource(99999,99999);

            AudMan = RandomEffect.item.gameObject.CreateAudioManager(99999,99999);
            AudMan.audioDevice = RandomEffect.item.gameObject.CreateAudioSource(99999,99999);


            yield return "Creating and editing new floors...";
            // new floors
            var SceneObjects = Resources.FindObjectsOfTypeAll<SceneObject>();

            SceneObject F1 = SceneObjects.Where(x => x.levelTitle == "F1").First();
            SceneObject F2 = SceneObjects.Where(x => x.levelTitle == "F2").First();
            SceneObject F3 = SceneObjects.Where(x => x.levelTitle == "F3").First();
            
            var F4 = F3.DuplicateAndNewLevel("F4",4,true);
            F4.MarkAsNeverUnload();
            F3.nextLevel = F4;







            CustomLevelGenerator(F4);
        }
    }
}
