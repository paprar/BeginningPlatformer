using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public GameObject DeathInfo;
    private PlayerHealth playerHealth;
    private HealthHeartController HC;
    private LifeController L;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        HC = FindAnyObjectByType<HealthHeartController>();
        L = FindAnyObjectByType<LifeController>();
        playerHealth = FindAnyObjectByType<PlayerHealth>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.Instance.PlaySFX("Died");
        Debug.Log("You Died");
        if (collision.gameObject.name == "Player")
        {

            StartCoroutine(ShowDeathInfo());
            
            StartCoroutine(TeleportAfterDelay(collision.gameObject));
        }
        isDead = true;
    }

    private IEnumerator ShowDeathInfo()
    {
        DeathInfo.SetActive(true);
        yield return new WaitForSeconds(3);
        DeathInfo.SetActive(false);
    }
    private IEnumerator TeleportAfterDelay(GameObject player)
    {
        yield return new WaitForSeconds(3);
        HC.RestoreAllHealth();
        HC.ResetHealthIndex();
        isDead = false ;

        playerHealth.health = playerHealth.maxHealth;

        // 목숨 UI 업데이트
        if (L != null)
        {
            L.ReduceLife();
        }

        if (player != null)
        {
            if(PlayerPrefs.HasKey("SavePoint0"))
            {
                player.transform.position = new Vector2(37, 0);
            }
            else
            {
                player.transform.position = new Vector2(-15, 1);
            }
            
        }
    }
}
