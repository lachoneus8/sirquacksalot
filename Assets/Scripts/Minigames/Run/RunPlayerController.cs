using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RunPlayerController : MonoBehaviour
{
    public MinigameController controller;
    public float distanceRan;
    public float playerSpeed;
    public float strafeSpeed;
    public float gooseCatchDist;
    public float stunTime;
    bool stunned = false;

    public float obstacleDist;//How far apart obstacles are
    public float obstacleOffset;//
    public float obstacleBounds;//How far on either side of 0 can the obstacle spawn
    public float obstacleAmount;
    public float startObstacleAmount;
    public Obstacle obstaclePrefab;

    public CharacterController player;
    public CharacterController goose;
    public Transform background;
    // Start is called before the first frame update
    void Start()
    {
        for(float i = obstacleDist; i <= obstacleOffset; i += obstacleDist)
        {
            SpawnObstacles(i, startObstacleAmount);
        }
        obstacleAmount = startObstacleAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.running)
        {
            float oldZ=player.transform.position.z;
            var movementDir = new Vector3(0, 0, Time.deltaTime * playerSpeed);
            if(!stunned )
            {
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    movementDir.x += strafeSpeed * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    movementDir.x -= strafeSpeed * Time.deltaTime;
                }
                player.Move(movementDir);
                distanceRan += (transform.position.z - oldZ);
            }
            goose.Move(movementDir);
            background.transform.position = new Vector3(0, 0, transform.position.z);


            if (Vector3.Distance(player.transform.position, goose.transform.position) <= gooseCatchDist)
            {
                controller.Lose();
            }
        }
    }
    public void Stun(float time)
    {
        StartCoroutine(StunRoutine(time));
    }
    void SpawnObstacles(float offset,float amount)
    {
        float generateDist = obstacleBounds * 2/amount;
        for (int i = 0; i < amount; i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-obstacleBounds+(generateDist*i), -obstacleBounds + (generateDist * (i+1))),0,transform.position.y+offset);
            Instantiate(obstaclePrefab,spawnPos,Quaternion.identity);
        }
    }

    IEnumerator StunRoutine(float delay)
    {
        stunned = true;
        yield return new WaitForSeconds(delay);
        stunned = false;
    }
}
