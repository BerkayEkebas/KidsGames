using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.WaterfallPianoGameScripts
{
    public class SetPianoColorBack : MonoBehaviour
    {
        Sprite firstSprite;
        float timer, setPrevSpriteTimer = 0.2f;

        void Start()
        {
            firstSprite = gameObject.GetComponent<Image>().sprite;
        }

        void Update()
        {
            if (gameObject.GetComponent<Image>().sprite != firstSprite)
            {
                timer += 0.011f;
                if (timer > setPrevSpriteTimer)
                {
                    gameObject.GetComponent<Image>().sprite = firstSprite;
                    timer = 0;
                }
            }
        }
    }
}