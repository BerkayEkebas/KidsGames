using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.MoleGameScripts
{
    public class ScoreBarControl : MonoBehaviour
    {
        public GameObject scoreDarkPotato, scoreBar;
        public List<GameObject> potatoList = new List<GameObject>();
        public Image scoreImage;
        GettingData _gettingData;
        int count, questionCounter = 0;

        void Start()
        {
            scoreDarkPotato = Resources.Load("Prefabs/newPotato") as GameObject;
            scoreImage = Resources.Load<Image>("Prefabs/Potato");
            _gettingData = FindObjectOfType<GettingData>();
            Invoke("CreateScorePotato", 0.1f);
        }

        void CreateScorePotato()
        {
            for (int i = 0; i < _gettingData.CorrectAnswerList[questionCounter].Count; i++)
            {
                scoreDarkPotato.name = "ScoreDarkPotato" + i.ToString();
                Instantiate(scoreDarkPotato, new Vector3(0, 0, 0), Quaternion.identity, scoreBar.transform);
            }
        }

        public void GetPoint()
        {
            scoreBar.transform.GetChild(count).gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
            count++;
        }

        public void SetColorBack()
        {
            for (int i = 0; i < scoreBar.transform.childCount; i++)
            {
                scoreBar.transform.GetChild(i).gameObject.GetComponent<Image>().color =
                    scoreImage.GetComponent<Image>().color;
            }

            count = 0;
        }

        public void CheckScore()
        {
            potatoList.Clear();
            for (int i = 0; i < scoreBar.transform.childCount; i++)
            {
                scoreBar.transform.GetChild(i).gameObject.GetComponent<Image>().color =
                    scoreImage.GetComponent<Image>().color;
            }

            count = 0;
            questionCounter++;
            for (int i = 0; i < scoreBar.transform.childCount; i++)
            {
                Destroy(scoreBar.transform.GetChild(i).gameObject);
            }

            CreateScorePotato();
        }
    }
}