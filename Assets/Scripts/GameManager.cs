using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager gm;
    public GameObject menuCanvas;

    [HideInInspector] public GameObject pauseMenu, selectArrow;

    private bool isPaused;

    private void Awake() {
        if(gm == null) {
            gm = this;
            DontDestroyOnLoad(this.gameObject);
            DontDestroyOnLoad(menuCanvas);
            pauseMenu.SetActive(false);
            isPaused = false;
            pauseMenu = menuCanvas.GetComponent<CanvasManager>().pauseMenu;
            selectArrow = menuCanvas.GetComponent<CanvasManager>().selectArrow;
        } else {
            Destroy(menuCanvas.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void Update() {
        if(Input.GetButtonDown("Pause")) {
            if(isPaused)
                UnpauseGame();
            else
                PauseGame();
        }
    }

    public void LoadScene(string scene) {
        try {
            if(isPaused)
                UnpauseGame();
            if(scene.Equals("MainMenu"))
                menuCanvas.SetActive(false);
            else
                menuCanvas.SetActive(true);
            SceneManager.LoadScene(scene);
        } catch {
            Debug.LogError("Attempted to load invalid scene: " + scene);
        }
    }

    public void PauseGame() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void UnpauseGame() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public bool IsPaused {
        get {
            return isPaused;
        }
        set {}
    }

    public void ExitGame() {
        Application.Quit();
    }

}
