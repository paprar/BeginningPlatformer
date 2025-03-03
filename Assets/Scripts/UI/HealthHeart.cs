using UnityEngine;
using UnityEngine.UI;

//�̹��� �ٲ� �� �ִ� ���� ����
public class HealthHeart : MonoBehaviour
{
    private Image healthImage; // UI Image ������Ʈ
    public Sprite fullHealthSprite;
    public Sprite halfHealthSprite;
    public Sprite emptyHealthSprite;

    public enum HealthState { Full, Half, Empty } //Heart�� ����

    void Start()
    {
        healthImage = GetComponent<Image>(); // Image ������Ʈ ��������
        UpdateHealthImage(HealthState.Full); // �ʱ� ü�� ���� ����
    }

    //HealthState�� full�̸� full, half�� half, empty�� empty
    public void UpdateHealthImage(HealthState state) 
    {
        if (healthImage == null)
        {
            Debug.LogError("Health Image component is missing!");
            return;
        }

        switch (state)
        {
            case HealthState.Full:
                healthImage.sprite = fullHealthSprite;
                break;
            case HealthState.Half:
                healthImage.sprite = halfHealthSprite;
                break;
            case HealthState.Empty:
                healthImage.sprite = emptyHealthSprite;
                break;
            default:
                Debug.LogWarning("Unknown health state");
                break;
        }
    }

    // �̹����� full�̸� state�� full, half�� half, empty�� empty, �� �޼���� HealthHeartController���� �ҷ��ö� ����
    // nextchild�� getcurrenthealthstate�� empty�� �ƴϸ� currentchild�� updatehealthimage�� full�� ������.
    public HealthState GetCurrentHealthState()
    {
        if (healthImage.sprite == fullHealthSprite)
            return HealthState.Full;
        if (healthImage.sprite == halfHealthSprite)
           return HealthState.Half;
        return HealthState.Empty;
    }
}




