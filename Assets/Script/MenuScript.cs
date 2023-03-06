using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public static MenuScript instance;
    public bool modeSelected;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        SaveManager.Instance.is2Player = false;
        SaveManager.Instance.footBallPlayer1 = 0;
        SaveManager.Instance.footBallPlayer2 = 0;
    }

    public void Activated(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void Deactivated(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void JumpOnScene(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }
}
