using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New Keybind Profile", menuName = "New Keybind Profile", order = 1)]
[System.Serializable]
public class CreateKeybinds : ScriptableObject {

    public KeyCode Attack_0;
    public KeyCode Attack_1;
    public KeyCode Attack_2;
    public KeyCode Attack_3;
    public KeyCode Attack_4;

    public KeyCode Target;
    public KeyCode Move;

    public KeyCode Menu;

}
