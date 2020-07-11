using UnityEngine;
using System.Collections;

public class DamageCalculations : MonoBehaviour {

    // raw damage calculation math goes in here. Not much to comment its all formulaic stuff 
    // like [[Power * SpellDamage] +/- resistances] - [shields] = finaldamage
    // tried to spell it all out in the variable names

    /// <summary>
    /// returns damage dealt by CreatNewDirectAttack
    /// </summary>
    /// <param name="Source"></param>
    /// <param name="Target"></param>
    /// <param name="Attack"></param>
    /// <returns></returns>
    public static float DirectDamage(GameObject Source, GameObject Target, CreatNewDirectAttack Attack)
    {
        float DamageCalc;
        
            float Resistance = DamageCalculations.DamageTypeCheck(Target, Attack.DamageType);        // checks to see if there is a weak/strong matchup between elemental types
            float Power = Source.GetComponent<Stats>().Power;

        if (Attack.IsHeal)
        {
            DamageCalc = Attack.Damage * Power;
        }
        else
        {
            DamageCalc = (Attack.Damage * Power) * Resistance;                                   // finds the final number for the attacks damage
            DamageCalc = DamageCalculations.DestroyShields(Source, Target, Attack.DamageType, DamageCalc);      // hits any shields that might blocks the attack
            Messages.Message("Damage clac = " + DamageCalc);
        }

        return DamageCalc;
    }

    /// <summary>
    /// returns damage amount dealt by CreateNewProjectile
    /// </summary>
    /// <param name="Source"></param>
    /// <param name="Target"></param>
    /// <param name="Attack"></param>
    /// <returns></returns>
    public static float ProjectileDamage(GameObject Source, GameObject Target, CreateNewProjectile Attack)
    {
        float DamageCalc;

        float Resistance = DamageCalculations.DamageTypeCheck(Target, Attack.DamageType);        // checks to see if there is a weak/strong matchup between elemental types
        float Power = Source.GetComponent<Stats>().Power;

        if (Attack.IsHeal)
        {
            DamageCalc = Attack.Damage * Power;
        }
        else
        {
            DamageCalc = (Attack.Damage * Power) * Resistance;                                   // finds the final number for the attacks damage
            DamageCalc = DamageCalculations.DestroyShields(Source, Target, Attack.DamageType, DamageCalc);      // hits any shields that might blocks the attack
            Messages.Message("Damage clac = " + DamageCalc);
        }

        return DamageCalc;
    }

    /// <summary>
    /// returns individual tick's damage amount from CreateNewDOT
    /// </summary>
    /// <param name="Source"></param>
    /// <param name="Target"></param>
    /// <param name="DOT"></param>
    /// <returns></returns>
    public static float DOTdamage(GameObject Source, GameObject Target, CreateNewDOT DOT)
    {
        float DamageCalc;

        float Resistance = DamageCalculations.DamageTypeCheck(Target, DOT.DamageType);        // checks to see if there is a weak/strong matchup between elemental types
        float Power = Source.GetComponent<Stats>().Power;

        if (DOT.IsHeal)
        {
            DamageCalc = ((DOT.CurrentDamage / DOT.Duration) * Power);
        }
        else
        {
            DamageCalc = ((DOT.CurrentDamage / DOT.Duration) * Power) * Resistance;          // finds the final number for the attacks damage
            DamageCalc = DamageCalculations.DestroyShields(Source, Target, DOT.DamageType, DamageCalc);      // hits any shields that might blocks the attack
            Messages.Message("Damage clac = " + DamageCalc);
        }

        return DamageCalc;
    }

