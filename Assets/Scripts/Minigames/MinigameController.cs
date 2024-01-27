using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class MinigameController : MonoBehaviour
{
    public List<string> loseMessages;
    public Image introPanel;
    public string introMessage;
    public TextMeshProUGUI introText;
    public Image losePanel;
    public TextMeshProUGUI loseText;
    public Image winPanel;
    // Start is called before the first frame update
    void Start()
    {
        introPanel.gameObject.SetActive(true);
        introText.text = introMessage;
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BeginMinigame()
    {
        Time.timeScale = 1f;
        introPanel.gameObject.SetActive(false);
    }
    public void Lose()
    {
        loseText.text = loseMessages[Random.Range(0, loseMessages.Count - 1)];
        losePanel.gameObject.SetActive(true);
    }
    public void Win()
    {
        winPanel.gameObject.SetActive(true);
    }
    public void LoadMinigame()
    {

    }
}
