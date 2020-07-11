using UnityEngine;
using System.Collections;
using UnityEditor;

//creates a new scriptable object for Buffs

[CreateAssetMenu(fileName = "New Buff", menuName = "New Buff", order = 1)]
[System.Serializable]
public class CreateNewBuff : ScriptableObject
{
    public string Type = "Buff";
    public string BuffID;
    public string BuffName;
    public string ToolTip;
    public Sprite Icon;

    public GameObject Target;
    public GameObject Source;
    public float SourcePowerOnCast;

    public float Health_Multiplier;
    public float Speed_Multiplier;
    public float Power_Multiplier;
    public float Exp_Multiplier;
    public float Accuracy;

    public bool UseParticle;
    public GameObject Particle;
    public int ParticleDuration;

    public int Duration;
    public int Ticks;
    public bool Imediate;
    public bool Over_Time;
    public bool Falling;
    public bool Applied;

    public bool Stackable;
    public int StackLimit;
    public int CurrentStacks;

    public bool HostileOnly;
    public bool FriendlyOnly;

    public float CastTime;
    public float CoolDown;
    public float CoolDownRemaining;
    public bool ReadyToCast;

    public int MaxRange;

    public float ResourceCost;
    public CreateNewResource ResourceType;

    public AudioClip Cast;
    public AudioClip Land;

}