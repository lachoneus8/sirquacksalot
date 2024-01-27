using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RythmController : MonoBehaviour
{
    public enum Lane
    {
        Lane1,
        Lane2,
        Lane3,
        Lane4
    }

    public float beatSpeed = 50;

    public float startY = 0;
    public float targetY;

    public List<GameObject> beatPrefabs;

    public List<GameObject> lanes;

    private Queue<BeatController> lane1Beats = new Queue<BeatController>();
    private Queue<BeatController> lane2Beats = new Queue<BeatController>();
    private Queue<BeatController> lane3Beats = new Queue<BeatController>();
    private Queue<BeatController> lane4Beats = new Queue<BeatController>();

    private List<Queue<BeatController>> beatQueues = new List<Queue<BeatController>>();

    public float beatSpawnCountdown;

    public GameObject targetLine;

    public MinigameController minigameController;

    public GameObject rythmPanel;

    private float nextBeat;

    private int failures = 0;
    private int successes = 0;

    private void Start()
    {
        nextBeat = beatSpawnCountdown;

        beatQueues.Add(lane1Beats);
        beatQueues.Add(lane2Beats);
        beatQueues.Add(lane3Beats);
        beatQueues.Add(lane4Beats);
    }

    void Update()
    {
        if (!minigameController.running)
        {
            return;
        }

        rythmPanel.gameObject.SetActive(true);

        nextBeat -= Time.deltaTime;

        if ( nextBeat < 0)
        {
            SpawnBeat();
            nextBeat = beatSpawnCountdown;
        }

        foreach (var beatQueue in beatQueues)
        {
            if (beatQueue.Count > 0)
            {
                beatQueue.Peek().isLowest = true;
            }
        }
    }

    private void SpawnBeat()
    {
        var beatLane = UnityEngine.Random.Range(0, lanes.Count);
        var beatObject = Instantiate(beatPrefabs[beatLane], lanes[beatLane].transform);
        var beat = beatObject.GetComponent<BeatController>();
        beat.OnBeatScored.AddListener(HandleBeatScored);
        beat.lane = beatLane;
        beat.perfectY = targetY;
        beat.failY = beat.perfectY - 40;
        beat.moveSpeed = beatSpeed;
        beat.isLowest = false;

        beatQueues[beatLane].Enqueue(beat);
    }

    private void HandleBeatScored(int lane, BeatController.BeatScore score)
    {
        beatQueues[lane].Dequeue();

        if (score == BeatController.BeatScore.Failed)
        {
            failures++;
        }
        else
        {
            successes++;
        }

        if (failures >= 3)
        {
            rythmPanel.gameObject.SetActive(false);
            minigameController.Lose();
        }

        if (successes >= 5)
        {
            rythmPanel.gameObject.SetActive(false);
            minigameController.Win();
        }
    }
}
