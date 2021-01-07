using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    [SerializeField]
    Toggle keyboardToggle;
	private void Start()
	{
       
        if (SettingsManager.instance.GetSetting(SettingsManager.SettingsKeys.isKeyboardAllowed) == "True")
            keyboardToggle.isOn = true;
        else
            keyboardToggle.isOn = false;
           
    }

	public void SetVolume(float volume){
        
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetQuality(int qualityIndex){

        QualitySettings.SetQualityLevel(qualityIndex);

    }

    public void SetFullScreen(bool isFullScreen){

        Screen.fullScreen = isFullScreen;
    }

    public void SetKeyboardInput(bool isKeyboardAllowed)
	{
        SettingsManager.instance.ChangeSetting(SettingsManager.SettingsKeys.isKeyboardAllowed, isKeyboardAllowed);
    }
}
