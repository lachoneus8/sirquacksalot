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
    public Image winPanel;
    public string winMessage;
    public TextMeshProUGUI  winText;

    public TextMeshProUGUI countdownText;
    public GameObject countdownBackground;
    public bool running;
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
        StartCoroutine(Countdown());
        introPanel.gameObject.SetActive(false);
 
    }
    public void Lose()
    {
        loseText.text = loseMessages[Random.Range(0, loseMessages.Count - 1)];
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
    }
    public void LoadMinigame()
    {
        SceneManager.LoadScene(Random.Range(3, SceneManager.sceneCountInBuildSettings-1));
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
