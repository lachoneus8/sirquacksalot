using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeatController : MonoBehaviour
{
    public enum BeatScore
    {
        Perfect,
        Excellent,
        Good,
        Failed
    }

    public UnityEvent<int, BeatScore> OnBeatScored;

    public float perfectY;
    public float perfectRange;
    public float excellentRange;
    public float goodRange;

    public float failY;

    public float moveSpeed;

    public bool isLowest = true;

    public KeyCode beatKey;

    public int lane;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var curPos = transform.position;
        curPos.y -= moveSpeed * Time.deltaTime;
        transform.position = curPos;

        if (Mathf.Abs(transform.localPosition.y - perfectY) < 1)
        {
            Debug.Log("p");
        }

        if (transform.localPosition.y < failY)
        {
            OnBeatScored.Invoke(lane, BeatScore.Failed);
            Destroy(gameObject);
        }

        if (isLowest && Input.GetKeyDown(beatKey))
        {
            if (WithinRange(transform.localPosition.y, perfectRange))
            {
                Debug.Log("Perfect!");
                OnBeatScored.Invoke(lane, BeatScore.Perfect);
            }
            else if (WithinRange(transform.localPosition.y, excellentRange))
            {
                Debug.Log("Excellent!");
                OnBeatScored.Invoke(lane, BeatScore.Excellent);
            }
            else if (WithinRange(transform.localPosition.y, goodRange))
            {
                Debug.Log("Good!");
                OnBeatScored.Invoke(lane, BeatScore.Good);
            }
            else
            {
                Debug.Log("Failed");
                OnBeatScored.Invoke(lane, BeatScore.Failed);
            }
            Destroy(gameObject);
        }
    }

    private bool WithinRange(float curY, float range)
    {
        return curY > perfectY - range && curY < perfectY + range;
    }
}
