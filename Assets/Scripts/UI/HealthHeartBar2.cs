using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHeartBar2 : MonoBehaviour
{
    public GameObject heartPrefab;
    public PlayerHealth playerHealth;
    
    List<HealthHeart2> hearts = new List<HealthHeart2>();

    public void DrawHearts()
    {
        //8 -> 4 hearts
        //determine how many hearts to make total
        //based off the max health
        ClearHearts();

        float maxHealthRemainder = playerHealth.maxHealth % 2;
        int heartsToMake = (int)((playerHealth.maxHealth /2)+maxHealthRemainder);
        for(int i = 0; i < heartsToMake; i++)
        {
            CreateEmptyHeart();
        }

        for(int i = 0; i < hearts.Count;i++)
        {
            int heartsStatusRemainder = (int)Mathf.Clamp(playerHealth.health - (i * 2), 0, 2);
            hearts[i].SetHeartImage((HealthHeart2.HeartStatus)heartsStatusRemainder);
        }
    }

    public void CreateEmptyHeart()
    {
        GameObject newHearth = Instantiate(heartPrefab);
        newHearth.transform.SetParent(transform);

        HealthHeart2 heartCommponent = newHearth.GetComponent<HealthHeart2>();
        heartCommponent.SetHeartImage(HealthHeart2.HeartStatus.Empty);
        hearts.Add(heartCommponent);
    }
    public void ClearHearts()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<HealthHeart2>();
    }

}
