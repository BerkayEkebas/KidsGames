using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.MoleGameScripts
{
    public class MoleGameTimer : MonoBehaviour
    {
        public Image time;
        public float currentTime;
        public float duration;

        void Start()
        {
            duration = 300;
            currentTime = duration;
        }

        public void StartAgain()
        {
            StopAllCoroutines();
            StartCoroutine(CountdownTime());
        }

        public IEnumerator CountdownTime()
        {
            while (currentTime >= 0)
            {
                time.fillAmount = Mathf.InverseLerp(0, duration, currentTime);
                yield return new WaitForSeconds(0.0625f);
                currentTime = currentTime - 0.67f;
            }

            yield return null;
        }
    }
}