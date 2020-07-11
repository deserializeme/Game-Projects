using UnityEngine;
using System.Collections;

public class Tick : MonoBehaviour {

    public int Ticks_per_Second;
    public static int Ticks;
    public float CurTime;
    public float Next_Tick;

    public delegate void TickEvent();
    public static event TickEvent NewTick;

    // set up a system wide "Master clock" that other scripts will sync with to perform times actions

    void Start()
    {
        Ticks_per_Second = 1;
        Next_Tick = Time.time + (1 / Ticks_per_Second);
    }

    void TickNow()
    {
        Next_Tick = Time.time + (1 / Ticks_per_Second);
        Ticks++;
        NewTick();
    }
	
    void Update()
    {
        CurTime = Time.time;

        if (CurTime >= Next_Tick)
        {
            TickNow();
        }
    }


}
