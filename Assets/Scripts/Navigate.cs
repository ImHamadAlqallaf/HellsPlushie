using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigate : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject instructionsMenu;
    public GameObject creditsMenu; 

    public void goToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void openSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void openInstructions()
    {
        mainMenu.SetActive(false);
        instructionsMenu.SetActive(true);
    }

    public void openCredits() 
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void backToMain()
    {
        settingsMenu.SetActive(false);
        instructionsMenu.SetActive(false);
        creditsMenu.SetActive(false); 
        mainMenu.SetActive(true);
    }
}
