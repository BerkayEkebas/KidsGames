using UnityEngine;

namespace GameScripts.MoleGameScripts
{
    public class MoleCreator : MonoBehaviour
    {
        [SerializeField] public GameObject molesPrefab, molesPos;
        int _currentLevelCreation;
        public GameObject[] moleGameObject, moles;
        SpriteRenderer[] _answerImages;

        void Start()
        {
            if (true)
            {
                for (int i = 0; i < 4; i++)
                {
                    _currentLevelCreation = 4; //How many mole you want to create
                    molesPrefab.name = "MoleHole" + i.ToString();
                    Instantiate(molesPrefab, new Vector3(-5.5f + (3.8f * i), -1, 0), Quaternion.identity,
                        molesPos.transform);
                }

                CreatingObjects(_currentLevelCreation);
            }
        }

        public void CreatingObjects(int a)
        {
            moleGameObject = new GameObject[a];
            moles = new GameObject[a];
            _answerImages = new SpriteRenderer[a];

            for (int i = 0; i < moleGameObject.Length; i++)
            {
                moleGameObject[i] = GameObject.Find("MoleHole" + i.ToString() + "(Clone)");
            }


            for (int i = 0; i < moles.Length; i++)
            {
                moles[i] = moleGameObject[i].transform.GetChild(0).gameObject;
            }

            for (int i = 0; i < moles.Length; i++)
            {
                _answerImages[i] = moles[i].GetComponentInChildren<SpriteRenderer>();
            }
        }
    }
}