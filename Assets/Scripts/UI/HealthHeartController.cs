using UnityEngine;

//이미지를 바꾸는 걸 제어하는데 다음 이미지 상태 보고 이전 이미지 상태 결정
public class HealthHeartController : MonoBehaviour
{
    public Transform parentObject; // 부모 오브젝트 (자식들을 관리하는 부모)
    private int currentIndex; // 현재 변경할 자식의 인덱스

    public int stateStep; // 현재 상태 변경 단계 (0: Full → Half, 1: Half → Empty)

    void Start()
    {
        ResetHealthIndex(); // 체력 인덱스 초기화
        UpdateChildrenHealthStatus();
        

    }

    public void UpdateChildrenHealthStatus()
    {
        int childCount = parentObject.childCount; // 부모 오브젝트의 자식 개수 가져오기

        if (childCount < 2) return; // 자식이 2개 이상이 아닐 경우 실행할 필요 없음

        for (int i = 0; i < childCount - 1; i++) // 마지막 자식은 검사할 필요 없음
        {
            HealthHeart currentChild = transform.GetChild(i).GetComponent<HealthHeart>();
            HealthHeart nextChild = transform.GetChild(i + 1).GetComponent<HealthHeart>();

            if (currentChild != null && nextChild != null)
            {
                if (nextChild.GetCurrentHealthState() != HealthHeart.HealthState.Empty)
                {
                    // 다음 자식의 상태가 Empty가 아니면 이전 자식을 Full로 고정
                    currentChild.UpdateHealthImage(HealthHeart.HealthState.Full);
                }
            }
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

            Debug.Log("After Change: currentIndex = " + currentIndex);
        }
    }

    // 모든 자식의 상태를 Full로 변경하는 메서드 추가
    public void RestoreAllHealth()
    {
        int childCount = parentObject.childCount; // 부모 오브젝트의 자식 개수 가져오기

        for (int i = 0; i < childCount; i++) // 모든 자식 검사
        {
            HealthHeart child = transform.GetChild(i).GetComponent<HealthHeart>();

            if (child != null)
            {
                child.UpdateHealthImage(HealthHeart.HealthState.Full); // 체력을 Full로 변경
            }
        }
    }

    public void ResetHealthIndex()
    {
        if (parentObject != null)
        {
            currentIndex = parentObject.childCount - 1; // 가장 마지막 자식부터 다시 시작
            //stateStep = 0; // Full → Half로 변경 시작
        }
    }

    public void RestoreCurrentHealth()
    {
        if (parentObject == null) return;

        int childCount = parentObject.childCount;
        if (childCount == 0) return;

        // 모든 child가 Full인지 확인
        bool allFull = true;
        for (int i = 0; i < childCount; i++)
        {
            HealthHeart child = parentObject.GetChild(i).GetComponent<HealthHeart>();
            if (child != null && child.GetCurrentHealthState() != HealthHeart.HealthState.Full)
            {
                allFull = false;
                break;
            }
        }

        if (allFull) return; // 모든 child가 Full이면 변화 없음

        // 가장 첫번째와 가까운 Empty 상태인 child를 찾아 Full로 변경
        for (int i = 0; i < childCount; i++)
        {
            HealthHeart child = parentObject.GetChild(i).GetComponent<HealthHeart>();
            if (child != null && child.GetCurrentHealthState() == HealthHeart.HealthState.Empty)
            {
                child.UpdateHealthImage(HealthHeart.HealthState.Full);
                currentIndex = i; //currentIndex를 최근 회복된 하트의 위치로 업데이트
                Debug.Log("Restored health at index: " + currentIndex);
                break; // 한 개만 변경하고 종료
            }
        }
    }


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
            Debug.Log("Changed " + index + " to " + state);
        }
        else
        {
            Debug.LogWarning("HealthHeart component not found on index " + index);
        }
    }
}
