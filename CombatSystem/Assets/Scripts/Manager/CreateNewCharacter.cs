using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

//creates a new scriptable object for Buffs

[CreateAssetMenu(fileName = "New Character", menuName = "New Character", order = 1)]
[System.Serializable]
public class CreateNewCharacter : ScriptableObject
{
    public string ToonID;
    public string Toon_Name;
    public string Toon_Description;
    public Sprite Icon;

    public GameObject Model;

    public float Base_Health;
    public float Base_Speed;
    public float Base_Power;
    public float Base_Accuracy;
    public float Base_Resource_Amount;
    public float Base_Resource_Regen;
    public float Base_Health_Regen;
    public float Experience;
    public float Gold;
    public int Level;
    public int EvolutionLevel;
    public CreateNewCharacter PreviousEvolution;
    public CreateNewCharacter NextEvolution;

    public CreateNewDamageType Type;
    public CreateNewResource ResourceType;

    public AudioClip Select;
    public AudioClip Attack;
    public AudioClip Error;
    public AudioClip Death;

    public List<CreateNewBuff> Buffs = new List<CreateNewBuff>();
    public List<CreateNewDOT> DOTs = new List<CreateNewDOT>();
    public List<CreatNewDirectAttack> DAttack = new List<CreatNewDirectAttack>();
    public List<CreateNewShield> Shields = new List<CreateNewShield>();
    public List<CreateNewProjectile> Projectiles = new List<CreateNewProjectile>();

}
