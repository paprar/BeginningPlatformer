using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinController : MonoBehaviour
{
    public TMP_Text coinText;
    public int currentCoin = 0;

    private LifeController life;

    // Start is called before the first frame update
    void Start()
    {
        life = FindAnyObjectByType<LifeController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetCoin(int coin)
    {
        currentCoin += coin;
        coinText.text = currentCoin.ToString();

        if (currentCoin >= 100)
        {
            currentCoin = 0;
            coin = 0;
            coinText.text = "0";
            life.GainLife();
        }

    }

}
