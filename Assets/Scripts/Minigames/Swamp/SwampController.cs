using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwampController : MonoBehaviour
{
    public Rigidbody player;
    public float jumpDist;
    public float deathThreshold;
    public float edgeBounds;
    public float winThreshold;

    public float logSpawnX;
    public float logWrapX;
    public GameObject lilyPad;
    public MinigameController controller;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = -18; i <= 18; i+=3)
        {
            Instantiate(lilyPad, new Vector3(i, -0.95f,9f), lilyPad.transform.rotation);
            Instantiate(lilyPad, new Vector3(i, -0.95f, -9f), lilyPad.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.running)
        {
            if(Mathf.Abs(player.velocity.y) < 0.1)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    player.transform.position += Vector3.left*jumpDist;
                    player.transform.localEulerAngles = new Vector3(0,-90,0);
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    player.transform.position += Vector3.right * jumpDist;
                    player.transform.localEulerAngles = new Vector3(0, 90, 0);
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    player.transform.position += Vector3.forward*jumpDist;
                    player.transform.localEulerAngles = new Vector3(0, 0, 0);
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    player.transform.position += Vector3.back*jumpDist;
                    player.transform.localEulerAngles = new Vector3(0, 180, 0);
                }
            }
            if (player.transform.position.y <= deathThreshold||Mathf.Abs(player.transform.position.x)>=edgeBounds)
            {
                controller.Lose();
            }
            else if (player.transform.position.z >= winThreshold)
            {
                controller.Win();
            }
        }
    }
}
