using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader_Start : MonoBehaviour
{
    public int SceneIndex = 3;
  public void LoadScene()
    {
        SceneManager.LoadScene(SceneIndex);
    }
}
