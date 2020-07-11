using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSpawn : MonoBehaviour
{
    [Range(0,100)]
    public int Length;
    public List<GameObject> Pieces;
    public List<GameObject> Track;
    public int Pos = 0;

    // Start is called before the first frame update
    public void Start()
    {

        for(int i = 0; i < Length; i++)
        {
            int r = Random.Range(0, Pieces.Count);
            Vector3 newPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, Pos);
            GameObject G = Instantiate(Pieces[r], newPos, Pieces[r].transform.rotation);
            Pos += 10;
            Track.Add(G);
            G.GetComponent<TreeSpawn>().SpawnTrees();
            G.GetComponent<TreeSpawn>().SpawnSnow();
            G.GetComponent<TreeSpawn>().SpawnPickups();
        }
    }

    public void NewPiece()
    {
        if(Track.Count < Length)
        {
            int r = Random.Range(0, Pieces.Count);
            Vector3 newPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, Pos);
            GameObject G = Instantiate(Pieces[r], newPos, Pieces[r].transform.rotation);
            Pos += 10;
            G.GetComponent<TreeSpawn>().SpawnTrees();
            G.GetComponent<TreeSpawn>().SpawnSnow();
            G.GetComponent<TreeSpawn>().SpawnPickups();
            Track.Add(G);
        }
    }

}
