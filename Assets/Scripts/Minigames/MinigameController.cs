using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameController : MonoBehaviour
{
    public List<string> loseMessages;
    public Image introPanel;
    public string introMessage;
    public TextMeshProUGUI introText;
    public Image losePanel;
    public TextMeshProUGUI loseText;
    public TextMeshProUGUI loseTitle;
    public string failMessage;
    public Image winPanel;
    public string winMessage;
    public TextMeshProUGUI  winText;

    public TextMeshProUGUI countdownText;
    public GameObject countdownBackground;
    public bool running;

    public Button winNextButton;
    public Button winQuestCompleteButton;
    public Image questCompletePanel;

    private static List<int> minigamesVisited = new List<int>();
    private static int minigamesWon = 0;


    // Start is called before the first frame update
    void Start()
    {
        introPanel.gameObject.SetActive(true);
        introText.text = introMessage;
        Time.timeScale = 0f;
        running = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BeginMinigame()
    {
        var curScene = SceneManager.GetActiveScene();
        minigamesVisited.Add(curScene.buildIndex);

        StartCoroutine(Countdown());
        introPanel.gameObject.SetActive(false);
 
    }
    public void Lose()
    {
        loseText.text = loseMessages[Random.Range(0, loseMessages.Count)];
        loseTitle.text = failMessage;
        losePanel.gameObject.SetActive(true);
        Time.timeScale = 0f;
        running = false;
    }
    public void Win()
    {
        winPanel.gameObject.SetActive(true);
        winText.text = winMessage;
        Time.timeScale = 0f;
        running = false;

        minigamesWon++;

        if (minigamesWon >= 5)
        {
            winNextButton.gameObject.SetActive(false);
            winQuestCompleteButton.gameObject.SetActive(true);
        }
    }
    public void LoadMinigame()
    {
        var scenesAvailable = new List<int>();

        for (int i = 2; i < SceneManager.sceneCountInBuildSettings; ++i)
        {
            if (!minigamesVisited.Contains(i))
            {
                scenesAvailable.Add(i);
            }
        }

        if (scenesAvailable.Count == 0)
        {
            minigamesVisited.Clear();

            for (int i = 2; i < SceneManager.sceneCountInBuildSettings; ++i )
            {
                scenesAvailable.Add(i);
            }
        }

        int nextGame = Random.Range(0, scenesAvailable.Count);
        while (nextGame == SceneManager.GetActiveScene().buildIndex)
        {
            nextGame = Random.Range(0, scenesAvailable.Count);
        }
        SceneManager.LoadScene(scenesAvailable[nextGame]);
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void GoToQuestComplete()
    {
        winPanel.gameObject.SetActive(false);
        questCompletePanel.gameObject.SetActive(true);
    }

    IEnumerator Countdown()
    {
        Time.timeScale = 1f;
        countdownBackground.SetActive(true);
        countdownText.text = "1!";
        yield return new WaitForSeconds(1f);
        countdownText.text = "2!";
        yield return new WaitForSeconds(1f);
        countdownText.text = "5!\n(3 sir)\n3!";
        yield return new WaitForSeconds(1f);
        countdownText.text = "Go!";
        yield return new WaitForSeconds(1f);
        running = true;
        countdownBackground.SetActive(false);
        countdownText.text = "";
    }
}
