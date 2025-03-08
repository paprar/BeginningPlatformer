using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // 종료 함수
    public void Quit()
    {
#if UNITY_EDITOR
        // 에디터 상에서 실행을 종료
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // 실제 빌드에서 게임을 종료
            Application.Quit();
#endif
    }
}

