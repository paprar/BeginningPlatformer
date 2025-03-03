using UnityEngine;
using UnityEngine.UI;

//이미지 바꿀 수 있는 상태 설정
public class HealthHeart : MonoBehaviour
{
    private Image healthImage; // UI Image 컴포넌트
    public Sprite fullHealthSprite;
    public Sprite halfHealthSprite;
    public Sprite emptyHealthSprite;

    public enum HealthState { Full, Half, Empty } //Heart의 상태

    void Start()
    {
        healthImage = GetComponent<Image>(); // Image 컴포넌트 가져오기
        UpdateHealthImage(HealthState.Full); // 초기 체력 상태 설정
    }

    //HealthState가 full이면 full, half면 half, empty면 empty
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

    // 이미지가 full이면 state도 full, half면 half, empty면 empty, 이 메서드는 HealthHeartController에서 불러올때 쓰임
    // nextchild의 getcurrenthealthstate가 empty가 아니면 currentchild의 updatehealthimage를 full로 설정함.
    public HealthState GetCurrentHealthState()
    {
        if (healthImage.sprite == fullHealthSprite)
            return HealthState.Full;
        if (healthImage.sprite == halfHealthSprite)
           return HealthState.Half;
        return HealthState.Empty;
    }
}




