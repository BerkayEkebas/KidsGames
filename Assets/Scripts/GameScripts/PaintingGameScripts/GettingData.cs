using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.PaintingGameScripts
{
    public class GettingData : MonoBehaviour
    {
        public List<Sprite> questionList = new List<Sprite>();
        public List<Sprite> assetQuestionList = new List<Sprite>();
        PanelControl _panelControl;
        public bool isGameContentNull = false;
        int random;
        void Start()
        {

            Screen.orientation = ScreenOrientation.LandscapeRight;
            _panelControl = FindObjectOfType<PanelControl>();
            SetQuestionImage();

        }
        void SetQuestionImage()
        {

            SetQuestionImageFromGameAssets();

        }
        void SetQuestionImageFromGameAssets()
        {
            random = UnityEngine.Random.Range(0, assetQuestionList.Count);
            _panelControl.questionImage.GetComponent<Image>().sprite = assetQuestionList[random];
        }
    }
}