using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallManager : MonoBehaviour
{
    public static BallManager instance;
    public Material material1, material2;
    public GameObject completed;
    public GameObject[] Formation;
    public int score1, score2;
    public Text scoreUI1, scoreUI2, uiResults, uiScoring;
    public bool gameFinish;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject objTeam = Instantiate(Formation[SaveManager.Instance.footBallPlayer1]);
        FindObjectOfType<BallScript>().team1 = objTeam.GetComponent<TeamManager>();
        material1.color = SaveManager.Instance.player1Color;

        if (SaveManager.Instance.is2Player)
        {
            material2.color = SaveManager.Instance.player2Color;
            GameObject player2 = Instantiate(Formation[SaveManager.Instance.footBallPlayer2]);
            FindObjectOfType<BallScript>().team2 = player2.GetComponent<TeamManager>();
            player2.transform.eulerAngles = new Vector3(0, 180, 0);
            player2.GetComponent<TeamManager>().Bot = false;
        }
        else
        {
            material2.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            GameObject player2 = Instantiate(Formation[Random.Range(0, Formation.Length)]);
            FindObjectOfType<BallScript>().team2 = player2.GetComponent<TeamManager>();
            player2.transform.eulerAngles = new Vector3(0, 180, 0);
            player2.GetComponent<TeamManager>().Bot = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(score1 >= 5)
        {
            gameFinish = true;
            completed.SetActive(true);
            uiResults.text = "Home Wins";
        }
        if(score2 >= 5)
        {
            gameFinish = true;
            completed.SetActive(true);
            uiResults.text = "Away Wins";
        }
    }

    public void GoScene(string sceneLoad)
    {
        SceneManager.LoadScene(sceneLoad);
    }
}
