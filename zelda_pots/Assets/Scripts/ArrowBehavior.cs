using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{

    public GameObject Ammo;
    public GameObject Crossbow;
    public GameObject AmmoPosition;
    public GameObject Camera;

    GameObject G;
    bool loaded = false;

    public float Force;

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //Force += Time.deltaTime / 4;
            Reload();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Fire();
            //Force = 1;
        }

    }

    void Reload()
    {
        if (!loaded)
        {
            loaded = true;
        }
    }

    void Fire()
    {
        G = Instantiate(Ammo, Crossbow.transform);
        Rigidbody Rb = G.GetComponent<Rigidbody>();
        Rb.useGravity = false;
        G.transform.position = AmmoPosition.transform.position;
        G.transform.localScale = Crossbow.transform.localScale;
        Rb.AddForce(Camera.transform.forward * Force, ForceMode.Impulse);
        G.GetComponent<Rigidbody>().useGravity = true;
        loaded = false;
        G.transform.SetParent(null);
        G.transform.localScale = new Vector3(.1f, .1f, .1f);
    }

}
