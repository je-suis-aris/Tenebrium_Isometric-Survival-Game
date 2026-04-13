using UnityEngine;
using UnityEngine.SceneManagement; 

public class MapController : MonoBehaviour
{
    [Header("UI Harta")]
    public GameObject miniMapUI; 
    public GameObject bigMapUI;  

    [Header("UI Joc (De ascuns)")]
    public GameObject gameHUD;   

    private bool isMapOpen = false;

    void Start()
    {
        
        bigMapUI.SetActive(false);
        miniMapUI.SetActive(true);
        if(gameHUD != null) gameHUD.SetActive(true);
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMap();
        }

        
        
        if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.H))
        {
            GoToMainMenu();
        }
    }

    void GoToMainMenu()
    {
        
        Time.timeScale = 1f;

        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        
        
        
        SceneManager.LoadScene("Game_Menu");
    }

    void ToggleMap()
    {
        isMapOpen = !isMapOpen;

        if (isMapOpen)
        {
            
            bigMapUI.SetActive(true);
            miniMapUI.SetActive(false);
            
            
            if(gameHUD != null) gameHUD.SetActive(false);
        }
        else
        {
            
            bigMapUI.SetActive(false);
            miniMapUI.SetActive(true);

            
            if(gameHUD != null) gameHUD.SetActive(true);
        }
    }
}