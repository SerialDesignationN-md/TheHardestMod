using UnityEngine;
using TheHardestMod.ObjectExtensions;
using TheHardestMod;


namespace TheHardestMod.Npcs
{
    internal class PandemoniumNPC : NPC
    {

        [SerializeField]
        public PropagatedAudioManager AudMan;




        public override void Initialize()
        {
            base.Initialize();
            behaviorStateMachine.ChangeState(new Pandemonium_Chase(this));
            
            this.Navigator.SetSpeed(1);

        }
    }

    internal class Pandemonium_StateBase(PandemoniumNPC pandemonium) : NpcState(pandemonium)
    {

        protected PandemoniumNPC pand = pandemonium;
        

    }
    internal class Pandemonium_Chase(PandemoniumNPC pandemonium) : Pandemonium_StateBase(pandemonium)
    {
        protected float speed = 0f;
        protected float timeBeforeChase = 10f;
        private PandemoniumNPC pandemoniumPC = pandemonium;
        public override void Enter()
        {
            base.Enter();
            
            pandemoniumPC.AudMan.QueueAudio(TheHardestMod.MainClass.Instance.Snd_Sfx_Pandemonium_Scream);
            pandemoniumPC.AudMan.SetLoop(true);
            
            

        }

        public override void Update()
                {
                    base.Update();
                    ChangeNavigationState(new NavigationState_TargetPlayer(pandemoniumPC,9999,Singleton<CoreGameManager>.Instance.GetPlayer(0).transform.position,true));  
                    pandemoniumPC.Navigator.SetSpeed(speed);
                    timeBeforeChase -= Time.deltaTime;
                    var distance = (pandemoniumPC.transform.position - Singleton<CoreGameManager>.Instance.GetPlayer(0).transform.position).magnitude;
                    if (timeBeforeChase < 0) {
                        speed = Mathf.Lerp(speed,2000,0.0001f);
                    }



                    if (distance < 30 && Singleton<CoreGameManager>.Instance.GetPlayer(0).plm.Entity.Frozen) {
                        pandemoniumPC.behaviorStateMachine.ChangeState(new Pandemonium_Minigame(pandemoniumPC));
                    }

                }

        public override void OnStateTriggerEnter(Collider other)
                {
                    base.OnStateTriggerEnter(other);
                    if (other.gameObject.GetComponent<PlayerManager>() != null) {
                        Singleton<BaseGameManager>.Instance.Ec.GetBaldi().CaughtPlayer(Singleton<CoreGameManager>.Instance.GetPlayer(0));

                    }

                }



    }


    internal class Pandemonium_Minigame(PandemoniumNPC pandemonium) : Pandemonium_StateBase(pandemonium) {
        protected float speed = 1f;
        private PandemoniumNPC pandemoniumPC = pandemonium;
        private PandemoniumMinigame PanMini;
        private bool caught = false;
        public override void Enter()
        {
            base.Enter();
            
            pandemoniumPC.AudMan.QueueAudio(TheHardestMod.MainClass.Instance.Snd_Sfx_Pandemonium_Scream);
            pandemoniumPC.AudMan.SetLoop(true);
            Singleton<CoreGameManager>.Instance.audMan.PlaySingle(MainClass.Instance.Snd_mus_Minigame_Pand);
            PanMini = Singleton<BaseGameManager>.Instance.gameObject.AddComponent<PandemoniumMinigame>();

        }

        public override void Update()
                {
                    base.Update();
                    ChangeNavigationState(new NavigationState_TargetPlayer(pandemoniumPC,9999,Singleton<CoreGameManager>.Instance.GetPlayer(0).transform.position,true));  
                    pandemoniumPC.Navigator.SetSpeed(speed);
                    
                    if (!Singleton<CoreGameManager>.Instance.GetPlayer(0).plm.Entity.Frozen) {
                        speed += 5;
                    } else speed = 0;

                    if (PanMini.done) {
                        PanMini.Stop();
                        Singleton<CoreGameManager>.Instance.audMan.audioDevice.Stop();
                        pandemoniumPC.Despawn();
                        Singleton<CoreGameManager>.Instance.AddPoints(150,0,true,true);
                    }
                    if (PanMini.failure && !caught) {
                        Singleton<BaseGameManager>.Instance.Ec.GetBaldi().CaughtPlayer(Singleton<CoreGameManager>.Instance.GetPlayer(0));
                        caught = true;
                        PanMini.Stop();
                    }

                }

        public override void OnStateTriggerEnter(Collider other)
                {
                    base.OnStateTriggerEnter(other);
                    if (other.gameObject.GetComponent<PlayerManager>() != null) {
                        Singleton<BaseGameManager>.Instance.Ec.GetBaldi().CaughtPlayer(Singleton<CoreGameManager>.Instance.GetPlayer(0));
                        PanMini.Stop();
                    }

                }



    }
}