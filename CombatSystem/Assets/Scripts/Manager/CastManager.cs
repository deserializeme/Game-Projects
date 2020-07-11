using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MovementEffects;

public class CastManager : MonoBehaviour
{

    public bool IsCasting;
    public bool Interruped;
    public float StartTime;
    public float EndTime;
    public float CurTime;
    public float Progress;
    public string Name;
    public bool Success;
    public GameObject Target;
    public CreateNewBuff Buff;
    public CreateNewDOT DOT;
    public CreateNewShield Shield;
    public CreateNewProjectile Projectile;
    public CreatNewDirectAttack DAttack;

    // Use this for initialization
    void Start()
    {

    }

    /// <summary>
    /// ends the attack process if attack was interrupted
    /// </summary>
    public void GotInterrupted()
    {
        StartTime = 0;
        EndTime = 0;
        Progress = 0;
        Name = null;
        IsCasting = false;
        Success = false;
        Interruped = false;
        Messages.Message("Got Interrupted");

    }

    /// <summary>
    /// if attack is a success, triggers a return to the attack checklist
    /// </summary>
    public void Successful()
    {
        Success = true;
        StartTime = 0;
        EndTime = 0;
        Progress = 0;
        Name = null;
        IsCasting = false;
        Messages.Message("Succesful spell cast");
        ReturnToAttack();

    }

    /// <summary>
    /// invokable method that will instantly hald the casting process
    /// </summary>
    public void InterruptNow()
    {
        Interruped = true;
    }

    /// <summary>
    /// begins the casting process
    /// </summary>
    /// <param name="EndTime"></param>
    /// <returns></returns>
    public IEnumerator<float> CastNow(float EndTime)
    {
        CurTime = Time.time;
        float Length = EndTime - CurTime;

        if (Length > 0)
        {
            Progress = ((CurTime - StartTime) / (EndTime - StartTime)) * 100;

            while (Progress <= 100)
            {
                CurTime = Time.time;

                Progress = ((CurTime - StartTime) / (EndTime - StartTime)) * 100;

                // maintains LOS during cast
                bool LOS = RangeCheck.LineOfSight(gameObject, Target);
                float TargetHP = Target.GetComponent<Stats>().Health;

                if (Interruped == false)
                {
                    if (CurTime >= EndTime)
                    {
                        Successful();
                        yield break;
                    }
                }
                else
                {
                    // lets us manually interrup
                    GotInterrupted();
                    yield break;
                }

                if (LOS == false)
                {
                    //interrupts if LOS is broken
                    GotInterrupted();
                    yield break;
                }

                if (TargetHP <= 0)
                {
                    //interrupts if target dies
                    GotInterrupted();
                    yield break;
                }
                yield return 0f;
            }
        }
        else
        {
            Successful();
        }
       
    }

    /// <summary>
    /// invokes the attack checklist 2nd phase
    /// </summary>
    public void ReturnToAttack()
    {
        Debug.Log("Returning to attack");
        
        if (Buff != null)
        {
            AttackChecklist.AttackPart2(gameObject, Target, Buff, null, null, null);
            Buff = null;
        }

        if (DOT != null)
        {
            AttackChecklist.AttackPart2(gameObject, Target, null, DOT, null, null);
            DOT = null;
        }

        if (DAttack != null)
        {
            Messages.Message("kicking off the next part");
            AttackChecklist.AttackPart2(gameObject, Target, null, null, DAttack, null);
            DAttack = null;
        }

        if (Shield != null)
        {
            AttackChecklist.AttackPart2(gameObject, Target, null, null, null, Shield);
            Shield = null;
        }

        if (Projectile != null)
        {
            Messages.Message("kicking off the next part");
            AttackChecklist.AttackPart2(gameObject, Target, null, null, null, null, Projectile);
            Projectile = null;
        }
    }

}
