using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MovementEffects;

public class AttackChecklist : MonoBehaviour
{

    #region Checklist layout
    /*
   -> check if target is Friend or Foe
   -> Check if Target is in Range
   -> Check is caster has Line of Sight
   -> Check if Caster has enough resources
   -> Begin Cast timer
   -> play cast sound
   [monitor for interrupts: Broken LOS, Target Dead, Spell Interrupts, Moving]
   -> finish cast timer
   -> remove resources from caster
   -> check if hit/miss
   -> create flight path
   -> move particles
   -> play flight sound
   -> calculate damage
   -> apply damage
   -> play damage/hit sound
   -> if needed kill.
   */
    #endregion
    
    /// <summary>
    /// gathers the information about the pending attack and carries out Friend or Foe, Range, Resource, Line of Sight checks then starts the cast timer if applicable
    /// </summary>
    /// <param name="Source"></param>
    /// <param name="Target"></param>
    /// <param name="Buff"></param>
    /// <param name="DOT"></param>
    /// <param name="DAttack"></param>
    /// <param name="Shield"></param>
    /// <param name="Projectile"></param>
    public static void Attack(GameObject Source, GameObject Target, CreateNewBuff Buff = null, CreateNewDOT DOT = null, CreatNewDirectAttack DAttack = null, CreateNewShield Shield = null, CreateNewProjectile Projectile = null)
    {

        float SpellRange = 0; //need to get this from the attack
        float SpellCost = 0; // need to get this from the attack
        bool InRange = false;
        float CastTime = 0;
        string SpellName = null;
        bool HosttilityCheck = false;
        bool SafeToCastOnTarget = false;
        bool ReadyToCast = true;
        bool OnGCD = true;

        // Get the attack info
        //
        #region DOT info
        if (DOT != null)
        {
            DOT = Spawn.NewDOT(Target, DOT, Source);
            SpellRange = DOT.MaxRange;
            SpellCost = DOT.ResourceCost * Source.GetComponent<Stats>().Toon_Profile.Base_Resource_Amount;
            CastTime = DOT.CastTime;
            SpellName = DOT.DOTName;
            Messages.Message("Attack is a DOT");

            OnGCD = SkillList.OnGCD(Source);
            Messages.Message("GCD = " + OnGCD);

            ReadyToCast = SkillList.IsMoveOnCD(Source, null, DOT, null, null);
            Messages.Message("Skill  Ready to cast = " + ReadyToCast);

            if (DOT.HostileOnly)
            {
                HosttilityCheck = true;
            }
            else
            {
                HosttilityCheck = false;
            }

        }
        #endregion

        #region Buff Info
        if (Buff != null)
        {
            Buff = Spawn.NewBuff(Source, Buff, Source);
            SpellRange = Buff.MaxRange;
            SpellCost = Buff.ResourceCost * Source.GetComponent<Stats>().Toon_Profile.Base_Resource_Amount;
            CastTime = Buff.CastTime;
            SpellName = Buff.BuffName;
            //Messages.Message("Attack is a Buff");

            OnGCD = SkillList.OnGCD(Source);
           // Messages.Message("GCD = " + OnGCD);

            ReadyToCast = SkillList.IsMoveOnCD(Source, Buff, null, null, null);
           // Messages.Message("Skill  Ready to cast = " + ReadyToCast);

            if (Buff.HostileOnly)
            {
                HosttilityCheck = true;
            }
            else {
                HosttilityCheck = false;
            }
        }
        #endregion

        #region Shield Info
        if (Shield != null)
        {
            Shield = Spawn.NewShield(Target, Shield, Source);
            SpellRange = Shield.MaxRange;
            SpellCost = Shield.ResourceCost * Source.GetComponent<Stats>().Toon_Profile.Base_Resource_Amount;
            CastTime = Shield.CastTime;
            SpellName = Shield.BuffName;
           // Messages.Message("Attack is a Shield");

            OnGCD = SkillList.OnGCD(Source);
           // Messages.Message("GCD = " + OnGCD);

            ReadyToCast = SkillList.IsMoveOnCD(Source, null, null, Shield, null);
           // Messages.Message("Skill  Ready to cast = " + ReadyToCast);

            if (Shield.HostileOnly)
            {
                HosttilityCheck = true;
            }
            else
            {
                {
                    HosttilityCheck = false;
                }
            }
        }
        #endregion

        #region DAttack Info
        if (DAttack != null)
        {
            DAttack = Spawn.NewDAttack(Target, DAttack, Source);
            SpellRange = DAttack.MaxRange;
            SpellCost = DAttack.ResourceCost * Source.GetComponent<Stats>().Toon_Profile.Base_Resource_Amount;
            CastTime = DAttack.CastTime;
            SpellName = DAttack.AttackName;
            //Messages.Message("Attack is a DAttack");

            OnGCD = SkillList.OnGCD(Source);
            //Messages.Message("GCD = " + OnGCD);

            ReadyToCast = SkillList.IsMoveOnCD(Source, null, null, null, DAttack);
            //Messages.Message("Skill  Ready to cast = " + ReadyToCast);

            if (DAttack.HostileOnly)
            {
                HosttilityCheck = true;
            }
            else
            {
                HosttilityCheck = false;
            }
        }
        #endregion

        #region Projectile Info
        if (Projectile != null)
        {
            SpellRange = Projectile.MaxRange;
            SpellCost = Projectile.ResourceCost * Source.GetComponent<Stats>().Toon_Profile.Base_Resource_Amount;
            CastTime = Projectile.CastTime;
            SpellName = Projectile.AttackName;
            //Messages.Message("Attack is a DAttack");

            OnGCD = SkillList.OnGCD(Source);
            //Messages.Message("GCD = " + OnGCD);

            ReadyToCast = SkillList.IsMoveOnCD(Source, null, null, null, null, Projectile);
            //Messages.Message("Skill  Ready to cast = " + ReadyToCast);

            if (Projectile.HostileOnly)
            {
                HosttilityCheck = true;
            }
            else
            {
                HosttilityCheck = false;
            }
        }
        #endregion

        //check if target is Friend or Foe
        //
        #region Friend or Foe Checks
        bool Hostile = TeamAffiliation.IsHostile(Source, Target);

        // makes sure that hostiule only attacks cant be cast on allies and vice versa
        if (Hostile == true)
        {
            //Messages.Message("Target is hostile");
            if (HosttilityCheck == true)
            {
                //Messages.Message("Spell is for hostile units");
                SafeToCastOnTarget = true;
            }
        }
        else
        {
            //Messages.Message("Target is friendly");
            if (HosttilityCheck == false)
            {
                //Messages.Message("Spell is for friendly units");
                SafeToCastOnTarget = true;
            }
        }

        //Messages.Message("Safe to cast at target? : " + SafeToCastOnTarget);
        #endregion

        //Check if Target is in Range
        //
        #region Range Checks
        float Range = RangeCheck.Distance(Source, Target);
        if (SpellRange >= Range)
        {
            InRange = true;
        }
        else
        {
            InRange = false;
        }
        //Messages.Message("In Range? :" + InRange + "( Spell range is : " + SpellRange + " Distance is : " + Range + ")");
        #endregion

        // Check is caster has Line of Sight
        //
        bool HasLOS = RangeCheck.LineOfSight(Source, Target);
        //Messages.Message("Has LOS? : " + HasLOS);

        //Check if Caster has enough resources
        //
        bool HasResources = ResourceManager.ResouceCheck(Source, SpellCost);

        // Begin Cast timer
        //
        #region Cast Timer Set Up
        if (OnGCD == false & ReadyToCast == true)
        {
            if (SafeToCastOnTarget)
            {
                if (HasLOS & HasResources & InRange)
                {
                    if (Source.GetComponent<CastManager>().IsCasting != true)
                    {
                        Source.GetComponent<CastManager>().StartTime = Time.time;
                        Source.GetComponent<CastManager>().EndTime = Source.GetComponent<CastManager>().StartTime + CastTime;
                        float EndTime = Source.GetComponent<CastManager>().StartTime + CastTime;
                        Source.GetComponent<CastManager>().Name = SpellName;
                        Source.GetComponent<CastManager>().Success = false;
                        Source.GetComponent<CastManager>().Interruped = false;
                        Source.GetComponent<CastManager>().IsCasting = true;
                        Source.GetComponent<CastManager>().Target = Target;

                        if (DOT != null)
                        {
                            Source.GetComponent<CastManager>().DOT = DOT;
                        }

                        if (Shield != null)
                        {
                            Source.GetComponent<CastManager>().Shield = Shield;
                        }

                        if (DAttack != null)
                        {
                            Source.GetComponent<CastManager>().DAttack = DAttack;
                        }

                        if (Buff != null)
                        {
                            Source.GetComponent<CastManager>().Buff = Buff;
                        }

                        if (Projectile != null)
                        {
                            Source.GetComponent<CastManager>().Projectile = Projectile;
                        }

                        Timing.RunCoroutine(Source.GetComponent<SkillList>().GCDTraacker());
                        Timing.RunCoroutine(Source.GetComponent<CastManager>().CastNow(EndTime));

                    }
                    else
                    {
                        Messages.Message("Already Casting Something");
                    }
                }
            }
        }
        #endregion

        //rest is triggered by the CastManager Script
    }

