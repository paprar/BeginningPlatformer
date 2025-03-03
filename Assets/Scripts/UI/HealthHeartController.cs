using UnityEngine;

//�̹����� �ٲٴ� �� �����ϴµ� ���� �̹��� ���� ���� ���� �̹��� ���� ����
public class HealthHeartController : MonoBehaviour
{
    

    void Start()
    {
        UpdateChildrenHealthStatus();

        
    }

    public void UpdateChildrenHealthStatus()
    {
        int childCount = transform.childCount; // �θ� ������Ʈ�� �ڽ� ���� ��������

        if (childCount < 2) return; // �ڽ��� 2�� �̻��� �ƴ� ��� ������ �ʿ� ����

        for (int i = 0; i < childCount - 1; i++) // ������ �ڽ��� �˻��� �ʿ� ����
        {
            HealthHeart currentChild = transform.GetChild(i).GetComponent<HealthHeart>();
            HealthHeart nextChild = transform.GetChild(i + 1).GetComponent<HealthHeart>();

            if (currentChild != null && nextChild != null)
            {
                if (nextChild.GetCurrentHealthState() != HealthHeart.HealthState.Empty)
                {
                    // ���� �ڽ��� ���°� Empty�� �ƴϸ� ���� �ڽ��� Full�� ����
                    currentChild.UpdateHealthImage(HealthHeart.HealthState.Full);
                }
            }
        }
    }

    // ��� �ڽ��� ���¸� Full�� �����ϴ� �޼��� �߰�
    public void RestoreAllHealth()
    {
        int childCount = transform.childCount; // �θ� ������Ʈ�� �ڽ� ���� ��������

        for (int i = 0; i < childCount; i++) // ��� �ڽ� �˻�
        {
            HealthHeart child = transform.GetChild(i).GetComponent<HealthHeart>();

            if (child != null)
            {
                child.UpdateHealthImage(HealthHeart.HealthState.Full); // ü���� Full�� ����
            }
        }
    }
}


