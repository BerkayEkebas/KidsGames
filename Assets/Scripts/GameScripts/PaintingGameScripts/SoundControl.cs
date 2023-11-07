using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.PaintingGameScripts
{
    public class SoundControl : MonoBehaviour
    {
        public GameObject  explainerSoundObject, backgroundSoundObject;
        public List<AudioClip> explainer, currentLanguage, trAudio, enAudio, finishSound= new List<AudioClip>();
        public AudioClip cameraSound;
        public Text startGameText;
        int randomNum;
        private void SetAllSounds()
        {
            //SetExplainer
            explainer.Add(currentLanguage[0]);
            //SetFinishSound
            finishSound.Add(currentLanguage[1]);
            finishSound.Add(currentLanguage[2]);
            finishSound.Add(currentLanguage[3]);
        }
        public void PlayExplainer()
        {
            explainerSoundObject.GetComponent<AudioSource>().PlayOneShot(explainer[0]);
        }
        public void PlayCameraSound()
        {
            explainerSoundObject.GetComponent<AudioSource>().PlayOneShot(cameraSound);
        }
        public void PlayFinishSound()
        {
            randomNum = Random.Range(0, finishSound.Count);
            explainerSoundObject.GetComponent<AudioSource>().PlayOneShot(finishSound[randomNum]);
        }

    }

}