using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarPlayer : MonoBehaviour
{
    public Camera m_camera;
    public GameObject brush;

    LineRenderer currentLine;

    Vector3 lastPos;

    public bool starting, placing, isDraw;
    public float timingStart;

    public Text txtField;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (placing)
        {
            timingStart += Time.deltaTime;
            if (!starting)
            {
                txtField.text = "Wait";
            }
            else
            {
                txtField.text = "Go";
            }
        }
        else
        {
            if (!starting)
            {
                txtField.text = "Place Pointer To Green Field";
            }
        }


        if(timingStart >= 3)
        {
            starting = true;
            timingStart = 0;
        }
        if (starting)
        {
            Draw();
        }
    }

    private void Draw()
    {
        Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (!isDraw)
            {
                CreateBrush();
                isDraw = true;
            }

            Vector3 mousePos = new Vector3(hit.point.x, 0.01f, hit.point.z);
            transform.position = mousePos;
            if(mousePos != lastPos)
            {
                AddPoint(mousePos);
                lastPos = mousePos;
            }
        }
    }

    void CreateBrush()
    {
        GameObject brushInstance = Instantiate(brush);
        currentLine = brushInstance.GetComponent<LineRenderer>();

        if (!WarManager.instance.turn)
        {
            currentLine.material.color = SaveManager.Instance.player1Color;
        }
        else
        {
            currentLine.material.color = SaveManager.Instance.player2Color;
        }
    }

    void AddPoint(Vector3 point)
    {
        currentLine.positionCount++;
        int positionIndex = currentLine.positionCount - 1;
        currentLine.SetPosition(positionIndex, point);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Trap" && starting)
        {
            starting = false;
            other.GetComponent<Trap>().paper.SetActive(true);
            other.GetComponent<Trap>().anim.Play("PenBroke");
            other.GetComponent<Trap>().Hide = false;
            WarManager.instance.playerStart = false;
            isDraw = false;
        }
        if(other.tag == "Player" && !starting)
        {
            placing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            placing = false;
            timingStart = 0;
        }
        if(other.name == "Ground" && starting)
        {
            starting = false;
            WarManager.instance.playerStart = false;
            isDraw = false;
        }
    }
}
