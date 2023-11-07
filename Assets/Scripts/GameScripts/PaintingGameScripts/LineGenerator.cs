using UnityEngine;
namespace GameScripts.PaintingGameScripts
{
    public class LineGenerator : MonoBehaviour
    {
        public GameObject linePrefab,Lines;
        FuntionButtonControl _fuctionButton;
        Line activeLine;
        PaintingArea paintingArea;
        public bool isStart = false;
        void Start()
        {
            _fuctionButton = FindObjectOfType<FuntionButtonControl>();
            paintingArea = FindObjectOfType<PaintingArea>();
        }
        public void SetStart()
        {
            isStart = true;
        }
        public void SetStart2()
        {
            isStart = false;
        }
        void Update()
        {
            if (isStart)
            {
                if (paintingArea.paintable == true)
                {
                    if (Input.GetMouseButtonDown(0))
                    {

                        GameObject newLine = Instantiate(linePrefab, Lines.transform.position, Quaternion.identity, Lines.transform);
                        activeLine = newLine.GetComponent<Line>();
                        _fuctionButton.AddLines(newLine);
                    }
                }
                if (Input.GetMouseButtonUp(0))
                {
                    activeLine = null; //there is no active line
                }
                if (activeLine != null)
                {
                    Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    activeLine.UpdateLine(mousePos);
                }
            }
        }
    }
}