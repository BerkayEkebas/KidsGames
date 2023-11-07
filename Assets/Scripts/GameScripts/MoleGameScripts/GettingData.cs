using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameScripts.MoleGameScripts
{
    public class GettingData : MonoBehaviour
    {
        public List<Sprite> temp = new();
        public List<List<Sprite>> CorrectAnswerList = new();
        public List<List<Sprite>> WrongAnswerList = new();
        [SerializeField] public Sprite[] firstStage, secondStage;
        public Sprite[] firstStageWrong, secondStageWrong;

        void Start()
        {
            SetArray();
        }

        public void SetArray()
        {
            CorrectAnswerList.Add(temp);
            CorrectAnswerList.Add(temp);
            CorrectAnswerList.Add(temp);
            WrongAnswerList.Add(temp);
            WrongAnswerList.Add(temp);
            WrongAnswerList.Add(temp);
            SetSprites();
        }

        void SetSprites()
        {
            CorrectAnswerList[0] = firstStage.ToList();
            CorrectAnswerList[1] = secondStage.ToList();

            WrongAnswerList[0] = firstStageWrong.ToList();
            WrongAnswerList[1] = secondStageWrong.ToList();
        }
    }
}