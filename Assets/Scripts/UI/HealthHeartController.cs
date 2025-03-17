using UnityEngine;

//�̹����� �ٲٴ� �� �����ϴµ� ���� �̹��� ���� ���� ���� �̹��� ���� ����
public class HealthHeartController : MonoBehaviour
{
    public Transform parentObject; // �θ� ������Ʈ (�ڽĵ��� �����ϴ� �θ�)
    private int currentIndex; // ���� ������ �ڽ��� �ε���

    public int stateStep; // ���� ���� ���� �ܰ� (0: Full �� Half, 1: Half �� Empty)

    void Start()
    {
        ResetHealthIndex(); // ü�� �ε��� �ʱ�ȭ
        UpdateChildrenHealthStatus();
        

    }

    public void UpdateChildrenHealthStatus()
    {
        int childCount = parentObject.childCount; // �θ� ������Ʈ�� �ڽ� ���� ��������

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

    public void ProcessHealthChange()
    {
        if (currentIndex >= 0) // ������ �ڽ��� �ִ� ���
        {
            //if (stateStep == 0)
            //{
            //ModifySpecificHealth(currentIndex, HealthHeart.HealthState.Half);
            //stateStep = 1; // �������� Half �� Empty ����
            //}
            //else if (stateStep == 1)
            //{
            //ModifySpecificHealth(currentIndex, HealthHeart.HealthState.Empty);
            //stateStep = 0; // �������� ���� �ڽ����� �̵�
            //currentIndex--; // ���� �ڽ����� �̵�
            //}

            ModifySpecificHealth(currentIndex, HealthHeart.HealthState.Empty);
            currentIndex--; // ���� �ڽ����� �̵�

            Debug.Log("After Change: currentIndex = " + currentIndex);
        }
    }

    // ��� �ڽ��� ���¸� Full�� �����ϴ� �޼��� �߰�
    public void RestoreAllHealth()
    {
        int childCount = parentObject.childCount; // �θ� ������Ʈ�� �ڽ� ���� ��������

        for (int i = 0; i < childCount; i++) // ��� �ڽ� �˻�
        {
            HealthHeart child = transform.GetChild(i).GetComponent<HealthHeart>();

            if (child != null)
            {
                child.UpdateHealthImage(HealthHeart.HealthState.Full); // ü���� Full�� ����
            }
        }
    }

    public void ResetHealthIndex()
    {
        if (parentObject != null)
        {
            currentIndex = parentObject.childCount - 1; // ���� ������ �ڽĺ��� �ٽ� ����
            //stateStep = 0; // Full �� Half�� ���� ����
        }
    }

    public void RestoreCurrentHealth()
    {
        if (parentObject == null) return;

        int childCount = parentObject.childCount;
        if (childCount == 0) return;

        // ��� child�� Full���� Ȯ��
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

        if (allFull) return; // ��� child�� Full�̸� ��ȭ ����

        // ���� ù��°�� ����� Empty ������ child�� ã�� Full�� ����
        for (int i = 0; i < childCount; i++)
        {
            HealthHeart child = parentObject.GetChild(i).GetComponent<HealthHeart>();
            if (child != null && child.GetCurrentHealthState() == HealthHeart.HealthState.Empty)
            {
                child.UpdateHealthImage(HealthHeart.HealthState.Full);
                currentIndex = i; //currentIndex�� �ֱ� ȸ���� ��Ʈ�� ��ġ�� ������Ʈ
                Debug.Log("Restored health at index: " + currentIndex);
                break; // �� ���� �����ϰ� ����
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
