using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.MoleGameScripts
{
    public class SoundControl : MonoBehaviour
    {
        public GameObject answerSoundObject,
            explainerSoundObject,
            backgroundSoundObject,
            questionSoundButton,
            questionSoundObject;

        public List<AudioClip> correctSound,
            wrongSound,
            explainer,
            currentLanguage,
            trAudio,
            enAudio,
            finishSound,
            questionSound = new List<AudioClip>();

        public Text failText, successText;
        int randomNum;
        bool isStarted = false;
        PanelButtonControl _panelControl;
        TrueOrFalseCheck _trueOrFalseCheck;

        private void OnEnable()
        {
            currentLanguage = null;
        }

        private void Start()
        {
            _panelControl = FindObjectOfType<PanelButtonControl>();
            _trueOrFalseCheck = FindObjectOfType<TrueOrFalseCheck>();
        }


        private void SetAllSounds()
        {
            //SetExplainer
            explainer.Add(currentLanguage[0]);
            explainer.Add(currentLanguage[1]);
            //SetQuestions
            questionSound.Add(currentLanguage[2]);
            questionSound.Add(currentLanguage[3]);
            //SetFinishSound
            finishSound.Add(currentLanguage[4]);
        }

        public void PlayExplainer()
        {
            Time.timeScale = 0;
            isStarted = true;
            explainerSoundObject.GetComponent<AudioSource>().PlayOneShot(explainer[0]);
        }

        public void PlayQuestionButton()
        {
            questionSoundObject.GetComponent<AudioSource>().PlayOneShot(questionSound[_panelControl.currentQuestion1]);
        }

        public void PlayCorrectSound()
        {
            randomNum = Random.Range(0, correctSound.Count);
            answerSoundObject.GetComponent<AudioSource>().PlayOneShot(correctSound[randomNum]);
        }

        public void PlayWrongSound()
        {
            randomNum = Random.Range(0, wrongSound.Count);
            answerSoundObject.GetComponent<AudioSource>().PlayOneShot(wrongSound[randomNum]);
        }

        public void PlayFinishSound()
        {
            questionSoundObject.GetComponent<AudioSource>().PlayOneShot(finishSound[0]);
        }

        public void PlayExplainerAgain()
        {
            explainerSoundObject.GetComponent<AudioSource>().PlayOneShot(explainer[1]);
        }

        public void Update()
        {
            if (explainerSoundObject.GetComponent<AudioSource>().isPlaying ||
                questionSoundObject.GetComponent<AudioSource>().isPlaying)
            {
                questionSoundButton.GetComponent<SoundAnimation>().enabled = true;
            }
            else
            {
                questionSoundButton.GetComponent<SoundAnimation>().enabled = false;
                questionSoundButton.GetComponent<Image>().sprite =
                    questionSoundButton.GetComponent<SoundAnimation>().anim[4];
            }

            if (questionSoundButton.GetComponent<SoundAnimation>().enabled ||
                _panelControl.PausePanel.activeInHierarchy)
            {
                questionSoundButton.GetComponent<Button>().enabled = false;
            }
            else
            {
                questionSoundButton.GetComponent<Button>().enabled = true;
            }

            if (isStarted)
            {
                if (!explainerSoundObject.GetComponent<AudioSource>().isPlaying)
                {
                    PlayQuestionButton();
                    isStarted = false;
                    Time.timeScale = 1;
                }
            }

            if (_trueOrFalseCheck.wrongCounter == 2)
            {
                PlayExplainerAgain();
                _trueOrFalseCheck.wrongCounter = 0;
            }
        }
    }
}