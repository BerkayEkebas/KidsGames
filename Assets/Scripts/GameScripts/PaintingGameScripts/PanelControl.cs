using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameScripts.PaintingGameScripts
{
    public class PanelControl : MonoBehaviour
    {
        public GameObject startPanel, questionPanel, finishPanel, popUp, Lines, insidePos, outsidePos, penTable, UIButtons, mainCamera, paintableArea, showPicButton, projectorScreen, projectorImage, projectorStopPos, projectorTop, saveCamera, questionImage, musicButton;
        bool pencilPopUpOpen, pencilPopUpClose, isFinished, isFlash, closeAtStartProjector, startProjector, projector = false;
        Color flashEffect = new Color(0, 0, 0, 0.035f);
        float projectorTimer = 0;
        public Sprite questionSprite, musicOn, musicOff;
        SoundControl _soundControl;
        LineGenerator _lineGenerator;
        PencilControl _pencilControl;
        void Start()
        {
            Application.targetFrameRate = 60;
            Screen.orientation = ScreenOrientation.LandscapeRight;
            _lineGenerator = FindObjectOfType<LineGenerator>();
            _pencilControl = FindObjectOfType<PencilControl>();
            _soundControl = FindObjectOfType<SoundControl>();
        }
        public void StartButton()
        {
            startPanel.SetActive(false);
            questionPanel.SetActive(true);
            startProjector = true;
            _soundControl.PlayExplainer();

        }
        public void FinishPanel()
        {
            musicButton.SetActive(false);

            if (Lines.transform.childCount < 1)
            {
                popUp.SetActive(true);
                Invoke("PopUpClose", 3f);
            }
            else
            {
                _soundControl.PlayCameraSound();
                UIButtons.SetActive(false);
                penTable.SetActive(false);
                isFinished = true;
                _lineGenerator.SetStart2();
                finishPanel.SetActive(true);
            }
        }
        void PopUpClose()
        {
            popUp.SetActive(false);
        }
        public void PencilPopUpButton()
        {
            pencilPopUpOpen = true;
            if (penTable.transform.position.x > insidePos.transform.position.x)
            {
                pencilPopUpClose = true;
            }
        }
        public void ShowPicture()
        {
            projector = !projector;
            questionPanel.SetActive(true);
        }
        void ShowPictureProjector()
        {
            if (closeAtStartProjector == false && startProjector == false)
            {
                if (projector)
                {
                    if (projectorScreen.transform.GetChild(0).gameObject.transform.position.y > projectorStopPos.transform.position.y)
                    {
                        projectorScreen.transform.position += Vector3.down * 14;
                        projectorImage.transform.position += Vector3.down * 14;
                    }
                }
                else
                {
                    if (projectorScreen.transform.GetChild(0).gameObject.transform.position.y < projectorTop.transform.GetChild(0).gameObject.transform.position.y)
                    {
                        projectorScreen.transform.position += Vector3.up * 14;
                        projectorImage.transform.position += Vector3.up * 14;

                    }
                }
            }
        }
        public void CloseShowPicture()
        {
            questionPanel.SetActive(false);
        }
        void SetCamera()
        {
            mainCamera.transform.position = paintableArea.transform.position;
            Time.timeScale = 0;
            _soundControl.PlayFinishSound();
        }
        void StartProjectorAnimation()
        {
            if (startProjector)
            {
                if (projectorScreen.transform.GetChild(0).gameObject.transform.position.y < projectorStopPos.transform.position.y)
                {
                    projectorTimer += Time.deltaTime;
                    if (projectorTimer > 3)
                    {
                        closeAtStartProjector = true;
                        startProjector = false;
                        projectorTimer = 0;
                    }
                }
                else
                {
                    projectorScreen.transform.position += Vector3.down * 10;
                    projectorImage.transform.position += Vector3.down * 10;

                }
            }
            if (closeAtStartProjector)
            {
                projectorScreen.transform.position += Vector3.up * 10;
                projectorImage.transform.position += Vector3.up * 10;
                if (projectorScreen.transform.GetChild(0).gameObject.transform.position.y > projectorTop.transform.GetChild(0).gameObject.transform.position.y)
                {
                    closeAtStartProjector = false;
                    _lineGenerator.SetStart();
                }
            }
        }
        public void SavePicture()
        {
            StartCoroutine(SaveImage());
            isFinished = true;
            _soundControl.PlayCameraSound();
        }
        public IEnumerator SaveImage()
        {
            string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
            string fileName = "Screenshot" + timeStamp + ".jpg";
            string pathToSave = fileName;
            ScreenCapture.CaptureScreenshot(pathToSave);
            yield return new WaitForEndOfFrame();
        }
        void PencilCasePopUpAnimation()
        {
            if (pencilPopUpOpen)
            {
                if (penTable.transform.position.x < insidePos.transform.position.x)
                {
                    penTable.transform.position += Vector3.right * 7;
                }
                else
                {
                    pencilPopUpOpen = false;
                    _pencilControl.SetAllPositions();
                }

            }
            if (pencilPopUpClose)
            {
                if (penTable.transform.position.x > outsidePos.transform.position.x)
                {
                    penTable.transform.position += Vector3.left * 7;
                }
                else
                {
                    pencilPopUpClose = false;

                }
            }
        }
        public void MusicButton()
        {
            if (musicButton.GetComponent<Image>().sprite == musicOn)
            {
                musicButton.GetComponent<Image>().sprite = musicOff;
                _soundControl.backgroundSoundObject.GetComponent<AudioSource>().volume = 0f;
            }
            else
            {
                musicButton.GetComponent<Image>().sprite = musicOn;
                _soundControl.backgroundSoundObject.GetComponent<AudioSource>().volume = 0.05f;
            }
        }
        void CameraFlashAnimation()
        {
            if (isFinished)
            {
                finishPanel.GetComponent<Image>().color += flashEffect;
                if (finishPanel.GetComponent<Image>().color.a > 0.7f)
                {
                    isFlash = true;
                    isFinished = false;
                }
            }
            if (isFlash)
            {
                finishPanel.GetComponent<Image>().color -= flashEffect;
                if (finishPanel.GetComponent<Image>().color.a < 0.02f)
                {
                    isFlash = false;
                    Invoke("SetCamera", 0.4f);
                }
            }
        }
        public void ExitButton()
        {
            SceneManager.LoadScene("Main");
            Screen.orientation = ScreenOrientation.Portrait;
        }
        void Update()
        {
            StartProjectorAnimation();
            PencilCasePopUpAnimation();
            CameraFlashAnimation();
            ShowPictureProjector();
        }
    }
}