    //health Buffs
    /// <summary>
    /// returns float values 0 and 1. 0 = new max HP, 1 = new current HP
    /// </summary>
    /// <param name="Buff"></param>
    /// <returns></returns>
    public static float[] BuffHealth(CreateNewBuff Buff)
    {
        float[] Values = new float[2];
        Stats stats = Buff.Target.GetComponent<Stats>();

        float TargetBaseHP = stats.Toon_Profile.Base_Health;
        float TargetMaxHP = stats.Maxhealth;
        float TargetCurrentHP = stats.Health;
        float SourcePower = Buff.SourcePowerOnCast;

        float SpellMultiplier = Buff.Health_Multiplier;
        float SpellDuration = Buff.Duration;
        float Ticks = Buff.Ticks;
        int Stacks = Buff.CurrentStacks;
        SpellMultiplier = SpellMultiplier * Stacks;

        if (Buff.Imediate == true)
        {
            if (Buff.Applied == false)                                                                      // because the buff script runs on the global tick repeatedly, the Applied flag lets the script know 
            {                                                                                               // when to ignore a buff it has already applied. Over time buffs dont get that 
                float TotalHeal = (TargetBaseHP * (SpellMultiplier * SourcePower));                         // treatment since there is something to do with them every tick.
                float NewMaxHP = TargetMaxHP + TotalHeal;
                float NewHP = TargetCurrentHP + TotalHeal;
                Values[0] = NewMaxHP;
                Values[1] = NewHP;
            }
        }

        if (Buff.Over_Time == true)
        {
            float PerTick = (TargetBaseHP * ((SpellMultiplier * SourcePower) / SpellDuration));
            float NewMaxHP = TargetMaxHP + PerTick;
            float NewHP = TargetCurrentHP + PerTick;
            Values[0] = NewMaxHP;
            Values[1] = NewHP;
        }


      if (Buff.Falling == true)
        {
            if (Buff.Applied == false)
            {
                float TotalHeal = (TargetBaseHP * (SpellMultiplier * SourcePower));
                float NewMaxHP = TargetMaxHP + TotalHeal;
                float NewHP = TargetCurrentHP + TotalHeal;
                Values[0] = NewMaxHP;
                Values[1] = NewHP;
                
            }
            else
            {
                float PerTick = (TargetBaseHP * ((SpellMultiplier * SourcePower) / SpellDuration));
                float NewMaxHP = TargetMaxHP - PerTick;
                float NewHP = TargetCurrentHP - PerTick;
                Values[0] = NewMaxHP;
                Values[1] = NewHP;
            }
        }
        return Values;
    }

    /// <summary>
    /// returns float values 0 and 1. 0 = new max HP, 1 = new current HP
    /// </summary>
    /// <param name="Buff"></param>
    /// <returns></returns>
    public static float[] RemoveHealthBuff(CreateNewBuff Buff)
    {
        float[] Values = new float[2];

        Stats stats = Buff.Target.GetComponent<Stats>();
        float TargetBaseHP = stats.Toon_Profile.Base_Health;
        float TargetMaxHP = stats.Maxhealth;
        float TargetCurrentHP = stats.Health;
        float SourcePower = Buff.SourcePowerOnCast;

        float SpellMultiplier = Buff.Health_Multiplier;
        float SpellDuration = Buff.Duration;
        int Ticks = Buff.Ticks;
        int Stacks = Buff.CurrentStacks;
        SpellMultiplier = SpellMultiplier * Stacks;

        if (Buff.Imediate == true)
        {
            float HealAmount = (TargetBaseHP * (SpellMultiplier * SourcePower));
            float NewMaxHP = TargetMaxHP - HealAmount;
            float NewHP = TargetCurrentHP - HealAmount;
            Values[0] = NewMaxHP;
            Values[1] = NewHP;
        }

        if (Buff.Over_Time == true)
        {
            float PerTick = (TargetBaseHP * ((SpellMultiplier * SourcePower) / SpellDuration));
            float TotalHeal = (PerTick * SpellDuration);
            float HealedAlready = (PerTick * Ticks);

            float NewMaxHP = TargetMaxHP - HealedAlready;
            float NewHP = TargetCurrentHP - HealedAlready;

            Values[0] = NewMaxHP;
            Values[1] = NewHP;
        }

        if (Buff.Falling == true)
        {
            float PerTick = (TargetBaseHP * ((SpellMultiplier * SourcePower) / SpellDuration));
            float TotalHeal = (PerTick * SpellDuration);
            float HealRemaining = (PerTick * (SpellDuration - Ticks));


            float NewMaxHP = TargetMaxHP - HealRemaining;
            float NewHP = TargetCurrentHP - HealRemaining;

            Values[0] = NewMaxHP;
            Values[1] = NewHP;
        }

        return Values;
    }

