using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kicker : MonoBehaviour
{
    BallScript m_Ball;
    TeamManager manager;
    float timingBot;
    public Animator anim;
    public Vector3 startPos;
    public Vector3 beginPos;
    public bool Follow, Bot;

    // Start is called before the first frame update
    void Start()
    {
        beginPos = transform.position;
        m_Ball = FindObjectOfType<BallScript>();
        manager = transform.parent.GetComponent<TeamManager>();
        anim = GetComponent<Animator>();

        if (transform.parent.eulerAngles.y == 0)
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<MeshRenderer>().material = BallManager.instance.material1;
            }
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<MeshRenderer>().material = BallManager.instance.material2;
            }
        }
    }

    private void Update()
    {
        if (Follow)
        {
            if(transform.position != startPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, startPos, 0.1f);
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).GetComponent<BoxCollider>().enabled = false;
                }
            }
            else
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).GetComponent<BoxCollider>().enabled = true;
                }
                if (Bot)
                {
                    m_Ball.BotShoot();
                    Follow = false;
                }
                else
                {
                    m_Ball.BallReset(transform.parent.eulerAngles.y);
                    Follow = false;
                }
            }
        }
        else
        {
            if(manager.Keeper == transform && m_Ball.nearestObj != this)
            {
                transform.position = Vector3.MoveTowards(transform.position, beginPos, 0.1f);
            }
        }
    }
}
