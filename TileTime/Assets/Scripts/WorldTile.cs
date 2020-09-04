using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Core behaviour to determine the overworld icon for a tile, it's relative level
/// as well as enemy count and difficulty.
/// NOTE: This clas needs to work with our phasing structure, and do to dependencies & serialization
///       we cannot alter any of the members directly.
/// </summary>
public class WorldTile : MonoBehaviour
{
    public const float ENEMY_WEIGHT = 0.5f;
    public const float LEVEL_WEIGHT = 14.5f;
    //NOTE: Only textures in Resources/Sprites/Tiles should be referenced here
    public Sprite tileSprite;

    [SerializeField]
    protected Image tileImage;

    //Design Note: This value needs to be constrained between 1 and 25
    public int level;

    //Design Note: This value needs to be constrained between 1 and 500
    public int numEnemies; 

    public int difficultyModifier;

    // Where does this tile lie relative to other tiles in the scene?
    public Vector2 worldCoordinates;

    // Calculate how many times our Bunny has landed on this tile this game.
    public int numTimesVisited;

    //Engineering note: Vestigal, but we cannot remove this due to a serialization issues.
    public int _cachedDangerValueOld;

    //Engineering note: Design no longer needs this, but we use it in game and require it for serialization.
    [SerializeField]
    protected Color dangerVis;


    public float CalculateDifficulty()
    {
        return (level * LEVEL_WEIGHT) + (numEnemies + ENEMY_WEIGHT) + difficultyModifier;
    }

    public void AssignSpriteToImage()
    {
        if (tileSprite == null || tileImage == null)
            return;

        tileImage.sprite = tileSprite;
    }


}
