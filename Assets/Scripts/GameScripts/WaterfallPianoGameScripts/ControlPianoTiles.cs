using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.WaterfallPianoGameScripts
{
    public class ControlPianoTiles : MonoBehaviour
    {
        public AudioClip[] notes;
        public Sprite[] shineSprites;
        public List<Sprite> normalSprites = new ();
        public GameObject[] pianoTiles;
        public GameObject tempPianoTiles;
        public AudioSource audioSource;
        public int numberOfTile;

        void Start()
        {
            numberOfTile = 19;
            foreach (var tile in pianoTiles)
            {
                normalSprites.Add(tile.GetComponent<Image>().sprite);
            }
        }

        public void ClickingTiles(int a)
        {
            numberOfTile = a;
            audioSource.PlayOneShot(notes[a]);
            pianoTiles[a].GetComponent<Image>().sprite = shineSprites[a];
            tempPianoTiles = pianoTiles[a];
        }
    }
}