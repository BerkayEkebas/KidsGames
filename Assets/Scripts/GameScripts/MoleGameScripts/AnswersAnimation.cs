using UnityEngine;

namespace GameScripts.MoleGameScripts
{
    public class AnswersAnimation : MonoBehaviour
    {
        [SerializeField] private GameObject answerAnimationText, falseAnimationText;
        MoleCreator _creator;

        void Start()
        {
            answerAnimationText.SetActive(false);
            falseAnimationText.SetActive(false);
            _creator = FindObjectOfType<MoleCreator>();
        }

        public void Answers(int i)
        {
            answerAnimationText.SetActive(true);
            GameObject answerPrefab = Instantiate(answerAnimationText, transform.position, Quaternion.identity,
                _creator.moles[i].transform);
        }

        public void FalseAnswers()
        {
            falseAnimationText.SetActive(true);
            GameObject answerfalsePrefab =
                Instantiate(falseAnimationText, new Vector3(105, 258, 0), Quaternion.identity);
        }
    }
}