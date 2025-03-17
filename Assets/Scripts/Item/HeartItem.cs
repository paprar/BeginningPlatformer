using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartItem : MonoBehaviour
{
    private ParticleSystem heartParticle;
    private ScoreController score;
    private HealthHeartController HC;
    private PlayerHealth PH;
    // Start is called before the first frame update
    void Start()
    {
        heartParticle = GetComponentInChildren<ParticleSystem>();
        score = FindAnyObjectByType<ScoreController>();
        HC = FindAnyObjectByType<HealthHeartController>();
        PH = FindAnyObjectByType<PlayerHealth>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("ÇÏÆ® È¹µæ");
            HC.RestoreCurrentHealth();
            PH.RestoreHealth();
            score.GainScore(20);
            heartParticle.Play();
            Destroy(gameObject, 0.5f);
        }
        
        
    }
}
