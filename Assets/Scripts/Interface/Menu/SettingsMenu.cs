using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
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
