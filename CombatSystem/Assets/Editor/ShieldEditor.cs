using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CreateNewShield))]
public class ShieldEditor : Editor {

    public override void OnInspectorGUI()
    {
        CreateNewShield myCreateNewShield = (CreateNewShield)target;

        EditorGUILayout.HelpBox("Basic Info", MessageType.None);
        myCreateNewShield.ShieldID = EditorGUILayout.TextField("Shield ID", myCreateNewShield.ShieldID);
        myCreateNewShield.BuffName = EditorGUILayout.TextField("Shield Name", myCreateNewShield.BuffName);
        myCreateNewShield.ToolTip = EditorGUILayout.TextField("Tool Tip", myCreateNewShield.ToolTip);
        myCreateNewShield.Icon = (Sprite)EditorGUILayout.ObjectField("Icon", myCreateNewShield.Icon, typeof(Sprite), false);

        EditorGUILayout.Space();
        myCreateNewShield.Target = (GameObject)EditorGUILayout.ObjectField("Target", myCreateNewShield.Target, typeof(GameObject), false);
        myCreateNewShield.Source = (GameObject)EditorGUILayout.ObjectField("Source", myCreateNewShield.Source, typeof(GameObject), false);

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Resource Consumption", MessageType.None);
        EditorGUILayout.BeginHorizontal();
        myCreateNewShield.ResourceType = (CreateNewResource)EditorGUILayout.ObjectField(myCreateNewShield.ResourceType, typeof(CreateNewResource), false);
        if (myCreateNewShield.ResourceType)
        {
            Color ResourceColor = myCreateNewShield.ResourceType.ResourceBarColor;
            EditorGUILayout.ColorField(ResourceColor);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Resource Cost (as decimal %)");
        myCreateNewShield.ResourceCost = EditorGUILayout.FloatField(myCreateNewShield.ResourceCost, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Damage Settings", MessageType.None);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Shield Starting Health");
        myCreateNewShield.MaxHealth = EditorGUILayout.FloatField(myCreateNewShield.MaxHealth, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Shield Current Health");
        myCreateNewShield.CurrentHealth = EditorGUILayout.FloatField(myCreateNewShield.CurrentHealth, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Duration");
        myCreateNewShield.Duration = EditorGUILayout.IntField(myCreateNewShield.Duration, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Block Specific Damage Type?");
        myCreateNewShield.Specific_Type = EditorGUILayout.Toggle(myCreateNewShield.Specific_Type, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        if (myCreateNewShield.Specific_Type)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Damage Type");
            myCreateNewShield.Damage_Type = (CreateNewDamageType)EditorGUILayout.ObjectField(myCreateNewShield.Damage_Type, typeof(CreateNewDamageType), false, GUILayout.MaxWidth(256));
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Maximum Range");
        myCreateNewShield.MaxRange = EditorGUILayout.IntField(myCreateNewShield.MaxRange, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Apply To Hostile Targets");
        myCreateNewShield.HostileOnly = EditorGUILayout.Toggle(myCreateNewShield.HostileOnly, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Apply To Friendly Target");
        myCreateNewShield.FriendlyOnly= EditorGUILayout.Toggle(myCreateNewShield.FriendlyOnly, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Shield Behavior", MessageType.None);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Cast Time");
        myCreateNewShield.CastTime = EditorGUILayout.FloatField(myCreateNewShield.CastTime, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("CoolDown");
        myCreateNewShield.CoolDown = EditorGUILayout.FloatField(myCreateNewShield.CoolDown, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("CD Remaining");
        myCreateNewShield.CoolDownRemaining = EditorGUILayout.FloatField(myCreateNewShield.CoolDownRemaining, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Ready to cast");
        myCreateNewShield.ReadyToCast = EditorGUILayout.Toggle(myCreateNewShield.ReadyToCast, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();



        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Stackable?");
        myCreateNewShield.Stackable = EditorGUILayout.Toggle(myCreateNewShield.Stackable, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        if (myCreateNewShield.Stackable)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Stack Limit");
            myCreateNewShield.StackLimit = EditorGUILayout.IntField(myCreateNewShield.StackLimit, GUILayout.MaxWidth(64));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Stack Limit");
            myCreateNewShield.StackLimit = EditorGUILayout.IntField(myCreateNewShield.StackLimit, GUILayout.MaxWidth(64));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Current Stacks");
            myCreateNewShield.CurrentStacks = EditorGUILayout.IntField(myCreateNewShield.CurrentStacks, GUILayout.MaxWidth(64));
            EditorGUILayout.EndHorizontal();

        }

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Trigger an effect on shield break?");
        myCreateNewShield.BreakEffect = EditorGUILayout.Toggle(myCreateNewShield.BreakEffect, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        if (myCreateNewShield.BreakEffect)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Buff");
            myCreateNewShield.BreakBuff = (CreateNewBuff)EditorGUILayout.ObjectField(myCreateNewShield.BreakBuff, typeof(CreateNewBuff), false, GUILayout.MaxWidth(256));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("DOT/HOT");
            myCreateNewShield.BreakDot = (CreateNewDOT)EditorGUILayout.ObjectField(myCreateNewShield.BreakDot, typeof(CreateNewDOT), false, GUILayout.MaxWidth(256));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Attack");
            myCreateNewShield.BreakAttack = (CreatNewDirectAttack)EditorGUILayout.ObjectField(myCreateNewShield.BreakAttack, typeof(CreatNewDirectAttack), false, GUILayout.MaxWidth(256));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Projectile");
            myCreateNewShield.BreakProjectile = (CreateNewProjectile)EditorGUILayout.ObjectField(myCreateNewShield.BreakProjectile, typeof(CreateNewProjectile), false, GUILayout.MaxWidth(256));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Apply to Self");
            myCreateNewShield.Self = EditorGUILayout.Toggle(myCreateNewShield.Self);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Apply to Breaker");
            myCreateNewShield.Breaker = EditorGUILayout.Toggle(myCreateNewShield.Breaker);
            EditorGUILayout.EndHorizontal();
        }

        
        



        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("");
        EditorGUILayout.EndHorizontal();
        //   GUILayout.MaxWidth(64)




        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Particle Settings", MessageType.None);
        myCreateNewShield.UseParticle = EditorGUILayout.Toggle("Use Particle", myCreateNewShield.UseParticle);
        myCreateNewShield.Particle = (GameObject)EditorGUILayout.ObjectField("Particle Prefab", myCreateNewShield.Particle, typeof(GameObject), false);
        myCreateNewShield.ParticleDuration = EditorGUILayout.IntField("Particle Duration", myCreateNewShield.ParticleDuration);

        //base.OnInspectorGUI();


        if (GUI.changed)
        {
            EditorUtility.SetDirty(myCreateNewShield);
        }
    }
}
