using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New Resource", menuName = "New Resource", order = 1)]
[System.Serializable]
public class CreateNewResource : ScriptableObject {

    public string Name;
    public Color ResourceBarColor;

}
