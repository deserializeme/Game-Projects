using UnityEngine;
using System.Collections;

public class RangeCheck : MonoBehaviour
{


    /// <summary>
    /// returns true if target is in line of sight of the caster
    /// </summary>
    /// <param name="Source"></param>
    /// <param name="Target"></param>
    /// <returns></returns>
    public static bool LineOfSight(GameObject Source, GameObject Target)
    {
        bool HasLOS = false;

        Vector3 Direction = Target.transform.position - Source.transform.position;
        float Distance = RangeCheck.Distance(Source, Target);
        RaycastHit hit;

        if (Physics.Raycast(Source.transform.position, Direction, out hit, Distance))
        {
            if (hit.collider.gameObject == Target)
            {
                HasLOS = true;
            }
        }
        else
        {
            HasLOS = false;
        }

        return HasLOS;
    }

    /// <summary>
    /// returns distance from caster to target
    /// </summary>
    /// <param name="Source"></param>
    /// <param name="Target"></param>
    /// <returns></returns>
    public static float Distance(GameObject Source, GameObject Target)
    {
        float Distance = Vector3.Distance(Source.transform.position, Target.transform.position);
        return Distance;
    }
}