using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�̹��� �ٲٴ°� ����
public class HealthHeartController2 : MonoBehaviour
{
    public Transform parentObject; // �θ� ������Ʈ (�ڽĵ��� �����ϴ� �θ�)
    private int currentIndex; // ���� ������ �ڽ��� �ε���

    public int stateStep; // ���� ���� ���� �ܰ� (0: Full �� Half, 1: Half �� Empty)

    private HealthHeartController HC;

    void Start()
    {
        HC = FindObjectOfType<HealthHeartController>();
        ResetHealthIndex(); // ü�� �ε��� �ʱ�ȭ

        //currentIndex = parentObject.childCount - 1; // ó������ ������ �ڽĺ��� ����
        //stateStep = 0; // Full �� Half ������� ����
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
            ProcessHealthChange(); // R Ű�� ������ ���� ���� ����
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
        }
    }

    //��Ȱ �� ȣ���Ͽ� ü�� ���Ұ� ���������� �����ϵ��� �ʱ�ȭ
    public void ResetHealthIndex()
    {
        if (parentObject != null)
        {
            currentIndex = parentObject.childCount - 1; // ���� ������ �ڽĺ��� �ٽ� ����
            //stateStep = 0; // Full �� Half�� ���� ����
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
    //    int lastIndex = parentObject.childCount - 1; // ���� ���� (������) �ڽ��� �ε���
    //    ModifySpecificHealth(lastIndex, newState);
    //}

    // child object�� HealthHeart�� �������� �ʴ°� �ƴ� ��� ���� ����
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






