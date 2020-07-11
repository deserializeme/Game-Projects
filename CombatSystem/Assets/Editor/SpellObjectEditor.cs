using UnityEngine;
using System.Collections;
using UnityEditor;


[CustomEditor(typeof(SpellObject))]
public class SpellObjectEditor : Editor {

    // public bools to use with the GUI foldouts
    #region FoldOut Bools
    public bool ID;
    public bool StackableType;
    public bool OverTimeType;
    public bool DirectType;
    public bool Stacks;
    public bool UseOnWho;
    public bool Range;
    public bool CastTime;
    public bool UseParticles;
    public bool Resources;
    public bool OTBehavior;
    public bool Multipliers;
    public bool Projectile;
    public bool Shield;
    public bool SecondEffect;
    public bool Audio;
    public bool Monitor;
    public bool Require;
    public bool AOE;
    public bool Dispell;
    #endregion

    public override void OnInspectorGUI()
    {
        StackableType = false;
        SpellObject S = (SpellObject)target;

        S.SType = (SpellObject.SpellType)EditorGUILayout.EnumPopup("Spell Type", S.SType);

        //categoy used for dispelling
        S.SCategory = (SpellObject.Category)EditorGUILayout.EnumPopup("Spell Category", S.SCategory);

        //spell identifiers, Icon, Name
        Identity();

        // who does the spell effect? (team affiliations)
        WhoDoes();

        // min and max range
        RangeInfo();

        // cast times and cool downs
        Casting();

        // how much of which resource does it cost
        Resource();

        // shield information (health absorb types, break effects)
        Shields();

        // how much damage does it do, is it a DOT, how does it behave over time
        DamageInfo();

        //can it stack, and how high
        StackingInfo();

        // buff debuff multipliers, how they alter a players stats
        // if you need to add a new multiplyer simply add another enum to Attributes.StatName
        // and the editor will automatically populate it
        Multis();

        // abilities that remove harmfull effects from slef and or others
        Dispells();

        // area of effect behaviors, radius size, and overlap object type
        AOEInfo();

        // attacks that use the spline system to travel to the target and their behavior
        Projectiles();

        // particle effect prefabs and their durations
        Particles();

        // does this ability apply secondary effects and what are they
        SecEffect();

        // requirements to be met before ability can be used, and does that grant a bonus
        Depend();

        // audio clips specific to this spell
        AudioInfo();

        // helpful information for debugging and testing
        Debugging();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(S);
        }
        //base.OnInspectorGUI();
    }

    //spell identifiers, Icon, Name
    public void Identity()
    {
        SpellObject S = (SpellObject)target;

        #region Identification data
        ID = EditorGUILayout.Foldout(ID, "Identification Info");
        if (ID)
        {
            S.SpellID = EditorGUILayout.TextField("Spell ID", S.SpellID);
            S.SpellName = EditorGUILayout.TextField("Spell Name", S.SpellName);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("UI Icon");
            S.Icon = (Sprite)EditorGUILayout.ObjectField(S.Icon, typeof(Sprite), true);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.HelpBox("ToolTip", MessageType.None);
            S.ToolTip = EditorGUILayout.TextArea(S.ToolTip);
        }
        #endregion
    }

    // who does the spell effect? (team affiliations)
    public void WhoDoes()
    {
        SpellObject S = (SpellObject)target;

        #region Who can we use the spell on?
        UseOnWho = EditorGUILayout.Foldout(UseOnWho, "Who Does Spell effect?");
        if (UseOnWho)
        {
            S.Hostile = EditorGUILayout.Toggle("Effect Hostile Targets", S.Hostile);
            S.Neutral = EditorGUILayout.Toggle("Effect Neutral Targets", S.Neutral);
            S.Friendly = EditorGUILayout.Toggle("Effect Friendly Targets", S.Friendly);
        }
        #endregion

    }

    // min and max range
    public void RangeInfo()
    {
        SpellObject S = (SpellObject)target;

        #region Range info
        Range = EditorGUILayout.Foldout(Range, "Range Information");
        if (Range)
        {
            S.MaxRange = EditorGUILayout.IntField("Maximum Range", S.MaxRange);
            S.MinRange = EditorGUILayout.IntField("Minimum Range", S.MinRange);
        }
        #endregion
    }

    // cast times and cool downs
    public void Casting()
    {
        SpellObject S = (SpellObject)target;
        #region Cast Time info
        CastTime = EditorGUILayout.Foldout(CastTime, "Cast Time Information");
        if (CastTime)
        {
            S.CastTime = EditorGUILayout.FloatField("Cast Time", S.CastTime);
            S.CoolDown = EditorGUILayout.FloatField("Cool Down Length", S.CoolDown);
            S.Channel = EditorGUILayout.Toggle("Channeled Ability", S.Channel);
            S.CastWhileMoving = EditorGUILayout.Toggle("Castable While Moving", S.CastWhileMoving);

            EditorGUILayout.HelpBox("Monitoring", MessageType.None);
            float cd_remain = EditorGUILayout.FloatField("Cool Down Remaining", S.CD_Remaining);
            bool ready = EditorGUILayout.Toggle("Ready To Cast", S.Ready);
        }
        #endregion

    }

    // how much of which resource does it cost
    public void Resource()
    {
        SpellObject S = (SpellObject)target;

        #region Resource Info
        Resources = EditorGUILayout.Foldout(Resources, "Resource Consumption");
        if (Resources)
        {
            S.Resource_Type = (CreateNewResource)EditorGUILayout.ObjectField("Type", S.Resource_Type, typeof(CreateNewResource), true);
            S.Resource_Cost = EditorGUILayout.FloatField("Cost", S.Resource_Cost);
        }
        #endregion
    }

    // shield information (health absorb types, break effects)
    public void Shields()
    {
        SpellObject S = (SpellObject)target;
        #region Shield information
        if (S.SType == SpellObject.SpellType.Shield)
        {
            Shield = EditorGUILayout.Foldout(Shield, "Shield Settings");
            if (Shield)
            {
                S.Shield_MaxHealth = EditorGUILayout.FloatField("Shield Absorb Amount", S.Shield_MaxHealth);
                S.Shield_Duration = EditorGUILayout.FloatField("Shield Duration", S.Shield_Duration);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Absorb specific damage types?");
                S.Specific_Type = EditorGUILayout.Toggle(S.Specific_Type, GUILayout.MaxWidth(32));
                EditorGUILayout.EndHorizontal();

                if (S.Specific_Type)
                {
                    if (S.ShieldType.Count == 0)
                    {
                        S.ShieldType.Add(null);
                    }
                    else
                    {
                        for (int i = 0; i < S.ShieldType.Count; i++)
                        {
                            S.ShieldType[i] = (CreateNewDamageType)EditorGUILayout.ObjectField("Damage Type :", S.ShieldType[i], typeof(CreateNewDamageType), true);
                        }
                    }

                    if (GUILayout.Button("Add Damage Type"))
                    {
                        S.ShieldType.Add(null);
                    }

                    if (GUILayout.Button("Remove Damage Type") && S.ShieldType.Count > 1)
                    {
                        int index = S.ShieldType.Count - 1;
                        S.ShieldType.RemoveAt(index);
                    }
                }

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Trigger effect upon breaking??");
                S.SpellOnBreak = EditorGUILayout.Toggle(S.SpellOnBreak, GUILayout.MaxWidth(32));
                EditorGUILayout.EndHorizontal();

                if (S.SpellOnBreak)
                {
                    if (S.BreakEffects.Count == 0)
                    {
                        SpellObject.BreakEffect effect = new SpellObject.BreakEffect();
                        S.BreakEffects.Add(effect);
                    }
                    else
                    {
                        for (int i = 0; i < S.BreakEffects.Count; i++)
                        {
                            string text;

                            EditorGUILayout.HelpBox("Break Effect " + i, MessageType.None);
                            S.BreakEffects[i].BreakSpell = (SpellObject)EditorGUILayout.ObjectField("Break Effect :", S.BreakEffects[i].BreakSpell, typeof(SpellObject), true);

                            if (S.BreakEffects[i].CastOnSelf)
                            {
                                text = "Cast Effect on Self";
                            }
                            else
                            {
                                text = "Cast Effect on Breaker";
                            }
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField(text, GUILayout.MaxWidth(128));
                            S.BreakEffects[i].CastOnSelf = EditorGUILayout.Toggle(S.BreakEffects[i].CastOnSelf, GUILayout.MaxWidth(32));
                            EditorGUILayout.EndHorizontal();
                        }

                        if (GUILayout.Button("Add Effect"))
                        {
                            SpellObject.BreakEffect effect = new SpellObject.BreakEffect();
                            S.BreakEffects.Add(effect);
                        }

                        if (GUILayout.Button("Remove Effect") && S.BreakEffects.Count > 1)
                        {
                            int index = S.BreakEffects.Count - 1;
                            S.BreakEffects.RemoveAt(index);
                        }
                    }
                }


            }
        }



        #endregion

    }

    // abilities that remove harmfull effects from slef and or others
    public void Dispells()
    {
        SpellObject S = (SpellObject)target;

        #region Dispell info
        if(S.SType == SpellObject.SpellType.Dispell)
        {
            Dispell = EditorGUILayout.Foldout(Dispell, "Dispell Options");
            if (Dispell)
            {
                S.RemoveAll = EditorGUILayout.Toggle("Remove All Dispellable Effects", S.RemoveAll);
                if (!S.RemoveAll)
                {
                    S.NumToRemove = EditorGUILayout.IntField("Max Num of effect to Remove", S.NumToRemove);
                }

                if (S.RemovableTypes.Count == 0)
                {
                    S.RemovableTypes.Add(SpellObject.Category.None);
                }

                EditorGUILayout.HelpBox("Removable Types", MessageType.None);
                for (int i = 0; i < S.RemovableTypes.Count; i++)
                {
                    S.RemovableTypes[i] = (SpellObject.Category)EditorGUILayout.EnumPopup("Removable Type", S.RemovableTypes[i]);
                }

                if (GUILayout.Button("Add Removable Type"))
                {
                    S.RemovableTypes.Add(SpellObject.Category.None);
                }

                if (GUILayout.Button("Delete Removable Type") && S.RemovableTypes.Count > 1)
                {
                    S.RemovableTypes.RemoveAt(S.RemovableTypes.Count - 1);
                }
            }
        }
        #endregion 
    }

    // how much damage does it do, is it a DOT, how does it behave over time
    public void DamageInfo()
    {
        SpellObject S = (SpellObject)target;

        #region /DOT/HOT/Buff/Debuff over time behaviors
        if (S.SType == SpellObject.SpellType.Buff ||
            S.SType == SpellObject.SpellType.Debuff ||
            S.SType == SpellObject.SpellType.HOT ||
            S.SType == SpellObject.SpellType.DOT)
        {
            OverTimeType = true;
        }
        else
        {
            OverTimeType = false;
        }

        if (S.isAOE && S.AOEstyle != SpellObject.AOEBehavior.OneShot)
        {
            OverTimeType = true;
        }

        if (S.SType == SpellObject.SpellType.DAttack ||
            S.SType == SpellObject.SpellType.DHeal ||
            S.SType == SpellObject.SpellType.PAttack ||
            S.SType == SpellObject.SpellType.PHeal)
        {
            DirectType = true;
        }
        else
        {
            DirectType = false;
        }



        if (OverTimeType)
        {
            OTBehavior = EditorGUILayout.Foldout(OTBehavior, "Damage Information");

            if (OTBehavior)
            {
                S.DamageType = (CreateNewDamageType)EditorGUILayout.ObjectField("Damage Type", S.DamageType, typeof(CreateNewDamageType), true);
                S.Behavior = (SpellObject.OverTimeBehavior)EditorGUILayout.EnumPopup("Behavior Model", S.Behavior);

                S.UnlimitedDuration = EditorGUILayout.Toggle("Unlimited Duration", S.UnlimitedDuration);
                if (!S.UnlimitedDuration)
                {
                    S.Duration = EditorGUILayout.FloatField("Duration", S.Duration);
                    S.Ticks = EditorGUILayout.IntField("Ticks", S.Ticks);

                    if (S.SType == SpellObject.SpellType.DOT || S.SType == SpellObject.SpellType.HOT)
                    {
                        S.ToatalDamage = EditorGUILayout.FloatField("Total Damage", S.ToatalDamage);
                    }
                }
                else
                {
                    if (S.SType == SpellObject.SpellType.DOT || S.SType == SpellObject.SpellType.HOT)
                    {
                        S.ToatalDamage = EditorGUILayout.FloatField("Damage Per Tick", S.ToatalDamage);
                    }

                    S.UTickLength = EditorGUILayout.FloatField("Tick Interval", S.ToatalDamage);
                }


                //just some helpful calulations to see while setting up
                EditorGUILayout.HelpBox("Calculations", MessageType.None);
                if (!S.UnlimitedDuration)
                {
                    float Ticklength = EditorGUILayout.FloatField("Tick Interval", S.Duration / S.Ticks);

                    if (S.SType == SpellObject.SpellType.DOT || S.SType == SpellObject.SpellType.HOT)
                    {
                        float DPT = EditorGUILayout.FloatField("Damage Per Tick", S.ToatalDamage / S.Ticks);
                        float DPS = EditorGUILayout.FloatField("Damage Per Second", S.ToatalDamage / S.Duration);
                    }
                }
                else
                {
                    float Ticklength = EditorGUILayout.FloatField("Tick Interval", S.UTickLength);

                    if (S.SType == SpellObject.SpellType.DOT || S.SType == SpellObject.SpellType.HOT)
                    {
                        float DPT = EditorGUILayout.FloatField("Damage Per Tick", S.ToatalDamage);
                        float DPS = EditorGUILayout.FloatField("Damage Per Second", S.ToatalDamage / S.UTickLength);
                    }
                }

                EditorGUILayout.HelpBox("Monitoring", MessageType.None);
                bool applied = EditorGUILayout.Toggle("Is Applied", S.Applied);

                if (S.SType == SpellObject.SpellType.DOT || S.SType == SpellObject.SpellType.HOT)
                {
                    float currentDamage = EditorGUILayout.FloatField("Current Damage", S.CurrentDamage);
                }

                Timer timer = (Timer)EditorGUILayout.ObjectField("Timer", S.SpellTimer, typeof(Timer), true);
            }
        }

        if (DirectType)
        {
            OTBehavior = EditorGUILayout.Foldout(OTBehavior, "Damage Information");
            if (OTBehavior)
            {
                S.DamageType = (CreateNewDamageType)EditorGUILayout.ObjectField("Damage Type", S.DamageType, typeof(CreateNewDamageType), true);
                S.ToatalDamage = EditorGUILayout.FloatField("Total Damage", S.ToatalDamage);
            }
        }

        #endregion
    }

    //can it stack, and how high
    public void StackingInfo()
    {
        SpellObject S = (SpellObject)target;

        #region Stackable Data
        if (S.SType == SpellObject.SpellType.Buff ||
            S.SType == SpellObject.SpellType.Debuff ||
            S.SType == SpellObject.SpellType.HOT ||
            S.SType == SpellObject.SpellType.DOT ||
            S.SType == SpellObject.SpellType.Shield)
        {
            StackableType = true;
        }
        else
        {
            StackableType = false;
        }

        if (StackableType)
        {
            if (!S.Stackable)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Enable Stacking Behavior");
                S.Stackable = EditorGUILayout.Toggle(S.Stackable, GUILayout.MaxWidth(32));
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                Stacks = EditorGUILayout.Foldout(Stacks, "Stacking Behavior");
                if (Stacks)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Enable Stacking Behavior");
                    S.Stackable = EditorGUILayout.Toggle(S.Stackable, GUILayout.MaxWidth(32));
                    EditorGUILayout.EndHorizontal();

                    S.StackLimit = EditorGUILayout.IntField("Maximum Number of Stacks", S.StackLimit);
                    S.CurrentStacks = EditorGUILayout.IntField("Current Stacks", S.CurrentStacks);

                    //enforce the stack limit as 1 for stackable buff, otherwise they wouldnt do anything
                    if (S.StackLimit == 0)
                    {
                        S.StackLimit = 1;
                    }
                }
            }
        }
        #endregion
    }

    // buff debuff multipliers, how they alter a players stats
    // if you need to add a new multiplyer simply add another enum to Attributes.StatName
    // and the editor will automatically populate and serialize it
    public void Multis()
    {
        SpellObject S = (SpellObject)target;
        #region Multiplier Manipulation
        //count how many stats there are to use
        int count = 0;
        foreach (Attributes.StatName stat in System.Enum.GetValues(typeof(Attributes.StatName)))
        {
            count++;
        }


        //add the stats and values to the SpellObject and populate it
        if (S.Attributes.Count != count)
        {
            Debug.Log("S.attributes contains different amount of enums than Attributes class, re-creating");
            S.Attributes.Clear();
            int enums = 0;
            foreach (Attributes.StatName stat in System.Enum.GetValues(typeof(Attributes.StatName)))
            {
                Attributes a = new Attributes();
                a.Stat = stat;
                S.Attributes.Add(a);
                enums++;
            }
        }

        //now add the multiplier stat/value and populate it
        if (S.Multipliers.Count != count)
        {
            Debug.Log("S.Multipliers contains different amount of floats than Attributes class, re-creating");
            S.Multipliers.Clear();
            foreach (Attributes.StatName stat in System.Enum.GetValues(typeof(Attributes.StatName)))
            {
                Attributes a = new Attributes();
                S.Multipliers.Add(1);
            }
        }




        if (S.SType == SpellObject.SpellType.Buff || S.SType == SpellObject.SpellType.Debuff)
        {
            Multipliers = EditorGUILayout.Foldout(Multipliers, "Stat Multipliers");
            if (Multipliers)
            {
                for (int i = 0; i < S.Multipliers.Count; i++)
                {
                    S.Multipliers[i] = EditorGUILayout.FloatField(S.Attributes[i].Stat.ToString() + " Multiplier", S.Multipliers[i]);
                }

            }
        }
        #endregion
    }

    // area of effect behaviors, radius size, and overlap object type
    public void AOEInfo()
    {
        SpellObject S = (SpellObject)target;
        #region AOE options
        if (!S.isAOE)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Use Area of Effect options");
            S.isAOE = EditorGUILayout.Toggle(S.isAOE, GUILayout.MaxWidth(32));
            EditorGUILayout.EndHorizontal();
        }
        else
        {
            AOE = EditorGUILayout.Foldout(AOE, "Area of Effect Options");
            if (AOE)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Use Area of Effect options");
                S.isAOE = EditorGUILayout.Toggle(S.isAOE, GUILayout.MaxWidth(32));
                EditorGUILayout.EndHorizontal();

                S.AOEstyle = (SpellObject.AOEBehavior)EditorGUILayout.EnumPopup("AOE Behavior", S.AOEstyle);
                if (S.SType == SpellObject.SpellType.PAttack || S.SType == SpellObject.SpellType.PHeal)
                {
                    S.ActiveDuringTravel = EditorGUILayout.Toggle("Active during travel", S.ActiveDuringTravel);
                }

                S.Radius = EditorGUILayout.FloatField("AOE Radius", S.Radius);

                string Shape = " ";
                if (S.Shape)
                {
                    Shape = "Using Cube Collider";
                }
                else
                {
                    Shape = "Using Sphere Collider";
                }

                S.Shape = EditorGUILayout.Toggle(Shape, S.Shape);
                S.MaxEnemiesEffected = EditorGUILayout.IntField("Max # Targets Effected", S.MaxEnemiesEffected);

                #region Advanced AOE Options

                #endregion
            }
        }

        #endregion
    }

    // attacks that use the spline system to travel to the target and their behavior
    public void Projectiles()
    {
        SpellObject S = (SpellObject)target;
        #region Projectile Information
        if (S.SType == SpellObject.SpellType.PAttack || S.SType == SpellObject.SpellType.PHeal)
        {
            Projectile = EditorGUILayout.Foldout(Projectile, "Projectile Behavior");
            if (Projectile)
            {
                S.Path = (CreateSplineProfile)EditorGUILayout.ObjectField("Spline Profile", S.Path, typeof(CreateSplineProfile), true);
                S.TravelBehavior = EditorGUILayout.CurveField("Travel Behavior", S.TravelBehavior);
                S.TravelTime = EditorGUILayout.FloatField("Travel Time", S.TravelTime);
            }
        }

        #endregion
    }

    // particle effect prefabs and their durations
    public void Particles()
    {
        SpellObject S = (SpellObject)target;
        #region Particle information
        if (S.Particles.Count == 0)
        {
            SpellObject.Particle part = new SpellObject.Particle();
            S.Particles.Add(part);
        }


        if (!S.UseParticles)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Use Particles");
            S.UseParticles = EditorGUILayout.Toggle(S.UseParticles, GUILayout.MaxWidth(32));
            EditorGUILayout.EndHorizontal();
        }
        else
        {
            UseParticles = EditorGUILayout.Foldout(UseParticles, "Particle Settings");
            if (UseParticles)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Use Particles");
                S.UseParticles = EditorGUILayout.Toggle(S.UseParticles, GUILayout.MaxWidth(32));
                EditorGUILayout.EndHorizontal();
                string text;

                for (int i = 0; i < S.Particles.Count; i++)
                {
                    string message = "Particle System " + i;
                    EditorGUILayout.HelpBox(message, MessageType.None);
                    S.Particles[i].ParticlePrefab = (GameObject)EditorGUILayout.ObjectField("Particle Prefab", S.Particles[i].ParticlePrefab, typeof(GameObject), true);
                    S.Particles[i].ParticleDuration = EditorGUILayout.FloatField("Particle Duration", S.Particles[i].ParticleDuration);
                    EditorGUILayout.BeginHorizontal();
                    if (S.Particles[i].OnSelf)
                    {
                        text = "If True, Spawn on Self";
                    }
                    else
                    {
                        text = "If False, Spawn on Target";
                    }
                    EditorGUILayout.LabelField(text);
                    S.Particles[i].OnSelf = EditorGUILayout.Toggle(S.Particles[i].OnSelf, GUILayout.MaxWidth(32));
                    EditorGUILayout.EndHorizontal();

                }

                if (GUILayout.Button("Add Particle"))
                {
                    SpellObject.Particle part = new SpellObject.Particle();
                    S.Particles.Add(part);
                }

                if (GUILayout.Button("Remove Particle") && S.Particles.Count > 1)
                {
                    SpellObject.Particle part = S.Particles[S.Particles.Count - 1];
                    S.Particles.Remove(part);
                }
            }
        }
        #endregion
    }

    // does this ability apply secondary effects and what are they
    public void SecEffect()
    {
        SpellObject S = (SpellObject)target;
        #region Secondary Effects

        if (!S.ApplyEffect)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Apply Secondary Effects?");
            S.ApplyEffect = EditorGUILayout.Toggle(S.ApplyEffect, GUILayout.MaxWidth(32));
            EditorGUILayout.EndHorizontal();
        }
        else
        {
            SecondEffect = EditorGUILayout.Foldout(SecondEffect, "Secondary Effects");
            if (SecondEffect)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Apply Secondary Effects?");
                S.ApplyEffect = EditorGUILayout.Toggle(S.ApplyEffect, GUILayout.MaxWidth(32));
                EditorGUILayout.EndHorizontal();

                if (S.SecondarySpells.Count == 0)
                {
                    SpellObject.SecondarySpell n = new SpellObject.SecondarySpell();
                    S.SecondarySpells.Add(n);
                }
                for (int i = 0; i < S.SecondarySpells.Count; i++)
                {
                    string text;
                    if (S.SecondarySpells[i].CastOnSelf)
                    {
                        text = "Cast Second effect on self";
                    }
                    else
                    {
                        text = "Cast Second effect on target";
                    }

                    EditorGUILayout.HelpBox("Secondary Effect 1", MessageType.None);
                    S.SecondarySpells[i].SecondSpell = (SpellObject)EditorGUILayout.ObjectField("SpellObject :", S.SecondarySpells[i].SecondSpell, typeof(SpellObject), true);

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(text);
                    S.SecondarySpells[i].CastOnSelf = EditorGUILayout.Toggle(S.SecondarySpells[i].CastOnSelf, GUILayout.MaxWidth(32));
                    EditorGUILayout.EndHorizontal();
                }

                if (GUILayout.Button("Add Effect"))
                {
                    SpellObject.SecondarySpell n = new SpellObject.SecondarySpell();
                    S.SecondarySpells.Add(n);
                }

                if (GUILayout.Button("Remove Effect") && S.SecondarySpells.Count > 1)
                {
                    int index = S.SecondarySpells.Count - 1;
                    S.SecondarySpells.RemoveAt(index);
                }


                GameObject secondTarget = (GameObject)EditorGUILayout.ObjectField("Secondary Target", S.SecondaryTarget, typeof(GameObject), true);
            }
        }

        #endregion
    }

    // requirements to be met before ability can be used, and does that grant a bonus
    public void Depend()
    {
        SpellObject S = (SpellObject)target;
        #region Requirements

        if (!S.Req)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Enable Dependancies");
            S.Req = EditorGUILayout.Toggle(S.Req, GUILayout.MaxWidth(32));
            EditorGUILayout.EndHorizontal();

        }
        else
        {
            Require = EditorGUILayout.Foldout(Require, "Requirments/Dependencies");
            if (Require)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Enable Dependencies");
                S.Req = EditorGUILayout.Toggle(S.Req, GUILayout.MaxWidth(32));
                EditorGUILayout.EndHorizontal();

                if (S.Require.Count == 0)
                {
                    SpellObject.Requirements r = new SpellObject.Requirements();
                    S.Require.Add(r);
                }

                for (int i = 0; i < S.Require.Count; i++)
                {
                    EditorGUILayout.HelpBox("Dependency " + i, MessageType.None);
                    if (!S.Require[i].UseSpell)
                    {
                        S.Require[i].UseHP = EditorGUILayout.Toggle("Use HP % as Dependency", S.Require[i].UseHP);
                    }

                    if (!S.Require[i].UseHP)
                    {
                        S.Require[i].UseSpell = EditorGUILayout.Toggle("Use SpellObject as Dependency", S.Require[i].UseSpell);
                    }

                    if (S.Require[i].UseSpell)
                    {
                        S.Require[i].Spell = (SpellObject)EditorGUILayout.ObjectField("Spell Dependency", S.Require[i].Spell, typeof(SpellObject), true);
                    }

                    if (S.Require[i].UseHP)
                    {
                        S.Require[i].HP_Percent = EditorGUILayout.FloatField("HP % Dependency", S.Require[i].HP_Percent);
                    }

                    if (S.Require[i].UseSpell)
                    {
                        S.Require[i].ReqItems = EditorGUILayout.IntField("Number of Reqired Spell", S.Require[i].ReqItems);
                    }

                    S.Require[i].useBonus = EditorGUILayout.Toggle("Grant Bonus Effect", S.Require[i].useBonus);
                    if (S.Require[i].useBonus)
                    {
                        S.Require[i].Bonus = EditorGUILayout.FloatField("% Bonus Granted", S.Require[i].Bonus);
                    }
                }

                if (GUILayout.Button("Add Dependency"))
                {
                    SpellObject.Requirements r = new SpellObject.Requirements();
                    S.Require.Add(r);
                }

                if (GUILayout.Button("Remove Dependency") && S.Require.Count > 1)
                {
                    int index = S.Require.Count - 1;
                    S.Require.RemoveAt(index);
                }

            }
        }

        #endregion
    }

    // audio clips specific to this spell
    public void AudioInfo()
    {
        SpellObject S = (SpellObject)target;
        #region Audio Clips
        Audio = EditorGUILayout.Foldout(Audio, "Audio Clips");

        if (Audio)
        {
            int clips = 0;
            foreach (SpellObject.ClipName clip in System.Enum.GetValues(typeof(SpellObject.ClipName)))
            {
                SpellObject.SpellSound s = new SpellObject.SpellSound();
                s.SoundType = clip;
                S.SpellSounds.Add(s);
                clips++;
            }

            for (int i = 0; i < clips; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(S.SpellSounds[i].SoundType.ToString() + " Sound:");
                S.SpellSounds[i].Sound = (AudioClip)EditorGUILayout.ObjectField(S.SpellSounds[i].Sound, typeof(AudioClip), true);
                EditorGUILayout.EndHorizontal();
            }


        }

        #endregion
    }

    // helpful information for debugging and testing
    public void Debugging()
    {
        SpellObject S = (SpellObject)target;
        #region Monitoring Info
        Monitor = EditorGUILayout.Foldout(Monitor, "Debug info");
        if (Monitor)
        {
            GameObject Source = (GameObject)EditorGUILayout.ObjectField("Source", S.Source, typeof(GameObject), true);
            GameObject Target = (GameObject)EditorGUILayout.ObjectField("Target", S.Target, typeof(GameObject), true);
            Vector3 Begin = EditorGUILayout.Vector3Field("Begin", S.Begin);
            Vector3 End = EditorGUILayout.Vector3Field("End", S.End);
            float POC = EditorGUILayout.FloatField("Source's Power on Cast", S.Source_PowerOnCast);
        }

        #endregion
    }

}
