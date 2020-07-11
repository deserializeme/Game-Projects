using UnityEngine;
using System.Collections;

public class ResourceManager : MonoBehaviour {

    public int StartingTick;
    public int NewTick;
    public int OldTick;

    public float HPRegen;
    public float ResourceRegen;
    public float CurrentHP;
    public float CurrentResources;
    public float MaxHP;
    public float MaxResources;

    public int RegenTime = 5;

    public bool SuspendRegen;

    Stats stats;

    void Start()
    {
        stats = gameObject.GetComponent<Stats>();


        StartingTick = Tick.Ticks;
        OldTick = 0;
    }

    void OnEnable()
    {
        Tick.NewTick += CheckTicks;
    }

    /// <summary>
    /// gets current stats from the Stats script
    /// </summary>
    void PullStats()
    {
        HPRegen = stats.HealthRegenPerTick;
        ResourceRegen = stats.ResourceRegenPerTick;
        CurrentHP = stats.Health;
        CurrentResources = stats.CurrentResourceAmount;
        MaxHP = stats.Maxhealth;
        MaxResources = stats.MaxResourceAmount;
    }

    /// <summary>
    /// handles resource regeneration over time
    /// </summary>
    public void TickNow()
    {
        // check to make sure we should be doing regen (no combat regen ETC...)
        if (SuspendRegen == false)
        {
            PullStats();

            if (CurrentHP < MaxHP)
            {
                float Difference = (MaxHP - CurrentHP);

                if (Difference > (HPRegen * RegenTime))
                {
                    stats.Health += (HPRegen * RegenTime);
                }
                else
                {
                    stats.Health += Difference;
                }
            }


            if (CurrentResources < MaxResources)
            {
                float Difference = (MaxResources - CurrentResources);

                if (Difference > (ResourceRegen * RegenTime))
                {
                    stats.CurrentResourceAmount += (ResourceRegen * RegenTime);
                }
                else
                {
                    stats.CurrentResourceAmount = MaxResources;
                }
            }
        }
      
    }

    /// <summary>
    /// monitors the global Ticks for next regen tick
    /// </summary>
    void CheckTicks()
    {
        NewTick = Tick.Ticks - StartingTick;

        if (NewTick > (OldTick + RegenTime))
        {
            TickNow();
            OldTick = (OldTick + RegenTime);
        }
    }

    /// <summary>
    /// returns true if source has enough of resource type
    /// </summary>
    /// <param name="Source"></param>
    /// <param name="SpellCost"></param>
    /// <returns></returns>
    public static bool ResouceCheck(GameObject Source, float SpellCost)
    {
        bool HasEnoughResources = false;

        float Resources = Source.GetComponent<Stats>().CurrentResourceAmount;
        //Messages.Message("move has a cost of " + SpellCost + " " + Source + " has " + Resources);

        if (SpellCost > Resources)
        {
            HasEnoughResources = false;
        }
        else
        {
            HasEnoughResources = true;
        }

        //Messages.Message("Has Resources? " + HasEnoughResources);
        return HasEnoughResources;
    }


    /// <summary>
    /// removes recources from caster
    /// </summary>
    /// <param name="Caster"></param>
    /// <param name="ResouceAmnt"></param>
    public static void DebitResources(GameObject Caster, float ResouceAmnt)
    {
        float Rescources = Caster.GetComponent<Stats>().CurrentResourceAmount;
        float Cost = ResouceAmnt;

        if (Rescources >= Cost)
        {
            Caster.GetComponent<Stats>().CurrentResourceAmount -= Cost;
            //Messages.Message("Deducted " + Cost  + " resources from " + Caster);
        }
        else
        {
            Caster.GetComponent<Stats>().CurrentResourceAmount = 0;
            //Messages.Message("Deducted " + Cost + " resources from" + Caster);
        }
        
    }

    /// <summary>
    /// adds resources to the caster
    /// </summary>
    /// <param name="Recipiant"></param>
    /// <param name="ResouceAmnt"></param>
    public static void CreditResources(GameObject Recipiant, float ResouceAmnt)
    {
        float Rescources = Recipiant.GetComponent<Stats>().CurrentResourceAmount;
        float MaxRescources = Recipiant.GetComponent<Stats>().MaxResourceAmount;
        float Check = Rescources + ResouceAmnt;

        if (Check <= MaxRescources)
        {
            Recipiant.GetComponent<Stats>().CurrentResourceAmount += ResouceAmnt;
        }
        else
        {
            Recipiant.GetComponent<Stats>().CurrentResourceAmount = MaxRescources;
            
        }

    }

    void OnDisable()
    {
        Tick.NewTick -= CheckTicks;
    }
}
