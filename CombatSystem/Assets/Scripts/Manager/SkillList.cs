using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MovementEffects;

public class SkillList : MonoBehaviour
{

    public List<CreateNewBuff> Buffs = new List<CreateNewBuff>();
    public List<CreateNewDOT> DOTs = new List<CreateNewDOT>();
    public List<CreatNewDirectAttack> DAttack = new List<CreatNewDirectAttack>();
    public List<CreateNewShield> Shields = new List<CreateNewShield>();
    public List<CreateNewProjectile> Projectiles = new List<CreateNewProjectile>();
    public int SpellsOnCoolDown;

    CreateNewCharacter Toon;

    public bool GCD;
    public float GCD_Remaining;
    public float GlobalCD_Length;

    /// <summary>
    /// poulates the toon's ability list
    /// </summary>
    public void GetMoves()  //need to create instances of the attacks so we dont write data permanantly to the templates
    {
        Toon = gameObject.GetComponent<Stats>().Toon_Profile;
    
        for(int i = 0; i < Toon.Buffs.Count; i++)
        {
            Buffs.Add(null);
            Buffs[i] = Instantiate<CreateNewBuff>(Toon.Buffs[i]);
            Buffs[i].name = Toon.Buffs[i].name;
            Buffs[i].ReadyToCast = true;
        }

        for (int i = 0; i < Toon.DOTs.Count; i++)
        {
            DOTs.Add(null);
            DOTs[i] = Instantiate<CreateNewDOT>(Toon.DOTs[i]);
            DOTs[i].name = Toon.DOTs[i].name;
            DOTs[i].ReadyToCast = true;
        }

        for (int i = 0; i < Toon.DAttack.Count; i++)
        {
            DAttack.Add(null);
            DAttack[i] = Instantiate<CreatNewDirectAttack>(Toon.DAttack[i]);
            DAttack[i].name = Toon.DAttack[i].name;
            DAttack[i].ReadyToCast = true;
        }

        for (int i = 0; i < Toon.Shields.Count; i++)
        {
            Shields.Add(null);
            Shields[i] = Instantiate<CreateNewShield>(Toon.Shields[i]);
            Shields[i].name = Toon.Shields[i].name;
            Shields[i].ReadyToCast = true;
        }

        for (int i = 0; i < Toon.Projectiles.Count; i++)
        {
            Projectiles.Add(null);
            Projectiles[i] = Instantiate<CreateNewProjectile>(Toon.Projectiles[i]);
            Projectiles[i].name = Toon.Projectiles[i].name;
            Projectiles[i].ReadyToCast = true;
        }
    }

    void Start()
    {
        GetMoves();
        SpellsOnCoolDown = 0;
    }

    /// <summary>
    /// tracks which moves are on cooldown
    /// </summary>
    /// <param name="CD_Time"></param>
    /// <param name="Buff"></param>
    /// <param name="DOT"></param>
    /// <param name="Shield"></param>
    /// <param name="DAttack"></param>
    /// <param name="Projectile"></param>
    /// <returns></returns>
    public IEnumerator<float> CoolDownTimer(float CD_Time, CreateNewBuff Buff = null, CreateNewDOT DOT = null, CreateNewShield Shield = null, CreatNewDirectAttack DAttack = null, CreateNewProjectile Projectile = null)
    {
        float Start = Time.time;
        float End = Start + CD_Time;

        while (End >= Time.time)
        {
            if (Buff != null)
            {
                Buff.ReadyToCast = false;
                float diff = End - Time.time;
                Buff.CoolDownRemaining = diff;
            }

            if (Shield != null)
            {
                Shield.ReadyToCast = false;
                float diff = End - Time.time;
                Shield.CoolDownRemaining = diff;
            }

            if (DOT != null)
            {
                DOT.ReadyToCast = false;
                float diff = End - Time.time;
                DOT.CoolDownRemaining = diff;
            }

            if (DAttack != null)
            {
                DAttack.ReadyToCast = false;
                float diff = End - Time.time;
                DAttack.CoolDownRemaining = diff;
            }

            if (Projectile != null)
            {
                Projectile.ReadyToCast = false;
                float diff = End - Time.time;
                Projectile.CoolDownRemaining = diff;
            }

            yield return 0f;
        }

        if (Buff != null)
        {
            Buff.ReadyToCast = true;
            Buff.CoolDownRemaining = 0;
        }

        if (Shield != null)
        {
            Shield.ReadyToCast = true;
            Shield.CoolDownRemaining = 0;
        }

        if (DOT != null)
        {
            DOT.ReadyToCast = true;
            DOT.CoolDownRemaining = 0;
        }

        if (DAttack != null)
        {
            DAttack.ReadyToCast = true;
            DAttack.CoolDownRemaining = 0;
        }

        if (Projectile != null)
        {
            Projectile.ReadyToCast = true;
            Projectile.CoolDownRemaining = 0;
        }

    }

    /// <summary>
    /// monitors and enforces the global cooldown
    /// </summary>
    /// <returns></returns>
    public IEnumerator<float> GCDTraacker()
    {
        GCD = true;
        float EndTime = Time.time + GlobalCD_Length;

        while (EndTime >= Time.time)
        {
            GCD_Remaining = EndTime - Time.time;
            yield return 0f;
        }

        GCD = false;
        GCD_Remaining = 0;
    }

