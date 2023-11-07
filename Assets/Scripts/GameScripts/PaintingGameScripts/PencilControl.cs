using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace GameScripts.PaintingGameScripts
{
    public class PencilControl : MonoBehaviour
    {
        public List<GameObject> pencils = new List<GameObject>();
        public List<Vector3> pencilsPos = new List<Vector3>();

        public void SetAllPositions()
        {
            for (int i = 0; i < pencils.Count; i++)
            {
                pencilsPos.Add(pencils[i].transform.position);
            }
        }
        public void SetAllBackPos()
        {
            for (int i = 0; i < pencils.Count; i++)
            {
                pencils[i].transform.position = pencilsPos[i];
            }
        }
        void OpenAllButton()
        {
            for (int i = 0; i < pencils.Count; i++)
            {
                pencils[i].GetComponent<Button>().interactable = true;
            }
        }
        public void SetAgainBlack()
        {
            SetAllBackPos();
            OpenAllButton();
            pencils[7].transform.position += new Vector3(40, 0, 0);
            pencils[7].GetComponent<Button>().interactable = false;
        }
        public void SetAgainBlue()
        {
            SetAllBackPos();
            OpenAllButton();
            pencils[5].transform.position += new Vector3(40, 0, 0);
            pencils[5].GetComponent<Button>().interactable = false;
        }
        public void SetAgainRed()
        {
            SetAllBackPos();
            OpenAllButton();
            pencils[1].transform.position += new Vector3(40, 0, 0);
            pencils[1].GetComponent<Button>().interactable = false;

        }
        public void SetAgainGreen()
        {
            SetAllBackPos();
            OpenAllButton();
            pencils[4].transform.position += new Vector3(40, 0, 0);
            pencils[4].GetComponent<Button>().interactable = false;
        }
        public void SetAgainYellow()
        {
            SetAllBackPos();
            OpenAllButton();
            pencils[3].transform.position += new Vector3(40, 0, 0);
            pencils[3].GetComponent<Button>().interactable = false;
        }
        public void SetAgainLightBlue()
        {
            SetAllBackPos();
            OpenAllButton();
            pencils[8].transform.position += new Vector3(40, 0, 0);
            pencils[8].GetComponent<Button>().interactable = false;
        }
        public void SetAgainLightPurple()
        {
            SetAllBackPos();
            OpenAllButton();
            pencils[9].transform.position += new Vector3(40, 0, 0);
            pencils[9].GetComponent<Button>().interactable = false;

        }
        public void SetAgainPurple()
        {
            SetAllBackPos();
            OpenAllButton();
            pencils[6].transform.position += new Vector3(40, 0, 0);
            pencils[6].GetComponent<Button>().interactable = false;
        }
        public void SetAgainLightGreen()
        {
            SetAllBackPos();
            OpenAllButton();
            pencils[10].transform.position += new Vector3(40, 0, 0);
            pencils[10].GetComponent<Button>().interactable = false;
        }
        public void SetAgainDarkGreen()
        {
            SetAllBackPos();
            OpenAllButton();
            pencils[11].transform.position += new Vector3(40, 0, 0);
            pencils[11].GetComponent<Button>().interactable = false;
        }
        public void SetAgainOrange()
        {
            SetAllBackPos();
            OpenAllButton();
            pencils[2].transform.position += new Vector3(40, 0, 0);
            pencils[2].GetComponent<Button>().interactable = false;
        }
        public void SetAgainPink()
        {
            SetAllBackPos();
            OpenAllButton();
            pencils[0].transform.position += new Vector3(40, 0, 0);
            pencils[0].GetComponent<Button>().interactable = false;
        }
    }
}