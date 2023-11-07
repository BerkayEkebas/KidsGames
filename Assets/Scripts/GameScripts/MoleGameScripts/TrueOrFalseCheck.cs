using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.MoleGameScripts
{
    public class TrueOrFalseCheck : MonoBehaviour
    {
        public GameObject panelGameOver, panelSuccess, MolePos;
        public Sprite[] happyMole, sadMole;
        PanelButtonControl _panelButtonControl;
        GettingData _gettingData;
        AnswersAnimation _answerAnim;
        MoleGameTimer _moleGameTimer;
        ScoreBarControl _scoreBarControl;
        SoundControl _soundControl;
        public int score = 0;
        public float timer;
        public Text _timetext;
        int _allQuestionCounter;
        public int allTrueAnswers, wrongCounter;
        float delay = 0;


        void Start()
        {
            _answerAnim = FindObjectOfType<AnswersAnimation>();
            _soundControl = FindObjectOfType<SoundControl>();
            _panelButtonControl = FindObjectOfType<PanelButtonControl>();
            _moleGameTimer = FindObjectOfType<MoleGameTimer>();
            _scoreBarControl = FindObjectOfType<ScoreBarControl>();
            _gettingData = FindObjectOfType<GettingData>();
            timer = 30;
            Invoke("SetCountOfQuestions", 1f); //Have to wait for gettingData init
        }

        public void SetCountOfQuestions()
        {
            _allQuestionCounter = 2;
        }

        public void CheckAnswer(int i, int question, GameObject mole, int pos)
        {
            for (int a = 0; a < _gettingData.CorrectAnswerList[question].Count; a++)
            {
                if (mole.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite ==
                    _gettingData.CorrectAnswerList[question][a])
                {
                    _gettingData.CorrectAnswerList[question].RemoveAt(a);
                    score++;
                    _answerAnim.Answers(i);
                    _scoreBarControl.GetPoint();
                    ChangeHittableforSameMoles(mole);
                    mole.GetComponent<SpriteRenderer>().sprite = happyMole[pos];
                    _soundControl.PlayCorrectSound();
                }
            }

            for (int j = 0; j < _gettingData.WrongAnswerList[question].Count; j++)
            {
                if (mole.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite ==
                    _gettingData.WrongAnswerList[question][j])
                {
                    _answerAnim.FalseAnswers();
                    timer -= 5;
                    _moleGameTimer.currentTime -= 50;
                    mole.GetComponent<SpriteRenderer>().sprite = sadMole[pos];
                    _soundControl.PlayWrongSound();
                    wrongCounter++;
                }
            }
        }

        void ChangeHittableforSameMoles(GameObject currentMole)
        {
            for (int i = 0; i < MolePos.transform.childCount; i++)
            {
                if (MolePos.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0)
                        .GetComponent<SpriteRenderer>().sprite == currentMole.transform.GetChild(0).gameObject
                        .GetComponent<SpriteRenderer>().sprite)
                {
                    if (MolePos.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject !=
                        currentMole.transform.GetChild(0).gameObject)
                    {
                        MolePos.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject
                            .GetComponent<MoleControl>().hittable = false;
                    }
                }
            }
        }

        public void CloseHittable()
        {
            for (int i = 0; i < MolePos.transform.childCount; i++)
            {
                MolePos.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.GetComponent<MoleControl>()
                    .hittable = false;
            }
        }

        public void OpenHittable()
        {
            for (int i = 0; i < MolePos.transform.childCount; i++)
            {
                MolePos.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.GetComponent<MoleControl>()
                    .hittable = true;
            }
        }

        void LateUpdate()
        {
            _timetext.text = Mathf.Round(timer).ToString();
            CheckGameSituation(_panelButtonControl.currentQuestion1);
        }

        public void TryAgain()
        {
            _scoreBarControl.SetColorBack();
            timer = 30;
            score = 0;
        }

        public void NextGameButtonisClicked()
        {
            _scoreBarControl.CheckScore();
            timer = 30;
            score = 0;
        }

        void CheckGameSituation(int question)
        {
            if (_gettingData.CorrectAnswerList[question].Count == 0)
            {
                delay += UnityEngine.Time.deltaTime;
                if (delay > 1)
                {
                    delay = 0;
                    allTrueAnswers = _gettingData.CorrectAnswerList[question].Count;
                    panelSuccess.SetActive(true);
                    _gettingData.SetArray(); //Set data
                    score = 0;
                    timer = 30;
                    _moleGameTimer.currentTime = 300;
                    _panelButtonControl.FinishedGame(_allQuestionCounter);
                    UnityEngine.Time.timeScale = 0;
                    CloseHittable();
                }
            }

            if (timer < 0)
            {
                _gettingData.SetArray(); //Set data
                timer = 30;
                UnityEngine.Time.timeScale = 0;
                _moleGameTimer.currentTime = 300;
                panelGameOver.SetActive(true);
            }

            timer -= UnityEngine.Time.deltaTime;
        }
    }
}