    //power Buffs
    /// <summary>
    /// returns float new power after buff is applied
    /// </summary>
    /// <param name="Buff"></param>
    /// <returns></returns>
    public static float BuffPower(CreateNewBuff Buff)
    {
        Stats stats = Buff.Target.GetComponent<Stats>();

        float TargetBasePower = stats.Toon_Profile.Base_Power;
        float TargetPower = stats.Power;
        float SourcePower = Buff.SourcePowerOnCast;
        float NewPower = TargetPower;

        float SpellMultiplier = Buff.Power_Multiplier;
        float SpellDuration = Buff.Duration;
        float Ticks = Buff.Ticks;
        int Stacks = Buff.CurrentStacks;
        SpellMultiplier = SpellMultiplier * Stacks;


        if (Buff.Imediate == true && Buff.Applied == false)
        {
            float PowerFromBuff = TargetBasePower * (SourcePower * SpellMultiplier);
            NewPower = TargetPower + PowerFromBuff;  
        }

        if (Buff.Over_Time == true)
        {
            float PerTick = TargetBasePower * ((SourcePower * SpellMultiplier) / SpellDuration);
            NewPower = TargetPower + PerTick;
        }

        if (Buff.Falling == true)
        {
            if (Buff.Applied == false)
            {
                float PerTick = TargetBasePower * ((SourcePower * SpellMultiplier) / SpellDuration);
                float PowerFromBuff = PerTick * SpellDuration;
                NewPower = TargetPower + PowerFromBuff;
            }
            else
            {
                float PerTick = TargetBasePower * ((SourcePower * SpellMultiplier) / SpellDuration);
                NewPower = TargetPower - PerTick;
            }
        }

        return NewPower;
    }

    /// <summary>
    /// returns float NewPower after buff is removed
    /// </summary>
    /// <param name="Buff"></param>
    /// <returns></returns>
    public static float RemovePowerBuff(CreateNewBuff Buff)
    {
        Stats stats = Buff.Target.GetComponent<Stats>();

        float TargetBasePower = stats.Toon_Profile.Base_Power;
        float TargetCurrentPower = stats.Power;
        float SourcePower = Buff.SourcePowerOnCast;

        float SpellMultiplier = Buff.Power_Multiplier;
        float SpellDuration = Buff.Duration;
        int Ticks = Buff.Ticks;
        int Stacks = Buff.CurrentStacks;
        SpellMultiplier = SpellMultiplier * Stacks;

        float NewPower = TargetCurrentPower;

        if (Buff.Imediate == true)
        {
            float BuffPower = (TargetBasePower * (SpellMultiplier * SourcePower));
            NewPower = TargetCurrentPower - BuffPower;
        }

        if (Buff.Over_Time == true)
        {
            float PerTick = (TargetBasePower * ((SpellMultiplier * SourcePower) / SpellDuration));
            float TotalPower = (PerTick * SpellDuration);
            float AlreadyAdded = (PerTick * Ticks);

            NewPower = TargetCurrentPower - AlreadyAdded;

        }

        if (Buff.Falling == true)
        {
            float PerTick = (TargetBasePower * ((SpellMultiplier * SourcePower) / SpellDuration));
            float TotalPower = (PerTick * SpellDuration);
            float Remainder = (PerTick * (SpellDuration - Ticks));

            NewPower = TargetCurrentPower - Remainder;
        }

        return NewPower;
    }

