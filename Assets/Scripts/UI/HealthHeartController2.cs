using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//이미지 바꾸는걸 제어
public class HealthHeartController2 : MonoBehaviour
{
    public Transform parentObject; // 부모 오브젝트 (자식들을 관리하는 부모)
    private int currentIndex; // 현재 변경할 자식의 인덱스

    public int stateStep; // 현재 상태 변경 단계 (0: Full → Half, 1: Half → Empty)

    private HealthHeartController HC;

    void Start()
    {
        HC = FindObjectOfType<HealthHeartController>();
        ResetHealthIndex(); // 체력 인덱스 초기화

        //currentIndex = parentObject.childCount - 1; // 처음에는 마지막 자식부터 시작
        //stateStep = 0; // Full → Half 변경부터 시작
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //ModifyYoungestHealth(HealthHeart.HealthState.Full);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            //ModifyYoungestHealth(HealthHeart.HealthState.Half);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            ProcessHealthChange(); // R 키를 누르면 상태 변경 실행
        }
    }

    public void ProcessHealthChange()
    {
        if (currentIndex >= 0) // 변경할 자식이 있는 경우
        {
            //if (stateStep == 0)
            //{
                //ModifySpecificHealth(currentIndex, HealthHeart.HealthState.Half);
                //stateStep = 1; // 다음에는 Half → Empty 변경
            //}
            //else if (stateStep == 1)
            //{
                //ModifySpecificHealth(currentIndex, HealthHeart.HealthState.Empty);
                //stateStep = 0; // 다음에는 다음 자식으로 이동
                //currentIndex--; // 이전 자식으로 이동
            //}

            ModifySpecificHealth(currentIndex, HealthHeart.HealthState.Empty);
            currentIndex--; // 이전 자식으로 이동
        }
    }

    //부활 시 호출하여 체력 감소가 정상적으로 동작하도록 초기화
    public void ResetHealthIndex()
    {
        if (parentObject != null)
        {
            currentIndex = parentObject.childCount - 1; // 가장 마지막 자식부터 다시 시작
            //stateStep = 0; // Full → Half로 변경 시작
        }
    }

    //void ModifyYoungestHealth(HealthHeart.HealthState newState)
    //{
    //    if (parentObject == null || parentObject.childCount == 0)
    //    {
    //        Debug.LogWarning("No children found in parent object!");
    //        return;
    //    }
    //
    //    int lastIndex = parentObject.childCount - 1; // 가장 막내 (마지막) 자식의 인덱스
    //    ModifySpecificHealth(lastIndex, newState);
    //}

    // child object의 HealthHeart가 존재하지 않는게 아닐 경우 상태 변경
    void ModifySpecificHealth(int index, HealthHeart.HealthState state)
    {
        if (parentObject == null || index < 0 || index >= parentObject.childCount)
        {
            Debug.LogWarning("Invalid child index: " + index);
            return;
        }

        HealthHeart target = parentObject.GetChild(index).GetComponent<HealthHeart>();
        if (target != null)
        {
            target.UpdateHealthImage(state);
        }
    }
}






