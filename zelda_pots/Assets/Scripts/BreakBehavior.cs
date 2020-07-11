using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBehavior : MonoBehaviour
{
    public List<GameObject> BrokenVariants;
    public Material material;


    // breaks an object with a breakbehavior attached
    public void BreakNow()
    {
        //disable the mesh collider so no physics get messed with
        gameObject.GetComponent<MeshCollider>().enabled = false;

        //chose a variant to spawn
        int r = Random.Range(0, BrokenVariants.Count);
        GameObject G = Instantiate(BrokenVariants[r], null);

        //get all rigidbodies attached to the broken variant
        Rigidbody[] Pieces = G.GetComponentsInChildren<Rigidbody>();

        // get all renderers
        Renderer[] Rend = G.GetComponentsInChildren<Renderer>();

        //apply material
        for(int i = 0; i < Rend.Length; i++)
        {
            Rend[i].material = material;
        }


        //set the position
        G.transform.position = this.gameObject.transform.position;

        //add an explosion force to each piece to give the shatter some variation and movement
        int Count = 0;
        for(int i = 0; i < Pieces.Length; i++)
        {
            if(Count < Pieces.Length)
            {
                int R = Random.Range(0, Pieces.Length);
                Pieces[R].AddExplosionForce(500f, G.transform.position, 5f);
                Count += 1;
            }

            //destroy the broken variant 10 seconds later
            GameObject.Destroy(G, 10f);
        }

        //disable other componenets
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<MeshCollider>().isTrigger = true;

        //trigger the loot to drop if applicable
        if (gameObject.GetComponent<LootDropper>())
        {
            gameObject.GetComponent<LootDropper>().Drop();
        }
        

        //deactivate and destroy the object
        GameObject.Destroy(this.gameObject, 2f);
        this.gameObject.SetActive(false);
    }
}