    //speed Buffs
    /// <summary>
    /// returns float NewSpeed after buff is applied
    /// </summary>
    /// <param name="Buff"></param>
    /// <returns></returns>
    public static float BuffSpeed(CreateNewBuff Buff)
    {
        Stats stats = Buff.Target.GetComponent<Stats>();

        float TargetBaseSpeed = stats.Toon_Profile.Base_Speed;
        float TargetSpeed = stats.Speed;
        float SourcePower = Buff.SourcePowerOnCast;
        float NewSpeed = TargetSpeed;

        float SpellMultiplier = Buff.Speed_Multiplier;
        float SpellDuration = Buff.Duration;
        float Ticks = Buff.Ticks;
        int Stacks = Buff.CurrentStacks;
        SpellMultiplier = SpellMultiplier * Stacks;

        if (Buff.Imediate == true && Buff.Applied == false)
        {
            float SpeedFromBuff = TargetBaseSpeed * (SourcePower * SpellMultiplier);
            NewSpeed = TargetSpeed + SpeedFromBuff;
            
        }

        if (Buff.Over_Time == true)
        {
            float PerTick = TargetBaseSpeed * ((SourcePower * SpellMultiplier) / SpellDuration);
            NewSpeed = TargetSpeed + PerTick;
        }

        if (Buff.Falling == true)
        {
            if (Buff.Applied == false)
            {
                float SpeedFromBuff = TargetBaseSpeed * (SourcePower * SpellMultiplier);
                NewSpeed = TargetSpeed + SpeedFromBuff;
                
            }
            else
            {
                float PerTick = TargetBaseSpeed * ((SourcePower * SpellMultiplier) / SpellDuration);
                NewSpeed = TargetSpeed - PerTick;
            }
        }

        return NewSpeed;
    }

    /// <summary>
    /// returns float NewSpeed after buff is removed
    /// </summary>
    /// <param name="Buff"></param>
    /// <returns></returns>
    public static float RemoveSpeedBuff(CreateNewBuff Buff)
    {
        Stats stats = Buff.Target.GetComponent<Stats>();

        float TargetBaseSpeed = stats.Toon_Profile.Base_Speed;
        float TargetCurrentSpeed = stats.Speed;
        float SourcePower = Buff.SourcePowerOnCast;

        float SpellMultiplier = Buff.Power_Multiplier;
        float SpellDuration = Buff.Duration;
        int Ticks = Buff.Ticks;
        int Stacks = Buff.CurrentStacks;
        SpellMultiplier = SpellMultiplier * Stacks;

        float NewSpeed = TargetCurrentSpeed;

        if (Buff.Imediate == true)
        {
            float BuffPower = (TargetBaseSpeed * (SpellMultiplier * SourcePower));
            NewSpeed = TargetCurrentSpeed - BuffPower;
        }

        if (Buff.Over_Time == true)
        {
            float PerTick = (TargetBaseSpeed * ((SpellMultiplier * SourcePower) / SpellDuration));
            float TotalSpeed = (PerTick * SpellDuration);
            float AlreadyAdded = (PerTick * Ticks);

            NewSpeed = TargetCurrentSpeed - AlreadyAdded;

        }

        if (Buff.Falling == true)
        {
            float PerTick = (TargetBaseSpeed * ((SpellMultiplier * SourcePower) / SpellDuration));
            float TotalPower = (PerTick * SpellDuration);
            float Remainder = (PerTick * (SpellDuration - Ticks));

            NewSpeed = TargetCurrentSpeed - Remainder;
        }

        return NewSpeed;
    }

