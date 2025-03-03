using UnityEngine;

//이미지를 바꾸는 걸 제어하는데 다음 이미지 상태 보고 이전 이미지 상태 결정
public class HealthHeartController : MonoBehaviour
{
    

    void Start()
    {
        UpdateChildrenHealthStatus();

        
    }

    public void UpdateChildrenHealthStatus()
    {
        int childCount = transform.childCount; // 부모 오브젝트의 자식 개수 가져오기

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

    // 모든 자식의 상태를 Full로 변경하는 메서드 추가
    public void RestoreAllHealth()
    {
        int childCount = transform.childCount; // 부모 오브젝트의 자식 개수 가져오기

        for (int i = 0; i < childCount; i++) // 모든 자식 검사
        {
            HealthHeart child = transform.GetChild(i).GetComponent<HealthHeart>();

            if (child != null)
            {
                child.UpdateHealthImage(HealthHeart.HealthState.Full); // 체력을 Full로 변경
            }
        }
    }
}