    //  -> finish cast timer
    /// <summary>
    /// after the cast timer has finished, checks if the attack hit, debits resources, and applies damage/healing. Also handles particle instantiation
    /// </summary>
    /// <param name="Source"></param>
    /// <param name="Target"></param>
    /// <param name="Buff"></param>
    /// <param name="DOT"></param>
    /// <param name="DAttack"></param>
    /// <param name="Shield"></param>
    /// <param name="Projectile"></param>
    public static void AttackPart2(GameObject Source, GameObject Target, CreateNewBuff Buff = null, CreateNewDOT DOT = null, CreatNewDirectAttack DAttack = null, CreateNewShield Shield = null, CreateNewProjectile Projectile = null)
    {
        float SpellCost = 0;
        float DamageCalc = 0;

        // Get the attack info
        //

        #region DOT application
        if (DOT != null)
        {
            SpellCost = DOT.ResourceCost * Source.GetComponent<Stats>().Toon_Profile.Base_Resource_Amount;

            // -> remove resources from caster
            Messages.Message("Debiting resources from " + Source);
            ResourceManager.DebitResources(Source, SpellCost);

            // -> trigger GCD and put spell on cooldown
            SkillList.PutMoveOnCD(Source, null, DOT, null, null);


            if (DOT.IsHeal)
            {
                if (DOT.UseParticle == true)
                {
                    Spawn.NewParticle(Target, null, null, null, null, DOT);
                }
                Stack.DOT(Target, DOT, Source);
            }
            else
            {
                //-> check if hit/miss
                bool Hit = DamageCalculations.RNGsus(Source.GetComponent<Stats>().HitChance);

                if (Hit)
                {
                    //-> apply damage
                    if (DOT.UseParticle == true)
                    {
                        Spawn.NewParticle(Target, null, null, null, null, DOT);
                    }
                    Stack.DOT(Target, DOT, Source);
                }
            }
        }
        #endregion

        #region Buff application
        if (Buff != null)
        {
            SpellCost = Buff.ResourceCost * Source.GetComponent<Stats>().Toon_Profile.Base_Resource_Amount;

            // -> remove resources from caster
            Messages.Message("Debiting resources from " + Source);
            ResourceManager.DebitResources(Source, SpellCost);

            // -> trigger GCD and put spell on cooldown
            SkillList.PutMoveOnCD(Source, Buff, null, null, null);


            //-> check if hit/miss
            bool Hit = DamageCalculations.RNGsus(Source.GetComponent<Stats>().HitChance);

            if (Hit)
            {
                //-> apply damage
                if (Buff.UseParticle == true)
                {
                    Spawn.NewParticle(Target, null, null, Buff);
                }
                Stack.Buff(Target, Buff, Source);
                Target.GetComponent<Buffs>().ApplyBuff();
            }
        }
        #endregion

        #region Shield application
        if (Shield != null)
        {
            SpellCost = Shield.ResourceCost * Source.GetComponent<Stats>().Toon_Profile.Base_Resource_Amount;

            // -> remove resources from caster
            Messages.Message("Debiting resources from " + Source);
            ResourceManager.DebitResources(Source, SpellCost);

            // -> trigger GCD and put spell on cooldown
            SkillList.PutMoveOnCD(Source, null, null, Shield, null);


            Stack.Shield(Target, Shield, Source);
            if (Shield.UseParticle == true)
            {
                Spawn.NewParticle(Target, null, null, null, Shield);
            }
        }
        #endregion

        #region DAttack application
        if (DAttack != null)
        {
            SpellCost = DAttack.ResourceCost * Source.GetComponent<Stats>().Toon_Profile.Base_Resource_Amount;
            DamageCalc = DamageCalculations.DirectDamage(Source, Target, DAttack);

            // -> trigger GCD and put spell on cooldown
            SkillList.PutMoveOnCD(Source, null, null, null, DAttack);


            if (DAttack.IsHeal)
            {
                DamageCalculations.ApplyHealing(Source, Target, DamageCalc, DAttack.AttackName);   //skip hit/miss because heals dont miss.

                if (DAttack.UseParticle == true)
                {
                    Spawn.NewParticle(Target, null, DAttack);
                }
            }
            else
            {
                bool Hit = DamageCalculations.RNGsus(Source.GetComponent<Stats>().HitChance);

                if (Hit)
                {
                    // -> apply damage
                    DamageCalculations.ApplyDamage(Source, Target, DamageCalc, DAttack.AttackName, DAttack.DamageType.name);

                    if (DAttack.UseParticle == true)
                    {
                        Spawn.NewParticle(Target, null, DAttack);
                    }
                }
            }
            // -> remove resources from caster
            ResourceManager.DebitResources(Source, SpellCost);
        }
        #endregion

        #region Projectile Launch
        if (Projectile != null)
        {
            SpellCost = Projectile.ResourceCost * Source.GetComponent<Stats>().Toon_Profile.Base_Resource_Amount;

            // -> trigger GCD and put spell on cooldown
            SkillList.PutMoveOnCD(Source, null, null, null, null, Projectile);

            // -> launch the projectile
            if (Projectile.UseParticle == true)
            {
                Spawn.NewProjectile(Target, Projectile, Source);
            }

            // -> remove resources from caster
            ResourceManager.DebitResources(Source, SpellCost);
        }
        #endregion
    }

