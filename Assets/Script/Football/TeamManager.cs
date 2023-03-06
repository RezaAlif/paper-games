using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    BallScript m_Ball;
    public bool Bot;
    public Transform Keeper;

    // Start is called before the first frame update
    void Start()
    {
        m_Ball = FindObjectOfType<BallScript>();

        for(int i = 0; i < transform.childCount; i++)
        {
            m_Ball.playerList.Add(transform.GetChild(i).GetComponent<Kicker>());
            transform.GetChild(i).GetComponent<Kicker>().Bot = Bot;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
