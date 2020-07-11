using UnityEngine;
using System.Collections;

public class LevelUp : MonoBehaviour {

    public static float EXPtoLevel(int CurrentLevel)
    {
        float EXP = CurrentLevel * 1000;
        return EXP * 1.5f;
    }
}
