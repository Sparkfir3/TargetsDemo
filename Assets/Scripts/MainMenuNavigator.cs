using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuNavigator : MonoBehaviour {

    /// <summary>
    /// 0 = Main Menu /
    /// 1 = Level Select /
    /// 2 = Options /
    /// 3 = Credits
    /// </summary>
	public int currentMenu;
    public RectTransform mainMenu, levelSelectMenu, optionsMenu, creditsMenu;

    public void BackToMain() {
        switch(currentMenu) {
            case 0:
                Debug.LogError("Attempting to exit to main menu from main menu.");
                break;
            case 1:
                levelSelectMenu.gameObject.SetActive(false);
                break;
            case 2:
                optionsMenu.gameObject.SetActive(false);
                break;
            case 3:
                creditsMenu.gameObject.SetActive(false);
                break;
            default:
                Debug.LogError("Attempting to exit to main menu from invalid menu position " + currentMenu + ".");
                break;
        }
        mainMenu.gameObject.SetActive(true);
        currentMenu = 0;
    }

    public void OpenLevelSelect() {
        mainMenu.gameObject.SetActive(false);
        levelSelectMenu.gameObject.SetActive(true);
        currentMenu = 1;
    }

    public void OpenOptionsMenu() {
        mainMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(true);
        currentMenu = 2;
    }

    public void OpenCreditsMenu() {
        mainMenu.gameObject.SetActive(false);
        creditsMenu.gameObject.SetActive(true);
        currentMenu = 3;
    }

    public void LoadScene(GameObject button) {
        if(Application.CanStreamedLevelBeLoaded(button.name)) {
            GameManager.isPaused = false;
            Time.timeScale = 1;
            GameManager.gm.menuCanvas.SetActive(true);
            SceneManager.LoadScene(button.name);
        } else {
            Debug.LogError("Attempted to load invalid scene: " + button.name);
        }
    }

    /*public void LoadScene(string scene) {
        try {
            GameManager.isPaused = false;
            Time.timeScale = 1;
            GameManager.gm.menuCanvas.SetActive(true);
            SceneManager.LoadScene(scene);
        } catch {
            Debug.LogError("Attempted to load invalid scene: " + scene);
        }
    }*/

}
