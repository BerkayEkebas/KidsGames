using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameScripts.FrogGameScripts
{
    public class OnDropObject : MonoBehaviour, IDropHandler
    {
        private PanelControl _panelControl;
        public GameObject finger;
        private AnimationControl _animationControl;
        private SoundControl _soundControl;

        public static int correctCounter = 0;
        public static bool isTrue = false;
        public static bool isFalse = false;

        private static float happyTimer = 0f;
        private static float sadTimer = 0f;

        private void Start()
        {
            correctCounter = 0;
            _panelControl = FindObjectOfType<PanelControl>();
            _animationControl = FindObjectOfType<AnimationControl>();
            _soundControl = FindObjectOfType<SoundControl>();
        }

        public void OnDrop(PointerEventData eventData)
        {
            GameObject draggedObject = eventData.pointerDrag.gameObject;
            _animationControl.isClickedAnim = false;

            if (eventData.pointerDrag != null && draggedObject.GetComponent<Image>().sprite == gameObject.GetComponent<Image>().sprite)
            {
                // Correct Answer
                finger.SetActive(false);
                isTrue = true;
                draggedObject.GetComponent<DragDrop>().EnableAllDragDropObjects();
                draggedObject.transform.position = gameObject.transform.position;
                DragDrop dragDrop = eventData.pointerDrag.gameObject.GetComponent<DragDrop>();
                dragDrop.enabled = false;
                dragDrop.canvasGroup.alpha = 1;
                dragDrop.endDrop = false;
                dragDrop.GetComponent<Image>().enabled = false;

                Image image = gameObject.GetComponent<Image>();
                image.color = new Color(255, 255, 255, 255);

                correctCounter++;
                CheckCorrectAnswers();
                _soundControl.PlayCorrectSound();
            }
            else
            {
                // False Answer
                isFalse = true;
                _soundControl.PlayWrongSound();
            }
        }

        private void CheckCorrectAnswers()
        {
            if (correctCounter == 3)
            {
                _panelControl.NextLevel();
                correctCounter = 0;
            }
        }
        void Update()
        {
            //TODO Arrange Animations
            if (isTrue)
            {
                isFalse = false;
                _animationControl.HappyFrog();
                happyTimer += Time.deltaTime;
                if (happyTimer > 3)
                {
                    _animationControl.isDefault = true;
                    isTrue = false;
                    happyTimer = 0;
                }
            }
            if (isFalse)
            {
                isTrue = false;
                _animationControl.SadFrog();
                sadTimer += Time.deltaTime;
                if (sadTimer > 3)
                {
                    _animationControl.isDefault = true;
                    isFalse = false;
                    sadTimer = 0;
                }
            }

        }
    }
}