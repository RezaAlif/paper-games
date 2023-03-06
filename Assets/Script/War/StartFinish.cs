using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartFinish : MonoBehaviour
{
    public GameObject objResult;
    public Text txtResult;
    public WarPlayer m_Player;
    public bool isFinish, endGame;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        if (isFinish)
        {
            if (m_Player.starting && !endGame)
            {
                objResult.SetActive(true);
                endGame = true;

                if (!WarManager.instance.turn)
                {
                    if (SaveManager.Instance.is2Player)
                    {
                        txtResult.text = "Player 1 Wins";
                    }
                    else
                    {
                        txtResult.text = "Congratulations, you've completed the game";
                    }
                }
                else
                {
                    txtResult.text = "Player 2 Wins";
                }
            }
        }
        else
        {
            m_Player.placing = true;
        }
    }

    private void OnMouseExit()
    {
        m_Player.placing = false;
        m_Player.timingStart = 0;
    }
}
