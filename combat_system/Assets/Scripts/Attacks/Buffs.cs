using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Stats))]
public class Buffs : MonoBehaviour
{

    public List<CreateNewBuff> BuffList = new List<CreateNewBuff>();
    public List<CreateNewShield> ShieldList = new List<CreateNewShield>();
    public List<CreateNewDOT> DOTList = new List<CreateNewDOT>();

    void OnEnable()
    {
        Tick.NewTick += TickNow;
    }

    //actions to peform on each "Tick" of the buff
    public void TickNow()
    {
        CountDown();

        if (BuffList.Count > 0)
        {
            ApplyBuff();
        }

        if (DOTList.Count > 0)
        {
            DOTtick();
        }
    }

    //decriment the duration of buff, remove and destroy it when it expires
    void CountDown()
    {
        for (int i = 0; i < BuffList.Count; i++)
        {
            CreateNewBuff Buff = BuffList[i];
      
            if (BuffList[i].Duration == BuffList[i].Ticks)
            {
                Messages.CombatLog(LogTemplates.FadesFrom(BuffList[i].Source, BuffList[i].BuffName, BuffList[i].Target));
                RemoveBuff();
                BuffList.Remove(BuffList[i]);
                Destroy(Buff);
            }
            else
            {
                ++BuffList[i].Ticks;
            }

        }

        for (int i = 0; i < DOTList.Count; i++)
        {
            CreateNewDOT DOT = DOTList[i];

            if (DOT.Duration == DOTList[i].Ticks)
            {
                Messages.CombatLog(LogTemplates.FadesFrom(DOTList[i].Source, DOTList[i].DOTName, DOTList[i].Target));
                DOTList.Remove(DOT);
                Destroy(DOT);
            }
            else
            {
                ++DOTList[i].Ticks;
            }
        }

        for (int i = 0; i < ShieldList.Count; i++)
        {
            CreateNewShield Shield = ShieldList[i];

            if (ShieldList[i].Duration == ShieldList[i].Ticks)
            {
                Messages.CombatLog(LogTemplates.FadesFrom(ShieldList[i].Source, ShieldList[i].BuffName, ShieldList[i].Target));
                ShieldList.Remove(ShieldList[i]);
                Destroy(Shield);
            }
            else
            {
                ++ShieldList[i].Ticks;
            }
        }
    }

    //logic for applying the buffs and setting each buff to the "Applied status"
    public void ApplyBuff()
    {
        for (int i = 0; i < BuffList.Count; i++)
        {
            if (BuffList[i].Imediate == true)
            {
                if (BuffList[i].Applied == false)
                {
                    float[] HealthAmounts = DamageCalculations.BuffHealth(BuffList[i]);
                    BuffList[i].Target.GetComponent<Stats>().Maxhealth = HealthAmounts[0];
                    BuffList[i].Target.GetComponent<Stats>().Health = HealthAmounts[1];
                    BuffList[i].Target.GetComponent<Stats>().Power = DamageCalculations.BuffPower(BuffList[i]);
                    BuffList[i].Target.GetComponent<Stats>().Speed = DamageCalculations.BuffSpeed(BuffList[i]);
                    BuffList[i].Target.GetComponent<Stats>().HitChance = DamageCalculations.BuffAccuracy(BuffList[i]);
                    BuffList[i].Applied = true;
                    #region send to combat log
                    Messages.CombatLog(LogTemplates.GainsFrom(BuffList[i].Target, BuffList[i].BuffName, BuffList[i].Source));
                    #endregion
                }
            }

            if (BuffList[i].Falling == true)
            {
                float[] HealthAmounts = DamageCalculations.BuffHealth(BuffList[i]);
                BuffList[i].Target.GetComponent<Stats>().Maxhealth = HealthAmounts[0];
                BuffList[i].Target.GetComponent<Stats>().Health = HealthAmounts[1];
                BuffList[i].Target.GetComponent<Stats>().Power = DamageCalculations.BuffPower(BuffList[i]);
                BuffList[i].Target.GetComponent<Stats>().Speed = DamageCalculations.BuffSpeed(BuffList[i]);
                BuffList[i].Target.GetComponent<Stats>().HitChance = DamageCalculations.BuffAccuracy(BuffList[i]);
                BuffList[i].Applied = true;
                Messages.CombatLog(LogTemplates.GainsFrom(BuffList[i].Target, BuffList[i].BuffName, BuffList[i].Source));
            }

            if (BuffList[i].Over_Time == true)
            {
                if (BuffList[i].Applied == false)
                {
                    BuffList[i].Ticks += 1;
                    //fudge the first tick so the spell applies instantly
                }
                float[] HealthAmounts = DamageCalculations.BuffHealth(BuffList[i]);
                BuffList[i].Target.GetComponent<Stats>().Maxhealth = HealthAmounts[0];
                BuffList[i].Target.GetComponent<Stats>().Health = HealthAmounts[1];
                BuffList[i].Target.GetComponent<Stats>().Power = DamageCalculations.BuffPower(BuffList[i]);
                BuffList[i].Target.GetComponent<Stats>().Speed = DamageCalculations.BuffSpeed(BuffList[i]);
                BuffList[i].Target.GetComponent<Stats>().HitChance = DamageCalculations.BuffAccuracy(BuffList[i]);
                BuffList[i].Applied = true;
                Messages.CombatLog(LogTemplates.GainsFrom(BuffList[i].Target, BuffList[i].BuffName, BuffList[i].Source));
            }

        }
    }

    //Remove the effects of the buff all at once
    public void RemoveBuff()
    {
        for (int i = 0; i < BuffList.Count; i++)
        {
            float[] HealthAmounts = DamageCalculations.RemoveHealthBuff(BuffList[i]);
            BuffList[i].Target.GetComponent<Stats>().Maxhealth = HealthAmounts[0];
            BuffList[i].Target.GetComponent<Stats>().Health = HealthAmounts[1];
            BuffList[i].Target.GetComponent<Stats>().Power = DamageCalculations.RemovePowerBuff(BuffList[i]);
            BuffList[i].Target.GetComponent<Stats>().Speed = DamageCalculations.RemoveSpeedBuff(BuffList[i]);
            BuffList[i].Target.GetComponent<Stats>().HitChance = DamageCalculations.RemoveAccuracyBuff(BuffList[i]);
            Messages.CombatLog(LogTemplates.FadesFrom(BuffList[i].Source, BuffList[i].BuffName, BuffList[i].Target));
        }
    }

    public void DOTtick()
    {
        Messages.Message("Checking for DOTs");
        if (DOTList.Count > 0)
        {
            Messages.Message("DOT detected");
            for (int i = 0; i < DOTList.Count; i++)
            {
                float Damage = DamageCalculations.DOTdamage(DOTList[i].Source, DOTList[i].Target, DOTList[i]);

                if (DOTList[i].IsHeal)
                {
                    DamageCalculations.ApplyHealing(DOTList[i].Source, DOTList[i].Target, Damage, DOTList[i].DOTName);
                }
                else
                {
                    // -> apply damage
                    DamageCalculations.ApplyDamage(DOTList[i].Source, DOTList[i].Target, Damage, DOTList[i].DOTName, DOTList[i].DamageType.name);
                }
            }
        }
    }

    public static void RemoveBuff(GameObject Target, CreateNewBuff NewBuff)
    {
        Target.GetComponent<Buffs>().RemoveBuff();

    }

    void OnDisable()
    {
        Tick.NewTick -= TickNow;
    }
}
