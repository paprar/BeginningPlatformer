using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : MonoBehaviour
{
    private ParticleSystem coinParticle;
    private ScoreController score;
    private CoinController coin;
    // Start is called before the first frame update
    void Start()
    {
        coinParticle = GetComponentInChildren<ParticleSystem>();
        score = FindAnyObjectByType<ScoreController>();
        coin = FindAnyObjectByType<CoinController>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Debug.Log("ÄÚÀÎ È¹µæ");
            coinParticle.Play();
            score.GainScore(20);
            coin.GetCoin(1);
            Destroy(gameObject,0.5f);
        }
        
    }
}
