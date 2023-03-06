using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseMode : MonoBehaviour
{
    public ChoosePos choosePos1, choosePos2;
    public Color colorDefault;
    public GameObject objMode;
    public GameObject Player2Mode;
    public Toggle is2Player;

    private void Update()
    {
        if (objMode.activeSelf)
        {
            if (is2Player.isOn)
            {
                Player2Mode.SetActive(true);
                SaveManager.Instance.is2Player = true;
            }
            else
            {
                Player2Mode.SetActive(false);
                SaveManager.Instance.is2Player = false;
            }
        }
    }

    private void OnMouseDown()
    {
        if (!MenuScript.instance.modeSelected)
        {
            objMode.SetActive(true);
            MenuScript.instance.modeSelected = true;
            SaveManager.Instance.player1Color = colorDefault;
            SaveManager.Instance.player2Color = colorDefault;
        }
    }

    public void Back()
    {
        objMode.SetActive(false);
        Player2Mode.SetActive(false);
        SaveManager.Instance.is2Player = false;
        MenuScript.instance.modeSelected = false;
        choosePos1.Move();
        choosePos2.Move();
    }
}