    //accuracy Buffs
    /// <summary>
    /// returns float NewAccuracy after buff is applied
    /// </summary>
    /// <param name="Buff"></param>
    /// <returns></returns>
    public static float BuffAccuracy(CreateNewBuff Buff)
    {
        Stats stats = Buff.Target.GetComponent<Stats>();

        float TargetBaseAccuracy = stats.Toon_Profile.Base_Accuracy;
        float TargetAccuracy = stats.HitChance;
        float SourcePower = Buff.SourcePowerOnCast;
        float NewAccuracy = TargetAccuracy;

        float SpellMultiplier = Buff.Accuracy;
        float SpellDuration = Buff.Duration;
        float Ticks = Buff.Ticks;
        int Stacks = Buff.CurrentStacks;
        SpellMultiplier = SpellMultiplier * Stacks;

        if (Buff.Imediate == true && Buff.Applied == false)
        {
            float SpeedFromBuff = TargetBaseAccuracy * (SourcePower * SpellMultiplier);
            NewAccuracy = TargetAccuracy + SpeedFromBuff;

        }

        if (Buff.Over_Time == true)
        {
            float PerTick = TargetBaseAccuracy * ((SourcePower * SpellMultiplier) / SpellDuration);
            NewAccuracy = TargetAccuracy + PerTick;
        }

        if (Buff.Falling == true)
        {
            if (Buff.Applied == false)
            {
                float SpeedFromBuff = TargetBaseAccuracy * (SourcePower * SpellMultiplier);
                NewAccuracy = TargetAccuracy + SpeedFromBuff;

            }
            else
            {
                float PerTick = TargetBaseAccuracy * ((SourcePower * SpellMultiplier) / SpellDuration);
                NewAccuracy = TargetAccuracy - PerTick;
            }
        }

        return NewAccuracy;
}

    /// <summary>
    /// returns float NewAccuracy after buff is removed
    /// </summary>
    /// <param name="Buff"></param>
    /// <returns></returns>
    public static float RemoveAccuracyBuff(CreateNewBuff Buff)
    {
        Stats stats = Buff.Target.GetComponent<Stats>();

        float TargetBaseAccuracy = stats.Toon_Profile.Base_Accuracy;
        float TargetCurrentAccuracy = stats.HitChance;
        float SourcePower = Buff.SourcePowerOnCast;

        float SpellMultiplier = Buff.Power_Multiplier;
        float SpellDuration = Buff.Duration;
        int Ticks = Buff.Ticks;
        int Stacks = Buff.CurrentStacks;
        SpellMultiplier = SpellMultiplier * Stacks;

        float NewAccuracy = TargetCurrentAccuracy;

        if (Buff.Imediate == true)
        {
            float BuffPower = (TargetBaseAccuracy * (SpellMultiplier * SourcePower));
            NewAccuracy = TargetCurrentAccuracy - BuffPower;
        }

        if (Buff.Over_Time == true)
        {
            float PerTick = (TargetBaseAccuracy * ((SpellMultiplier * SourcePower) / SpellDuration));
            float TotalSpeed = (PerTick * SpellDuration);
            float AlreadyAdded = (PerTick * Ticks);

            NewAccuracy = TargetCurrentAccuracy - AlreadyAdded;

        }

        if (Buff.Falling == true)
        {
            float PerTick = (TargetBaseAccuracy * ((SpellMultiplier * SourcePower) / SpellDuration));
            float TotalPower = (PerTick * SpellDuration);
            float Remainder = (PerTick * (SpellDuration - Ticks));

            NewAccuracy = TargetCurrentAccuracy - Remainder;
        }

        return NewAccuracy;
    }

    /// <summary>
    /// returns true if attack was successful
    /// </summary>
    /// <param name="Chance"></param>
    /// <returns></returns>
    public static bool RNGsus(float Chance)
    {
        bool Success;
        int Roll = Random.Range(1, 101);

        if (Roll <= Chance)
        {
            Success = true;
            Messages.Message("Successful Hit!");
        }
        else
        {
            Success = false;
            Messages.Message("Missed!");
        }

        return Success;
    }

