using UnityEngine;
using System.Collections;

public class Keys : MonoBehaviour
{
    public CreateKeybinds myKeybinds;

    public GameObject Target;
    public GameObject[] Source;

    // Use this for initialization
    void Start()
    {
        myKeybinds = Instantiate(Resources.Load<CreateKeybinds>("Manager/DefaultKeymap"));
        Target = GameObject.Find("Cube");

    }

    // Update is called once per frame
    void Update()
    {
        if (Target != null)
        {
            if (Input.GetKeyDown(myKeybinds.Target))
            {
                for(int i = 0; i < Source.Length; i++)
                {
                    if (Source[i] != null)
                    {
                        AttackChecklist.Attack(Source[i], Target, null, null, null, null, gameObject.GetComponent<SkillList>().Projectiles[0]);
                        Messages.Message(Source[i] + " attempting to cast " + gameObject.GetComponent<SkillList>().Shields[0] + " at " + Target + " ");
                    }
                }
            }

            if (Input.GetKeyDown(myKeybinds.Move))
            {
                for (int i = 0; i < Source.Length; i++)
                {
                    if (Source[i] != null)
                    {
                        AttackChecklist.Attack(Source[i], Target, null, gameObject.GetComponent<SkillList>().DOTs[0], null, null, null);
                        Messages.Message(Source[i] + " attempting to cast " + gameObject.GetComponent<SkillList>().Projectiles[0] + " at " + Target + " ");
                    }
                }
            }
        }

    }
}
