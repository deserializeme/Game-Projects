using UnityEngine;
using System.Collections;

public class DrawRadar : MonoBehaviour
{
    [Range(0, 1)]
    public float ThetaScale;
    [Range(0, 10)]
    public float radius;
    public float Theta = 0f;
    public Vector3 Center;
    public bool Handles;
    public Color myColor;


}
