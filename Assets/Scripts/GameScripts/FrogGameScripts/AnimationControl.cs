using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.FrogGameScripts
{
    public class AnimationControl : MonoBehaviour
    {
        public Sprite[] anim, defaultAnim, clickAnim, sadFrog, happyFrog, waterAnim;
        public GameObject frog, dragObject, water, pedalPos, shadowPos;
        public Sprite lookDown, lookRight, lookRightDown, lookUp, giveFood;

        float defaultAnimTime, emotionsAnimTime, waterTime;
        int animCounter, defaultAnimCounter, happyCounter, sadCounter, waterCounter;
        public bool isStarted, isDefault, isClickedAnim, isWaterAnim, moving;

        private void Update()
        {
            if (isStarted)
            {
                Animation();
            }           
            if (isClickedAnim)
            {
                OnDropObject.isTrue = false;
                OnDropObject.isFalse = false;
                isDefault = false;
                ClickAnimation();
            }
            else if (isDefault)
            {
                DefaultAnimation();
            }

            if (isWaterAnim)
            {
                WaterAnimation();
            }
        }

        private void Animation()
        {
            isDefault = false;
            defaultAnimTime += Time.deltaTime;
            if (defaultAnimTime > 0.15f)
            {
                frog.GetComponent<Image>().sprite = anim[animCounter++];
                if (animCounter == anim.Length)
                {
                    animCounter = 0;
                    isDefault = true;
                    isStarted = false;
                    HelperFinger();
                }
                defaultAnimTime = 0;          
            }
        }
        void HelperFinger() //TODO fix Helper
        {
            GameObject helperObject = pedalPos.transform.GetChild(0).gameObject;
            helperObject.transform.GetChild(1).gameObject.SetActive(true);
            Vector2 startPoint = (Vector2)helperObject.transform.GetChild(1).gameObject.transform.position;

            for (int i = 0; i < shadowPos.transform.childCount; i++)
            {
                if (shadowPos.transform.GetChild(i).gameObject.GetComponent<Image>().sprite == helperObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite)
                {
                    Vector2 endPoint = shadowPos.transform.GetChild(i).gameObject.transform.position;
                    StartCoroutine(MoveObject(helperObject.transform.GetChild(1).gameObject, endPoint, startPoint));
                    break;
                }
            }
        }

        private IEnumerator MoveObject(GameObject movingObject, Vector2 endPoint, Vector2 startPoint)
        {
            float journeyLength = Vector2.Distance(movingObject.transform.position, endPoint);
            float journeyTime = journeyLength / 200;
            float startTime = Time.time;

            while (Time.time < startTime + journeyTime)
            {
                float distanceCovered = (Time.time - startTime) * 200; 
                float fractionOfJourney = distanceCovered / journeyLength;

                movingObject.transform.position = Vector2.Lerp(startPoint, endPoint, fractionOfJourney);
                yield return null;
            }          
            movingObject.transform.position = endPoint;
            movingObject.SetActive(false);
            StopAllCoroutines();
        }
        private void DefaultAnimation()
        {
            defaultAnimTime += Time.deltaTime;
            if (defaultAnimTime > 0.15f)
            {
                frog.GetComponent<Image>().sprite = defaultAnim[defaultAnimCounter++];
                if (defaultAnimCounter == defaultAnim.Length)
                {
                    defaultAnimCounter = 0;
                }

                defaultAnimTime = 0;
            }
        }
        private void ClickAnimation()
        {
            float x = dragObject.transform.position.x;
            float y = dragObject.transform.position.y;
            Image frogImage = frog.GetComponent<Image>();

            if (x < 350 && y > 700)
            {
                frogImage.sprite = lookUp;
            }
            else if (x < 300 && x > 100 && y < 350)
            {
                frogImage.sprite = lookDown;
            }
            else if (x > 300 && y < 600)
            {
                frogImage.sprite = lookRightDown;
            }
            else if (x > 350 && y > 500)
            {
                frogImage.sprite = lookRight;
            }
            else if (x < 330 && y < 600 && y > 350)
            {
                frogImage.sprite = giveFood;
            }
        }

        private void WaterAnimation()
        {
            waterTime += Time.deltaTime;
            if (waterTime > 0.3f)
            {
                water.GetComponent<Image>().sprite = waterAnim[waterCounter++];
                if (waterCounter == waterAnim.Length)
                {
                    waterCounter = 0;
                }
                waterTime = 0;
            }
        }

        public void HappyFrog()
        {
            isDefault = false;
            emotionsAnimTime += Time.deltaTime;
            if (emotionsAnimTime > 0.5f)
            {
                frog.GetComponent<Image>().sprite = happyFrog[happyCounter++];
                if (happyCounter == happyFrog.Length)
                {
                    happyCounter = 0;
                }
                emotionsAnimTime = 0;
            }
        }

        public void SadFrog()
        {
            isDefault = false;
            emotionsAnimTime += Time.deltaTime;
            if (emotionsAnimTime > 0.6f)
            {
                frog.GetComponent<Image>().sprite = sadFrog[sadCounter++];
                if (sadCounter == sadFrog.Length)
                {
                    sadCounter = 0;
                }
                emotionsAnimTime = 0;
            }
        }
    }
}
