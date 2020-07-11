using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CreateAssetMenu(fileName = "New Spell Object", menuName = "New Spell Object", order = 1)]
[System.Serializable]
public class SpellObject : ScriptableObject {

    /// <summary>
    /// Scriptable object for creating new spells and saving them as prefabs/templates
    /// use SpellInstance for instantiation.
    /// </summary>

    //enum to declare the type of spell
    public enum SpellType
    {
        Buff,
        Debuff,
        DOT,
        HOT,
        DAttack,
        DHeal,
        PAttack,
        PHeal,
        Shield,
        Dispell
    }

    public enum Category
    {
        None,
        Magic,
        Curse,
        Elemental,
        Zone,
        Poison,
        Status
    }

    //Identification Data
    public SpellType SType;
    public Category SCategory;
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

    [System.Serializable]
    public class Particle
    {
        public GameObject ParticlePrefab;
        public float ParticleDuration;
        //spawn on self if true, on traget if false
        public bool OnSelf;
    }
    public List<Particle> Particles = new List<Particle>();

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

    //Damage behaviors
    public float ToatalDamage;
    public float CurrentDamage;
    public float Duration;
    public bool UnlimitedDuration;
    public float UTickLength;
    public int Ticks;
    public bool Applied;
    public enum OverTimeBehavior
    {
        Rising,
        Falling,
        Immediate,
        OnExpire
    }
    public OverTimeBehavior Behavior;
    public Timer SpellTimer;

    //Buff/Debuff Multipliers
    public List<Attributes> Attributes = new List<Attributes>();
    public List<float> Multipliers = new List<float>();

    //Dispell variables
    public bool RemoveAll;
    public int NumToRemove;
    public List<Category> RemovableTypes = new List<Category>();

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
    [System.Serializable]
    public class BreakEffect
    {
        // spell to cast on break
        public SpellObject BreakSpell;

        //if true, cast on self, 
        //if false, cast on breaker
        public bool CastOnSelf;
    }
    public List<BreakEffect> BreakEffects = new List<BreakEffect>();
    public float Shield_Duration;


    //secondary effect application
    public bool ApplyEffect;
    public GameObject SecondaryTarget;
    [System.Serializable]
    public class SecondarySpell
    {
        // spell to cast on break
        public SpellObject SecondSpell;

        //if true, cast on self, 
        //if false, cast on SecondaryTarget
        public bool CastOnSelf;
    }
    public List<SecondarySpell> SecondarySpells = new List<SecondarySpell>();

    // damage type
    public CreateNewDamageType DamageType;

    //audio information
    public enum ClipName
    {
        DuringCast,
        OnCast,
        InFlight,
        OnLand,
        OOM,
        Error
    }

    //Requirements before able to cast, ie only targets with X effect active can be affected by Y
    [System.Serializable]
    public class Requirements
    {
        public enum Reaction{
            Extra,
            Multiplicative,
            Enable, 
            Disable
        }

        public bool useBonus;
        public float Bonus;
        public int ReqItems;

        public SpellObject Spell;
        public bool UseSpell;

        public float HP_Percent;
        public bool UseHP;
    }
    public bool Req;
    public List<Requirements> Require = new List<Requirements>();

    //AOE options
    public bool isAOE;
    public enum AOEBehavior
    {
        OneShot,
        Pulse,
        Constant,
    }
    public AOEBehavior AOEstyle;
    public bool ActiveDuringTravel;
    public float Radius;
    public bool Shape;
    public int MaxEnemiesEffected;




    public class SpellSound
    {
        public ClipName SoundType;
        public AudioClip Sound;
    }
    public List<SpellSound> SpellSounds = new List<SpellSound>();


}
