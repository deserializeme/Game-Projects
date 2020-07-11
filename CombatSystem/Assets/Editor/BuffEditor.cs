using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CreateNewBuff))]
public class BuffEditor : Editor
{
    bool Selected;

    public override void OnInspectorGUI()
    {
        CreateNewBuff myCreateNewBuff = (CreateNewBuff)target;

        EditorGUILayout.HelpBox("Basic Info", MessageType.None);
        myCreateNewBuff.BuffID = EditorGUILayout.TextField("Buff ID", myCreateNewBuff.BuffID);
        myCreateNewBuff.BuffName = EditorGUILayout.TextField("Buff Name", myCreateNewBuff.BuffName);
        myCreateNewBuff.ToolTip = EditorGUILayout.TextField("Tool Tip", myCreateNewBuff.ToolTip);

        EditorGUILayout.Space();
        myCreateNewBuff.Icon = (Sprite)EditorGUILayout.ObjectField("GUI Sprite", myCreateNewBuff.Icon, typeof(Sprite), false);
        myCreateNewBuff.Target = (GameObject)EditorGUILayout.ObjectField("Target Object", myCreateNewBuff.Target, typeof(GameObject), false);
        myCreateNewBuff.Source = (GameObject)EditorGUILayout.ObjectField("Source Object", myCreateNewBuff.Source, typeof(GameObject), false);

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Resource Consumption", MessageType.None);
        EditorGUILayout.BeginHorizontal();
        myCreateNewBuff.ResourceType = (CreateNewResource)EditorGUILayout.ObjectField(myCreateNewBuff.ResourceType, typeof(CreateNewResource), false);
        if (myCreateNewBuff.ResourceType)
        {
            Color ResourceColor = myCreateNewBuff.ResourceType.ResourceBarColor;
            EditorGUILayout.ColorField(ResourceColor);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Resource Cost (as decimal %)");
        myCreateNewBuff.ResourceCost = EditorGUILayout.FloatField(myCreateNewBuff.ResourceCost, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Maximum Range");
        myCreateNewBuff.MaxRange = EditorGUILayout.IntField(myCreateNewBuff.MaxRange, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Cast Time");
        myCreateNewBuff.CastTime = EditorGUILayout.FloatField(myCreateNewBuff.CastTime, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("CoolDown");
        myCreateNewBuff.CoolDown = EditorGUILayout.FloatField(myCreateNewBuff.CoolDown, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("CD Remaining");
        myCreateNewBuff.CoolDownRemaining = EditorGUILayout.FloatField(myCreateNewBuff.CoolDownRemaining, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Ready to cast");
        myCreateNewBuff.ReadyToCast = EditorGUILayout.Toggle(myCreateNewBuff.ReadyToCast, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Stat Effects (Enter as a decimal percentage)", MessageType.None);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Health Multiplier");
        myCreateNewBuff.Health_Multiplier = EditorGUILayout.FloatField(myCreateNewBuff.Health_Multiplier, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Speed Multiplier");
        myCreateNewBuff.Speed_Multiplier = EditorGUILayout.FloatField(myCreateNewBuff.Speed_Multiplier, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Power Multiplier");
        myCreateNewBuff.Power_Multiplier = EditorGUILayout.FloatField(myCreateNewBuff.Power_Multiplier, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("EXP Multiplier");
        myCreateNewBuff.Exp_Multiplier = EditorGUILayout.FloatField(myCreateNewBuff.Exp_Multiplier, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Accuracy Change");
        myCreateNewBuff.Accuracy = EditorGUILayout.FloatField(myCreateNewBuff.Accuracy, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Stackable?");
        myCreateNewBuff.Stackable = EditorGUILayout.Toggle(myCreateNewBuff.Stackable, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        if (myCreateNewBuff.Stackable)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Maximum Stacks");
            myCreateNewBuff.StackLimit = EditorGUILayout.IntField(myCreateNewBuff.StackLimit, GUILayout.MaxWidth(64));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Current Stacks");
            myCreateNewBuff.CurrentStacks = EditorGUILayout.IntField(myCreateNewBuff.CurrentStacks, GUILayout.MaxWidth(64));
            EditorGUILayout.EndHorizontal();

        }

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Duration in number of Ticks", MessageType.None);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Buff Duration");
        myCreateNewBuff.Duration = EditorGUILayout.IntField(myCreateNewBuff.Duration, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Particle Prefab", MessageType.None);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Use Particles?");
        myCreateNewBuff.UseParticle = EditorGUILayout.Toggle(myCreateNewBuff.UseParticle, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        if (myCreateNewBuff.UseParticle)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Particle Prefab");
            myCreateNewBuff.Particle = (GameObject)EditorGUILayout.ObjectField(myCreateNewBuff.Particle, typeof(GameObject), false, GUILayout.MaxWidth(256));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Particle Duration");
            myCreateNewBuff.ParticleDuration = EditorGUILayout.IntField(myCreateNewBuff.ParticleDuration, GUILayout.MaxWidth(256));
            EditorGUILayout.EndHorizontal();

        }


        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Buff Application Method", MessageType.None);


        if (myCreateNewBuff.Imediate || myCreateNewBuff.Falling || myCreateNewBuff.Over_Time)
        {
            Selected = true;
        }
        else
        {
            Selected = false;
        }

        if (Selected == true && myCreateNewBuff.Imediate != true)
        {

        }
        else
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Apply buff immediately");
            myCreateNewBuff.Imediate = EditorGUILayout.Toggle(myCreateNewBuff.Imediate, GUILayout.MaxWidth(64));
            EditorGUILayout.EndHorizontal();
        }

        if (Selected == true && myCreateNewBuff.Over_Time != true)
        {

        }
        else
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Apply buff over time");
            myCreateNewBuff.Over_Time = EditorGUILayout.Toggle(myCreateNewBuff.Over_Time, GUILayout.MaxWidth(64));
            EditorGUILayout.EndHorizontal();
        }

        if (Selected == true && myCreateNewBuff.Falling != true)
        {

        }
        else
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Apply buff and decrease over time");
            myCreateNewBuff.Falling = EditorGUILayout.Toggle(myCreateNewBuff.Falling, GUILayout.MaxWidth(64));
            EditorGUILayout.EndHorizontal();
        }

        if (Selected)
        {
            EditorGUILayout.LabelField("Deselect to view other options");
        }

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Apply To Hostile Targets");
        myCreateNewBuff.HostileOnly = EditorGUILayout.Toggle(myCreateNewBuff.HostileOnly, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Apply To Friendly Target");
        myCreateNewBuff.FriendlyOnly = EditorGUILayout.Toggle(myCreateNewBuff.FriendlyOnly, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Sounds", MessageType.None);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Cast Sound");
        myCreateNewBuff.Cast = (AudioClip)EditorGUILayout.ObjectField(myCreateNewBuff.Cast, typeof(AudioClip), false, GUILayout.MaxWidth(256));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Land Sound");
        myCreateNewBuff.Land = (AudioClip)EditorGUILayout.ObjectField(myCreateNewBuff.Land, typeof(AudioClip), false, GUILayout.MaxWidth(256));
        EditorGUILayout.EndHorizontal();


        //myCreateNewBuff.Health_Multiplier = EditorGUILayout.FloatField("Health Multiplier", myCreateNewBuff.Health_Multiplier);
        //base.OnInspectorGUI();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(myCreateNewBuff);
        }
    }


}

