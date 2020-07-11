using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CreateNewProjectile))]
public class ProjectileEditor : Editor
{

    public override void OnInspectorGUI()
    {
        CreateNewProjectile myProjectile = (CreateNewProjectile)target;

        EditorGUILayout.HelpBox("Basic Info", MessageType.None);
        myProjectile.AttackID = EditorGUILayout.TextField("Attack ID", myProjectile.AttackID);
        myProjectile.AttackName = EditorGUILayout.TextField("Attack Name", myProjectile.AttackName);
        myProjectile.ToolTip = EditorGUILayout.TextField("Tool Tip", myProjectile.ToolTip);
        myProjectile.Icon = (Sprite)EditorGUILayout.ObjectField("GUI Sprite", myProjectile.Icon, typeof(Sprite), false);

        EditorGUILayout.Space();
        myProjectile.Target = (GameObject)EditorGUILayout.ObjectField("Target Object", myProjectile.Target, typeof(GameObject), false);
        myProjectile.Source = (GameObject)EditorGUILayout.ObjectField("Source Object", myProjectile.Source, typeof(GameObject), false);

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Resource Consumption", MessageType.None);
        EditorGUILayout.BeginHorizontal();
        myProjectile.ResourceType = (CreateNewResource)EditorGUILayout.ObjectField(myProjectile.ResourceType, typeof(CreateNewResource), false, GUILayout.MaxWidth(128));
        if (myProjectile.ResourceType)
        {
            Color ResourceColor = myProjectile.ResourceType.ResourceBarColor;
            EditorGUILayout.ColorField(ResourceColor);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Resource Cost (as decimal %)");
        myProjectile.ResourceCost = EditorGUILayout.FloatField(myProjectile.ResourceCost, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Damage info", MessageType.None);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Damage Type");
        myProjectile.DamageType = (CreateNewDamageType)EditorGUILayout.ObjectField(myProjectile.DamageType, typeof(CreateNewDamageType), false, GUILayout.MaxWidth(256));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Damage Amount");
        myProjectile.Damage = EditorGUILayout.FloatField(myProjectile.Damage, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Is Heal?");
        myProjectile.IsHeal = EditorGUILayout.Toggle(myProjectile.IsHeal, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Maximum Range");
        myProjectile.MaxRange = EditorGUILayout.IntField(myProjectile.MaxRange, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Cast Time");
        myProjectile.CastTime = EditorGUILayout.FloatField(myProjectile.CastTime, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("CoolDown");
        myProjectile.CoolDown = EditorGUILayout.FloatField(myProjectile.CoolDown, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("CD Remaining");
        myProjectile.CoolDownRemaining = EditorGUILayout.FloatField(myProjectile.CoolDownRemaining, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Ready to cast");
        myProjectile.ReadyToCast = EditorGUILayout.Toggle(myProjectile.ReadyToCast, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Apply To Hostile Targets");
        myProjectile.HostileOnly = EditorGUILayout.Toggle(myProjectile.HostileOnly, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Apply To Friendly Target");
        myProjectile.FriendlyOnly = EditorGUILayout.Toggle(myProjectile.FriendlyOnly, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Aplly Buff/Debuffs on hit", MessageType.None);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Apply an Effect?");
        myProjectile.ApplyEffect = EditorGUILayout.Toggle(myProjectile.ApplyEffect, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        if (myProjectile.ApplyEffect)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Buff/Debuff to Apply");
            myProjectile.SecondaryEffect = (CreateNewBuff)EditorGUILayout.ObjectField(myProjectile.SecondaryEffect, typeof(CreateNewBuff), false, GUILayout.MaxWidth(256));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("DOT/HOT to Apply");
            myProjectile.SecondaryEffectDOT = (CreateNewDOT)EditorGUILayout.ObjectField(myProjectile.SecondaryEffectDOT, typeof(CreateNewDOT), false, GUILayout.MaxWidth(256));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Attack");
            myProjectile.SecondaryEffectAttack = (CreatNewDirectAttack)EditorGUILayout.ObjectField(myProjectile.SecondaryEffectAttack, typeof(CreatNewDirectAttack), false, GUILayout.MaxWidth(256));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Projectile");
            myProjectile.SecondaryEffectProjectile = (CreateNewProjectile)EditorGUILayout.ObjectField(myProjectile.SecondaryEffectProjectile, typeof(CreateNewProjectile), false, GUILayout.MaxWidth(256));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.HelpBox("Apply Effect to...", MessageType.None);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Self");
            myProjectile.Self = EditorGUILayout.Toggle(myProjectile.Self, GUILayout.MaxWidth(64));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Target");
            myProjectile.EffectTarget = EditorGUILayout.Toggle(myProjectile.EffectTarget, GUILayout.MaxWidth(64));
            EditorGUILayout.EndHorizontal();

        }

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Spline Info", MessageType.None);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Spline Profile");
        myProjectile.Path = (CreateSplineProfile)EditorGUILayout.ObjectField(myProjectile.Path, typeof(CreateSplineProfile), false, GUILayout.MaxWidth(256));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Travel Time");
        myProjectile.TravelTime = EditorGUILayout.FloatField(myProjectile.TravelTime, GUILayout.MaxWidth(256));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Travel Behavior");
        myProjectile.TravelBehavior = EditorGUILayout.CurveField(myProjectile.TravelBehavior, GUILayout.MaxWidth(256));
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Moving Particle Prefab", MessageType.None);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Use Primary Particles?");
        myProjectile.UseParticle = EditorGUILayout.Toggle(myProjectile.UseParticle, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        if (myProjectile.UseParticle)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Particle Prefab");
            myProjectile.Particle = (GameObject)EditorGUILayout.ObjectField(myProjectile.Particle, typeof(GameObject), false, GUILayout.MaxWidth(256));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Particle Duration");
            myProjectile.ParticleDuration = EditorGUILayout.IntField(myProjectile.ParticleDuration, GUILayout.MaxWidth(256));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.HelpBox("Secondary Particle Prefab", MessageType.None);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Use On Hit Particles?");
            myProjectile.UseSecondaryParticle = EditorGUILayout.Toggle(myProjectile.UseSecondaryParticle, GUILayout.MaxWidth(64));
            EditorGUILayout.EndHorizontal();

            if (myProjectile.UseSecondaryParticle)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Particle Prefab");
                myProjectile.Secondary_Particle = (GameObject)EditorGUILayout.ObjectField(myProjectile.Secondary_Particle, typeof(GameObject), false, GUILayout.MaxWidth(256));
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Particle Duration");
                myProjectile.SecondaryParticleDuration = EditorGUILayout.IntField(myProjectile.SecondaryParticleDuration, GUILayout.MaxWidth(256));
                EditorGUILayout.EndHorizontal();

            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Sounds", MessageType.None);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Cast Sound");
        myProjectile.Cast = (AudioClip)EditorGUILayout.ObjectField(myProjectile.Cast, typeof(AudioClip), false, GUILayout.MaxWidth(256));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Land Sound");
        myProjectile.Land = (AudioClip)EditorGUILayout.ObjectField(myProjectile.Land, typeof(AudioClip), false, GUILayout.MaxWidth(256));
        EditorGUILayout.EndHorizontal();


        // base.OnInspectorGUI();
        if (GUI.changed)
        {
            EditorUtility.SetDirty(myProjectile);
        }

    }

}
