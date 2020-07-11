using UnityEngine;
using System.Collections;

public class LogTemplates : MonoBehaviour
{

    /// <summary>
    /// creates a string using the StringFast class regarding the stacking of buffs/shields etc
    /// </summary>
    /// <param name="Target"></param>
    /// <param name="StackWhat"></param>
    /// <param name="Source"></param>
    /// <param name="Stacks"></param>
    /// <returns></returns>
    public static StringFast Stack(GameObject Target, string StackWhat, GameObject Source, int Stacks)
    {
        StringFast myString = new StringFast(64);
        myString.Append(Time.time).Append(": ").Append(LogTemplates.HostileColorPicker(Source));
        myString.Append(" applies a stack of ").Append(StackWhat).Append(" on ");
        myString.Append(LogTemplates.HostileColorPicker(Target)).Append(". ").Append("\n");
        myString.Append(" Total stacks = ").Append(Stacks);
        return myString;
    }

    /// <summary>
    /// creates a string using the StringFast class regarding one object revieveing a buff/debuff from another
    /// </summary>
    /// <param name="Target"></param>
    /// <param name="GainedWhat"></param>
    /// <param name="Source"></param>
    /// <returns></returns>
    public static StringFast GainsFrom(GameObject Target, string GainedWhat, GameObject Source)
    {
        StringFast myString = new StringFast(64);
        myString.Append(Time.time).Append(": ");
        myString.Append(LogTemplates.HostileColorPicker(Target));
        myString.Append(" gained ").Append(GainedWhat).Append(" from ");
        myString.Append(LogTemplates.HostileColorPicker(Source));
        return myString;
    }

    /// <summary>
    /// creates a string using the StringFast class regarding an effect fading from an object
    /// </summary>
    /// <param name="Source"></param>
    /// <param name="WhatFades"></param>
    /// <param name="Target"></param>
    /// <returns></returns>
    public static StringFast FadesFrom(GameObject Source, string WhatFades, GameObject Target)
    {
        StringFast myString = new StringFast(64);
        myString.Append(Time.time).Append(": ");
        myString.Append(LogTemplates.HostileColorPicker(Source)).Append("'s ").Append(WhatFades).Append(" fades from ");
        myString.Append(LogTemplates.HostileColorPicker(Target));
        return myString;
    }

    /// <summary>
    /// creates a string using the StringFast class regarding one object healing another
    /// </summary>
    /// <param name="Source"></param>
    /// <param name="WhatHeals"></param>
    /// <param name="Target"></param>
    /// <param name="Damage"></param>
    /// <returns></returns>
    public static StringFast HealsFor(GameObject Source, string WhatHeals, GameObject Target, float Damage, bool OverHeal = false)
    {
        if (!OverHeal)
        {
            StringFast myString = new StringFast(64);
            myString.Append(Time.time).Append(": ").Append(Source.name).Append("'s").Append(WhatHeals).Append(" heals ").Append(Target.name).Append(" for ").Append(Damage);
            return myString;
        }
        else
        {
            StringFast myString = new StringFast(64);
            myString.Append(Time.time).Append(": ").Append(Source.name).Append("'s").Append(WhatHeals).Append(" heals ").Append(Target.name).Append(" for ").Append(Damage).Append(" (Over-Heal) ");
            return myString;
        }
    }

    /// <summary>
    /// creates a string using the StringFast class regarding one object damaging another
    /// </summary>
    /// <param name="Source"></param>
    /// <param name="WhatHurts"></param>
    /// <param name="Target"></param>
    /// <param name="Damage"></param>
    /// <param name="DamageType"></param>
    /// <returns></returns>
    public static StringFast Damages(GameObject Source, string WhatHurts, GameObject Target, float Damage, string DamageType)
    {
        StringFast myString = new StringFast(64);
        myString.Append(Time.time).Append(": ").Append(LogTemplates.HostileColorPicker(Source));
        myString.Append("'s ").Append(WhatHurts).Append(" damages ");
        myString.Append(LogTemplates.HostileColorPicker(Target)).Append(" for ").Append(Damage).Append("(").Append(DamageType).Append(")");
        return myString;
    }

    /// <summary>
    /// creates a string using the StringFast class regarding one object killign another
    /// </summary>
    /// <param name="Killer"></param>
    /// <param name="Victim"></param>
    /// <returns></returns>
    public static StringFast HasKilled(GameObject Killer, GameObject Victim)
    {
        StringFast myString = new StringFast(64);
        myString.Append(Time.time).Append(": ");
        myString.Append(LogTemplates.HostileColorPicker(Killer));
        myString.Append(" killed ");
        myString.Append(LogTemplates.HostileColorPicker(Victim));
        return myString;
    }

    /// <summary>
    /// creates a string using the StringFast class regarding the current kill score for the match 
    /// </summary>
    /// <param name="Killer"></param>
    /// <returns></returns>
    public static StringFast ShowKills(GameObject Killer)
    {
        StringFast myString = new StringFast(64);
        myString.Append(Time.time).Append(": ").Append(LogTemplates.HostileColorPicker(Killer)).Append(" has ").Append(Killer.GetComponent<Stats>().Kills).Append(" Kills.");
        return myString;
    }

    /// <summary>
    /// creates a string using the StringFast class regarding the current death score for the match
    /// </summary>
    /// <param name="Victim"></param>
    /// <returns></returns>
    public static StringFast ShowDeaths(GameObject Victim)
    {
        StringFast myString = new StringFast(64);
        myString.Append(Time.time).Append(": ").Append(LogTemplates.HostileColorPicker(Victim)).Append(" has died ").Append(Victim.GetComponent<Stats>().Deaths).Append(" times.");
        return myString;
    }

    /// <summary>
    /// creates a string using the StringFast class indicating a buff of the same type already exists on the target
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="Target"></param>
    /// <returns></returns>
    public static StringFast BuffExists(string Name, GameObject Target)
    {
        StringFast myString = new StringFast(64);
        myString.Append(Time.time).Append(": ").Append(Name).Append(" already exists on ").Append(LogTemplates.HostileColorPicker(Target));
        return myString;
    }

    /// <summary>
    /// creates a string using the StringFast class and RichText Markup about the 'Who' object and its hostility towars the Static variable 'Player'
    /// </summary>
    /// <param name="Who"></param>
    /// <returns></returns>
    public static StringFast HostileColorPicker(GameObject Who)
    {
        bool Hostile = TeamAffiliation.IsHostile(FindPlayer.Player, Who);
        StringFast color = new StringFast(64);
        if (Hostile)
        {
            color.Append("<color=red>");
        }
        else
        {
            color.Append("<color=cyan>");
        }

        color.Append(Who.name);
        color.Append("</color>");

        return color;
    }

    /// <summary>
    /// creates a string using the StringFast class and RichText Markup about gaining experience;
    /// </summary>
    /// <param name="Who"></param>
    /// <returns></returns>
    public static StringFast GainsThing(GameObject Who, float Amount)
    {
        StringFast myString = new StringFast(64);
        myString.Append(Time.time).Append(": ");
        myString.Append(LogTemplates.HostileColorPicker(Who));
        myString.Append("gained ").Append(Amount).Append(" ");
        myString.Append("Experience");
        return myString;
    }
}