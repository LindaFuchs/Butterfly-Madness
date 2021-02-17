using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class Playcat : MonoBehaviour
{
    public void playTheGame()
    {
        SceneManager.LoadScene("Game");
    }
}
