using UnityEngine;
using System.Collections;

public class Stack : MonoBehaviour {

    /// <summary>
    /// handles the application and stacking of buffs
    /// </summary>
    /// <param name="Target"></param>
    /// <param name="NewBuff"></param>
    /// <param name="Source"></param>
    public static void Buff(GameObject Target, CreateNewBuff NewBuff, GameObject Source)
    {
        Buffs buffs = Target.GetComponent<Buffs>();

        bool Unique = true;
        bool Dup = false;

        for (int i = 0; i < buffs.BuffList.Count; i++)               // starts a for loop that iterates through all buffs currently attached to the taregt
        {

            if (buffs.BuffList[i].BuffID == NewBuff.BuffID)
            {
                Dup = true;                                                                 // checking for duplicates so we know if this is a fresh application of if we need to stack the buff
                Debug.Log("Duplicate Detected");
                if (buffs.BuffList[i].Stackable == true)             // if it is stackable, check and see if it is at max stacks yet
                {
                    int Stacks = buffs.BuffList[i].CurrentStacks;
                    int MaxStacks = buffs.BuffList[i].StackLimit;
                    Debug.Log(Stacks + " " + MaxStacks);

                    if (Stacks < MaxStacks)                                                     // if not, remove the old effect, increase the stacks by 1, re apply the effect
                    {
                        Debug.Log("Duplicate is Stackable");                                    // they get removed first so that increasing/decreasing buffs dont get spammed to keep them at max effect
                        Buffs.RemoveBuff(Target, NewBuff);
                        CreateNewBuff StackMe = buffs.BuffList[i];
                        StackMe.Ticks = 0;
                        StackMe.CurrentStacks += 1;
                        StackMe.Applied = false;
                        Messages.CombatLog(LogTemplates.Stack(Target, StackMe.BuffName, Source, StackMe.CurrentStacks));
                    }

                    if (Stacks >= MaxStacks)                                                    // if it is, remove the old effect, reset duration, apply it again.
                    {
                        Debug.Log("Duplicate is at max stacks");
                        Buffs.RemoveBuff(Target, NewBuff);
                        CreateNewBuff StackMe = buffs.BuffList[i];
                        StackMe.Ticks = 0;
                        StackMe.Applied = false;
                        Messages.CombatLog(LogTemplates.Stack(Target, StackMe.BuffName, Source, StackMe.CurrentStacks));
                    }

                }
                else
                {                                                                                   // if its not stackable, remove, destroy it, apply it again
                    Buffs.RemoveBuff(Target, NewBuff);
                    CreateNewBuff DeadMan = buffs.BuffList[i];
                    buffs.BuffList.Remove(buffs.BuffList[i]);
                    CreateNewBuff.Destroy(DeadMan);
                    buffs.BuffList.Add(NewBuff);
                    Messages.CombatLog(LogTemplates.GainsFrom(Target, NewBuff.BuffName, Source));
                }
            }
            else
            {
                Dup = false;
            }
        }

        if (Dup == false)                                                                 
        {

            int Count = buffs.BuffList.Count;
            if (Count > 0)
            {
                for (int i = 0; i < Count; i++)
                {
                    if (buffs.BuffList[i].BuffName == NewBuff.BuffName)
                    {
                        Messages.CombatLog(LogTemplates.BuffExists(buffs.BuffList[i].BuffName, Target));
                        Unique = false;
                    }
                }
            }

            if (Unique == true)
            {
                buffs.BuffList.Add(NewBuff);
            }
        }
    }

