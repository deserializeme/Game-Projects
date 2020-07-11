using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CreateNewDOT))]
public class DOTEditor : Editor {

    public override void OnInspectorGUI()
    {
        CreateNewDOT myCreateNewDOT = (CreateNewDOT)target;

        EditorGUILayout.HelpBox("Basic Info", MessageType.None);
        myCreateNewDOT.DOT_ID = EditorGUILayout.TextField("DOT ID", myCreateNewDOT.DOT_ID);
        myCreateNewDOT.DOTName = EditorGUILayout.TextField("DOT Name", myCreateNewDOT.DOTName);
        myCreateNewDOT.ToolTip = EditorGUILayout.TextField("Tool Tip", myCreateNewDOT.ToolTip);
        myCreateNewDOT.Icon = (Sprite)EditorGUILayout.ObjectField("GUI Sprite", myCreateNewDOT.Icon, typeof(Sprite), false);

        EditorGUILayout.Space();
        myCreateNewDOT.Target = (GameObject)EditorGUILayout.ObjectField("Target Object", myCreateNewDOT.Target, typeof(GameObject), false);
        myCreateNewDOT.Source = (GameObject)EditorGUILayout.ObjectField("Source Object", myCreateNewDOT.Source, typeof(GameObject), false);

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Resource Consumption", MessageType.None);
        EditorGUILayout.BeginHorizontal();
        myCreateNewDOT.ResourceType = (CreateNewResource)EditorGUILayout.ObjectField(myCreateNewDOT.ResourceType, typeof(CreateNewResource), false);
        if (myCreateNewDOT.ResourceType)
        {
            Color ResourceColor = myCreateNewDOT.ResourceType.ResourceBarColor;
            EditorGUILayout.ColorField(ResourceColor);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Resource Cost (as a decimal %)");
        myCreateNewDOT.ResourceCost = EditorGUILayout.FloatField(myCreateNewDOT.ResourceCost, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Damage Info", MessageType.None);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Damage Type");
        myCreateNewDOT.DamageType = (CreateNewDamageType)EditorGUILayout.ObjectField(myCreateNewDOT.DamageType, typeof(CreateNewDamageType), false, GUILayout.MaxWidth(256));
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Damage Amount");
        myCreateNewDOT.Damage = EditorGUILayout.FloatField(myCreateNewDOT.Damage, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Is Heal?");
        myCreateNewDOT.IsHeal = EditorGUILayout.Toggle(myCreateNewDOT.IsHeal, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Maximum Range");
        myCreateNewDOT.MaxRange = EditorGUILayout.IntField(myCreateNewDOT.MaxRange, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Duration (number of Ticks)");
        myCreateNewDOT.Duration = EditorGUILayout.IntField(myCreateNewDOT.Duration, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Ticks");
        myCreateNewDOT.Ticks = EditorGUILayout.IntField(myCreateNewDOT.Ticks, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Cast Time");
        myCreateNewDOT.CastTime = EditorGUILayout.FloatField(myCreateNewDOT.CastTime, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Cool Down");
        myCreateNewDOT.CoolDown = EditorGUILayout.FloatField(myCreateNewDOT.CoolDown, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("CD Remaining");
        myCreateNewDOT.CoolDownRemaining = EditorGUILayout.FloatField(myCreateNewDOT.CoolDownRemaining, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Ready to cast");
        myCreateNewDOT.ReadyToCast = EditorGUILayout.Toggle(myCreateNewDOT.ReadyToCast, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Stackable?");
        myCreateNewDOT.Stackable = EditorGUILayout.Toggle(myCreateNewDOT.Stackable, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        if (myCreateNewDOT.Stackable)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Stack Limit");
            myCreateNewDOT.StackLimit = EditorGUILayout.IntField(myCreateNewDOT.StackLimit, GUILayout.MaxWidth(64));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Current Stacks");
            myCreateNewDOT.CurrentStacks = EditorGUILayout.IntField(myCreateNewDOT.CurrentStacks, GUILayout.MaxWidth(64));
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Apply To Hostile Targets");
        myCreateNewDOT.HostileOnly = EditorGUILayout.Toggle(myCreateNewDOT.HostileOnly, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Apply To Friendly Target");
        myCreateNewDOT.FriendlyOnly = EditorGUILayout.Toggle(myCreateNewDOT.FriendlyOnly, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Particle Prefab", MessageType.None);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Use Particle?");
        myCreateNewDOT.UseParticle = EditorGUILayout.Toggle(myCreateNewDOT.UseParticle, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        if (myCreateNewDOT.UseParticle)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Particle Prefabs");
            myCreateNewDOT.Particle = (GameObject)EditorGUILayout.ObjectField(myCreateNewDOT.Particle, typeof(GameObject), false, GUILayout.MaxWidth(256));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Particle Duration");
            myCreateNewDOT.ParticleDuration = EditorGUILayout.IntField(myCreateNewDOT.ParticleDuration, GUILayout.MaxWidth(256));
            EditorGUILayout.EndHorizontal();

        }

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Sounds", MessageType.None);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Cast");
        myCreateNewDOT.Cast = (AudioClip)EditorGUILayout.ObjectField(myCreateNewDOT.Cast, typeof(AudioClip), false, GUILayout.MaxWidth(256));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Land");
        myCreateNewDOT.Cast = (AudioClip)EditorGUILayout.ObjectField(myCreateNewDOT.Land, typeof(AudioClip), false, GUILayout.MaxWidth(256));
        EditorGUILayout.EndHorizontal();





        if (GUI.changed)
        {
            EditorUtility.SetDirty(myCreateNewDOT);
        }
    }
}
