using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellBook : MonoBehaviour {

    public CreateSpellBook mySpells;

    bool ShowDebuffs;
    bool ShowDirectHeals;
    bool ShowHOTs;
    bool ShowRangedBehaviors;


    public List<string> BuffKeys = new List<string>();
    public List<CreateNewBuff> Buffs = new List<CreateNewBuff>();

    public List<string> DeBuffKeys = new List<string>();
    public List<CreateNewBuff> DeBuffs = new List<CreateNewBuff>();

    public List<string> DOTKeys = new List<string>();
    public List<CreateNewDOT> DOTs = new List<CreateNewDOT>();

    public List<string> HOTKeys = new List<string>();
    public List<CreateNewDOT> HOTs = new List<CreateNewDOT>();

    public List<string> ShieldKeys = new List<string>();
    public List<CreateNewShield> Shields = new List<CreateNewShield>();

    public List<string> DAKeys = new List<string>();
    public List<CreatNewDirectAttack> DAs = new List<CreatNewDirectAttack>();

    public List<string> DHKeys = new List<string>();
    public List<CreatNewDirectAttack> DHs = new List<CreatNewDirectAttack>();

    public List<string> DTKeys = new List<string>();
    public List<CreateNewDamageType> DTs = new List<CreateNewDamageType>();

    public List<string> ProjectileAKeys = new List<string>();
    public List<CreateNewProjectile> ProjectileAtks = new List<CreateNewProjectile>();

    public List<string> ProjectileHKeys = new List<string>();
    public List<CreateNewProjectile> ProjectileHeals = new List<CreateNewProjectile>();

    public List<string> ToonKeys = new List<string>();
    public List<CreateNewCharacter> Toons = new List<CreateNewCharacter>();
}