    /// <summary>
    /// recieves a callback from the projectile at the end of its life, applies damage/particles, triggers the secondart effect callback
    /// </summary>
    /// <param name="Target"></param>
    /// <param name="Projectile"></param>
    /// <param name="Source"></param>
    public static void ProjectileDamageCallback(GameObject Target, CreateNewProjectile Projectile, GameObject Source)
    {
        float DamageCalc = DamageCalculations.ProjectileDamage(Source, Target, Projectile);
        bool hit = DamageCalculations.RNGsus(Source.GetComponent<Stats>().HitChance);

        if (Projectile.IsHeal)
        {
            DamageCalculations.ApplyHealing(Source, Target, DamageCalc, Projectile.AttackName);   //skip hit/miss because heals dont miss.
        }
        else
        {
            if (hit)
            {
                DamageCalculations.ApplyDamage(Source, Target, DamageCalc, Projectile.AttackName, Projectile.DamageType.name);
            }
        }

        if (Projectile.UseSecondaryParticle)
        {
            Spawn.NewParticle(Target, Source, null, null, null, null, Projectile);
        }

        if (Projectile.SecondaryEffect)
        {
            ProjectileEffectCallback(Source, Target, Projectile);
        }
    }

    /// <summary>
    /// if a projectile triggers a secondary effect, this callback references to projectile object and initiates the combat checks/applications cycle
    /// </summary>
    /// <param name="Caster"></param>
    /// <param name="Target"></param>
    /// <param name="Projectile"></param>
    public static void ProjectileEffectCallback(GameObject Caster, GameObject Target, CreateNewProjectile Projectile)
    {
        if (Projectile.ApplyEffect)
        {
            GameObject newTarget = null;

            if (Projectile.Self)
            {
                newTarget = Caster;
            }

            if (Projectile.EffectTarget)
            {
                newTarget = Target;
            }

            if (Projectile.SecondaryEffect != null)
            {
                CreateNewBuff BreakBuff = Spawn.NewBuff(newTarget, Projectile.SecondaryEffect, Caster);
                Stack.Buff(newTarget, BreakBuff, Caster);
                if (BreakBuff.UseParticle)
                {
                    Spawn.NewParticle(newTarget, null, null, BreakBuff);
                }
                newTarget.GetComponent<Buffs>().ApplyBuff();
            }

            if (Projectile.SecondaryEffectDOT != null)
            {
                CreateNewDOT BreakDOT = Spawn.NewDOT(newTarget, Projectile.SecondaryEffectDOT, Caster);
                Stack.DOT(newTarget, BreakDOT, Caster);
                if (BreakDOT.UseParticle == true)
                {
                    Spawn.NewParticle(newTarget, null, null, null, null, BreakDOT);
                }
            }

            if (Projectile.SecondaryEffectAttack!= null)
            {
                CreatNewDirectAttack DAttack = Spawn.NewDAttack(newTarget, Projectile.SecondaryEffectAttack, Caster);
                float DamageCalc = DamageCalculations.DirectDamage(Caster, newTarget, DAttack);

                if (DAttack.IsHeal)
                {
                    DamageCalculations.ApplyHealing(Caster, newTarget, DamageCalc, DAttack.AttackName);
                    if (DAttack.UseParticle == true)
                    {
                        Spawn.NewParticle(newTarget, null, DAttack);
                    }
                }
                else
                {
                    // -> apply damage
                    DamageCalculations.ApplyDamage(Caster, newTarget, DamageCalc, DAttack.AttackName, DAttack.DamageType.name);

                    if (DAttack.UseParticle == true)
                    {
                        Spawn.NewParticle(newTarget, null, DAttack);
                    }
                }
            }

            if (Projectile.SecondaryEffectProjectile != false)
            {
                if (Projectile.SecondaryEffectProjectile.UseParticle == true)
                {
                    Spawn.NewProjectile(newTarget, Projectile.SecondaryEffectProjectile, Caster);
                }
            }
        }

    }