    /// <summary>
    /// handles the application and stacking of shields
    /// </summary>
    /// <param name="Target"></param>
    /// <param name="Shield"></param>
    /// <param name="Source"></param>
    public static void Shield(GameObject Target, CreateNewShield Shield, GameObject Source)
    {
        Buffs buffs = Target.GetComponent<Buffs>();
        bool Dup = false;

        if (buffs.ShieldList.Count > 0)  
        {

            for (int i = 0; i < buffs.ShieldList.Count; i++)                             // if the target has any shield on it, start iterating through the list
            {
                if (buffs.ShieldList[i].ShieldID == Shield.ShieldID)
                {
                    Dup = true;
                    if (buffs.ShieldList[i].Stackable == true)                              // check for duplicates and if they are stackable
                    {
                        int Stacks = buffs.ShieldList[i].CurrentStacks;
                        int MaxStacks = buffs.ShieldList[i].StackLimit;

                        if (Stacks < MaxStacks)
                        {
                            CreateNewShield myShield = buffs.ShieldList[i];                  // if its not at max stacks, add a stack and reset the duration/health
                            myShield.Ticks = 0;                                                                     // dont have to remve/reset these because we want people to keep the uptime high by recasting
                            myShield.CurrentStacks += 1;
                            myShield.CurrentHealth = myShield.MaxHealth * Stacks;
                            Messages.CombatLog(LogTemplates.Stack(Target, myShield.BuffName, Source, myShield.CurrentStacks));
                        }

                        if (Stacks >= MaxStacks)                                                                       // if its at max stacks, just refresh it by resetting the duration/health
                        {
                            CreateNewShield myShield = buffs.ShieldList[i];
                            myShield.Ticks = 0;
                            myShield.CurrentHealth = myShield.MaxHealth * MaxStacks;
                            Messages.CombatLog(LogTemplates.Stack(Target, myShield.BuffName, Source, myShield.CurrentStacks));
                        }

                    }
                    else
                    {                                                                                               // if its a duplicate but not stackable, just reset it;
                        CreateNewShield myShield = buffs.ShieldList[i];
                        myShield.Ticks = 0;
                        myShield.CurrentHealth = myShield.MaxHealth;
                        Messages.CombatLog(LogTemplates.GainsFrom(Target, myShield.BuffName, Source));
                    }
                }
                else
                {
                    Dup = false;                                                                                // if not a duplicate add a new one
                    buffs.ShieldList.Add(Shield);
                    Messages.CombatLog(LogTemplates.GainsFrom(Target, Shield.BuffName, Source));
                }
            }
        }

        if (Dup == false)                                                                                       // adding a new one when the list has nothing in it already               
        {
            buffs.ShieldList.Add(Shield);
            Messages.CombatLog(LogTemplates.GainsFrom(Target, Shield.BuffName, Source));
        }
    }

    /// <summary>
    /// handles the application and stacking of DOT's
    /// </summary>
    /// <param name="Target"></param>
    /// <param name="DOT"></param>
    /// <param name="Source"></param>
    public static void DOT(GameObject Target, CreateNewDOT DOT, GameObject Source)
    {
        Buffs buffs = Target.GetComponent<Buffs>();
        bool Dup = false;

        if (buffs.DOTList.Count > 0)
        {

            for (int i = 0; i < buffs.DOTList.Count; i++)                             // if the target has any shield on it, start iterating through the list
            {
                if (buffs.DOTList[i].DOT_ID == DOT.DOT_ID)
                {
                    Messages.Message("DOT is a duplicate");
                    Dup = true;
                    if (buffs.DOTList[i].Stackable == true)                              // check for duplicates and if they are stackable
                    {
                        
                        CreateNewDOT myDOT = buffs.DOTList[i];
                        int Stacks = myDOT.CurrentStacks;
                        int MaxStacks = myDOT.StackLimit;

                        if (Stacks < MaxStacks)
                        {                                                                                         // if its not at max stacks, add a stack and reset the duration/health
                            myDOT.Ticks = 0;                                                                     // dont have to remve/reset these because we want people to keep the uptime high by recasting
                            myDOT.CurrentStacks += 1;
                            myDOT.CurrentDamage = myDOT.Damage * myDOT.CurrentStacks;
                            Messages.Message("DOT is stackable: currently at " + myDOT.CurrentStacks);
                            Messages.CombatLog(LogTemplates.Stack(Target, myDOT.DOTName, Source, myDOT.CurrentStacks));
                        }

                        if (Stacks >= MaxStacks)                                                                       // if its at max stacks, just refresh it by resetting the duration/health
                        {
                            myDOT.Ticks = 0;
                            myDOT.CurrentDamage = myDOT.Damage * MaxStacks;
                            Messages.Message("DOT is at max stacks");
                        }

                    }
                    else
                    {                                                                                               // if its a duplicate but not stackable, just reset it;
                        CreateNewDOT myDOT = buffs.DOTList[i];
                        myDOT.Ticks = 0;
                        Messages.CombatLog(LogTemplates.GainsFrom(Target, DOT.DOTName, Source));
                    }
                }
                else
                {
                    Dup = false;                                                                                // if not a duplicate add a new one
                    buffs.DOTList.Add(DOT);
                    Messages.CombatLog(LogTemplates.GainsFrom(Target, DOT.DOTName, Source));
                }
            }
        }

        if (Dup == false)                                                                                       // adding a new one when the list has nothing in it already               
        {
            buffs.DOTList.Add(DOT);
            Messages.CombatLog(LogTemplates.GainsFrom(Target, DOT.DOTName, Source));
        }
    }
}
