using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader_Intro : MonoBehaviour
{

    public int SceneIndex = 0;
    public void LoadScene()
    {
        SceneManager.LoadScene(SceneIndex);
    }
}
