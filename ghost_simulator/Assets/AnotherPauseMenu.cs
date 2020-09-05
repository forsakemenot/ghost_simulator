using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;
public class AnotherPauseMenu : MonoBehaviour
{
    public static bool isPause = false;
    public GameObject pauseMenuUI;

    private FirstPersonController firstPersonController;

    private void Start()
    {
        firstPersonController = (FirstPersonController)FindObjectOfType(typeof(FirstPersonController));
    }


    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    if (isPause)
        //    {
        //        HideCursor();
        //        Resume();
        //    }
        //    else
        //    {
        //        ShowCursor();
        //        Pause();
        //    }
        //}
    }

    private void HideCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }

    private void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void Resume ()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;
        firstPersonController.IsPause = false;
    }

    void Pause ()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPause = true;
        firstPersonController.IsPause = true;
    }

    public void LoadMenu ()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        firstPersonController.IsPause = false;
    }

    public void QuitGame ()
    {
        Application.Quit();
    }
}
