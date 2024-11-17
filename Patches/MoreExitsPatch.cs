using HarmonyLib;


namespace TheHardestMod
{
    public class MoreExitsPatch
    {
        [HarmonyPatch(typeof(Directions))]
        [HarmonyPatch("All")]
        class AllowStuff
        {
            static bool Prefix(ref List<Direction> __result)
            {

                if ((Singleton<CoreGameManager>.Instance != null && Singleton<CoreGameManager>.Instance.sceneObject.levelObject != null ) && Singleton<CoreGameManager>.Instance.sceneObject.levelObject.exitCount <= 4 ) return true;
                System.Reflection.MethodBase fo = (new System.Diagnostics.StackTrace()).GetFrame(2).GetMethod(); //gets the thing that called it
                if (fo.Name == "MoveNext")
                {
                    List<Direction> directions = new List<Direction>
                {
                    Direction.North,
                    Direction.East,
                    Direction.South,
                    Direction.West,
                };
                    __result = new List<Direction>
                {
                    Direction.North,
                    Direction.East,
                    Direction.South,
                    Direction.West,
                };
                    for (int i = 0; i < Singleton<CoreGameManager>.Instance.sceneObject.levelObject.exitCount; i++)
                    {
                        __result.Add(directions[i % 4]); //this make it look pretty :D
                    }
                    return false;
                }

                return true;
            }
        }
    }
}