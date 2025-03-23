using UnityEngine;
using UnityEditor;

public class QuitGame : MonoBehaviour
{
    public void QuittheGame()
    {
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
