using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using System.Linq;
using UnityEditor.PackageManager.UI;

[CustomEditor(typeof(WorldTile))]
public class world_tile_editor : Editor
{
    public WorldTile my_tile;
    string enemy_weight;
    string level_weight;

    public override void OnInspectorGUI()
    {
        my_tile = (WorldTile)target;

        //initialize the sprite if non exists
        if(!my_tile.tileSprite)
        {
            DirectoryInfo dir = new DirectoryInfo("Assets/Resources/Sprites/Tile");
            FileInfo[] info = dir.GetFiles("*.png");
            my_tile.tileSprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Resources/Sprites/Tile/" + info[0].Name, typeof(Sprite));
        }

        //nasty regex to get the constants out of the world tile
        #region
        DirectoryInfo mydir = new DirectoryInfo("Assets/Scripts");
        FileInfo[] fi = mydir.GetFiles("*.cs");
        foreach (FileInfo f in fi)
        {
            if (f.Name == "WorldTile.cs")
            {
                string result = string.Empty;
                var lines = File.ReadAllLines(f.FullName);
                foreach (var line in lines)
                {
                    if (line.Contains("public const float ENEMY_WEIGHT ="))
                    {
                        enemy_weight = System.Text.RegularExpressions.Regex.Replace(line, "public const float ENEMY_WEIGHT =", "");
                        enemy_weight = enemy_weight.Trim(' ', ';', 'f');
                    }

                    if (line.Contains("public const float LEVEL_WEIGHT"))
                    {
                        level_weight = System.Text.RegularExpressions.Regex.Replace(line, "public const float LEVEL_WEIGHT =", "");
                        level_weight = level_weight.Trim(' ', ';', 'f');
                    }
                }
            }
        }
        #endregion


        //sprite selection
        #region 
        Texture tex = AssetPreview.GetAssetPreview(my_tile.tileSprite);

        //title
        EditorGUILayout.BeginHorizontal("TextArea");
        EditorGUILayout.LabelField("Curret Sprite Selection: " + my_tile.tileSprite.name, EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal();

        //button and preview
        EditorGUILayout.BeginHorizontal("Box");
        EditorGUILayout.BeginVertical();
        if (GUILayout.Button("change sprite", GUILayout.ExpandWidth(true)))
        {
            //create a popup
            MyWindow.Init(my_tile);
        }
        EditorGUILayout.EndVertical();
        GUILayout.Label(tex);
        EditorGUILayout.EndHorizontal();
        #endregion


        //Difficulty Settings
        #region
        //title

        EditorGUILayout.BeginVertical("TextArea");
        EditorGUILayout.LabelField("Difficulty Level: " + my_tile.CalculateDifficulty(), EditorStyles.boldLabel);
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.BeginHorizontal("Box");
        EditorGUILayout.LabelField("enemy & level weights: (" + enemy_weight + " / " + level_weight+ ")");
        EditorGUILayout.EndHorizontal();

        //level setting
        #region
        //title
        EditorGUILayout.BeginHorizontal("Box");
        EditorGUILayout.LabelField("Tile Level: " + my_tile.level, EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal();

        //Slider
        my_tile.level = EditorGUILayout.IntSlider(my_tile.level, 1, 25);
        #endregion

        //Enemy Count setting
        #region
        //title
        EditorGUILayout.BeginHorizontal("Box");
        EditorGUILayout.LabelField("Number of Enemies: " + my_tile.numEnemies, EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal();

        //Slider
        my_tile.numEnemies = EditorGUILayout.IntSlider(my_tile.numEnemies, 1, 500);
        #endregion

        EditorGUILayout.EndVertical();
        #endregion


        //save a prefab
        #region
        //title and text iput
        EditorGUILayout.BeginHorizontal("TextArea");
        if (GUILayout.Button("Save as prefab", GUILayout.ExpandWidth(true)))
        {
            SaveWindow.Init(my_tile);
        }
        EditorGUILayout.EndHorizontal();
        #endregion




        if (GUI.changed)
        {
            EditorUtility.SetDirty(my_tile);
        }
    }
}

public class MyWindow : EditorWindow
{
    public List<Sprite> sprites = new List<Sprite>();
    public Texture[] textures; 
    public WorldTile my_tile;
    int selGridInt = 0;

    public static void Init(WorldTile tile)
    {
        // Get existing open window or if none, make a new one:
        MyWindow window = (MyWindow)EditorWindow.GetWindow(typeof(MyWindow));
        window.my_tile = tile;
        window.Show();
    }

    void OnGUI()
    {
        //populate the sprites
        Sprite_Field(my_tile);

        //create a selection grid
        selGridInt = GUILayout.SelectionGrid(selGridInt, textures, 4);

        if (GUILayout.Button("select"))
        {
            my_tile.tileSprite = sprites[selGridInt];
            this.Close();
        }


    }

    void Sprite_Field(WorldTile my_tile)
    {
        //load all our sprites from the folder we want to use
        DirectoryInfo dir = new DirectoryInfo("Assets/Resources/Sprites/Tile");
        FileInfo[] info = dir.GetFiles("*.png");
        foreach (FileInfo f in info)
        {
            Sprite my_sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Resources/Sprites/Tile/" + f.Name, typeof(Sprite));
            if (!sprites.Contains(my_sprite))
            {
                sprites.Add(my_sprite);
            }
        }

        //setup an array because we have to give the selection grid the data type it wants
        textures = new Texture[sprites.Count];
        for(int i = 0; i < sprites.Count; i++)
        {
            textures[i] = sprites[i].texture;
        }
    }
}


public class SaveWindow : EditorWindow
{
    public WorldTile my_tile;
    public string prefab_name = "Enter Name";

    public static void Init(WorldTile tile)
    {
        SaveWindow window = (SaveWindow)EditorWindow.GetWindow(typeof(SaveWindow));
        window.my_tile = tile;
        window.Show();
    }

    void OnGUI()
    {
        prefab_name = GUILayout.TextField(prefab_name, 255);

        if (GUILayout.Button("Save"))
        {
            PrefabUtility.SaveAsPrefabAsset(Selection.activeGameObject, "Assets/Resources/Sprites/Prefabs/" + prefab_name + ".prefab", out bool success);
            this.Close();
        }
    }
}
