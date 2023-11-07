using UnityEngine;
using UnityEngine.EventSystems;
namespace GameScripts.FrogGameScripts
{
    public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public GameObject canvas;
        private RectTransform rectTransform;
        public CanvasGroup canvasGroup;
        AnimationControl _animationControl;
        public Vector2 startPoint;
        public bool endDrop = false, beginDrag = false, isDragging=false;
        public static bool blockRay = true;
        private int objectSpeed = 3;
        void Start()
        {
            blockRay = true;
            _animationControl = FindObjectOfType<AnimationControl>();
            rectTransform = GetComponent<RectTransform>();
            canvas = GameObject.Find("Canvas").gameObject;
        }
        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
            isDragging = true;
            DisableOtherDragDropObjects();
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.GetComponent<Canvas>().scaleFactor;
            endDrop = false;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            endDrop = true;
            beginDrag = false;
            _animationControl.isClickedAnim = false;
            _animationControl.isDefault = false;
            EnableAllDragDropObjects();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _animationControl.isClickedAnim = true;
            _animationControl.dragObject = eventData.pointerEnter.gameObject;
            beginDrag = true;
        }
        public void SetStartPoint()
        {
            startPoint = gameObject.transform.position;
        }
        private void DisableOtherDragDropObjects()
        {
            DragDrop[] allDragDropObjects = FindObjectsOfType<DragDrop>();
            foreach (DragDrop obj in allDragDropObjects)
            {
                obj.canvasGroup.blocksRaycasts = false;
            }
        }

        public void EnableAllDragDropObjects()
        {
            DragDrop[] allDragDropObjects = FindObjectsOfType<DragDrop>();
            foreach (DragDrop obj in allDragDropObjects)
            {
                obj.canvasGroup.blocksRaycasts = true;
            }
        }
        void Update()
        {
            if (endDrop)
            {
                gameObject.transform.position = Vector2.Lerp(gameObject.transform.position, startPoint, objectSpeed * Time.deltaTime);
                gameObject.transform.parent.GetChild(1).gameObject.SetActive(false);

                if (Mathf.Abs(startPoint.y - gameObject.transform.position.y) < 4)
                {
                    gameObject.transform.position = startPoint;
                    endDrop = false;
                    Debug.Log("yerine oturdu");
                    isDragging = false;
                }
            }
        }
    }
}