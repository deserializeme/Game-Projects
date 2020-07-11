using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MovementEffects;

public class Timer : MonoBehaviour {

    public float StartTime;
    public float EndTime;
    public float Elapsed;

    public int Ticks;
    public int TicksRemaining;
    public bool UnlimitedDuration;

    float tick_length;
    public float Tick_Length
    {
        get
        {
            if (!UnlimitedDuration)
            {
                return (EndTime - StartTime) / Ticks;
            }
            else
            {
                return tick_length;
            }
        }

        set
        {
            if (UnlimitedDuration)
            {
                tick_length = value;
            }
        }
    }

    public bool Expire;

    //simple timer to count down from start to end
    public IEnumerator<float> SimpleTimer()
    {
        while(Time.time < EndTime)
        {
            yield return 0f;
        }
        Expire = true;
    }


    // tick timer
    public IEnumerator<float> TickTimer()
    {
        while (Time.time < EndTime)
        {
            //time sincle last tick
            Elapsed += Time.deltaTime;

            //when is our next tick
            if(Elapsed >= Tick_Length)
            {
                Elapsed = 0;
                TicksRemaining--;
            }

            // expire after all ticks
            if(TicksRemaining <= 0)
            {
                Expire = true;
            }

            yield return 0f;
        }
    }
}
