using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootAttactor : MonoBehaviour
{
    public int collected;

    //create a trigger that will tell us when we collect a coin
    private void OnTriggerEnter(Collider other)
    {
        GameObject G = other.gameObject;
        if (G.layer == 9)
        {
            //once the object is collected, deactivate and destroy it
            collected += 1;
            G.SetActive(false);
            GameObject.Destroy(G, 10f);
        }
    }


   // private void FixedUpdate()
   // {
   //     GameObject[] Loot = GameObject.FindGameObjectsWithTag("Loot");
   //     GameObject Target = GameObject.Find("Character");
   //     Vector3 angle = Vector3.Normalize(Target.transform.position - gameObject.transform.position);
   //
   //     for (int i = 0; i < Loot.Length; i++)
   //     {
   //         Loot[i].transform.position = Vector3.Lerp(Loot[i].transform.position, Target.transform.position, Time.deltaTime * 2);
   //     }
   // }
}
