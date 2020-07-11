using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SpellBook", menuName = "SpellBook", order = 1)]
[System.Serializable]
public class CreateSpellBook : ScriptableObject
{
    public Dictionary<string, CreateNewBuff> myBuffs = new Dictionary<string, CreateNewBuff>();
    public Dictionary<string, CreateNewBuff> myDeBuffs = new Dictionary<string, CreateNewBuff>();
    public Dictionary<string, CreateNewDOT> myDOTs = new Dictionary<string, CreateNewDOT>();
    public Dictionary<string, CreateNewDOT> myHOTs = new Dictionary<string, CreateNewDOT>();
    public Dictionary<string, CreateNewShield> myShields = new Dictionary<string, CreateNewShield>();
    public Dictionary<string, CreatNewDirectAttack> myDirectAttacks = new Dictionary<string, CreatNewDirectAttack>();
    public Dictionary<string, CreatNewDirectAttack> myDirectHeals = new Dictionary<string, CreatNewDirectAttack>();
    public Dictionary<string, CreateNewProjectile> myProjectileAttacks = new Dictionary<string, CreateNewProjectile>();
    public Dictionary<string, CreateNewProjectile> myProjectileHeals = new Dictionary<string, CreateNewProjectile>();
    public Dictionary<string, CreateNewDamageType> myDamageTypes = new Dictionary<string, CreateNewDamageType>();
    public Dictionary<string, CreateNewCharacter> myCharacters = new Dictionary<string, CreateNewCharacter>();

}

