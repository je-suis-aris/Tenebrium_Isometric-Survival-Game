using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoadPrefs : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private bool canUse = true;
    [SerializeField] private MenuController menuController;

    [Header("Volume")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;

    [Header("Sensitivity")]
    [SerializeField] private TMP_Text controllerSenTextValue = null;
    [SerializeField] private Slider controllerSenSlider = null;

    [Header("Invert Y")]
    [SerializeField] private Toggle invertYToggle = null;

    private void Awake()
    {
        if (!canUse)
            return;


        if (PlayerPrefs.HasKey("masterVolume"))
        {
            float v = PlayerPrefs.GetFloat("masterVolume");
            volumeSlider.value = v;
            volumeTextValue.text = v.ToString("0.0");
            AudioListener.volume = v;
        }
        else
        {
            menuController.ResetButton("Audio");
        }

        
        if (PlayerPrefs.HasKey("masterSen"))
        {
            float s = PlayerPrefs.GetFloat("masterSen");
            controllerSenSlider.value = s;
            controllerSenTextValue.text = s.ToString("0");
            menuController.mainControllerSen = Mathf.RoundToInt(s);
        }

        
        if (PlayerPrefs.HasKey("masterInvertY"))
        {
            invertYToggle.isOn = PlayerPrefs.GetInt("masterInvertY") == 1;
        }
    }
}
