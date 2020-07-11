using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackDespawn : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Ground;
    public GameObject Manager;
    public GameObject Sled;

    private void Awake()
    {
        Manager = GameObject.Find("TrackSpawner");
        Sled = GameObject.Find("sled");
    }

    // Update is called once per frame
    void Update()
    {
        TrackSpawn T = Manager.GetComponent<TrackSpawn>();
        float Zdiff = gameObject.transform.position.z - Sled.transform.position.z;
        if (Zdiff < -(T.Length/2 * 2))
        {
            T.Track.Remove(this.gameObject);
            gameObject.SetActive(false);
            Destroy(this.gameObject);

            if (T.Track.Count < T.Length)
            {
                T.NewPiece();
            }
        }
    }
}
