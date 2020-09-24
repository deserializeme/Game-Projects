﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using TMPro;
using UnityEngine.Video;
using UnityEngine.UI;

public class effects_controls : MonoBehaviour
{
    public PostProcessProfile profile;
   
    public Bloom bloom;

    DepthOfField dof;

    public List<Renderer> emissive_lights = new List<Renderer>();
    public List<Light> spot_lights = new List<Light>();
    public Light sun;
    public Material emissive_material;
    public Renderer tv_material_renderer;
    public Light tv_point_light;
    Color tv_color;
    public GameObject camera_mmanager_object;
    public GameObject outside_video;
    camera_manager cm;
    public Color startColor;
    public TMP_Text hex_code_field;
    public TMP_Text video_url;
    VideoPlayer video_player;
    public Image swatch;

    Color finalValue;
    bool flickering;

    //overhead light color
    #region overhead Light color
    Color _overhead_light_color;
    public Color overhead_light_color
    {
        get
        {
            return _overhead_light_color;
        }
        set
        {
            var input = value;
            Debug.Log(overhead_light_color + " " + value);
            if (input != _overhead_light_color)
            {
                _overhead_light_color = value;
                finalValue = overhead_light_color * overhead_light_intensity;
                Emissive_Lights();
                Spot_Lights();
                swatch.color = overhead_light_color;
                Color c = overhead_light_color * ambient_intensity;
                RenderSettings.ambientLight = c;
            }
        }
    }
    #endregion

    //Color R
    #region Color R value
    float _color_r_value;
    public float color_r_value
    {
        get
        {
            return _color_r_value;
        }
        set
        {
            var input = value;
            if (input != _color_r_value)
            {
                _color_r_value = value;
                Color c = overhead_light_color;
                c.r = color_r_value;
                overhead_light_color = c;
            }
        }
    }
    #endregion

    //Color G
    #region Color G value
    float _color_g_value;
    public float color_g_value
    {
        get
        {
            return _color_g_value;
        }
        set
        {
            var input = value;
            if (input != _color_g_value)
            {
                _color_g_value = value;
                Color c = overhead_light_color;
                c.g = color_g_value;
                overhead_light_color = c;
            }
        }
    }
    #endregion

    //Color B
    #region Color B value
    float _color_b_value;
    public float color_b_value
    {
        get
        {
            return _color_b_value;
        }
        set
        {
            var input = value;
            if (input != _color_b_value)
            {
                _color_b_value = value;
                Color c = overhead_light_color;
                c.b = color_b_value;
                overhead_light_color = c;
            }
        }
    }
    #endregion

    //Color A
    #region Color A value
    float _color_a_value;
    public float color_a_value
    {
        get
        {
            return _color_a_value;
        }
        set
        {
            var input = value;
            if (input != _color_a_value)
            {
                _color_a_value = value;
                Color c = overhead_light_color;
                c.a = color_a_value;
                overhead_light_color = c;
            }
        }
    }
    #endregion

    //overhead light intensity
    #region overhead Light Intensity
    float _overhead_light_intensity;
    public float overhead_light_intensity
    {
        get
        {
            return _overhead_light_intensity;
        }
        set
        {
            var input = value;
            if (input != _overhead_light_intensity)
            {
                _overhead_light_intensity = value;
                finalValue = overhead_light_color * overhead_light_intensity;
                Emissive_Lights();
            }
        }
    }
    #endregion

    //Spot light intensity 
    #region spot light intensity
    float _spot_light_intensity;
    public float spot_light_intensity
    {
        get
        {
            return _spot_light_intensity;
        }
        set
        {
            var input = value;
            if (input != _spot_light_intensity)
            {
                _spot_light_intensity = value;
                Spot_Lights();
            }
        }
    }
    #endregion

    //Sun Intensity
    #region sun intensity 
    float _sun_intensity;
    public float sun_intensity
    {
        get
        {
            return _sun_intensity;
        }
        set
        {
            var input = value;
            if (input != _sun_intensity)
            {
                sun.intensity = input;
            }
        }
    }
    #endregion

    //ambient light Intensity
    #region ambient light
    float _ambient_intensity;
    public float ambient_intensity
    {
        get
        {
            return _ambient_intensity;
        }
        set
        {
            var input = value;
            if (input != _ambient_intensity)
            {
                _ambient_intensity = input;
                Color c = overhead_light_color * ambient_intensity;
                RenderSettings.ambientLight = c;
            }
        }
    }
    #endregion

    //Bloom Intensity
    #region bloom intensity 
    float _bloom_intensity;
    public float bloom_intensity
    {
        get
        {
            return _bloom_intensity;
        }
        set
        {
            var input = value;
            if (input != _bloom_intensity)
            {
                _bloom_intensity = value;
                profile.TryGetSettings(out bloom);
                if (bloom != null)
                {
                    bloom.intensity.value = bloom_intensity;
                }
            }
        }
    }
    #endregion

