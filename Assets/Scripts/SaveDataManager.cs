using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    private void OnApplicationQuit()
    {
        // 게임 종료 시 SavePoint1 값 제거
        if (PlayerPrefs.HasKey("SavePoint1"))
        {
            PlayerPrefs.DeleteKey("SavePoint1");
            Debug.Log("게임 종료: SavePoint1 값 삭제됨!");
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            if(PlayerPrefs.HasKey("SavePoint1"))
            {
                PlayerPrefs.DeleteKey("SavePoint1");
                Debug.Log("플레이 모드 중지: SavePoint1 값 삭제됨!");
            }
        }
    }
}
