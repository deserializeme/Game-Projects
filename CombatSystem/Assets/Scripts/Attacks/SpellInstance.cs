using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellInstance : MonoBehaviour {

    /// <summary>
    /// Class for pooling and instantiating spells
    /// </summary>
    /// 

    //Identification Data
    public SpellObject.SpellType SType;
    public SpellObject.Category SCategory;
    public string SpellID;
    public string SpellName;
    public string ToolTip;
    public Sprite Icon;

    //Stackable Behavior
    public bool Stackable;
    public int StackLimit;
    public int CurrentStacks;

    //Particle options
    public bool UseParticles;
    public List<SpellObject.Particle> Particles = new List<SpellObject.Particle>();

    //Who can be affected by the spell
    public bool Hostile;
    public bool Friendly;
    public bool Neutral;

    //Cast Info
    public float CastTime;
    public bool CastWhileMoving;
    public bool Channel;
    public float CoolDown;
    public float CD_Remaining;
    public bool Ready;

    //Range
    public int MaxRange;
    public int MinRange;

    //Resource Consumption
    public float Resource_Cost;
    public CreateNewResource Resource_Type;

    //Data Holding
    public GameObject Source;
    public GameObject Target;

    public float Source_PowerOnCast;

    //DOT/HOT/Buff/Debuff over time behaviors
    public float ToatalDamage;
    public float CurrentDamage;
    public float Duration;
    public bool UnlimitedDuration;
    public float UTickLength;
    public int Ticks;
    public bool Applied;
    public SpellObject.OverTimeBehavior Behavior;
    public Timer SpellTimer;

    //Buff/Debuff Multipliers
    public List<Attributes> Attributes = new List<Attributes>();
    public List<float> Multipliers = new List<float>();

    //Dispell variables
    public bool RemoveAll;
    public int NumToRemove;
    public List<SpellObject.Category> RemovableTypes = new List<SpellObject.Category>();

    //projectile information
    public CreateSplineProfile Path;
    public float TravelTime;
    public AnimationCurve TravelBehavior;

    //general use vectors
    public Vector3 Begin;
    public Vector3 End;

    //Shield Information
    public float Shield_MaxHealth;
    public float Shield_CurrentHealth;
    public bool Specific_Type;
    public List<CreateNewDamageType> ShieldType = new List<CreateNewDamageType>();
    public bool SpellOnBreak;
    public List<SpellObject.BreakEffect> BreakEffects = new List<SpellObject.BreakEffect>();
    public float Shield_Duration;


    //secondary effect application
    public bool ApplyEffect;
    public GameObject SecondaryTarget;
    public List<SpellObject.SecondarySpell> SecondarySpells = new List<SpellObject.SecondarySpell>();

    // damage type
    public CreateNewDamageType DamageType;

    //Requirements before able to cast, ie only targets with X effect active can be affected by Y
    public bool Req;
    public List<SpellObject.Requirements> Require = new List<SpellObject.Requirements>();

    //AOE Options
    public bool isAOE;
    public SpellObject.AOEBehavior AOEstyle;
    public bool ActiveDuringTravel;
    public float Radius;
    //true = Cube, False = Sphere.
    public bool Shape;
    public int MaxEnemiesEffected;

    //audio information
    public List<SpellObject.SpellSound> SpellSounds = new List<SpellObject.SpellSound>();
}
