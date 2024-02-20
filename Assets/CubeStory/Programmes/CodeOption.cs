using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CodeOption : MonoBehaviour
{

    public AudioMixer mainMixer;

    public void SetVolume(float volume)
        {
            mainMixer.SetFloat("Volume",volume);
        }


    // changer la qualité dans les parametres
   public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

}
