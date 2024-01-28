using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhackController : MonoBehaviour
{
    public MinigameController minigameController;

    public List<GameObject> gooseHoles;

    public GameObject goosePrefab;

    public float gooseAppearTime;

    public LayerMask whackmeLayer;

    private float nextGooseTime;

    private List<Whackum> activeGeese = new List<Whackum>();

    private int failures = 0;
    private int successes = 0;

    // Start is called before the first frame update
    void Start()
    {
        nextGooseTime = gooseAppearTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!minigameController.running)
        {
            return;
        }

        nextGooseTime -= Time.deltaTime;

        if (nextGooseTime < 0 )
        {
            nextGooseTime = gooseAppearTime;

            var holeIdx = UnityEngine.Random.Range( 0, gooseHoles.Count );
            var hole = gooseHoles[ holeIdx ];

            var gooseGameObject = Instantiate(goosePrefab, hole.transform);
            var gooseScript = gooseGameObject.GetComponent<Whackum>();
            gooseScript.onLeft.AddListener(HandleGooseLeft);
            activeGeese.Add(gooseScript);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, whackmeLayer))
            {
                var gooseChild = hit.transform.gameObject;
                Whackum gooseScript = null;
                if (gooseChild.transform.parent != null)
                {
                    gooseScript = gooseChild.transform.parent.gameObject.GetComponent<Whackum>();
                }
                if (gooseScript != null)
                {
                    successes++;

                    activeGeese.Remove(gooseScript);

                    Destroy(gooseScript.gameObject);

                    if (successes >= 5)
                    {
                        minigameController.Win();
                    }
                }
            }
        }
    }

    private void HandleGooseLeft(Whackum gooseScript)
    {
        activeGeese.Remove(gooseScript);

        failures++;

        if (failures >= 3)
        {
            minigameController.Lose();
        }
    }
}
