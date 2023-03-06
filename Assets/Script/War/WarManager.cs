using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarManager : MonoBehaviour
{
    public static WarManager instance;
    public GameObject startPlayer1, startPlayer2;
    WarPlayer m_Player;
    public bool isPlayer2, turn;
    public bool playerStart;
    float timingStart;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Player = FindObjectOfType<WarPlayer>();
        isPlayer2 = SaveManager.Instance.is2Player;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerStart)
        {
            timingStart += Time.deltaTime;
        }
        if (timingStart > 3)
        {
            if (isPlayer2)
            {
                if (!turn)
                {
                    startPlayer1.SetActive(true);
                    startPlayer2.SetActive(false);
                    turn = true;
                }
                else
                {
                    startPlayer2.SetActive(true);
                    startPlayer1.SetActive(false);
                    turn = false;
                }
            }
            else
            {
                startPlayer1.SetActive(true);
            }
            timingStart = 0;
            playerStart = true;
        }
    }
}
