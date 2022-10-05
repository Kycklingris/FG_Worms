using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

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
