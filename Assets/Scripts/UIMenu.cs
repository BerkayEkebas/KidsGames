using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    public void ChoosingGames(int i)
    {
        if(i == 0)
        {
            SceneManager.LoadScene("WaterfallPianoGameScene");
        }
        else if(i == 1)
        {
            SceneManager.LoadScene("FrogGameScene");
        }
        else if (i == 2)
        {
            SceneManager.LoadScene("MoleGameScene");
        }
        else if( i == 3)
        {
            SceneManager.LoadScene("PaintingGameScene");
        }
    }
}
