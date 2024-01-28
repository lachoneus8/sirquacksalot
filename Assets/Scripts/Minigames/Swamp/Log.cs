using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    public SwampController controller;
    public float moveSpeed;
    Collider myCollider;
    // Start is called before the first frame update
    void Start()
    {
        controller=FindObjectOfType<SwampController>();
        myCollider=GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right* moveSpeed*Time.deltaTime;
        if (transform.position.x >= controller.logWrapX&&moveSpeed>0)
        {
            transform.position = new Vector3(controller.logSpawnX, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= controller.logSpawnX && moveSpeed < 0)
        {
            transform.position = new Vector3(controller.logWrapX, transform.position.y, transform.position.z);
        }
        var playerPos = controller.player.transform.position;
        if (playerPos.x<=myCollider.bounds.max.x&& playerPos.x >= myCollider.bounds.min.x&& playerPos.z <= myCollider.bounds.max.z && playerPos.z >= myCollider.bounds.min.z)
        {
            controller.player.transform.position+= Vector3.right * moveSpeed * Time.deltaTime;
        }
    }
}
