using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasBehaviour : MonoBehaviour
{
    public void ReloadScene()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        // Load the menu
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGame()
    {
        // Load the game
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }
}
