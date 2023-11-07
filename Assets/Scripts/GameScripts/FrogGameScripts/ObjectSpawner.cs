using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace GameScripts.FrogGameScripts
{
    public class ObjectSpawner : MonoBehaviour
    {
        public GameObject pedal, pedalPosition, startPos, screenPos, stopPos, TempPos1, TempPos2, shadowPos, shadowObject;
        PanelControl _panelControl;
        AnimationControl _animationControl;
        public List<Sprite> allSpirtes, allShadowSpirtes = new List<Sprite>();
        public List<Sprite> all = new List<Sprite>();
        public List<Sprite> allSpirtesFromAssets = new List<Sprite>();
        public bool letMove = false, letMoveFinish = false,isGameContentNull=false;
        int randomNum, randomSprite,answerSetCounter;
        private float speed = 12;
        void Start()
        {
            _animationControl = FindObjectOfType<AnimationControl>();
            _panelControl = FindObjectOfType<PanelControl>();          
            SetAllSpritesFromGameAsstes();
            
        }
        public void SetAllSpritesFromGameAsstes()
        {
            allSpirtes.Clear();
            for (int i = 0; i < 3; i++)
            {
                randomNum = UnityEngine.Random.Range(0, all.Count);
                allSpirtes.Add(all[randomNum]);
                all.RemoveAt(randomNum);
            }
            allShadowSpirtes.AddRange(allSpirtes);

        }
        public void SpawnPedal()
        {
            pedalPosition.transform.position = startPos.transform.position;

            for (int i = 0; i < 3; i++)
            {
                randomSprite = UnityEngine.Random.Range(0, allSpirtes.Count);//getData
                pedal.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = allSpirtes[randomSprite];
                pedal.transform.GetChild(0).gameObject.name = allSpirtes[randomSprite].name;
                Instantiate(pedal, pedalPosition.transform.position, Quaternion.identity, pedalPosition.transform);
                allSpirtes.RemoveAt(randomSprite);
            }

        }
        public void SpawnShadow()
        {
            for (int i = 0; i < 3; i++)
            {
                randomSprite = Random.Range(0, allShadowSpirtes.Count);
                shadowObject.GetComponent<Image>().sprite = allShadowSpirtes[randomSprite];
                shadowObject.GetComponent<Image>().color = new Color(0.21f, 0.21f, 0.21f, 1);
                shadowObject.name = allShadowSpirtes[randomSprite].name;
                Instantiate(shadowObject, shadowPos.transform.position, Quaternion.identity, shadowPos.transform);
                allShadowSpirtes.RemoveAt(randomSprite);
            }
        }
        private void SetActiveDragDrop(bool set)
        {
            foreach (Transform pedal in pedalPosition.transform)
            {
                DragDrop dragDrop = pedal.GetComponentInChildren<DragDrop>();
                dragDrop.canvasGroup.blocksRaycasts = set;
            }
        }
        void SetAllStartPoint()
        {
            foreach (Transform child in pedalPosition.transform)
            {
                child.GetComponentInChildren<DragDrop>().SetStartPoint();
            }
        }
        void ControlPedalAnimation()
        {
            if (letMove && !_panelControl.isPaused)
            {
                if (pedalPosition.transform.position.x < screenPos.transform.position.x)
                {
                    pedalPosition.transform.SetParent(TempPos2.transform);
                    pedalPosition.transform.position += speed * Vector3.right;
                    _animationControl.isWaterAnim = true;
                    SetActiveDragDrop(false);
                }
                else
                {

                    pedalPosition.transform.SetParent(TempPos1.transform);
                    SetAllStartPoint();
                    SetActiveDragDrop(true);
                    _animationControl.isWaterAnim = false;
                    letMove = false;

                }
            }
            if (letMoveFinish && !_panelControl.isPaused)
            {
                if (pedalPosition.transform.position.x < stopPos.transform.position.x)
                {
                    _animationControl.isWaterAnim = true;
                    pedalPosition.transform.position += speed * Vector3.right;
                    SetActiveDragDrop(false);
                }
                else
                {
                    SetActiveDragDrop(true);
                    pedalPosition.transform.position = startPos.transform.position;
                    letMoveFinish = false;
                    letMove = false;
                    _panelControl.DeleteAll();
                }
            }
        }
        void LateUpdate()
        {
            ControlPedalAnimation();
        }
    }
}