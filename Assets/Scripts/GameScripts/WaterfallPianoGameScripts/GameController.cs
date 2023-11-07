using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameScripts.WaterfallPianoGameScripts
{
    public class GameController : MonoBehaviour
    {
        public GameObject
            startGameFairy,
            startGamePanel,
            musicChoosingPanel,
            startGameButton,
            clouds,
            backgroundImage,
            gameSoundObject;
        public static GameObject tempObject; // For Cloud Animation
        public AudioClip sparkleSound, sparkleEffectSound, trExplainer, enExplainer, currentExplainer;
        public Sprite fairyStartFlySprite, fairyLastSprite;
        public RectTransform cloudStopPos;
        PlayPianoController _playPianoController;
        List<string> musics = new List<string>();
        float cloudSpeed = 17f;
        public static bool cloudAnim = false;
        private bool isCloudAnimationRunning = false, startGameExplainerSound = false;


        void Start()
        {
            Application.targetFrameRate = 60;
            Screen.orientation = ScreenOrientation.LandscapeRight;
            _playPianoController = FindObjectOfType<PlayPianoController>();
        }
        public void StartGameButton()
        {
            cloudAnim = true;
            tempObject = startGamePanel;
            startGameButton.GetComponent<Button>().interactable = false;
            startGameFairy.GetComponent<Animator>().enabled = true;
            musicChoosingPanel.SetActive(true);
            startGameExplainerSound = true;
            SetAllMusicTemp();
        }
        void ControlFairyAndExplainerSound()
        {
            if (startGameFairy.GetComponent<Image>().sprite == fairyStartFlySprite && !gameSoundObject.GetComponent<AudioSource>().isPlaying) //Start Effect Sound While Fairy Fly.
            {
                gameSoundObject.GetComponent<AudioSource>().PlayOneShot(sparkleSound);
            }
            else if (!startGameFairy.GetComponent<Animator>().enabled && !gameSoundObject.GetComponent<AudioSource>().isPlaying) //Start Explainer Sound When Fairy Animation end.
            {
                gameSoundObject.GetComponent<AudioSource>().PlayOneShot(currentExplainer);
                startGameExplainerSound = false;
            }
        }
        public void SetAllMusicTemp()
        {
            musics.Add("0.3.3.3.5.7.5.3.4.2.0.7.5.3.0.3.3.3.5.7.5.3.4.0.0.3"); //Otobusun tekeri
            musics.Add(
                "4.2.2.2.4.2.2.2.4.5.4.2.3.1.3.1.1.1.3.1.1.1.3.4.3.1.2.0.4.2.2.2.4.2.2.2.4.5.4.2.3.1.3.1.1.1.3.1.1.1.3.4.3.1.2.0."); //Kucuk kurbaga
            musics.Add("0.01.0.1.2.2.1.01.0.1.2.2.1.0.01.0.1.2.2.1.01.0.1.2.2.1."); //Kirmizi Balik
            musics.Add(
                "0.2.4.4.0.2.4.4.5.7.4.5.3.3.4.2.2.1.2.2.0.0.2.4.4.0.2.4.4.5.7.4.5.3.3.4.2.2.1.2.2.0"); //Parmak ailesi
            musics.Add(
                "4.3.4.5.4.3.2.1.3.2.1.2.4.3.4.5.4.3.2.1.3.2.1.2.4.3.4.5.4.3.2.1.3.2.1.2.4.3.4.5.4.3.2.1.3.2.1.0."); //Bak Postaci geliyor
            musics.Add(
                "4.444.1.4.5.6.7.7.5.5.6.5.4.7.7.5.5.6.6.4.4.7.7.5.5.6.6.4.4.4.444.1.4.5.6.7.7.5.5.6.5.4."); //mini mini bir kus
        }

        public void SetMusicFromMusicChoosingPanel(int i)
        {
            _playPianoController.isSongFinished = false;
            _playPianoController.SetMusic(musics[i]);
            cloudAnim = true;
            tempObject = musicChoosingPanel;
            backgroundImage.SetActive(true);
        }
        void CheckSongFinished()
        {
            if (_playPianoController.isSongFinished)
            {
                musicChoosingPanel.SetActive(true);
                _playPianoController.sparkle.SetActive(false);
                gameSoundObject.GetComponent<AudioSource>().volume = 0.6f;
                gameSoundObject.GetComponent<AudioSource>().Stop();
                backgroundImage.SetActive(false);
            }
        }
        void MusicSetButtonsActive(bool set)
        {
            bool enableButtons = set;

            foreach (Transform child in musicChoosingPanel.transform)
            {
                Button button = child.GetComponent<Button>();
                if (button != null)
                {
                    button.enabled = enableButtons;
                }
            }
        }
        void SparkleEffectSoundControl()
        {
            if (_playPianoController.sparkle.activeInHierarchy)
            {
                gameSoundObject.GetComponent<AudioSource>().PlayOneShot(sparkleEffectSound);
                gameSoundObject.GetComponent<AudioSource>().volume -= 0.004f;
            }
        }
        IEnumerator CloudAnimationCoroutine(GameObject panel)
        {
            if (startGameFairy.GetComponent<Image>().sprite == fairyLastSprite)
            {
                startGameFairy.GetComponent<Animator>().enabled = false;
                float cloudAnimTimer = 0f;

                while (cloudAnimTimer <= 5f)
                {
                    if (cloudAnimTimer > 2.5f && clouds.transform.GetChild(0).gameObject.GetComponent<RectTransform>().anchoredPosition.x > cloudStopPos.anchoredPosition.x)
                    {
                        MoveClouds(-cloudSpeed);
                        panel.SetActive(false);
                        CheckSongFinished();
                        _playPianoController.DeleteDroppingObjects();
                    }
                    else
                    {
                        MusicSetButtonsActive(false);
                        MoveClouds(cloudSpeed);
                    }

                    yield return null;
                    cloudAnimTimer += Time.deltaTime;
                }

                MusicSetButtonsActive(true);
                cloudAnim = false;
            }

            isCloudAnimationRunning = false;
        }
        void MoveClouds(float speed)
        {
            clouds.transform.GetChild(0).gameObject.transform.position += Vector3.right * speed;
            clouds.transform.GetChild(1).gameObject.transform.position += Vector3.left * speed;
        }
        public void PausePanelExitButton()
        {
            SceneManager.LoadScene("Main");
            Screen.orientation = ScreenOrientation.Portrait;
        }
        void Update()
        {
            if (!isCloudAnimationRunning && cloudAnim)
            {
                isCloudAnimationRunning = true;
                StartCoroutine(CloudAnimationCoroutine(tempObject));
            }

            SparkleEffectSoundControl();

            if (startGameExplainerSound)
            {
                ControlFairyAndExplainerSound();
            }

        }
    }
}