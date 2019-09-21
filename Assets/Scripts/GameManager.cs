using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager gm;
    public GameObject menuCanvas;

    [HideInInspector] public GameObject pauseMenu, selectArrow, winScreen;

    public static int currentLevel = 0;
    public static bool isPaused, hasWon = false;

    private void Awake() {
        if(gm == null) {
            gm = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(menuCanvas);
            pauseMenu.SetActive(false);
            isPaused = false;
            pauseMenu = menuCanvas.GetComponent<CanvasManager>().pauseMenu;
            selectArrow = menuCanvas.GetComponent<CanvasManager>().selectArrow;
            winScreen = menuCanvas.GetComponent<CanvasManager>().winScreen;
        } else {
            Destroy(menuCanvas.gameObject);
            Destroy(gameObject);
        }
    }

    private void Update() {
        if(Input.GetButtonDown("Pause")) {
            if(isPaused)
                UnpauseGame();
            else
                PauseGame();
        }
        if(TargetManager.canWin) {
            if(!hasWon) {
                TargetManager.canWin = false;
                hasWon = true;
                TargetManager.winBuffer = 0;
                Win();
            }
        }/* else {
            winScreen.SetActive(false);
        }*/
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

    public void Win() {
        PauseGame();
        pauseMenu.SetActive(false);
        winScreen.SetActive(true);
        //Debug.Log("win");
    }

    public void NextLevel() {
        //Debug.Log("next");
        TargetManager.winBuffer = 0;
        TargetManager.canWin = false;
        winScreen.SetActive(false);
        UnpauseGame();
        try {
            if(SceneManager.GetActiveScene().buildIndex == 16)
                SceneManager.LoadScene("MainMenu");
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        } catch {
            SceneManager.LoadScene("MainMenu");
        }
        hasWon = false;
    }

    public void ExitGame() {
        Application.Quit();
    }

}
