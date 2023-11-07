using System.Collections.Generic;
using UnityEngine;

namespace GameScripts.FrogGameScripts
{
    public class SoundControl : MonoBehaviour
    {
        public GameObject answerSoundObject, explainerSoundObject, answerEffectSoundObject, backgroundSoundObject, effectSoundObject;
        public List<AudioClip> correctSound, correctEffectSound, wrongSound, wrongEffectSound, explainer, currentLanguage, trAudio, enAudio = new List<AudioClip>();
        int randomNum;
        private void OnEnable()
        {
            currentLanguage = null;
        }
        private void SetLanguage(string language)
        {
            if (language == "tr")
            {
                currentLanguage = trAudio;
            }
            else if (language == "en")
            {
                currentLanguage = enAudio;
            }
            SetAllSounds();
        }
        private void SetAllSounds()
        {
            //SetExplainer
            explainer.Add(currentLanguage[0]);
            //SetCorrectSounds
            correctSound.Add(currentLanguage[1]);
            correctSound.Add(currentLanguage[2]);
            correctSound.Add(currentLanguage[3]);
            correctSound.Add(currentLanguage[4]);
            //SetWrongSounds
            wrongSound.Add(currentLanguage[5]);
            wrongSound.Add(currentLanguage[6]);
            wrongSound.Add(currentLanguage[7]);
        }
        public void PlayExplainer()
        {
            explainerSoundObject.GetComponent<AudioSource>().PlayOneShot(explainer[0]);
        }
        public void PlayCorrectSound()
        {
            randomNum = Random.Range(0, correctSound.Count);
            answerSoundObject.GetComponent<AudioSource>().PlayOneShot(correctSound[randomNum]);
            answerEffectSoundObject.GetComponent<AudioSource>().PlayOneShot(correctEffectSound[0]);

        }
        public void PlayWrongSound()
        {
            randomNum = Random.Range(0, wrongSound.Count);
            answerSoundObject.GetComponent<AudioSource>().PlayOneShot(wrongSound[randomNum]);
            answerEffectSoundObject.GetComponent<AudioSource>().PlayOneShot(wrongEffectSound[0]);
        }
    }
}