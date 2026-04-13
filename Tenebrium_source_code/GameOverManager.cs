using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameOverManager : MonoBehaviour
{
    
    public void GoToMainMenu()
    {
        
        Time.timeScale = 1f;

        
        
        SceneManager.LoadScene("Game_Menu");
    }
}