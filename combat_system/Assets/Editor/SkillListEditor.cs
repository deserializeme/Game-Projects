using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(SkillList))]
public class SkillListEditor : Editor
{
    float End;
    float Current;
    float fraction;

    public override void OnInspectorGUI()
    {
        SkillList mySkills = (SkillList)target;

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Global Cool Down", MessageType.None);
        EditorGUILayout.Space();

        if (mySkills.GCD)
        {
            fraction = mySkills.GCD_Remaining / mySkills.GlobalCD_Length;
        }
        else
        {
            fraction = 0;
        }

        Rect r = EditorGUILayout.BeginVertical();
        EditorGUI.ProgressBar(r, fraction, "Gloabl Cooldown " + mySkills.GCD_Remaining);
        GUILayout.Space(16);
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space();
        mySkills.GlobalCD_Length = EditorGUILayout.FloatField("GCD Length", mySkills.GlobalCD_Length);

        EditorGUILayout.HelpBox("Skills", MessageType.None);
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.HelpBox("Name", MessageType.None);
        EditorGUILayout.HelpBox("Object", MessageType.None);
        EditorGUILayout.HelpBox("Off CD", MessageType.None);
        EditorGUILayout.HelpBox("CD End", MessageType.None);
        EditorGUILayout.EndHorizontal();

        for (int i = 0; i < mySkills.Buffs.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(mySkills.Buffs[i].BuffName, GUILayout.MaxWidth(128));
            EditorGUILayout.ObjectField(mySkills.Buffs[i], typeof(CreateNewBuff), false, GUILayout.MaxWidth(128));
            EditorGUILayout.Toggle(mySkills.Buffs[i].ReadyToCast, GUILayout.MaxWidth(128));
            EditorGUILayout.FloatField(mySkills.Buffs[i].CoolDownRemaining, GUILayout.MaxWidth(128));
            EditorGUILayout.EndHorizontal();
        }

        for (int i = 0; i < mySkills.DOTs.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(mySkills.DOTs[i].DOTName, GUILayout.MaxWidth(128));
            EditorGUILayout.ObjectField(mySkills.DOTs[i], typeof(CreateNewDOT), false, GUILayout.MaxWidth(128));
            EditorGUILayout.Toggle(mySkills.DOTs[i].ReadyToCast, GUILayout.MaxWidth(128));
            EditorGUILayout.FloatField(mySkills.DOTs[i].CoolDownRemaining, GUILayout.MaxWidth(128));
            EditorGUILayout.EndHorizontal();
        }

        for (int i = 0; i < mySkills.DAttack.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(mySkills.DAttack[i].AttackName, GUILayout.MaxWidth(128));
            EditorGUILayout.ObjectField(mySkills.DAttack[i], typeof(CreatNewDirectAttack), false, GUILayout.MaxWidth(128));
            EditorGUILayout.Toggle(mySkills.DAttack[i].ReadyToCast, GUILayout.MaxWidth(128));
            EditorGUILayout.FloatField(mySkills.DAttack[i].CoolDownRemaining, GUILayout.MaxWidth(128));
            EditorGUILayout.EndHorizontal();
        }

        for (int i = 0; i < mySkills.Shields.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(mySkills.Shields[i].BuffName, GUILayout.MaxWidth(128));
            EditorGUILayout.ObjectField(mySkills.Shields[i], typeof(CreateNewShield), false, GUILayout.MaxWidth(128));
            EditorGUILayout.Toggle(mySkills.Shields[i].ReadyToCast, GUILayout.MaxWidth(128));
            EditorGUILayout.FloatField(mySkills.Shields[i].CoolDownRemaining, GUILayout.MaxWidth(128));
            EditorGUILayout.EndHorizontal();
        }

        for (int i = 0; i < mySkills.Projectiles.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(mySkills.Projectiles[i].AttackName, GUILayout.MaxWidth(128));
            EditorGUILayout.ObjectField(mySkills.Projectiles[i], typeof(CreateNewProjectile), false, GUILayout.MaxWidth(128));
            EditorGUILayout.Toggle(mySkills.Projectiles[i].ReadyToCast, GUILayout.MaxWidth(128));
            EditorGUILayout.FloatField(mySkills.Projectiles[i].CoolDownRemaining, GUILayout.MaxWidth(128));
            EditorGUILayout.EndHorizontal();
        }

        EditorUtility.SetDirty(target);

    }
}
