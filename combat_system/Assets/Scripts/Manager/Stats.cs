using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Buffs))]
[RequireComponent(typeof(ResourceManager))]
[RequireComponent(typeof(CastManager))]
[RequireComponent(typeof(TeamAffiliation))]
public class Stats : MonoBehaviour
{
    public CreateNewCharacter Toon_Profile;
    public GameObject LastAttackedBy;

    string ToonID;
    string Name;
    string Toon_Description;
    Sprite Icon;
    public float Experience;
    public float ExperianceToLevel;
    public int EvolutionLevel;
    public float ExperienceOnKill;
    public float Gold;
    public float GoldOnKill;

    CreateNewDamageType Type;
    CreateNewResource ResourceType;
    public float ResourceRegenPerTick;
    public float HealthRegenPerTick;


    float Base_Health;
    float Base_Speed;
    float Base_Power;

    public float Health_Percent;


    [SerializeField]
    private float healthMultiplier;
    public float HealthMultiplier
    {
        get
        {
            return healthMultiplier;
        }
        set
        {
            healthMultiplier = value;
        }
    }

    [SerializeField]
    private float speedMultiplier;
    public float SpeedMultiplier
    {
        get
        {
            return speedMultiplier;
        }
        set
        {
            speedMultiplier = value;
        }
    }

    [SerializeField]
    private float powerMultiplier;
    public float PowerMultiplier
    {
        get
        {
            return powerMultiplier;
        }
        set
        {
            powerMultiplier = value;
        }
    }

    [SerializeField]
    private float eXPMultiplier;
    public float EXPMultiplier
    {
        get
        {
            return eXPMultiplier;
        }
        set
        {
            eXPMultiplier = value;
        }
    }

    [SerializeField]
    private float health;
    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            Health_Percent = Health / Maxhealth * 100;
            if(Health <= 0)
            {
                DamageCalculations.Death(LastAttackedBy, gameObject);
            }
        }
    }

    [SerializeField]
    private float maxhealth;
    public float Maxhealth
    {
        get
        {
            return maxhealth;
        }
        set
        {
            maxhealth = value;
        }
    }

    [SerializeField]
    private float currentResourceAmount;
    public float CurrentResourceAmount
    {
        get
        {
            return currentResourceAmount;
        }
        set
        {
            currentResourceAmount = value;
        }
    }

    [SerializeField]
    private float maxResourceAmount;
    public float MaxResourceAmount
    {
        get
        {
            return maxResourceAmount;
        }
        set
        {
            maxResourceAmount = value;
        }
    }

    [SerializeField]
    private float speed;
    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }

    [SerializeField]
    private float power;
    public float Power
    {
        get
        {
            return power;
        }
        set
        {
            power = value;
        }
    }

    [SerializeField]
    private float hitChance;
    public float HitChance
    {
        get
        {
            return hitChance;
        }
        set
        {
            hitChance = value;
        }
    }

    [SerializeField]
    private int level;
    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
            ExperianceToLevel = LevelUp.EXPtoLevel(level);
            //put code to trigger an evolution here
        }
    }

    [SerializeField]
    private float damage_taken;
    public float Damage_Taken
    {
        get
        {
            return damage_taken;
        }
        set
        {
            damage_taken = value;
        }
    }

    [SerializeField]
    private float damage_dealt;
    public float Damage_Dealt
    {
        get
        {
            return damage_dealt;
        }
        set
        {
            damage_dealt = value;
        }
    }

    [SerializeField]
    private float healing_done;
    public float Healing_Done
    {
        get
        {
            return healing_done;
        }
        set
        {
            healing_done = value;
        }
    }

    [SerializeField]
    private float healing_taken;
    public float Healing_Taken
    {
        get
        {
            return healing_taken;
        }
        set
        {
            healing_taken = value;
        }
    }

    [SerializeField]
    private int kills;
    public int Kills
    {
        get
        {
            return kills;
        }
        set
        {
            kills = value;
        }
    }

    [SerializeField]
    private int deaths;
    public int Deaths
    {
        get
        {
            return deaths;
        }
        set
        {
            deaths = value;
        }
    }

    void Awake()
    {
        Name = Toon_Profile.Toon_Name;

        Base_Health = Toon_Profile.Base_Health;
        Base_Speed = Toon_Profile.Base_Speed;
        Base_Power = Toon_Profile.Base_Power;
        ExperienceOnKill = Toon_Profile.Experience;
        EvolutionLevel = Toon_Profile.EvolutionLevel;

        Speed = Base_Speed;
        Power = Base_Power;

        Maxhealth = Base_Health;
        Health = Maxhealth;
        Health_Percent = Health / Maxhealth * 100;
        HitChance = Toon_Profile.Base_Accuracy;

        Experience = 0;
        Gold = 0;

        GoldOnKill = Toon_Profile.Gold;
        Level = Toon_Profile.Level;
        Type = Toon_Profile.Type;

        HealthMultiplier = 1;
        SpeedMultiplier = 1;
        EXPMultiplier = 1;
        PowerMultiplier = 1;

        MaxResourceAmount = Toon_Profile.Base_Resource_Amount;
        CurrentResourceAmount = MaxResourceAmount;

        HealthRegenPerTick = Toon_Profile.Base_Health_Regen;
        ResourceRegenPerTick = Toon_Profile.Base_Resource_Regen;

    }

}
