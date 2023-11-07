using System.Collections.Generic;
using UnityEngine;

namespace GameScripts.PaintingGameScripts
{
    public class FuntionButtonControl : MonoBehaviour
    {
        public GameObject Lines,PenSize;
        public List<GameObject> AllLines = new List<GameObject>();
        static bool active = false;
        public void AddLines(GameObject a)
        {
            AllLines.Add(a);
        }
        public void DeleteAllButton()
        {
            for (int i = 0; i < Lines.transform.childCount; i++)
            {
                Destroy(Lines.transform.GetChild(i).gameObject);
            }
        }
        public void TakeitBackButton()
        {
            Debug.Log("TakeitBack");
            Destroy(Lines.transform.GetChild(Lines.transform.childCount - 1).gameObject);
        }
        public void SizeButton()
        {
            if (active)
            {
                PenSize.SetActive(false);
                active = false;
            }
            else
            {
                PenSize.SetActive(true);
                active = true;
            }

        }
    }
}