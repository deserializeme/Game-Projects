using UnityEngine;
using XInputDotNetPure;
using System.Collections.Generic;
using MovementEffects;

public class RumbleManager
{
    public bool Rumbling;

    public bool ControllerConnected()
    {
        bool connected = false;

        //check through playerindexes and see if there is a controller connnected
        for (int i = 0; i < 4; i++)
        {    
            PlayerIndex testPlayerIndex = (PlayerIndex)i;
            GamePadState testState = GamePad.GetState(testPlayerIndex);
            if (testState.IsConnected)
            {
                connected = true;
            }
        }
        return connected;
    }

    public void DoRumble(PlayerIndex index, float IntensityLeft, float IntensityRight, float Duration)
    {
        if (!Rumbling)
        {
            Timing.RunCoroutine(RumbleNow(Duration, index, IntensityLeft, IntensityRight));
        }
    }

    IEnumerator<float> RumbleNow(float Duration, PlayerIndex index, float IntensityLeft, float IntensityRight)
    {
        Rumbling = true;
        float start = Time.time;
        float end = start + Duration;
        GamePad.SetVibration(index, IntensityLeft, IntensityRight);

        while (Time.time < end)
        {
            yield return 0f;
        }

        GamePad.SetVibration(index, 0, 0);
        Rumbling = false;
    }
}
   
