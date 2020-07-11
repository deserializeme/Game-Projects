using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawn : MonoBehaviour
{

    public List<GameObject> Tree_Prefabs;

    public GameObject SnowPuddle;

    public List<GameObject> Pickup_Prefabs;

    public GameObject[] Points;

    bool spawned = false;

    public void SpawnTrees()
    {
        Points = GameObject.FindGameObjectsWithTag("TreeSpawnPoint");

        for (int i = 0; i < Points.Length; i++)
        {
            if (!Points[i].GetComponent<TreeSpawnPointGizmo>().occupied)
            {
                int r = Random.Range(0, Tree_Prefabs.Count);
                GameObject G = Instantiate(Tree_Prefabs[r], Points[i].transform.position, Points[i].transform.rotation);
                G.transform.SetParent(Points[i].transform);
                Points[i].GetComponent<TreeSpawnPointGizmo>().occupied = true;
            }
        }
    }

    public void SpawnSnow()
    {
        Points = GameObject.FindGameObjectsWithTag("SnowSpawnPoint");

        for (int i = 0; i < Points.Length; i++)
        {
            if (!Points[i].GetComponent<SnowPuddleGizmo>().occupied)
            {
                int r = Random.Range(0, 10);
                if (r < 5)
                {
                    GameObject G = Instantiate(SnowPuddle, Points[i].transform.position, Points[i].transform.rotation);
                    G.transform.SetParent(Points[i].transform);
                    Points[i].GetComponent<SnowPuddleGizmo>().occupied = true;
                }
                else
                {
                    Points[i].GetComponent<SnowPuddleGizmo>().occupied = true;
                }

            }
        }
    }

    public void SpawnPickups()
    {
        Points = GameObject.FindGameObjectsWithTag("PickupSpawnPoint");

        GameObject RandomPoint = Points[Random.Range(0, Points.Length)];
        Debug.Log("The number of points is " + Points.Length + ". The Random point Selected is " + RandomPoint + ".");


        int RandomDrop = Random.Range(0, Pickup_Prefabs.Count);
        Debug.Log("The Drop is " + Pickup_Prefabs.Count + ". The Random number is " + RandomDrop + ".");

        if (!RandomPoint.GetComponent<PickupGizmo>().occupied)
        {

            GameObject G = Instantiate(Pickup_Prefabs[RandomDrop], RandomPoint.transform.position, RandomPoint.transform.rotation);
            G.transform.SetParent(RandomPoint.transform);
            RandomPoint.GetComponent<PickupGizmo>().occupied = true;

        }

    }
}


