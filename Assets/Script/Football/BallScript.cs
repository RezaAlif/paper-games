using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallScript : MonoBehaviour
{
    public TeamManager team1, team2;
    bool Turn, SetNearest, onGround, Goal, Kick, Shoot, isPlayerGoal;
    public ParticleSystem convetti;
    public Image imgBar;
    public Transform aimBar;
    public float minRot, maxRot, minScale, maxScale;
    public float currentEulerZ, currentEulerX, currentScale, percentage;
    public GameObject Arrow;
    public Kicker nearestObj;
    Rigidbody ballRigid;
    public List<Kicker> playerList;
    public float currentVelo;
    float timingGoal;

    // Start is called before the first frame update
    void Start()
    {
        ballRigid = GetComponent<Rigidbody>();
        playerList.Add(FindObjectOfType<Kicker>());
        percentage = maxScale - minScale;
        currentScale = minScale;
        BallManager.instance.uiScoring.text = "Press Space To Set Target";
    }

    public void BallReset(float YPos)
    {
        transform.eulerAngles = new Vector3(0, YPos, 0);
        ballRigid.velocity = Vector3.zero;
        ballRigid.isKinematic = true;
        currentEulerX = 0;
        SetNearest = false;
        onGround = false;
        transform.position = new Vector3(transform.position.x, 0.25f, transform.position.z);
        currentEulerZ = 0;
        currentScale = minScale;
        Arrow.SetActive(true);
        BallManager.instance.uiScoring.text = "";
        Shoot = false;
        Goal = false;
        Kick = false;
        nearestObj = null;
        timingGoal = 0;
        imgBar.fillAmount = 1;
        BallManager.instance.uiScoring.text = "Press Space To Set Target";
    }

    public void BotShoot()
    {
        if(transform.position.x < 0)
        {
            currentEulerZ = Random.Range(minRot, 0);
        }
        else
        {
            currentEulerZ = Random.Range(0, maxRot);
        }
        transform.eulerAngles = new Vector3(0, 180, 0);
        Goal = false;
        ballRigid.useGravity = true;
        SetNearest = false;
        GetComponent<Collider>().enabled = true;
        currentEulerX = Random.Range(0, 45);
        ballRigid.isKinematic = false;
        ballRigid.AddForce(aimBar.forward * Random.Range(minScale, maxScale));
        ballRigid.AddForce(aimBar.up * Random.Range(50, 500));
        nearestObj.anim.Play("Kick");
    }

    public void Shooting()
    {
        if (!Shoot)
        {
            if (!Kick)
            {
                BallManager.instance.uiScoring.text = "Press Space To Kick";
                Kick = true;
                ballRigid.isKinematic = false;
            }
            else
            {
                ballRigid.AddForce(aimBar.forward * currentScale);
                nearestObj.anim.Play("Kick");
                Arrow.SetActive(false);
                ballRigid.useGravity = true;
                GetComponent<Collider>().enabled = true;
                Shoot = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        aimBar.localEulerAngles = new Vector3(currentEulerX, currentEulerZ, transform.localEulerAngles.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shooting();
        }

        float nearest = Mathf.Infinity;

        foreach (Kicker player in playerList)
        {
            float distancePly = Vector3.Distance(transform.position, player.transform.position);

            if (distancePly < nearest)
            {
                nearestObj = player;
                nearest = Vector3.Distance(transform.position, nearestObj.transform.position);
            }
        }
        Debug.DrawLine(transform.position, nearestObj.transform.position);

        if (Goal)
        {
            BallManager.instance.uiScoring.text = "Goal";
            timingGoal += Time.deltaTime;

            if(timingGoal > 6.5f)
            {
                for (int i = 0; i < playerList.Count; i++)
                {
                    playerList[i].transform.position = playerList[i].beginPos;
                }
                if (isPlayerGoal)
                {
                    transform.position = new Vector3(0, 0.25f, team2.Keeper.position.z - 1);
                    if (team2.Bot)
                    {
                        BotShoot();
                    }
                    else
                    {
                        BallReset(180);
                    }
                }
                else
                {
                    BallReset(0);
                    transform.position = new Vector3(0, 0.25f, team1.Keeper.position.z + 1);
                }
            }
        }

        if (Shoot)
        {
            currentVelo = ballRigid.velocity.magnitude;

            if (currentVelo <= 0.1f && onGround && !SetNearest)
            {
                GetComponent<Collider>().enabled = false;
                ballRigid.useGravity = false;
                ballRigid.velocity = Vector3.zero;
                if (!Goal)
                {
                    if (nearestObj.transform.parent.eulerAngles.y == 180)
                    {
                        nearestObj.startPos = new Vector3(transform.position.x, nearestObj.transform.position.y, transform.position.z + 1);
                        nearestObj.Follow = true;
                        SetNearest = true;
                    }
                    else
                    {
                        nearestObj.startPos = new Vector3(transform.position.x, nearestObj.transform.position.y, transform.position.z - 1);
                        nearestObj.Follow = true;
                        SetNearest = true;
                    }
                }
            }
        }

        if (!Kick)
        {
            if (!Turn)
            {
                currentEulerZ += Time.deltaTime * 50;

                if (currentEulerZ > maxRot)
                {
                    Turn = true;
                }
            }
            else
            {
                currentEulerZ -= Time.deltaTime * 50;

                if (currentEulerZ < minRot)
                {
                    Turn = false;
                }
            }
        }
        else
        {
            imgBar.fillAmount = (currentScale - minScale) / percentage;
            if (!Turn)
            {
                currentScale += Time.deltaTime * 500;
                currentEulerX -= Time.deltaTime * 25;

                if(currentScale >= maxScale)
                {
                    Turn = true;
                }
            }
            else
            {
                currentScale -= Time.deltaTime * 500;
                currentEulerX += Time.deltaTime * 25;

                if (currentScale <= minScale)
                {
                    Turn = false;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        onGround = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Goal")
        {
            convetti.Play();
            if(other.gameObject.name == "GoalPlayer")
            {
                Goal = true;
                isPlayerGoal = true;
                BallManager.instance.score1++;
                BallManager.instance.scoreUI1.text = BallManager.instance.score1.ToString();
            }
            else
            {
                Goal = true;
                isPlayerGoal = false;
                BallManager.instance.score2++;
                BallManager.instance.scoreUI2.text = BallManager.instance.score2.ToString();
            }
        }
    }
}