    /// <summary>
    /// removes HP from target, updates total damage taken and dealt, relays information to combat log
    /// </summary>
    /// <param name="Source"></param>
    /// <param name="Target"></param>
    /// <param name="Damage"></param>
    /// <param name="Attack"></param>
    /// <param name="DamageType"></param>
    public static void ApplyDamage(GameObject Source, GameObject Target, float Damage, string Attack, string DamageType)
    {
        Stats TargetStats = Target.GetComponent<Stats>();
        Stats SourceStats = Source.GetComponent<Stats>();

        float CurrentHP = TargetStats.Health;
        float diff = CurrentHP - Damage;
        Target.GetComponent<Stats>().LastAttackedBy = Source;

        if(diff > 0)
        {
            TargetStats.Health -= Damage;
            TargetStats.Damage_Taken += Damage;
            SourceStats.Damage_Dealt += Damage;
            Messages.CombatLog(LogTemplates.Damages(Source, Attack, Target, Damage, DamageType));
        }
        else
        {
            TargetStats.Health -= Damage + diff;
            TargetStats.Damage_Taken += Damage + diff;
            SourceStats.Damage_Dealt += Damage + diff;
            Messages.CombatLog(LogTemplates.Damages(Source, Attack, Target, (Damage + diff), DamageType));
        }
    }

    /// <summary>
    /// adds HP to target, updates total healing recived and done. relays information to combat log
    /// </summary>
    /// <param name="Source"></param>
    /// <param name="Target"></param>
    /// <param name="TotalDamage"></param>
    /// <param name="Attack"></param>
    public static void ApplyHealing(GameObject Source, GameObject Target, float TotalDamage, string Attack)
    {
        Stats TargetStats = Target.GetComponent<Stats>();
        Stats SourceStats = Source.GetComponent<Stats>();

        //Messages.Message("Applying Heal");
        float CurrentHP = TargetStats.Health;
        float MaxHP = TargetStats.Maxhealth;
        float Diff = MaxHP - CurrentHP;
        //Messages.Message("MaxHP = " + MaxHP + " currentHP = " + CurrentHP + " Diff = " + Diff);

        if (CurrentHP < MaxHP)
        {
            if (TotalDamage <= Diff)
            {
                //Messages.Message("TotalHeal = " + TotalDamage + " Diff = " + Diff);
                TargetStats.Health += TotalDamage;
                TargetStats.Healing_Taken += TotalDamage;
                SourceStats.Healing_Done += TotalDamage;
                Messages.CombatLog(LogTemplates.HealsFor(Source, Attack, Target, TotalDamage));
            }
            else
            {
                TargetStats.Health += Diff;
                TargetStats.Healing_Taken += Diff;
                SourceStats.Healing_Done += Diff;
                Messages.CombatLog(LogTemplates.HealsFor(Source, Attack, Target, Diff));
            }
        }
        else
        {
            Messages.CombatLog(LogTemplates.HealsFor(Source, Attack, Target, TotalDamage, true));
        }

    }

    /// <summary>
    /// triggers death of target, updates kill count and death count, relays information to combat log
    /// </summary>
    /// <param name="Source"></param>
    /// <param name="Target"></param>
    public static void Death(GameObject Source, GameObject Target)
    {
        Stats TargetStats = Target.GetComponent<Stats>();
        Stats SourceStats = Source.GetComponent<Stats>();

        if (Target.activeInHierarchy)
        {
            Messages.CombatLog(LogTemplates.HasKilled(Source, Target));

            TargetStats.Deaths += 1;
            Messages.CombatLog(LogTemplates.ShowDeaths(Target));

            SourceStats.Kills += 1;
            Messages.CombatLog(LogTemplates.ShowKills(Source));

            float exp = TargetStats.ExperienceOnKill * SourceStats.EXPMultiplier;
            SourceStats.Experience += exp;
            Messages.EventLog(LogTemplates.GainsThing(Source, exp));
            // to do: add in dropping gold and loot here.
            Target.SetActive(false);
        }

    }

