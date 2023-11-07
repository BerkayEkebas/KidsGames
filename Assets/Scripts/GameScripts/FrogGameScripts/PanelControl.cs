using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameScripts.FrogGameScripts
{
    public class PanelControl : MonoBehaviour
    {
        public GameObject startGamePanel, finishGamePanel, shadowPos, pedalPos, PausePanel, PauseButton, musicButton;
        ObjectSpawner _objectSpawner;
        AnimationControl _animationControl;
        SoundControl _soundControl;
        public Sprite musicOn, musicOff;
        public int questionCounter = 0, allGameStage = 4;
        public bool isPaused = false;
        void Start()
        {
            Application.targetFrameRate = 60;
            Screen.orientation = ScreenOrientation.LandscapeRight;
            _animationControl = FindObjectOfType<AnimationControl>();
            _objectSpawner = FindObjectOfType<ObjectSpawner>();
            _soundControl = FindObjectOfType<SoundControl>();
        }

        public void StartGameButton()
        {
            Time.timeScale = 1f;
            startGamePanel.SetActive(false);
            SpawnAllObjects();
            PauseButton.SetActive(true);
            _animationControl.isStarted = true;
            _soundControl.PlayExplainer();
        }
        void SpawnAllObjects()
        {
            _objectSpawner.SpawnShadow();
            _objectSpawner.SpawnPedal();
            _objectSpawner.letMove = true;
        }
        public void NextLevel()
        {
            questionCounter++;
            _objectSpawner.letMoveFinish = true;
            _objectSpawner.allSpirtes.Clear();
            _objectSpawner.allShadowSpirtes.Clear();
        }
        public void DeleteAll()
        {
            DestroyAllChildren(pedalPos.transform);
            DestroyAllChildren(shadowPos.transform);

            if (questionCounter == allGameStage)  //Decide How many step child play
            {
                PauseButton.SetActive(false);
                FinishGamePanel();
            }
            else
            {
                StartNewLevel();
            }
        }
        private void DestroyAllChildren(Transform parent)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                Destroy(parent.GetChild(i).gameObject);
            }
        }
        public void PlayAgainButton()
        {
            SceneManager.LoadScene("FrogGameScene");
        }
        public void FinishGamePanel()
        {
            finishGamePanel.SetActive(true);
        }
        public void StartNewLevel()
        {

            _objectSpawner.SetAllSpritesFromGameAsstes();          
            SpawnAllObjects();
        }
        public void MusicButton()
        {
            Image musicButtonImage = musicButton.GetComponent<Image>();
            AudioSource backgroundSound = _soundControl.backgroundSoundObject.GetComponent<AudioSource>();
            if (musicButtonImage.sprite == musicOn)
            {
                musicButtonImage.sprite = musicOff;
                backgroundSound.volume = 0f;
            }
            else
            {
                musicButtonImage.sprite = musicOn;
                backgroundSound.volume = 0.05f;
            }
        }
        public void PausePanelExitButton()
        {
            SceneManager.LoadScene("Main");
            Screen.orientation = ScreenOrientation.Portrait;
        }

        public void SetPausePanelState(bool isPaused)
        {
            bool enableDragDrop = !isPaused;
            foreach (Transform pedal in pedalPos.transform) //Close DragDrop when game is pasued
            {
                pedal.GetChild(0).GetComponent<DragDrop>().enabled = enableDragDrop;
            }
            PausePanel.SetActive(isPaused);
            Time.timeScale = isPaused ? 0 : 1;
            this.isPaused = isPaused;
        }

    }
}