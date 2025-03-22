using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader_Start : MonoBehaviour
{
    int StartSceneIndex = 3;
    int LoadSceneIndex = 3;

    private void Start()
    {
        LoadSceneIndex = PlayerPrefs.GetInt("StageNumber", 3); //±âº» 3¹ø
    }

    public void StartScene()
    {
        SceneManager.LoadScene(StartSceneIndex);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(LoadSceneIndex);
    }
}