    /// <summary>
    /// returns damage done after shields have absorbed the attack, trigger onShieldBreak effects
    /// </summary>
    /// <param name="Source"></param>
    /// <param name="Target"></param>
    /// <param name="attackType"></param>
    /// <param name="TotalDamage"></param>
    /// <returns></returns>
    public static float DestroyShields(GameObject Source, GameObject Target, CreateNewDamageType attackType, float TotalDamage)
    {
        float AttackDamage = TotalDamage;
        Buffs TargetBuffs = Target.GetComponent<Buffs>();

        if (TargetBuffs.ShieldList.Count > 0)
        {
            for (int i = 0; i < Target.GetComponent<Buffs>().ShieldList.Count; i++)
            {
                CreateNewShield TargetShield = Target.GetComponent<Buffs>().ShieldList[i];

                Stats TargetStats = TargetShield.Target.GetComponent<Stats>();
                Stats SourceStats = TargetShield.Source.GetComponent<Stats>();

                if (AttackDamage != 0)
                {
                    if (TargetShield.Specific_Type == true)
                    {
                        if (TargetShield.Damage_Type == attackType)
                        {
                            if (TargetShield.CurrentHealth >= AttackDamage)
                            {
                                TargetShield.CurrentHealth -= AttackDamage;
                                SourceStats.Healing_Done += AttackDamage;
                                TargetStats.Healing_Taken += AttackDamage;
                                AttackDamage = 0;
                            }
                            if (TargetShield.CurrentHealth < AttackDamage)
                            {
                                AttackDamage -= TargetShield.CurrentHealth;
                                SourceStats.Healing_Done += TargetShield.CurrentHealth;
                                TargetStats.Healing_Taken += TargetShield.CurrentHealth;
                                TargetShield.CurrentHealth = 0;
                                TargetBuffs.ShieldList.Remove(TargetShield);
                                AttackChecklist.ShieldBreakCallback(Target, Source, TargetShield);
                            }
                        }
                    }
                    else
                    {
                        if (TargetShield.Specific_Type == false)
                        {
                            if (TargetShield.CurrentHealth >= AttackDamage)
                            {
                                TargetShield.CurrentHealth -= AttackDamage;
                                SourceStats.Healing_Done += AttackDamage;
                                TargetStats.Healing_Taken += AttackDamage;
                                AttackDamage = 0;
                            }
                            if (TargetShield.CurrentHealth < AttackDamage)
                            {
                                AttackDamage -= TargetShield.CurrentHealth;
                                SourceStats.Healing_Done += TargetShield.CurrentHealth;
                                TargetStats.Healing_Taken += TargetShield.CurrentHealth;
                                TargetShield.CurrentHealth = 0;
                                TargetBuffs.ShieldList.Remove(TargetShield);
                                AttackChecklist.ShieldBreakCallback(Target, Source, TargetShield);
                            }
                        }
                    }
                }
            }


        }

        return AttackDamage;
    }

    /// <summary>
    /// returns the wekness modifier of type matchups
    /// </summary>
    /// <param name="Target"></param>
    /// <param name="DamageType"></param>
    /// <returns></returns>
    public static float DamageTypeCheck(GameObject Target, CreateNewDamageType DamageType)
    {
        CreateNewDamageType TargetType = Target.GetComponent<Stats>().Toon_Profile.Type;

        float ResistModifier = 0;

        if (TargetType.Resist.Contains(DamageType))
        {
            int Index = TargetType.Resist.IndexOf(DamageType);
            ResistModifier = TargetType.ResistAmnt[Index];
        }

        if (TargetType.Weak.Contains(DamageType))
        {
            int Index = TargetType.Weak.IndexOf(DamageType);
            ResistModifier = TargetType.WeakAmnt[Index] + 1;
        }

        Messages.Message("ResistModifier is :" + ResistModifier);
        return ResistModifier;
    }

}
