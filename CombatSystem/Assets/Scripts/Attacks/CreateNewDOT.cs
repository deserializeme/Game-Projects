using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New DOT", menuName = "New DOT", order = 1)]
[System.Serializable]
public class CreateNewDOT : ScriptableObject {

    public string Type = "DOT";
    public string DOT_ID;
    public string DOTName;
    public string ToolTip;
    public Sprite Icon;

    public GameObject Target;
    public GameObject Source;

    public float Damage;
    public bool IsHeal;
    public float CurrentDamage;

    public bool UseParticle;
    public GameObject Particle;
    public int ParticleDuration;

    public int Duration;
    public int Ticks;

    public bool Stackable;
    public int StackLimit;
    public int CurrentStacks;

    public float CastTime;
    public float CoolDown;
    public float CoolDownRemaining;
    public bool ReadyToCast;

    public bool HostileOnly;
    public bool FriendlyOnly;

    public float ResourceCost;
    public CreateNewResource ResourceType;

    public CreateNewDamageType DamageType;

    public int MaxRange;

    public AudioClip Cast;
    public AudioClip Land;

}
