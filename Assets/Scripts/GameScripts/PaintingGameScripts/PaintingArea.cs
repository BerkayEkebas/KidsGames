using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameScripts.PaintingGameScripts
{
    public class PaintingArea : MonoBehaviour
    {
        public bool paintable = false;
        private void OnMouseDown()
        {
            paintable = true;
        }
    }
}