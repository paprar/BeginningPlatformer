using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : MonoBehaviour
{
    private void Awake()
    {
        // 게임이 실행될 때 저장된 키 값 초기화
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save(); // 변경사항을 즉시 적용
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
