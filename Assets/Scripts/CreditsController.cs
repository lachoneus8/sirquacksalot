using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    // Start is called before the first frame update
    public void GoToTitle()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