    //Bloom Intensity
    #region bloom threshold 
    float _bloom_threshold;
    public float bloom_threshold
    {
        get
        {
            return _bloom_threshold;
        }
        set
        {
            var input = value;
            if (input != _bloom_threshold)
            {
                _bloom_threshold = value;
                profile.TryGetSettings(out bloom);
                if (bloom != null)
                {
                    bloom.threshold.value = bloom_threshold;
                }
            }
        }
    }
    #endregion

    //Bloom enabled
    #region bloom enabled
    bool _bloom_enabled;
    public bool bloom_enabled
    {
        get
        {
            return _bloom_enabled;
        }
        set
        {
            bool input = value;
            if (input != _bloom_enabled)
            {
                _bloom_enabled = value;
                profile.TryGetSettings(out bloom);
                if (bloom != null)
                {
                    bloom.enabled.value = bloom_enabled;
                }
            }
        }
    }
    #endregion

    //DOF focal distance
    #region depth of field focal distance
    float _DOF_focal_distance;
    public float DOF_focal_distance
    {
        get
        {
            return _DOF_focal_distance;
        }
        set
        {
            var input = value;
            Debug.Log(value);
            if (input != _DOF_focal_distance)
            {
                _DOF_focal_distance = value;

                profile.TryGetSettings(out dof);
                if (dof != null)
                {
                    if (auto_DOF)
                    {
                        dof.focusDistance.value = auto_DOF_focal_distance;
                    }
                    else
                    {
                        dof.focusDistance.value = DOF_focal_distance;
                    }

                    DOF_Values();
                }
            }
        }
    }
    #endregion

    //DOF depth of field aperture
    #region depth of field aperture
    float _DOF_aperture;
    public float DOF_aperture
    {
        get
        {
            return _DOF_focal_distance;
        }
        set
        {
            var input = value;
            if (input != _DOF_aperture)
            {
                _DOF_aperture = value;
                profile.TryGetSettings(out dof);
                if (dof != null)
                {
                    if (auto_DOF)
                    {
                        DOF_aperture = dof.aperture.value;
                    }
                    else
                    {
                        dof.aperture.value = DOF_aperture;
                    }

                }
            }
        }
    }
    #endregion

    //DOF focal length
    #region depth of field focal length
    float _DOF_focal_length;
    public float DOF_focal_length
    {
        get
        {
            return _DOF_focal_length;
        }
        set
        {
            var input = value;
            if (input != _DOF_focal_length)
            {
                _DOF_focal_length = value;

                profile.TryGetSettings(out dof);
                if (dof != null)
                {
                    if (auto_DOF)
                    {
                        DOF_focal_length = dof.focalLength.value;
                    }
                    else
                    {
                        dof.focalLength.value = DOF_focal_length;
                    }

                }
            }
        }
    }
    #endregion

    //DOF enabled
    #region depth of field enabled
    bool _DOF_enabled;
    public bool DOF_enabled
    {
        get
        {
            return _DOF_enabled;
        }
        set
        {
            var input = value;
            if (input != _DOF_enabled)
            {
                _DOF_enabled = value;
                profile.TryGetSettings(out dof);
                if (dof != null)
                {
                    dof.enabled.value = DOF_enabled;
                }
            }
        }
    }
    #endregion

    //Auto depth of field enabled
    #region Auto depth of field enabled
    bool _auto_DOF;
    public bool auto_DOF
    {
        get
        {
            return _auto_DOF;
        }
        set
        {
            var input = value;
            if (input != _auto_DOF)
            {
                _auto_DOF = value;
            }
        }
    }
    #endregion

    // auto depth of field focal distance
    #region auto depth of field focal distance
    float _auto_DOF_focal_distance;
    public float auto_DOF_focal_distance
    {
        get
        {
            return _auto_DOF_focal_distance;
        }
        set
        {
            var input = value;
            if (input != _auto_DOF_focal_distance)
            {
                _auto_DOF_focal_distance = value;

                profile.TryGetSettings(out dof);
                if (dof != null)
                {
                    if (auto_DOF)
                    {
                        DOF_Values();
                    }
                }
            }
        }
    }
    #endregion

    //window video
    #region window as video
    bool _window_video;
    public bool window_video
    {
        get
        {
            return _window_video;
        }
        set
        {
            var input = value;
            if (input != _window_video)
            {
                _window_video = value;
                Window_Options();
            }
        }
    }
    #endregion

    //flicker lights
    #region flicker lights
    bool _flicker_lights;
    public bool flicker_lights
    {
        get
        {
            return _window_video;
        }
        set
        {
            var input = value;
            if (input != _flicker_lights)
            {
                _flicker_lights = value;
                if (!flickering)
                {
                    if (_flicker_lights)
                    {
                        flickering = true;
                        StartCoroutine(flicker());
                    }
                }
            }
        }
    }
    #endregion

