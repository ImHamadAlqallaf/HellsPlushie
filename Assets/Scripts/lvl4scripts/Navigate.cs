using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigate : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;

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

    public void backToMain()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

   

}
