using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRestarter : MonoBehaviour
{
    public void RestartGame()
    {
        AsyncSceneLoader.Instance.LoadSceneWithSequenceID(0);
    }
}
