using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Damage Type", menuName = "New Damage Type", order = 1)]
[System.Serializable]
public class CreateNewDamageType : ScriptableObject {


    public string Name;
    public List<CreateNewDamageType> Resist;
    public List<float> ResistAmnt;
    public List<CreateNewDamageType> Weak;
    public List<float> WeakAmnt;

}
