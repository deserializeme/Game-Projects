using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Attributes {


public enum StatName
    {
        Health,
        Speed,
        Power,
        Accuracy,
        Haste,
        Defense
    }


public class Values
    {
        public int Base;
        public int Max;
        public int Current;
    }

    public StatName Stat;
    public Values Value;

    
}