    /// <summary>
    /// returns true if ability is on cooldown
    /// </summary>
    /// <param name="Source"></param>
    /// <param name="Buff"></param>
    /// <param name="DOT"></param>
    /// <param name="Shield"></param>
    /// <param name="DAttack"></param>
    /// <param name="Projectile"></param>
    /// <returns></returns>
    public static bool IsMoveOnCD(GameObject Source, CreateNewBuff Buff = null, CreateNewDOT DOT = null, CreateNewShield Shield = null, CreatNewDirectAttack DAttack = null, CreateNewProjectile Projectile = null)
    {
        // the fucky for loop to find the index by comparing names is there because for some reason List.IndexOf always returns -1.

        bool ReadyToCast = false;
        SkillList Skills = Source.GetComponent<SkillList>();

        if (Buff != null)
        {
            int Index = -1;

            for (int i = 0; i < Skills.Buffs.Count; i++)
            {
                string Name = Skills.Buffs[i].name;
                if (Name == Buff.name)
                {
                    Index = i;
                }
            }
            ReadyToCast = Skills.Buffs[Index].ReadyToCast;
        }


        if (DOT != null)
        {
            int Index = -1;

            for (int i = 0; i < Skills.DOTs.Count; i++)
            {
                string Name = Skills.DOTs[i].name;
                if (Name == DOT.name)
                {
                    Index = i;
                }
            }
            ReadyToCast = Skills.DOTs[Index].ReadyToCast;
        }

        if (Shield != null)
        {
            int Index = -1;

            for (int i = 0; i < Skills.Shields.Count; i++)
            {
                string Name = Skills.Shields[i].name;
                if (Name == Shield.name)
                {
                    Index = i;
                }
            }
            ReadyToCast = Skills.Shields[Index].ReadyToCast;
        }

        if (DAttack != null)
        {
            int Index = -1;

            for (int i = 0; i < Skills.DAttack.Count; i++)
            {
                string Name = Skills.DAttack[i].name;
                if (Name == DAttack.name)
                {
                    Index = i;
                }
            }

            ReadyToCast = Skills.DAttack[Index].ReadyToCast;
        }

        if (Projectile != null)
        {
            int Index = -1;

            for (int i = 0; i < Skills.Projectiles.Count; i++)
            {
                string Name = Skills.Projectiles[i].name;
                if (Name == Projectile.name)
                {
                    Index = i;
                }
            }

            ReadyToCast = Skills.Projectiles[Index].ReadyToCast;
        }

        return ReadyToCast;
    }

    /// <summary>
    /// starts the cooldown timer on an ability
    /// </summary>
    /// <param name="Source"></param>
    /// <param name="Buff"></param>
    /// <param name="DOT"></param>
    /// <param name="Shield"></param>
    /// <param name="DAttack"></param>
    /// <param name="Projectile"></param>
    public static void PutMoveOnCD(GameObject Source, CreateNewBuff Buff = null, CreateNewDOT DOT = null, CreateNewShield Shield = null, CreatNewDirectAttack DAttack = null, CreateNewProjectile Projectile = null)
    {
        // the fucky for loop to find the index by comparing names is there because for some reason List.IndexOf always returns -1.

        SkillList Skills = Source.GetComponent<SkillList>();

        if (Buff != null)
        {
            int Index = -1;

            for (int i = 0; i < Skills.Buffs.Count; i++)
            {
                string Name = Skills.Buffs[i].name;
                if (Name == Buff.name)
                {
                    Index = i;
                }
            }

            float CD = Buff.CoolDown;
            CreateNewBuff myBuff = Skills.Buffs[Index];
            Timing.RunCoroutine(Skills.CoolDownTimer(CD, myBuff, null, null, null));
        }

        if (DOT != null)
        {
            int Index = -1;

            for (int i = 0; i < Skills.DOTs.Count; i++)
            {
                string Name = Skills.DOTs[i].name;
                if (Name == DOT.name)
                {
                    Index = i;
                }
            }

            float CD = DOT.CoolDown;
            CreateNewDOT myDOT = Skills.DOTs[Index];
            Timing.RunCoroutine(Skills.CoolDownTimer(CD, null, myDOT, null, null));
        }

        if (Shield != null)
        {
            int Index = -1;

            for (int i = 0; i < Skills.Shields.Count; i++)
            {
                string Name = Skills.Shields[i].name;
                if (Name == Shield.name)
                {
                    Index = i;
                }
            }

            float CD = Shield.CoolDown;
            CreateNewShield myShield = Skills.Shields[Index];
            Timing.RunCoroutine(Skills.CoolDownTimer(CD, null, null, myShield, null));
        }

        if (DAttack != null)
        {
            int Index = -1;

            for (int i = 0; i < Skills.DAttack.Count; i++)
            {
                string Name = Skills.DAttack[i].name;
                if (Name == DAttack.name)
                {
                    Index = i;
                    //Debug.Log(Index);
                }
            }
            float CD = DAttack.CoolDown;
            CreatNewDirectAttack myDattack = Skills.DAttack[Index];
            Timing.RunCoroutine(Skills.CoolDownTimer(CD, null, null, null, myDattack));
        }

        if (Projectile != null)
        {
            int Index = -1;

            for (int i = 0; i < Skills.Projectiles.Count; i++)
            {
                string Name = Skills.Projectiles[i].name;
                if (Name == Projectile.name)
                {
                    Index = i;
                    //Debug.Log(Index);
                }
            }
            float CD = Projectile.CoolDown;
            CreateNewProjectile myProjectile = Skills.Projectiles[Index];
            Timing.RunCoroutine(Skills.CoolDownTimer(CD, null, null, null, null, myProjectile));
        }
    }

    /// <summary>
    /// returns true if the global cooldown is in effect
    /// </summary>
    /// <param name="Source"></param>
    /// <returns></returns>
    public static bool OnGCD(GameObject Source)
    {
        bool OnGCD = Source.GetComponent<SkillList>().GCD;

        return OnGCD;
    }

}
