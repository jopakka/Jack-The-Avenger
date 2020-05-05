using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour {
    [SerializeField]
    GameObject pauseMenu;
    [SerializeField]
    GameObject gameOverMenu;
    [SerializeField]
    GameObject wonMenu;
    PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start() {
        Time.timeScale = 1f;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update() {
        if(playerHealth.IsDead) {
            gameOverMenu.SetActive(true);
            Cursor.visible = true;
            Time.timeScale = 0f;
            return;
        }

        if(GameObject.FindGameObjectsWithTag("FatSam").Length <= 0) {
            wonMenu.SetActive(true);
            Cursor.visible = true;
            Time.timeScale = 0f;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        }
    }

    public void TogglePause() {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        Cursor.visible = !Cursor.visible;
        Time.timeScale = 1f - Time.timeScale;
    }

    public void QuitGame() {
        Debug.Log("Quitting game");
        Application.Quit();
    }

    public void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
