﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject MapMenu;

    public bool isPaused = false;

    void Start()
    {
        //SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLo)
        MapMenu.SetActive(false);
        pauseMenu.SetActive(false);
        //PolygonCollider2D savedMapBoundry = GameObject.Find(saveData.mapBoundary).GetComponent<PolygonCollider2D>();
        MapController_Dynamic.Instance?.GenerateMap();
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void OpenMap()
    {
        MapMenu.SetActive(true);
    }
    public void CloseMap()
    {
        MapMenu.SetActive(false);
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void RestartGame()
    {
        // Reset the timescale (in case it was paused)
        Time.timeScale = 1f;

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Debug.Log("Game Restarted!"); // Optional confirmation
    }
}