using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Whackum : MonoBehaviour
{
    public UnityEvent<Whackum> onLeft;

    public float targetY;
    public float moveSpeed;

    public float waitLength;

    private float startY;

    private bool rising;
    private bool falling;
    private float waitTimeLeft;

    // Start is called before the first frame update
    void Start()
    {
        waitTimeLeft = waitLength;
        startY = transform.position.y;

        rising = true;
        falling = false;
    }

    // Update is called once per frame
    void Update()
    {
        var curPos = transform.position;
        if (rising)
        {
            curPos.y += moveSpeed * Time.deltaTime;
            if (curPos.y > targetY)
            {
                curPos.y = targetY;
                rising = false;
            }
        }
        else if (falling)
        {
            curPos.y -= moveSpeed * Time.deltaTime;
            if (curPos.y < startY)
            {
                curPos.y = startY;
                falling = false;

                onLeft.Invoke(this);

                Destroy(gameObject);
            }

        }
        else // waiting
        {
            waitTimeLeft -= Time.deltaTime;

            if (waitTimeLeft < 0)
            {
                falling = true;
                waitTimeLeft = 0;
            }    
        }

        transform.position = curPos;
    }
}
