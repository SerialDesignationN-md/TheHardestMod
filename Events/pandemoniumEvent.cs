using MTM101BaldAPI;

namespace TheHardestMod.Events
{

    public class pandemoniumEvent : RandomEvent
    {
        RoomController CurrentRoom;
        public override void Begin()
        {

            Singleton<BaseGameManager>.Instance.Ec.SpawnNPC(TheHardestMod.MainClass.Instance.Pandemonium,CurrentRoom.AllEntitySafeCellsNoGarbage()[15].position);


            foreach (Door Door in CurrentRoom.doors) 
            {
                Door.Block(false);
            }
            base.Begin();
            
        }

       

        public override void AssignRoom(RoomController room)
        {
            CurrentRoom = room;
            foreach (Door Door in CurrentRoom.doors) 
            {
                Door.Block(true);
            }
            base.AssignRoom(room);
                    
                    
        }

    }
}