using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void StartOnClick()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
