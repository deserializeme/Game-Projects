using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DiscoPole : MonoBehaviour
{
    public Material meshMaterial;
    public Color CurrentColor;
    public Color NextColor;
    public float Speed;

    void Start()
    {
        meshMaterial = gameObject.GetComponent<MeshRenderer>().material;
        PickColor();
        Debug.Log("Started");
    }

    void PickColor()
    {
        NextColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }

    void LerpColor()
    {
        if (CurrentColor != NextColor)
        {
            meshMaterial.color = Color.Lerp(meshMaterial.color, NextColor, Time.deltaTime * Speed);
            meshMaterial.SetColor("_EmissionColor", meshMaterial.color);
        }
    }

    void Loop()
    {
        if (CurrentColor == NextColor)
        {
            PickColor();
        }
    }

    private void Update()
    {
        CurrentColor = meshMaterial.color;
        LerpColor();
        Loop();
    }
}


