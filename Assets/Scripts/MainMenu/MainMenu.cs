using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {
    public void LoadGameplayLevel()
    {
        SceneManager.LoadScene("GameplayLevel0");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
