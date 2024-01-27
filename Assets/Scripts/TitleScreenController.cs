using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        int nextGame = Random.Range(2, SceneManager.sceneCountInBuildSettings);
        SceneManager.LoadScene(nextGame);
    }
    public void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}