    /// <summary>
    /// handles the application of effects process when a shield has been broken
    /// </summary>
    /// <param name="ShieldOwner"></param>
    /// <param name="ShieldBreaker"></param>
    /// <param name="Shield"></param>
    public static void ShieldBreakCallback(GameObject ShieldOwner, GameObject ShieldBreaker, CreateNewShield Shield)
    {
        if (Shield.BreakEffect)
        {
            GameObject newTarget = null;

            if (Shield.Self)
            {
                newTarget = ShieldOwner;
            }

            if (Shield.Breaker)
            {
                newTarget = ShieldBreaker;
            }

            #region Buff
            if (Shield.BreakBuff != null)
            {
                CreateNewBuff BreakBuff = Spawn.NewBuff(newTarget, Shield.BreakBuff, ShieldOwner);
                Stack.Buff(newTarget, BreakBuff, ShieldOwner);
                if (BreakBuff.UseParticle)
                {
                    Spawn.NewParticle(newTarget, null, null, BreakBuff);
                }
                newTarget.GetComponent<Buffs>().ApplyBuff();
            }
            #endregion

            #region DOT
            if (Shield.BreakDot != null)
            {
                CreateNewDOT BreakDOT = Spawn.NewDOT(newTarget, Shield.BreakDot, ShieldOwner);
                Stack.DOT(newTarget, BreakDOT, ShieldOwner);
                if (BreakDOT.UseParticle == true)
                {
                    Spawn.NewParticle(newTarget, null, null, null, null, BreakDOT);
                }
            }
            #endregion

            #region DAttack
            if (Shield.BreakAttack != null)
            {
                CreatNewDirectAttack DAttack = Spawn.NewDAttack(newTarget, Shield.BreakAttack, ShieldOwner);
                float DamageCalc = DamageCalculations.DirectDamage(ShieldOwner, newTarget, DAttack);

                if (DAttack.IsHeal)
                {
                    DamageCalculations.ApplyHealing(ShieldOwner, newTarget, DamageCalc, DAttack.AttackName);
                    if (DAttack.UseParticle == true)
                    {
                        Spawn.NewParticle(newTarget, null, DAttack);
                    }
                }
                else
                {
                    // -> apply damage
                    DamageCalculations.ApplyDamage(ShieldOwner, newTarget, DamageCalc, DAttack.AttackName, DAttack.DamageType.name);

                    if (DAttack.UseParticle == true)
                    {
                        Spawn.NewParticle(newTarget, null, DAttack);
                    }
                }
            }
            #endregion

            #region Projectile
            if (Shield.BreakProjectile != false)
            {
                if (Shield.BreakProjectile.UseParticle == true)
                {
                    Spawn.NewProjectile(newTarget, Shield.BreakProjectile, ShieldOwner);
                }
            }
            #endregion
        }

    }
}
