using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDropper : MonoBehaviour
{
    public List<GameObject> LootTable;
    public int Drops;
    public List<GameObject> SpawnedItems;
    float Force = .3f;



    void Start()
    {
        //initialize the object pool 
        GameObject Pool = new GameObject("Pool");
        Pool.name = "Pool";

        for (int i = 0; i < Drops; i++)
        {
            // select a random item
            int R = Random.Range(0, LootTable.Count);

            // spawn random item
            GameObject G = Instantiate(LootTable[R], Pool.transform) as GameObject;

            //disable box collider so theres no physics fuckery
            G.GetComponent<BoxCollider>().enabled = false;

            // reposition object
            G.transform.position = gameObject.transform.position;

            //deactivate until later
            G.SetActive(false);

            //add to list so we can retrieve later
            SpawnedItems.Add(G);
        }
    }

    public void Drop()
    {
        for(int i = 0; i < SpawnedItems.Count; i++)
        {
            //grab the first item from the list
            GameObject G = SpawnedItems[i];

            //enable the cllider
            G.GetComponent<BoxCollider>().enabled = true;

            //set it to active
            G.SetActive(true);
            G.GetComponent<LootBehavior>().StartCoroutine("DestroyItem");

            //apply upward force to make the effect look a little juicier
            G.GetComponent<Rigidbody>().AddForce(Vector3.up * Force, ForceMode.Impulse);
        }
    }
    
}
