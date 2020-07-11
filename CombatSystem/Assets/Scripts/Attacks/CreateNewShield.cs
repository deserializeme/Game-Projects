using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New Shield", menuName = "New Shield", order = 1)]
[System.Serializable]
public class CreateNewShield : ScriptableObject {

    public string Type = "Shield";
    public string ShieldID;
    public string BuffName;
    public string ToolTip;
    public Sprite Icon;

    public GameObject Target;
    public GameObject Source;
    public float SourcePowerOnCast;

    public bool UseParticle;
    public GameObject Particle;
    public int ParticleDuration;

    public float MaxHealth;
    public float CurrentHealth;
    public bool Specific_Type;
    public CreateNewDamageType Damage_Type;
    public bool BreakEffect;
    public bool Self;
    public bool Breaker;
    public CreateNewBuff BreakBuff;
    public CreateNewDOT BreakDot;
    public CreatNewDirectAttack BreakAttack;
    public CreateNewProjectile BreakProjectile;

    public int Duration;
    public int Ticks;

    public bool HostileOnly;
    public bool FriendlyOnly;

    public bool Stackable;
    public int StackLimit;
    public int CurrentStacks;
    public bool Applied;

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
