using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_manager : MonoBehaviour
{

    //all our pre-made camera looks
    public List<camera_look> looks_book = new List<camera_look>();
    
    //index that indicates the actice look
    public int active_look;
    
    //the current look
    public camera_look look;
    
    //the camera that does the looking
    public Camera look_camera;

    public bool draw_gizmos;

    private void Start()
    {
        look = looks_book[active_look];
        change_look();
        StartCoroutine(changelook());
    }

    void Update()
    {
        if(looks_book[active_look].camera_position != look.camera_position)
        {
            look = looks_book[active_look];
            change_look();
        }

        look_camera.transform.LookAt(look.camera_target);   
    }

    void change_look()
    {
        look_camera.transform.position = look.camera_position;
    }

    public void nextlook()
    {
        active_look = Random.Range(0, looks_book.Count - 1);
        StartCoroutine(changelook());

    }
    public IEnumerator changelook()
    {

        yield return new WaitForSeconds(2);
        nextlook();
    }

    private void OnDrawGizmos()
    {
        if (looks_book.Count > 0)
        {
            if (draw_gizmos)
            {
                look = looks_book[active_look];
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(look.camera_position, 1f);
                Gizmos.DrawSphere(look.camera_target, 1f);
                Gizmos.DrawLine(look.camera_target, look.camera_position);
            }
        }
    }
}
