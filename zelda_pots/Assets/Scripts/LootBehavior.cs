using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBehavior : MonoBehaviour
{

    IEnumerator DestroyItem()
    {
        //make the object blink prior to despawning
        float TimeToBlink = 1f;
        float TimeElapsed = 0;
        float TimeToGo = 30f;

        MeshRenderer MR = gameObject.GetComponent<MeshRenderer>();

        while (TimeToGo > 0)
        {
            //measure timers
            TimeToGo -= Time.deltaTime;
            TimeToBlink -= Time.deltaTime;
            TimeElapsed += Time.deltaTime;

            //start blinking X seconds from end
            if(TimeToGo < 5f)
            {
                if (TimeElapsed > TimeToBlink)
                {
                    //swap the renderer on and off
                    if (MR.enabled)
                    {
                        MR.enabled = false;
                        TimeElapsed = 0;
                    }
                    else
                    {
                        MR.enabled = true;
                        TimeElapsed = 0;
                    }
                }
            }

            yield return null;
        }

        //disable then delete game object
        GameObject.Destroy(this.gameObject, 10f);

    }

    IEnumerator Sleep()
    {
        float TimeElapsed = 5f;

        while (TimeElapsed > 0)
        {
            //measure timers
            TimeElapsed -= Time.deltaTime;



            yield return null;
        }

        //disable then delete game object
        gameObject.GetComponent<Rigidbody>().isKinematic = true;

    }
}

