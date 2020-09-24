using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New settings profile", menuName = "New settings profile", order = 1)]
[System.Serializable]
public class profile_so : ScriptableObject
{

    public float sun_strength;
    public float overhead_strength;
    public float spot_strength;
    public float light_color_r;
    public float light_color_g;
    public float light_color_b;
    public float light_color_a;

    public bool bloom_enabled;
    public float bloom_intensity;
    public float bloom_threshold;

    public bool auto_dof;
    public bool dof_enabled;
    public float dof_focal_distance;

    public bool window_video;
    public string window_video_url;

    public bool tv_on;
    public string tv_video;

    public string poster_1_url;
    public string poster_2_url;
    public string poster_3_url;



}