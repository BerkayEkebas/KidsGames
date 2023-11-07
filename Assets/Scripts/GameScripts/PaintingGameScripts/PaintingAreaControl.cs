using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts.PaintingGameScripts
{
    public class PaintingAreaControl : MonoBehaviour
    {
        PaintingArea paintingArea;
        void Start()
        {
            paintingArea = FindObjectOfType<PaintingArea>();
        }
        private void OnMouseDown()
        {
            paintingArea.paintable = false;
        }
    }
}