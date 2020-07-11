using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New DirectAttack", menuName = "New DirectAttack", order = 1)]
public class CreatNewDirectAttack : ScriptableObject
{
    public string Type = "Direct Attack";
    public string AttackID;
    public string AttackName;
    public string ToolTip;
    public Sprite Icon;

    public GameObject Target;
    public GameObject Source;

    public float Damage;
    public bool IsHeal;
    public CreateNewDamageType DamageType;

    public bool UseParticle;
    public GameObject Particle;
    public GameObject Secondary_Particle;

    public bool UseSecondaryParticle;
    public int ParticleDuration;
    public int SecondaryParticleDuration;

    public bool ApplyEffect;
    public bool Self;
    public bool EffectTarget;
    public CreateNewBuff SecondaryEffect;
    public CreateNewDOT SecondaryEffectDOT;

    public float CastTime;
    public float CoolDown;
    public float CoolDownRemaining;
    public bool ReadyToCast;

    public float ResourceCost;
    public CreateNewResource ResourceType;

    public bool HostileOnly;
    public bool FriendlyOnly;

    public int MaxRange;

    public Vector3 End;
    public Vector3 Begin;

    public AudioClip Cast;
    public AudioClip Land;

}
