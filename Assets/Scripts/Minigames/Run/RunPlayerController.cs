using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunPlayerController : MonoBehaviour
{
    public MinigameController controller;
    public float timeAlive;
    public float playerSpeed;
    public float strafeSpeed;

    public CharacterController player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var movementDir = new Vector3(0, 0, Time.deltaTime * playerSpeed);
        if (Input.GetKey(KeyCode.RightArrow))
        {
            movementDir.x += strafeSpeed*Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            movementDir.x -= strafeSpeed * Time.deltaTime;
        }
        player.Move(movementDir);
        timeAlive += Time.deltaTime;
    }
}
