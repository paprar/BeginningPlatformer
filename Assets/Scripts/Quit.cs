using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // ���� �Լ�
    public void Quit()
    {
#if UNITY_EDITOR
        // ������ �󿡼� ������ ����
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // ���� ���忡�� ������ ����
            Application.Quit();
#endif
    }
}

