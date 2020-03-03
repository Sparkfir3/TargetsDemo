using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {
    
    public Text[] abilityNum = new Text[5];
    public GameObject pauseMenu, selectArrow, winScreen;

    public void NextLevel() {
        GameManager.gm.NextLevel();
    }

    public void MainMenu() {
        GameManager.gm.LoadScene("MainMenu");
    }

    public void ExitGame() {
        GameManager.gm.ExitGame();
    }

}
