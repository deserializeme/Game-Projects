using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Stats))]
public class StatsEditor : Editor {

    float HPfraction;
    float ManaFraction;
    float EXPfraction;
    [SerializeField]
    bool Scoreboard;

    public override void OnInspectorGUI()
    {
        Stats myStats = (Stats)target;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Toon Profile");
        myStats.Toon_Profile = (CreateNewCharacter)EditorGUILayout.ObjectField(myStats.Toon_Profile, typeof(CreateNewCharacter), false, GUILayout.MaxWidth(128));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Progression Stats", MessageType.None);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Current Level");
        myStats.Level = EditorGUILayout.IntField(myStats.Level, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Next Evolution Level");
        myStats.EvolutionLevel = EditorGUILayout.IntField(myStats.EvolutionLevel, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        if (myStats.Experience > 0)
        {
            EXPfraction = myStats.Experience / myStats.ExperianceToLevel;
        }
        else
        {
            EXPfraction = 0;
        }

        Rect r3 = EditorGUILayout.BeginVertical();
        EditorGUI.ProgressBar(r3, EXPfraction, "Experience : " + myStats.Experience + " / " + myStats.ExperianceToLevel);
        GUILayout.Space(16);
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Current Stats", MessageType.None);

        if (myStats.Maxhealth > 0)
        {
            HPfraction = myStats.Health / myStats.Maxhealth;
        }
        else
        {
            HPfraction = 0;
        }

        Rect r = EditorGUILayout.BeginVertical();
        EditorGUI.ProgressBar(r, HPfraction, "Health : " + myStats.Health + " / " + myStats.Maxhealth);
        GUILayout.Space(16);
        EditorGUILayout.EndVertical();

        if (myStats.MaxResourceAmount > 0)
        {
            ManaFraction = myStats.CurrentResourceAmount / myStats.MaxResourceAmount;
        }
        else
        {
            ManaFraction = 0;
        }

        Rect r2 = EditorGUILayout.BeginVertical();
        EditorGUI.ProgressBar(r2, ManaFraction, "Resources : " + myStats.CurrentResourceAmount + " / " + myStats.MaxResourceAmount);
        GUILayout.Space(16);
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Health/sec :", GUILayout.MaxWidth(128));
        myStats.HealthRegenPerTick = EditorGUILayout.FloatField(myStats.HealthRegenPerTick, GUILayout.MaxWidth(64));
        EditorGUILayout.LabelField("Resources/sec :", GUILayout.MaxWidth(128));
        myStats.ResourceRegenPerTick = EditorGUILayout.FloatField(myStats.ResourceRegenPerTick, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Speed");
        myStats.Speed = EditorGUILayout.FloatField(myStats.Speed, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Power");
        myStats.Power = EditorGUILayout.FloatField(myStats.Power, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Hit Chance");
        myStats.HitChance = EditorGUILayout.FloatField(myStats.HitChance, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Multipliers", MessageType.None);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Health Multiplier");
        myStats.HealthMultiplier = EditorGUILayout.FloatField(myStats.HealthMultiplier, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Speed Multiplier");
        myStats.SpeedMultiplier = EditorGUILayout.FloatField(myStats.SpeedMultiplier, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Power Multiplier");
        myStats.PowerMultiplier = EditorGUILayout.FloatField(myStats.PowerMultiplier, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("EXP Multiplier");
        myStats.EXPMultiplier = EditorGUILayout.FloatField(myStats.EXPMultiplier, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Wallet", MessageType.None);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Gold");
        myStats.Gold = EditorGUILayout.FloatField(myStats.Gold);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("On Death", MessageType.None);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Gold Dropped");
        myStats.GoldOnKill = EditorGUILayout.FloatField(myStats.GoldOnKill);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("EXP granted");
        myStats.ExperienceOnKill = EditorGUILayout.FloatField(myStats.ExperienceOnKill);
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("ScoreBoard", MessageType.None);
        Scoreboard = EditorGUILayout.Foldout(Scoreboard, "Show");
        if (Scoreboard)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Total Damage Taken");
            EditorGUILayout.FloatField(myStats.Damage_Taken);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Total Damage Dealt");
            EditorGUILayout.FloatField(myStats.Damage_Dealt);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Total Healing Done");
            EditorGUILayout.FloatField(myStats.Healing_Done);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Total Healing Taken");
            EditorGUILayout.FloatField(myStats.Healing_Taken);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Kills");
            EditorGUILayout.IntField(myStats.Kills);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Deaths");
            EditorGUILayout.IntField(myStats.Deaths);
            EditorGUILayout.EndHorizontal();
        }


        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("");
        EditorGUILayout.EndHorizontal();
        //   GUILayout.MaxWidth(64)


        //base.OnInspectorGUI();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(myStats);
        }
    }
}
