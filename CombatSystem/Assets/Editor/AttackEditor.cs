using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CreatNewDirectAttack))]
public class AttackEditor : Editor {

    public override void OnInspectorGUI()
    {
        CreatNewDirectAttack myCreateNewDirectAttack = (CreatNewDirectAttack)target;

        EditorGUILayout.HelpBox("Basic Info", MessageType.None);
        myCreateNewDirectAttack.AttackID = EditorGUILayout.TextField("Attack ID", myCreateNewDirectAttack.AttackID);
        myCreateNewDirectAttack.AttackName = EditorGUILayout.TextField("Attack Name", myCreateNewDirectAttack.AttackName);
        myCreateNewDirectAttack.ToolTip = EditorGUILayout.TextField("Tool Tip", myCreateNewDirectAttack.ToolTip);
        myCreateNewDirectAttack.Icon = (Sprite)EditorGUILayout.ObjectField("GUI Sprite", myCreateNewDirectAttack.Icon, typeof(Sprite), false);

        EditorGUILayout.Space();
        myCreateNewDirectAttack.Target = (GameObject)EditorGUILayout.ObjectField("Target Object", myCreateNewDirectAttack.Target, typeof(GameObject), false);
        myCreateNewDirectAttack.Source = (GameObject)EditorGUILayout.ObjectField("Source Object", myCreateNewDirectAttack.Source, typeof(GameObject), false);

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Resource Consumption", MessageType.None);
        EditorGUILayout.BeginHorizontal();
        myCreateNewDirectAttack.ResourceType = (CreateNewResource)EditorGUILayout.ObjectField(myCreateNewDirectAttack.ResourceType, typeof(CreateNewResource), false, GUILayout.MaxWidth(128));
        if (myCreateNewDirectAttack.ResourceType)
        {
            Color ResourceColor = myCreateNewDirectAttack.ResourceType.ResourceBarColor;
            EditorGUILayout.ColorField(ResourceColor);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Resource Cost (as decimal %)");
        myCreateNewDirectAttack.ResourceCost = EditorGUILayout.FloatField(myCreateNewDirectAttack.ResourceCost, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Damage info", MessageType.None);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Damage Type");
        myCreateNewDirectAttack.DamageType = (CreateNewDamageType)EditorGUILayout.ObjectField(myCreateNewDirectAttack.DamageType, typeof(CreateNewDamageType), false, GUILayout.MaxWidth(256));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Damage Amount");
        myCreateNewDirectAttack.Damage = EditorGUILayout.FloatField(myCreateNewDirectAttack.Damage, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Is Heal?");
        myCreateNewDirectAttack.IsHeal = EditorGUILayout.Toggle(myCreateNewDirectAttack.IsHeal, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Maximum Range");
        myCreateNewDirectAttack.MaxRange = EditorGUILayout.IntField(myCreateNewDirectAttack.MaxRange, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Cast Time");
        myCreateNewDirectAttack.CastTime = EditorGUILayout.FloatField(myCreateNewDirectAttack.CastTime, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("CoolDown");
        myCreateNewDirectAttack.CoolDown = EditorGUILayout.FloatField(myCreateNewDirectAttack.CoolDown, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("CD Remaining");
        myCreateNewDirectAttack.CoolDownRemaining = EditorGUILayout.FloatField(myCreateNewDirectAttack.CoolDownRemaining, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Ready to cast");
        myCreateNewDirectAttack.ReadyToCast = EditorGUILayout.Toggle(myCreateNewDirectAttack.ReadyToCast, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Apply To Hostile Targets");
        myCreateNewDirectAttack.HostileOnly = EditorGUILayout.Toggle(myCreateNewDirectAttack.HostileOnly, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Apply To Friendly Target");
        myCreateNewDirectAttack.FriendlyOnly = EditorGUILayout.Toggle(myCreateNewDirectAttack.FriendlyOnly, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Aplly Buff/Debuffs on hit", MessageType.None);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Apply an Effect?");
        myCreateNewDirectAttack.ApplyEffect = EditorGUILayout.Toggle(myCreateNewDirectAttack.ApplyEffect, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        if (myCreateNewDirectAttack.ApplyEffect)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Buff/Debuff to Apply");
            myCreateNewDirectAttack.SecondaryEffect = (CreateNewBuff)EditorGUILayout.ObjectField(myCreateNewDirectAttack.SecondaryEffect, typeof(CreateNewBuff), false, GUILayout.MaxWidth(256));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("DOT/HOT to Apply");
            myCreateNewDirectAttack.SecondaryEffectDOT = (CreateNewDOT)EditorGUILayout.ObjectField(myCreateNewDirectAttack.SecondaryEffectDOT, typeof(CreateNewDOT), false, GUILayout.MaxWidth(256));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.HelpBox("Apply Effect to...", MessageType.None);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Self");
            myCreateNewDirectAttack.Self = EditorGUILayout.Toggle(myCreateNewDirectAttack.Self,GUILayout.MaxWidth(64));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Target");
            myCreateNewDirectAttack.EffectTarget = EditorGUILayout.Toggle(myCreateNewDirectAttack.EffectTarget, GUILayout.MaxWidth(64));
            EditorGUILayout.EndHorizontal();

        }



        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Primary Particle Prefab", MessageType.None);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Use Primary Particles?");
        myCreateNewDirectAttack.UseParticle = EditorGUILayout.Toggle(myCreateNewDirectAttack.UseParticle, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();
        
        if (myCreateNewDirectAttack.UseParticle)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Particle Prefab");
            myCreateNewDirectAttack.Particle = (GameObject)EditorGUILayout.ObjectField(myCreateNewDirectAttack.Particle, typeof(GameObject), false, GUILayout.MaxWidth(256));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Particle Duration");
            myCreateNewDirectAttack.ParticleDuration = EditorGUILayout.IntField(myCreateNewDirectAttack.ParticleDuration, GUILayout.MaxWidth(256));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.HelpBox("Secondary Particle Prefab", MessageType.None);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Use Secondary Particles?");
            myCreateNewDirectAttack.UseSecondaryParticle = EditorGUILayout.Toggle(myCreateNewDirectAttack.UseSecondaryParticle, GUILayout.MaxWidth(64));
            EditorGUILayout.EndHorizontal();

            if (myCreateNewDirectAttack.UseSecondaryParticle)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Particle Prefab");
                myCreateNewDirectAttack.Secondary_Particle = (GameObject)EditorGUILayout.ObjectField(myCreateNewDirectAttack.Secondary_Particle, typeof(GameObject), false, GUILayout.MaxWidth(256));
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Particle Duration");
                myCreateNewDirectAttack.SecondaryParticleDuration = EditorGUILayout.IntField(myCreateNewDirectAttack.SecondaryParticleDuration, GUILayout.MaxWidth(256));
                EditorGUILayout.EndHorizontal();

            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Sounds", MessageType.None);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Cast Sound");
        myCreateNewDirectAttack.Cast = (AudioClip)EditorGUILayout.ObjectField(myCreateNewDirectAttack.Cast, typeof(AudioClip), false, GUILayout.MaxWidth(256));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Land Sound");
        myCreateNewDirectAttack.Land = (AudioClip)EditorGUILayout.ObjectField(myCreateNewDirectAttack.Land, typeof(AudioClip), false, GUILayout.MaxWidth(256));
        EditorGUILayout.EndHorizontal();


        // base.OnInspectorGUI();
        if (GUI.changed)
        {
            EditorUtility.SetDirty(myCreateNewDirectAttack);
        }

    }

}
