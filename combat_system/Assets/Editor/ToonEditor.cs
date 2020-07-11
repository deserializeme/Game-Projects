using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CreateNewCharacter))]
public class ToonEditor : Editor {

    public override void OnInspectorGUI()
    {
        CreateNewCharacter myCreateNewCharacter = (CreateNewCharacter)target;

        EditorGUILayout.HelpBox("Basic Info", MessageType.None);
        myCreateNewCharacter.ToonID = EditorGUILayout.TextField("Toon ID", myCreateNewCharacter.ToonID);
        myCreateNewCharacter.Toon_Name = EditorGUILayout.TextField("Toon Name", myCreateNewCharacter.Toon_Name);
        myCreateNewCharacter.Toon_Description = EditorGUILayout.TextField("Toon Description", myCreateNewCharacter.Toon_Description);

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Evolution Info", MessageType.None);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Previous Evolution");
        myCreateNewCharacter.PreviousEvolution = (CreateNewCharacter)EditorGUILayout.ObjectField(myCreateNewCharacter.PreviousEvolution, typeof(CreateNewCharacter), false);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Next Evolution");
        myCreateNewCharacter.NextEvolution = (CreateNewCharacter)EditorGUILayout.ObjectField(myCreateNewCharacter.NextEvolution, typeof(CreateNewCharacter), false);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Next Evolution Level");
        myCreateNewCharacter.EvolutionLevel = EditorGUILayout.IntField(myCreateNewCharacter.EvolutionLevel, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();



        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Elemental Type", MessageType.None);
        myCreateNewCharacter.Type = (CreateNewDamageType)EditorGUILayout.ObjectField("Type", myCreateNewCharacter.Type, typeof(CreateNewDamageType), false);

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Resource Type/Color", MessageType.None);
        EditorGUILayout.BeginHorizontal();
        myCreateNewCharacter.ResourceType = (CreateNewResource)EditorGUILayout.ObjectField(myCreateNewCharacter.ResourceType, typeof(CreateNewResource), false);
        if (myCreateNewCharacter.ResourceType)
        {
            Color ResourceColor = myCreateNewCharacter.ResourceType.ResourceBarColor;
            EditorGUILayout.ColorField(ResourceColor);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Base Resource Amount");
        myCreateNewCharacter.Base_Resource_Amount = EditorGUILayout.FloatField(myCreateNewCharacter.Base_Resource_Amount, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();



        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Appearance", MessageType.None);
        myCreateNewCharacter.Icon = (Sprite)EditorGUILayout.ObjectField("Toon GUI Icon", myCreateNewCharacter.Icon, typeof(Sprite), false);
        myCreateNewCharacter.Model = (GameObject)EditorGUILayout.ObjectField("Toon Model/Prefab", myCreateNewCharacter.Model, typeof(GameObject), false);

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Stats", MessageType.None);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Starting Level");
        myCreateNewCharacter.Level = EditorGUILayout.IntField(myCreateNewCharacter.Level, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Base Health");
        myCreateNewCharacter.Base_Health = EditorGUILayout.FloatField(myCreateNewCharacter.Base_Health, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Base Speed");
        myCreateNewCharacter.Base_Speed = EditorGUILayout.FloatField(myCreateNewCharacter.Base_Speed, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Base Power");
        myCreateNewCharacter.Base_Power = EditorGUILayout.FloatField(myCreateNewCharacter.Base_Power, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Base Accuracy");
        myCreateNewCharacter.Base_Accuracy = EditorGUILayout.FloatField(myCreateNewCharacter.Base_Accuracy, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Base Resource Regen Per Tick");
        myCreateNewCharacter.Base_Resource_Regen = EditorGUILayout.FloatField(myCreateNewCharacter.Base_Resource_Regen, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Base Health Regen Per Tick");
        myCreateNewCharacter.Base_Health_Regen = EditorGUILayout.FloatField(myCreateNewCharacter.Base_Health_Regen, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Experience granted upon death");
        myCreateNewCharacter.Experience = EditorGUILayout.FloatField(myCreateNewCharacter.Experience, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Resources granted upon death");
        myCreateNewCharacter.Gold = EditorGUILayout.FloatField(myCreateNewCharacter.Gold, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Sounds", MessageType.None);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Select");
        myCreateNewCharacter.Select = (AudioClip)EditorGUILayout.ObjectField(myCreateNewCharacter.Select, typeof(AudioClip), false, GUILayout.MaxWidth(256));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Attack");
        myCreateNewCharacter.Attack = (AudioClip)EditorGUILayout.ObjectField(myCreateNewCharacter.Attack, typeof(AudioClip), false, GUILayout.MaxWidth(256));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Error");
        myCreateNewCharacter.Error = (AudioClip)EditorGUILayout.ObjectField(myCreateNewCharacter.Error, typeof(AudioClip), false, GUILayout.MaxWidth(256));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Death");
        myCreateNewCharacter.Death = (AudioClip)EditorGUILayout.ObjectField(myCreateNewCharacter.Death, typeof(AudioClip), false, GUILayout.MaxWidth(256));
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.HelpBox("Base Moves", MessageType.None);
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.HelpBox("Buffs and Debuffs", MessageType.None);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Buff/Debuff"))
        {
            myCreateNewCharacter.Buffs.Add(null);
        }
        if (GUILayout.Button("Delete Buff/Debuff"))
        {
            myCreateNewCharacter.Buffs.RemoveAt(myCreateNewCharacter.Buffs.Count - 1);
        }
        EditorGUILayout.EndHorizontal();

        for (int i = 0; i < myCreateNewCharacter.Buffs.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("# " + i);
            myCreateNewCharacter.Buffs[i] = (CreateNewBuff)EditorGUILayout.ObjectField(myCreateNewCharacter.Buffs[i], typeof(CreateNewBuff), false, GUILayout.MaxWidth(256));
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.HelpBox("DOTs and HOTs", MessageType.None);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add DOT/HOT"))
        {
            myCreateNewCharacter.DOTs.Add(null);
        }
        if (GUILayout.Button("Delete DOT/HOT"))
        {
            myCreateNewCharacter.DOTs.RemoveAt(myCreateNewCharacter.DOTs.Count - 1);
        }
        EditorGUILayout.EndHorizontal();

        for (int i = 0; i < myCreateNewCharacter.DOTs.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("# " + i);
            myCreateNewCharacter.DOTs[i] = (CreateNewDOT)EditorGUILayout.ObjectField(myCreateNewCharacter.DOTs[i], typeof(CreateNewDOT), false, GUILayout.MaxWidth(256));
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.HelpBox("Direct Attacks and Heals", MessageType.None);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Direct Attacks and Heals"))
        {
            myCreateNewCharacter.DAttack.Add(null);
        }
        if (GUILayout.Button("Delete Direct Attacks and Heals"))
        {
            myCreateNewCharacter.DAttack.RemoveAt(myCreateNewCharacter.DAttack.Count - 1);
        }
        EditorGUILayout.EndHorizontal();

        for (int i = 0; i < myCreateNewCharacter.DAttack.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("# " + i);
            myCreateNewCharacter.DAttack[i] = (CreatNewDirectAttack)EditorGUILayout.ObjectField(myCreateNewCharacter.DAttack[i], typeof(CreatNewDirectAttack), false, GUILayout.MaxWidth(256));
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.HelpBox("Shields", MessageType.None);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Shield"))
        {
            myCreateNewCharacter.Shields.Add(null);
        }
        if (GUILayout.Button("Delete Shield"))
        {
            myCreateNewCharacter.Shields.RemoveAt(myCreateNewCharacter.Shields.Count - 1);
        }
        EditorGUILayout.EndHorizontal();

        for (int i = 0; i < myCreateNewCharacter.Shields.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("# " + i);
            myCreateNewCharacter.Shields[i] = (CreateNewShield)EditorGUILayout.ObjectField(myCreateNewCharacter.Shields[i], typeof(CreateNewShield), false, GUILayout.MaxWidth(256));
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.HelpBox("Projectiles", MessageType.None);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Projectile"))
        {
            myCreateNewCharacter.Projectiles.Add(null);
        }
        if (GUILayout.Button("Delete Projectile"))
        {
            myCreateNewCharacter.Projectiles.RemoveAt(myCreateNewCharacter.Projectiles.Count - 1);
        }
        EditorGUILayout.EndHorizontal();

        for (int i = 0; i < myCreateNewCharacter.Projectiles.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("# " + i);
            myCreateNewCharacter.Projectiles[i] = (CreateNewProjectile)EditorGUILayout.ObjectField(myCreateNewCharacter.Projectiles[i], typeof(CreateNewProjectile), false, GUILayout.MaxWidth(256));
            EditorGUILayout.EndHorizontal();
        }

        //base.OnInspectorGUI();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(myCreateNewCharacter);
        }
    }

}
