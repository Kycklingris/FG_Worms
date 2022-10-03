using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    void OnContinue()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    void OnExit()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
