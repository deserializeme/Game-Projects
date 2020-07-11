using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

[CustomEditor(typeof(SpellBook))]
public class SpellBookReader : Editor
{
    [SerializeField]
    bool ShowBuffs;
    bool ShowDebuffs;
    bool ShowDirectAttacks;
    bool ShowCharacters;
    bool ShowHOTs;
    bool ShowShields;
    bool ShowDOTs;
    bool ShowDirectHeals;
    bool ShowProjectileAttacks;
    bool ShowProjectileHeals;
    bool ShowDamageTypes;
    bool ShowRangedBehaviors;
    bool ShowResourceTypes;

    [SerializeField]
    bool All = false;

    [SerializeField]
    string Expand = "Expand All";


    public override void OnInspectorGUI()
    {
        SpellBook myBook = (SpellBook)target;

        //base.OnInspectorGUI();

        EditorGUILayout.BeginHorizontal();
        


        if (GUILayout.Button("Load"))
        {
            // clears existing entries to make room for new data
            myBook.BuffKeys.Clear();
            myBook.Buffs.Clear();
            myBook.mySpells.myBuffs.Clear();

            myBook.DeBuffKeys.Clear();
            myBook.DeBuffs.Clear();
            myBook.mySpells.myDeBuffs.Clear();

            myBook.DTKeys.Clear();
            myBook.DTs.Clear();
            myBook.mySpells.myDamageTypes.Clear();

            myBook.DAKeys.Clear();
            myBook.DAs.Clear();
            myBook.mySpells.myDirectAttacks.Clear();

            myBook.DHKeys.Clear();
            myBook.DHs.Clear();
            myBook.mySpells.myDirectHeals.Clear();

            myBook.DOTKeys.Clear();
            myBook.DOTs.Clear();
            myBook.mySpells.myDOTs.Clear();

            myBook.HOTKeys.Clear();
            myBook.HOTs.Clear();
            myBook.mySpells.myHOTs.Clear();

            myBook.ShieldKeys.Clear();
            myBook.Shields.Clear();
            myBook.mySpells.myShields.Clear();

            myBook.ToonKeys.Clear();
            myBook.Toons.Clear();
            myBook.mySpells.myCharacters.Clear();

            myBook.ProjectileAKeys.Clear();
            myBook.ProjectileAtks.Clear();
            myBook.mySpells.myProjectileAttacks.Clear();

            myBook.ProjectileHKeys.Clear();
            myBook.ProjectileHeals.Clear();
            myBook.mySpells.myProjectileHeals.Clear();

            // loads all the scripatable objects into arrays
            CreateNewBuff[] buffs = Resources.LoadAll<CreateNewBuff>("Attacks/Buffs").OrderBy(x => x.BuffName).ToArray();
            CreateNewBuff[] Debuffs = Resources.LoadAll<CreateNewBuff>("Attacks/Debuffs").OrderBy(x => x.BuffName).ToArray();
            CreateNewDamageType[] damageTypes = Resources.LoadAll<CreateNewDamageType>("Attacks/DamageTypes").OrderBy(x => x.Name).ToArray();
            CreatNewDirectAttack[] DirectDamage = Resources.LoadAll<CreatNewDirectAttack>("Attacks/DirectDamage").OrderBy(x => x.AttackName).ToArray();
            CreatNewDirectAttack[] DirectHeals = Resources.LoadAll<CreatNewDirectAttack>("Attacks/DirectHeals").OrderBy(x => x.AttackName).ToArray();
            CreateNewProjectile[] ProjectileAtks = Resources.LoadAll<CreateNewProjectile>("Attacks/ProjectileAttacks").OrderBy(x => x.AttackName).ToArray();
            CreateNewProjectile[] ProjectileHeals = Resources.LoadAll<CreateNewProjectile>("Attacks/ProjectileHeals").OrderBy(x => x.AttackName).ToArray();
            CreateNewDOT[] DOTs = Resources.LoadAll<CreateNewDOT>("Attacks/DOTs").OrderBy(x => x.DOTName).ToArray();
            CreateNewDOT[] HOTs = Resources.LoadAll<CreateNewDOT>("Attacks/HOTs").OrderBy(x => x.DOTName).ToArray();
            CreateNewShield[] Shields = Resources.LoadAll<CreateNewShield>("Attacks/Shields").OrderBy(x => x.BuffName).ToArray();
            CreateNewCharacter[] Toons = Resources.LoadAll<CreateNewCharacter>("Toons").OrderBy(x => x.Toon_Name).ToArray();

            // populates the script with data from the arrays
            // populates the scriptable object dictionary
            for (int i = 0; i < buffs.Length; i++)
            {
                CreateNewBuff Buff = buffs[i];
                myBook.BuffKeys.Add(buffs[i].name);
                myBook.Buffs.Add(Buff);
                myBook.mySpells.myBuffs.Add(Buff.BuffName, Buff);
            }

            for (int i = 0; i < Debuffs.Length; i++)
            {
                CreateNewBuff DeBuff = Debuffs[i];
                myBook.DeBuffKeys.Add(Debuffs[i].name);
                myBook.DeBuffs.Add(DeBuff);
                myBook.mySpells.myDeBuffs.Add(DeBuff.BuffName, DeBuff);
            }

            for (int i = 0; i < damageTypes.Length; i++)
            {
                CreateNewDamageType DT = damageTypes[i];
                myBook.DTKeys.Add(damageTypes[i].name);
                myBook.DTs.Add(DT);
            }

            for (int i = 0; i < DirectDamage.Length; i++)
            {
                CreatNewDirectAttack DA = DirectDamage[i];
                myBook.DAKeys.Add(DirectDamage[i].name);
                myBook.DAs.Add(DA);
            }

            for (int i = 0; i < DirectDamage.Length; i++)
            {
                CreateNewProjectile PA = ProjectileAtks[i];
                myBook.ProjectileAKeys.Add(ProjectileAtks[i].name);
                myBook.ProjectileAtks.Add(PA);
            }

            for (int i = 0; i < DirectDamage.Length; i++)
            {
                CreateNewProjectile PH = ProjectileHeals[i];
                myBook.ProjectileHKeys.Add(ProjectileHeals[i].name);
                myBook.ProjectileHeals.Add(PH);
            }

            for (int i = 0; i < DirectHeals.Length; i++)
            {
                CreatNewDirectAttack DH = DirectHeals[i];
                myBook.DHKeys.Add(DirectHeals[i].name);
                myBook.DHs.Add(DH);
            }

            for (int i = 0; i < DOTs.Length; i++)
            {
                CreateNewDOT Dot = DOTs[i];
                myBook.DOTKeys.Add(DOTs[i].name);
                myBook.DOTs.Add(Dot);
            }

            for (int i = 0; i < HOTs.Length; i++)
            {
                CreateNewDOT Hot = HOTs[i];
                myBook.HOTKeys.Add(HOTs[i].name);
                myBook.HOTs.Add(Hot);
            }

            for (int i = 0; i < Shields.Length; i++)
            {
                CreateNewShield Shield = Shields[i];
                myBook.ShieldKeys.Add(Shields[i].name);
                myBook.Shields.Add(Shield);
            }

            for (int i = 0; i < Toons.Length; i++)
            {
                CreateNewCharacter Toon = Toons[i];
                myBook.ToonKeys.Add(Toons[i].name);
                myBook.Toons.Add(Toon);
            }
        }
        EditorGUILayout.EndHorizontal();

        ShowBuffs = EditorGUILayout.Foldout(ShowBuffs, "Buffs");
        if (ShowBuffs)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.HelpBox("Name", MessageType.None);
            EditorGUILayout.HelpBox("Buff", MessageType.None);
            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < myBook.BuffKeys.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField( "#" + i + ". " + myBook.Buffs[i].BuffName);
                EditorGUILayout.ObjectField(myBook.Buffs[i], typeof(CreateNewBuff), false);
                EditorGUILayout.EndHorizontal();
            }
        }

        ShowDebuffs = EditorGUILayout.Foldout(ShowDebuffs, "DeBuffs");
        if (ShowDebuffs)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.HelpBox("Name", MessageType.None);
            EditorGUILayout.HelpBox("DeBuff", MessageType.None);
            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < myBook.DeBuffKeys.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("#" + i + ". " + myBook.DeBuffs[i].BuffName);
                EditorGUILayout.ObjectField(myBook.DeBuffs[i], typeof(CreateNewBuff), false);
                EditorGUILayout.EndHorizontal();
            }
        }

        ShowDOTs = EditorGUILayout.Foldout(ShowDOTs, "DOTs");
        if (ShowDOTs)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.HelpBox("Name", MessageType.None);
            EditorGUILayout.HelpBox("DOT", MessageType.None);
            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < myBook.DOTKeys.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("#" + i + ". " + myBook.DOTs[i].DOTName);
                EditorGUILayout.ObjectField(myBook.DOTs[i], typeof(CreateNewDOT), false);
                EditorGUILayout.EndHorizontal();
            }
        }

        ShowHOTs = EditorGUILayout.Foldout(ShowHOTs, "HOTs");
        if (ShowHOTs)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.HelpBox("Name", MessageType.None);
            EditorGUILayout.HelpBox("HOT", MessageType.None);
            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < myBook.HOTKeys.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("#" + i + ". " + myBook.DOTs[i].DOTName);
                EditorGUILayout.ObjectField(myBook.HOTs[i], typeof(CreateNewDOT), false);
                EditorGUILayout.EndHorizontal();
            }
        }

        ShowDirectAttacks = EditorGUILayout.Foldout(ShowDirectAttacks, "Direct Attacks");
        if (ShowDirectAttacks)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.HelpBox("Name", MessageType.None);
            EditorGUILayout.HelpBox("Direct Attack", MessageType.None);
            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < myBook.DAKeys.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("#" + i + ". " + myBook.DAs[i].AttackName);
                EditorGUILayout.ObjectField(myBook.DAs[i], typeof(CreatNewDirectAttack), false);
                EditorGUILayout.EndHorizontal();
            }
        }

        ShowDirectHeals = EditorGUILayout.Foldout(ShowDirectHeals, "Direct Heals");
        if (ShowDirectHeals)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.HelpBox("Name", MessageType.None);
            EditorGUILayout.HelpBox("Direct Heal", MessageType.None);
            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < myBook.DHKeys.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("#" + i + ". " + myBook.DHs[i].AttackName);
                EditorGUILayout.ObjectField(myBook.DHs[i], typeof(CreatNewDirectAttack), false);
                EditorGUILayout.EndHorizontal();
            }
        }

        ShowProjectileAttacks = EditorGUILayout.Foldout(ShowProjectileAttacks, "Projectile Attacks");
        if (ShowProjectileAttacks)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.HelpBox("Name", MessageType.None);
            EditorGUILayout.HelpBox("Projectile Attack", MessageType.None);
            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < myBook.ProjectileAKeys.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("#" + i + ". " + myBook.ProjectileAtks[i].AttackName);
                EditorGUILayout.ObjectField(myBook.ProjectileAtks[i], typeof(CreateNewProjectile), false);
                EditorGUILayout.EndHorizontal();
            }
        }

        ShowProjectileHeals = EditorGUILayout.Foldout(ShowProjectileHeals, "Projectile Heals");
        if (ShowProjectileHeals)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.HelpBox("Name", MessageType.None);
            EditorGUILayout.HelpBox("Projectile Heal", MessageType.None);
            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < myBook.ProjectileHKeys.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("#" + i + ". " + myBook.ProjectileHeals[i].AttackName);
                EditorGUILayout.ObjectField(myBook.ProjectileHeals[i], typeof(CreateNewProjectile), false);
                EditorGUILayout.EndHorizontal();
            }
        }

        ShowShields = EditorGUILayout.Foldout(ShowShields, "Shields");
        if (ShowShields)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.HelpBox("Name", MessageType.None);
            EditorGUILayout.HelpBox("Shield", MessageType.None);
            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < myBook.ShieldKeys.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("#" + i + ". " + myBook.Shields[i].BuffName);
                EditorGUILayout.ObjectField(myBook.Shields[i], typeof(CreateNewShield), false);
                EditorGUILayout.EndHorizontal();
            }
        }

        ShowDamageTypes = EditorGUILayout.Foldout(ShowDamageTypes, "Damage Types");
        if (ShowDamageTypes)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.HelpBox("Name", MessageType.None);
            EditorGUILayout.HelpBox("Damage Type", MessageType.None);
            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < myBook.DTKeys.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("#" + i + ". " + myBook.DTs[i].name);
                EditorGUILayout.ObjectField(myBook.DTs[i], typeof(CreateNewDamageType), false);
                EditorGUILayout.EndHorizontal();
            }
        }

        ShowCharacters = EditorGUILayout.Foldout(ShowCharacters, "Toons");
        if (ShowCharacters)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.HelpBox("Name", MessageType.None);
            EditorGUILayout.HelpBox("Toons", MessageType.None);
            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < myBook.ToonKeys.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("#" + i + ". " + myBook.Toons[i].name);
                EditorGUILayout.ObjectField(myBook.Toons[i], typeof(CreateNewCharacter), false);
                EditorGUILayout.EndHorizontal();
            }
        }






    }

    void OnInspectorUpdate()
    {
        this.Repaint();
    }



}
