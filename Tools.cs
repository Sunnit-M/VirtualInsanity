using UnityEngine;

namespace VirtualInsanity;

public class Tools
{
    public static bool checkNear(Vector3 Player, Vector3 target, int range)
    {
        if (Player.x > target.x - range && Player.x < target.x + range && Player.z > target.z - range &&
            Player.z < target.z - range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}