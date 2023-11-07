using System.Collections;
using UnityEngine;

namespace GameScripts.MoleGameScripts
{
    public class MoleControl : MonoBehaviour
    {
        [SerializeField] public Sprite[] mole;
        [SerializeField] public Sprite moleHit;

        [SerializeField] public Sprite[] moleHit2;

        //Hard code
        private Vector2 startPosition = new Vector2(0f, -2.70f);
        private Vector2 endPosition = new Vector2(0f, 0.5f);

        // How long it takes to show a mole.
        private float showDuration = 0.6f;
        private float duration = 1f;

        private TrueOrFalseCheck _trueOrFalse;
        private SpriteRenderer spriteRenderer;
        private PanelButtonControl panelButtonControl;
        public MoleCreator creator;
        public GettingData gettingDataa;

        public int returnNumber;
        public bool hittable, isStarted = false;
        public int questionCounter, randomMole, memorizeNum;
        float waitingTime, randomHideNumber;
        public static int currentQuestion = 0;

        Vector3[] getVec;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            creator = FindAnyObjectByType<MoleCreator>();
            panelButtonControl = FindObjectOfType<PanelButtonControl>();
            gettingDataa = FindObjectOfType<GettingData>();
            _trueOrFalse = FindObjectOfType<TrueOrFalseCheck>();
            SetLevel(0);
            StartCoroutine(ShowHide(startPosition, endPosition, 1));
            hittable = true;
            getVec = new Vector3[creator.moles.Length];
            getPosition();
            questionCounter = 2; //deneme
        }

        private IEnumerator ShowHide(Vector2 start, Vector2 end, float randomNum)
        {
            transform.localPosition = start;

            // Show the mole
            float elapsed = 0f;
            while (elapsed < showDuration)
            {
                transform.localPosition = Vector2.Lerp(start, end, elapsed / showDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(duration);

            transform.localPosition = end;

            // Hide the mole
            elapsed = 0f;
            while (elapsed < showDuration)
            {
                transform.localPosition = Vector2.Lerp(end, start, elapsed / showDuration);

                elapsed += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(randomNum);
            transform.localPosition = start;
            hittable = false;
        }

        void Update()
        {
            Animation();
            CloseImage();
        }

        private void OnMouseDown()
        {
            for (int a = 0; a < mole.Length; a++)
            {
                if (gameObject.GetComponent<SpriteRenderer>().sprite == mole[a])
                {
                    memorizeNum = a;
                    break;
                }
            }

            if (hittable != true) return;
            spriteRenderer.sprite = moleHit;
            StopAllCoroutines();
            StartCoroutine(QuickHide());
            for (int i = 0; i < creator.moles.Length; i++)
            {
                if (creator.moles[i].GetComponent<SpriteRenderer>().sprite == moleHit)
                {
                    returnNumber = i; // which mole
                    _trueOrFalse.CheckAnswer(returnNumber, panelButtonControl.currentQuestion1, creator.moles[i],
                        memorizeNum);
                    //spriteRenderer.sprite = moleHit2[memorizeNum];// change next
                }
            }

            hittable = false;
        }

        public void HideAll()
        {
            StartCoroutine(QuickHide());
        }

        public IEnumerator QuickHide()
        {
            yield return new WaitForSeconds(1f);

            if (hittable == false)
            {
                Hide();
            }

            StopCoroutine(QuickHide());
        }

        public void Hide()
        {
            transform.localPosition = startPosition;
            gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
            StopAllCoroutines();
        }

        private void SetLevel(int level)
        {
            float durationMin = Mathf.Clamp(1 - level * 0.1f, 0.01f, 1f);
            float durationMax = Mathf.Clamp(2 - level * 0.1f, 0.01f, 2f);
            duration = Random.Range(durationMin, durationMax);
        }

        void getPosition()
        {
            for (int i = 0; i < creator.moles.Length; i++)
            {
                getVec[i] = creator.moles[i].transform.position;
            }
        }

        void CloseImage()
        {
            for (int i = 0; i < creator.moles.Length; i++)
            {
                if (creator.moles[i].transform.position.y - 2.2f > getVec[0].y)
                {
                    creator.moles[i].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    creator.moles[i].GetComponent<BoxCollider2D>().enabled = true;
                }
                else
                {
                    creator.moles[i].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    creator.moles[i].GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }

        void Animation()
        {
            int randomTrueAns =
                Random.Range(0, gettingDataa.CorrectAnswerList[panelButtonControl.currentQuestion1].Count);
            int randomWrongAns =
                Random.Range(0, gettingDataa.WrongAnswerList[panelButtonControl.currentQuestion1].Count);

            for (int j = 0; j < 4; j++)
            {
                if (transform.position == creator.moleGameObject[j].transform.position + new Vector3(0, -2.70f, 0))
                {
                    int randomTrue = Random.Range(0, 4);

                    if (randomTrue == 1)
                    {
                        creator.moles[j].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite =
                            gettingDataa.WrongAnswerList[panelButtonControl.currentQuestion1][randomWrongAns];
                    }
                    else
                    {
                        if (gettingDataa.CorrectAnswerList[panelButtonControl.currentQuestion1].Count != 0)
                        {
                            creator.moles[j].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite =
                                gettingDataa.CorrectAnswerList[panelButtonControl.currentQuestion1][randomTrueAns];
                        }
                    }

                    waitingTime += Time.deltaTime;
                    if (waitingTime > 1f)
                    {
                        randomMole = Random.Range(0, mole.Length);
                        randomHideNumber = Random.Range(0, 8);
                        spriteRenderer.sprite = mole[randomMole];
                        StartCoroutine(ShowHide(startPosition, endPosition, randomHideNumber));
                        hittable = true;
                        waitingTime = 0;
                    }
                }
            }
        }
    }
}