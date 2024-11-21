using Unity.VisualScripting;
using UnityEngine;

public class GameFlow
{

    public static bool key1Obtained;
    public static bool key2Obtained;

    public static int kills = 0;

    public static void updateKills()
    {
        kills ++;        
    }
}
