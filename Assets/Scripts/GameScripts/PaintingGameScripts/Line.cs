using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameScripts.PaintingGameScripts
{
    public class Line : MonoBehaviour
    {
        public LineRenderer lineRenderer;
        public Material lineMaterialYellow, lineMaterialLightPurple, lineMaterialPurple, lineMaterialLightBlue, lineMaterialBlue, lineMaterialBlack, lineMaterialGreen, lineMaterialLightGreen, lineMaterialDarkGreen, lineMaterialRed, lineMaterialPink, lineMaterialOrange, lineMaterialEraser;
        static int sortingLayer = 0;
        List<Vector2> points;
        public int sizeNum;
        float Timer = 0;

        void Start()
        {
            SetColor(Color.black);
            sortingLayerUpdate();
        }
        public void UpdateLine(Vector2 position)
        {
            if (points == null)
            {
                points = new List<Vector2>();
                SetPoint(position);
                return;
            }
            if (Vector2.Distance(points.Last(), position) > .1f)
            {
                SetPoint(position);
            }

        }
        void SetPoint(Vector2 point)
        {
            points.Add(point);

            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPosition(points.Count - 1, point);
        }
        public void SetColor(Color a)
        {
            lineMaterialBlack.color = a;
        }
        public void SetAgainBlack()
        {
            sortingLayer++;
            lineRenderer.material = lineMaterialBlack;
            sortingLayerUpdate();
        }
        public void SetAgainBlue()
        {
            sortingLayer++;
            lineRenderer.material = lineMaterialBlue;
            sortingLayerUpdate();
        }
        public void SetAgainRed()
        {
            sortingLayer++;
            lineRenderer.material = lineMaterialRed;
            sortingLayerUpdate();
        }
        public void SetAgainGreen()
        {
            sortingLayer++;
            lineRenderer.material = lineMaterialGreen;
            sortingLayerUpdate();
        }
        public void SetAgainYellow()
        {
            sortingLayer++;
            lineRenderer.material = lineMaterialYellow;
            sortingLayerUpdate();
        }
        public void SetAgainLightBlue()
        {
            sortingLayer++;
            lineRenderer.material = lineMaterialLightBlue;
            sortingLayerUpdate();
        }
        public void SetAgainLightPurple()
        {
            sortingLayer++;
            lineRenderer.material = lineMaterialLightPurple;
            sortingLayerUpdate();
        }
        public void SetAgainPurple()
        {
            sortingLayer++;
            lineRenderer.material = lineMaterialPurple;
            sortingLayerUpdate();
        }
        public void SetAgainLightGreen()
        {
            sortingLayer++;
            lineRenderer.material = lineMaterialLightGreen;
            sortingLayerUpdate();
        }
        public void SetAgainDarkGreen()
        {
            sortingLayer++;
            lineRenderer.material = lineMaterialDarkGreen;
            sortingLayerUpdate();
        }
        public void SetAgainOrange()
        {
            sortingLayer++;
            lineRenderer.material = lineMaterialOrange;
            sortingLayerUpdate();
        }
        public void SetAgainPink()
        {
            sortingLayer++;
            lineRenderer.material = lineMaterialPink;
            sortingLayerUpdate();
        }
        public void SetAgainEraser()
        {
            sortingLayer++;
            lineRenderer.material = lineMaterialEraser;
        }
        public void SetWidth(float a)
        {
            lineRenderer.startWidth = a;
            lineRenderer.endWidth = a;

        }
        public void SetSmall()
        {
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
        }
        public void SetNormal()
        {
            lineRenderer.startWidth = 0.3f;
            lineRenderer.endWidth = 0.3f;
        }
        public void SetBig()
        {
            lineRenderer.startWidth = 0.5f;
            lineRenderer.endWidth = 0.5f;
        }

        void sortingLayerUpdate()
        {
            lineRenderer.sortingOrder = sortingLayer;
        }

        void DeleteDot()
        {
            if (lineRenderer.positionCount == 1)
            {
                Destroy(gameObject);
            }
        }
        void Update()
        {
            Timer += Time.deltaTime;
            if (Timer > 2)
            {
                DeleteDot();
                Timer = 0;
            }

        }
    }
}