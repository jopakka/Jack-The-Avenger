﻿using System;
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
    bool over;

    // Start is called before the first frame update
    void Start() {
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update() {
        if (over) return;

        if(PlayerHealth.IsDead) {
            StartCoroutine(WaitBeforeEnd());
            return;
        }

        if(GameObject.FindGameObjectsWithTag("FatSam").Length <= 0) {
            wonMenu.SetActive(true);
            Cursor.visible = true;
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        }
    }

    public void TogglePause() {
        if (Cursor.lockState == CursorLockMode.Locked) {
            Cursor.lockState = CursorLockMode.None;
        } else {
            Cursor.lockState = CursorLockMode.Locked;
        }

        pauseMenu.SetActive(!pauseMenu.activeSelf);
        Cursor.visible = !Cursor.visible;
        Time.timeScale = 1f - Time.timeScale;
    }

    public void QuitGame() {
        Debug.Log("Quitting game");
        Application.Quit();
    }

    public void ReloadScene() {
        PlayerHealth.IsDead = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator WaitBeforeEnd() {
        yield return new WaitForSeconds(2);
        gameOverMenu.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0f;
        over = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