    //TV light intensity
    #region TV Light Intensity
    float _tv_light_intensity;
    public float tv_light_intensity
    {
        get
        {
            return _tv_light_intensity;
        }
        set
        {
            var input = value;
            if (input != _tv_light_intensity)
            {
                _tv_light_intensity = value;
                tv_color = Color.white * tv_light_intensity;
                TV_Options();
            }
        }
    }
    #endregion

    //window video
    #region TV on or off
    bool _tv_on;
    public bool tv_on
    {
        get
        {
            return _tv_on;
        }
        set
        {
            var input = value;
            if (input != _tv_on)
            {
                _tv_on = value;
                TV_Options();
            }
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        cm = camera_mmanager_object.GetComponent<camera_manager>();
        overhead_light_color = startColor;
        video_player = outside_video.GetComponent<VideoPlayer>();
        video_player.url = "https://media.istockphoto.com/videos/zombie-silhouettes-behind-glass-video-id483034544";
        bloom_enabled = true;
        auto_DOF = true;
        DOF_enabled = true;
        DOF_focal_length = 10;
        window_video = true;
    }

    // Update is called once per frame
    void Update()
    {
        auto_DOF_focal_distance = Vector3.Distance(cm.look.camera_position, cm.look.camera_target);
    }

    void Bloom_Values()
    {
        profile.TryGetSettings(out bloom);
        if (bloom != null)
        {
            bloom.intensity.value = bloom_intensity;
        }

        profile.TryGetSettings(out bloom);
        if (bloom != null)
        {
            bloom.enabled.value = bloom_enabled;
        }
    }

    void DOF_Values()
    {

        profile.TryGetSettings(out dof);
        if (dof != null)
        {
            dof.enabled.value = DOF_enabled;
        }

        profile.TryGetSettings(out dof);
        if (dof != null)
        {
            if (auto_DOF)
            {
                dof.focusDistance.value = auto_DOF_focal_distance;
                DOF_focal_distance = dof.focusDistance.value;
                DOF_aperture = dof.aperture.value;
                DOF_focal_length = dof.focalLength.value;
            }
            else
            {
                dof.focusDistance.value = DOF_focal_distance;
               //dof.aperture.value = DOF_aperture;
               //dof.focalLength.value = DOF_focal_length;
            }

        }
    }

    void Emissive_Lights()
    {
        foreach (Renderer g in emissive_lights)
        {
            g.material.SetColor("_EmissionColor", finalValue);
            DynamicGI.SetEmissive(g, finalValue); // Pass your objet's renderer which emissive material
        }
    }

    void Spot_Lights()
    {
        foreach (Light g in spot_lights)
        {   
            g.color = overhead_light_color;
            g.intensity = spot_light_intensity;
        }
    }

    void Flicker_fuctions()
    {
        if (!flickering)
        {
            if (flicker_lights)
            {
                flickering = true;
                StartCoroutine(flicker());
            }
        }
    }

    void Window_Options()
    {
        if (window_video)
        {
            outside_video.SetActive(true);
        }
        else
        {
            outside_video.SetActive(false);
        }
    }

    void TV_Options()
    {
        if (tv_on)
        {
            tv_point_light.intensity = 1.5f;
            tv_material_renderer.material.SetColor("_EmissionColor", tv_color);
            DynamicGI.SetEmissive(tv_material_renderer, tv_color); // Pass your objet's renderer which emissive material
        }
        else
        {
            tv_point_light.intensity = 0;
            tv_color = Color.black;
            tv_material_renderer.material.SetColor("_EmissionColor", tv_color);
            tv_material_renderer.material.SetColor("Albedo", tv_color);
            DynamicGI.SetEmissive(tv_material_renderer, tv_color); // Pass your objet's renderer which emissive material
        }
    }

    public IEnumerator flicker()
    {
        float old_0 = overhead_light_intensity;
        float old_1 = spot_light_intensity;

        overhead_light_intensity = .5f;
        spot_light_intensity =0;
        
        yield return new WaitForSeconds(Random.Range(.2f, .5f));
        
        overhead_light_intensity = old_0;
        spot_light_intensity = old_1;

        yield return new WaitForSeconds(Random.Range(.1f, 1f));
        flickering = false;
    }

    public void change_window_video()
    {
        StartCoroutine(play_new_video());
    }

    IEnumerator play_new_video()
    {
        video_player.Stop();
        video_player.url = video_url.text;
        video_player.Prepare();

        while (!video_player.isPrepared)
        {
            Debug.Log("Preparing Video");
            yield return null;
        }

        Debug.Log("Done Preparing Video");
        //video_player.Play();
    }

}
