using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    [Header("Level To Load")]
    public string newGameScene;

    [Header("Options Menu")]
    [SerializeField] private GameObject optionsMenu;

    [Header("Volume Settings")]
    [SerializeField] private TMP_Text volumeValueText = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaultVolume = 1.0f;

    private float currentVolume;

    [Header("Gameplay Settings")]
    [SerializeField] private TMP_Text controllerSenTextValue = null;
    [SerializeField] private Slider controllerSenSlider = null;
    [SerializeField] private float defaultSen = 4f;
    public int mainControllerSen = 4;

    [Header("Toggle Settings")]
    [SerializeField] private Toggle invertYToggle = null;

    private void Start()
    {
        currentVolume = PlayerPrefs.GetFloat("masterVolume", defaultVolume);
        volumeSlider.value = currentVolume;
        AudioListener.volume = currentVolume;
        volumeValueText.text = currentVolume.ToString("0.0");

        mainControllerSen = PlayerPrefs.GetInt("masterSen", (int)defaultSen);
        controllerSenSlider.value = mainControllerSen;
        controllerSenTextValue.text = mainControllerSen.ToString("0");

        int invertValue = PlayerPrefs.GetInt("masterInvertY", 0);
        invertYToggle.isOn = invertValue == 1;
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void OpenOptions()
    {
        optionsMenu.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsMenu.SetActive(false);
    }

    public void OnVolumeSliderChange(float value)
    {
        currentVolume = value;
        AudioListener.volume = value;
        volumeValueText.text = value.ToString("0.0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", currentVolume);
        PlayerPrefs.Save();
    }

    public void SetControllerSen(float sensitivity)
    {
        mainControllerSen = Mathf.RoundToInt(sensitivity);
        controllerSenTextValue.text = sensitivity.ToString("0");
    }

    public void GameplayApply()
    {
        if (invertYToggle.isOn)
            PlayerPrefs.SetInt("masterInvertY", 1);
        else
            PlayerPrefs.SetInt("masterInvertY", 0);

        PlayerPrefs.SetInt("masterSen", mainControllerSen);
        PlayerPrefs.Save();
    }

    public void ResetButton(string menuType)
    {
        if (menuType == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeValueText.text = defaultVolume.ToString("0.0");
            currentVolume = defaultVolume;
            VolumeApply();
        }

        if (menuType == "Gameplay")
        {
            controllerSenTextValue.text = defaultSen.ToString("0");
            controllerSenSlider.value = defaultSen;
            mainControllerSen = (int)defaultSen;
            invertYToggle.isOn = false;
            GameplayApply();
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
