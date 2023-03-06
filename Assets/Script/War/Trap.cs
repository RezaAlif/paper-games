using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public GameObject paper;
    public Animator anim;
    public bool Hide;
    float timingHide;
    public float xMin, xMax;
    public float zMin, zMax;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(xMin, xMax), transform.position.y, Random.Range(zMin, zMax));
    }

    // Update is called once per frame
    void Update()
    {
        if (!Hide)
        {
            timingHide += Time.deltaTime;
            paper.SetActive(true);

            if(timingHide >= 3)
            {
                timingHide = 0;
                paper.SetActive(false);
                Hide = true;
            }
        }
    }
}
