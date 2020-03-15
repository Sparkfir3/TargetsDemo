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
    private float resetTimer;

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
            if(IsPaused)
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

        // Reset Level
        if(Input.GetButton("Reset")) {
            resetTimer += Time.deltaTime;
            if(resetTimer >= 1f) {
                ResetLevel();
                resetTimer = -100f;
            }
        } else
            resetTimer = 0;
    }

    public void LoadScene(string scene) {
        if(Application.CanStreamedLevelBeLoaded(scene)) {
            OnLoadNewScene();
            SceneManager.LoadScene(scene);
        } else {
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

    public void ResetLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel() {
        //Debug.Log("next");
        TargetManager.winBuffer = 0;
        TargetManager.canWin = false;
        UnpauseGame();
        OnLoadNewScene();
        if(Application.CanStreamedLevelBeLoaded(SceneManager.GetActiveScene().buildIndex + 1))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
            SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void OnLoadNewScene() {
        if(IsPaused)
            UnpauseGame();
        if(SceneManager.GetActiveScene().name.Equals("MainMenu"))
            menuCanvas.SetActive(false);
        else
            menuCanvas.SetActive(true);
        winScreen.SetActive(false);
        hasWon = false;
        selectArrow.GetComponent<RectTransform>().localPosition = new Vector2(15, 42.5f);
    }

}
