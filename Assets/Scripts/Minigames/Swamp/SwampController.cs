using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwampController : MonoBehaviour
{
    public Rigidbody player;
    public float jumpDist;
    public MinigameController controller;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.running)
        {
            if(player.velocity.sqrMagnitude < 0.1)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    player.transform.position += Vector3.left*jumpDist;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    player.transform.position += Vector3.right * jumpDist;
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    player.transform.position += Vector3.forward*jumpDist;
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    player.transform.position += Vector3.back*jumpDist;
                }
            }
        }
    }
}
