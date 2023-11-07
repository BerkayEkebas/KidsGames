using UnityEngine;

namespace GameScripts.WaterfallPianoGameScripts
{
    public class DotController : MonoBehaviour
    {
        ControlPianoTiles _controlPianoTiles;
        PlayPianoController _playPianoController;
        GameObject tempObject;
        public static int destroyedCounter = 0;
        bool triggered = false;

        void Start()
        {
            _controlPianoTiles = FindObjectOfType<ControlPianoTiles>();
            _playPianoController = FindObjectOfType<PlayPianoController>();
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "ClickArea")
            {
                _controlPianoTiles.numberOfTile = 20;
                triggered = true;
                tempObject = gameObject;
            }
            else if (collision.gameObject.name == "DotStopPos")
            {
                StopGame();
            }
        }
        void StopGame()
        {
            _playPianoController.isPlay = false;
        }

        void Update()
        {
            if (triggered && tempObject.name == _controlPianoTiles.numberOfTile + "(Clone)")
            {
                _playPianoController.isPlay = true;
                Destroy(tempObject);
                triggered = false;
                destroyedCounter++;
            }
        }
    }
}