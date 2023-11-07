using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameScripts.MoleGameScripts
{
    public class PanelButtonControl : MonoBehaviour
    {
        MoleGameTimer _moleGameTimer;
        TrueOrFalseCheck _trueOrFalseCheck;
        SoundControl _soundControl;

        public GameObject startPanel,
            successPanel,
            gameoverPanel,
            gameFinishedPanel,
            PausePanel,
            PauseButton,
            SoundButton,
            MolePos;

        public Sprite[] SoundSprite;
        public int currentQuestion1 = 0;

        void Start()
        {
            Application.targetFrameRate = 60;
            Screen.orientation = ScreenOrientation.LandscapeRight;
            _trueOrFalseCheck = FindObjectOfType<TrueOrFalseCheck>();
            _soundControl = FindObjectOfType<SoundControl>();
            _moleGameTimer = GetComponent<MoleGameTimer>();
            Time.timeScale = 0;
        }

        public void startGame()
        {
            Time.timeScale = 1f;
            startPanel.SetActive(false);
            _moleGameTimer.StartAgain();
            PauseButton.SetActive(true);
            for (int i = 0; i < MolePos.transform.childCount; i++)
            {
                MolePos.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.GetComponent<MoleControl>()
                    .isStarted = true;
            }

            _soundControl.PlayExplainer();
        }

        public void BacktoMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void RestartLevel()
        {
            gameoverPanel.SetActive(false);
            _trueOrFalseCheck.TryAgain();
            _moleGameTimer.StartAgain();
            Time.timeScale = 1;
        }

        public void PausePanelResumeButton()
        {
            PausePanel.SetActive(false);
            Time.timeScale = 1;
            _trueOrFalseCheck.OpenHittable();
        }

        public void PausePanelButton()
        {
            PausePanel.SetActive(true);
            Time.timeScale = 0;
            _trueOrFalseCheck.CloseHittable();
        }

        public void PausePanelExitButton()
        {

        }

        public void PlayAgainButton()
        {
            SceneManager.LoadScene("MoleGameScene");
        }

        public void NextLevel()
        {
            currentQuestion1++;
            MoleControl.currentQuestion++;
            successPanel.SetActive(false);
            _trueOrFalseCheck.NextGameButtonisClicked();
            _moleGameTimer.StartAgain();
            Time.timeScale = 1f;
            PauseButton.SetActive(true);
            _soundControl.PlayQuestionButton();
        }

        public void FinishedGame(int questionCount)
        {
            if (currentQuestion1 + 1 == questionCount)
            {
                Time.timeScale = 0;
                gameFinishedPanel.SetActive(true);
                _soundControl.PlayFinishSound();
                successPanel.SetActive(false);
            }

            PauseButton.SetActive(false);
        }

        public void SoundControlButton()
        {
            if (SoundButton.GetComponent<Image>().sprite == SoundSprite[0])
            {
                SoundButton.GetComponent<Image>().sprite = SoundSprite[1];
                _soundControl.backgroundSoundObject.GetComponent<AudioSource>().volume = 0f;
            }
            else
            {
                SoundButton.GetComponent<Image>().sprite = SoundSprite[0];
                _soundControl.backgroundSoundObject.GetComponent<AudioSource>().volume = 0.03f;
            }
        }

        public void FinishGameButton()
        {
            SceneManager.LoadScene("Main");
            Screen.orientation = ScreenOrientation.Portrait;
        }
    }
}