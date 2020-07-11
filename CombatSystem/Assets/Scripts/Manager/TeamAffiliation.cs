using UnityEngine;
using System.Collections;

public class TeamAffiliation : MonoBehaviour
{

    public enum TeamColor
    {
        Red,
        Blue,
        Green,
        Neutral
    }

    public TeamColor Team;

    public static bool IsHostile(GameObject Source, GameObject Target)
    {
        TeamColor MyColor = Source.GetComponent<TeamAffiliation>().Team;
        TeamColor TargetColor = Target.GetComponent<TeamAffiliation>().Team;

        bool HostileDetected = false;

        if (MyColor != TargetColor)
        {
            if(TargetColor != TeamColor.Neutral)
            {
                HostileDetected = true;
            }
        }

        if (MyColor == TargetColor)
        {
            HostileDetected = false;
        }

        return HostileDetected;

    }
}
