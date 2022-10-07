using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LossScreenHandler : MonoBehaviour
{
    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnExit()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
