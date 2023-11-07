using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.WaterfallPianoGameScripts
{
    public class PlayPianoController : MonoBehaviour
    {
        public GameObject dot, droppingObjectsPos, pianoGamePanel, sparkle, pianoButtons, homeButton;
        public Sprite[] dotSprites;
        Vector3 sparkleStartPos;
        string musicTemp;
        float timer = 0, dotCreateSpeed = 0.320f;
        int musicLengthCounter = 0, dotCounter = 0;
        public bool isPlay = false, isSongFinished = false;
        public void SetMusic(string music)
        {
            homeButton.SetActive(true);
            musicTemp = null;
            pianoButtons.SetActive(true);
            pianoGamePanel.SetActive(true);
            musicTemp = music;
            isPlay = true;
        }
        void PlayMusic()
        {
            timer += Time.deltaTime;

            if (musicLengthCounter < musicTemp.Length && timer > dotCreateSpeed)
            {
                char note = musicTemp[musicLengthCounter];
                if (TryGetNoteIndex(note, out int index))
                {
                    CreateDot(note, index);
                }
                musicLengthCounter++;
                timer = 0;
            }
            else if (musicLengthCounter == musicTemp.Length && DotController.destroyedCounter == dotCounter)
            {
                SongFinished();
            }
        }
        bool TryGetNoteIndex(char note, out int index)
        {
            Dictionary<char, int> charToIndex = new Dictionary<char, int>
          {
            { '0', 0 },
            { '1', 1 },
            { '2', 2 },
            { '3', 3 },
            { '4', 4 },
            { '5', 5 },
            { '6', 6 },
            { '7', 7 }
          };

            return charToIndex.TryGetValue(note, out index);
        }

        void CreateDot(char note, int index)
        {
            dot.name = note.ToString();
            dot.GetComponent<Image>().sprite = dotSprites[index];
            Instantiate(dot, droppingObjectsPos.transform.GetChild(index).gameObject.transform.position,
                       Quaternion.identity, droppingObjectsPos.transform.GetChild(index).gameObject.transform);
            dotCounter++;
        }
        void SongFinished()
        {
            homeButton.SetActive(false);
            SparkleControl();
            SetCountersInitValue();
            Invoke("FinishGameCloudDelay", 2.5f);
        }
        public void HomeButton()
        {
             pianoButtons.SetActive(false);
             SetCountersInitValue();
             GameController.cloudAnim = true;
        }
        void SetCountersInitValue()
        {
            isSongFinished = true;
            isPlay = false;
            musicLengthCounter = 0;
            dotCounter = 0;
            GameController.tempObject = pianoGamePanel;
            DotController.destroyedCounter = 0;
        }
        void SparkleControl()
        {
            sparkle.GetComponent<RectTransform>().anchoredPosition = sparkleStartPos;
            sparkle.SetActive(true);
            sparkle.GetComponent<Animator>().enabled = true;
        }
        void FinishGameCloudDelay()
        {
            GameController.cloudAnim = true;
        }
        void GoingDownTiles()
        {
            float yOffset = -0.4f * 13; 

            foreach (Transform row in droppingObjectsPos.transform)
            {
                foreach (Transform tile in row)
                {
                    tile.position += new Vector3(0, yOffset, 0);
                }
            }
        }
        public void DeleteDroppingObjects()
        {
            foreach (Transform row in droppingObjectsPos.transform)
            {
                foreach (Transform tile in row)
                {
                    Destroy(tile.gameObject);
                }
            }
        }
        void Update()
        {
            if (isPlay && GameController.cloudAnim == false)
            {
                PlayMusic();
                GoingDownTiles();
            }
        }
    }
}