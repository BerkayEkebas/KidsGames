using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.MoleGameScripts
{
    public class SoundAnimation : MonoBehaviour
    {
        public Sprite[] anim;
        Image _image;
        float time;
        public int animCounter = 0;

        void Start()
        {
            _image = GetComponent<Image>();
        }

        public void Animation()
        {
            time += Time.deltaTime;
            if (time > 0.2f)
            {
                _image.sprite = anim[animCounter++];
                if (animCounter == anim.Length)
                {
                    animCounter = 0;
                }

                time = 0;
            }
        }

        void Update()
        {
            Animation();
        }
    }
}