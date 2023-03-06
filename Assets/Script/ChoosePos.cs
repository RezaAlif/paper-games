using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoosePos : MonoBehaviour
{
    public int unitFootball;
    public bool isPlayer2;
    public bool chooseColor;
    public bool chooseFootball;
    public Image img;
    Button m_Button;

    // Start is called before the first frame update
    void Start()
    {
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(Move);
    }

    public void Move()
    {
        img.rectTransform.position = m_Button.GetComponent<RectTransform>().position;

        if (chooseColor)
        {
            if (!isPlayer2)
            {
                SaveManager.Instance.player1Color = GetComponent<Image>().color;
            }
            else
            {
                SaveManager.Instance.player2Color = GetComponent<Image>().color;
            }
        }
        if (chooseFootball)
        {
            if (!isPlayer2)
            {
                SaveManager.Instance.footBallPlayer1 = unitFootball;
            }
            else
            {
                SaveManager.Instance.footBallPlayer2 = unitFootball;
            }
        }
    }
}
