using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        var player=other.gameObject.GetComponent<RunPlayerController>();
        if (player != null)
        {
            player.Stun(player.stunTime);

        }
        Destroy(gameObject);
    }
}
