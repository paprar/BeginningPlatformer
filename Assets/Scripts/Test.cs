using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Test : MonoBehaviour
{
    int speed = 10;
    int yspeed = 30;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float x = Input.GetAxis("Horizontal") * moveSpeed;
        //float y = Input.GetAxis("Vertical") * moveSpeed;
        float xMove = Input.GetAxis("Horizontal") * speed * Time.deltaTime; //x축으로 이동할 양
        float yMove = Input.GetAxis("Vertical") * yspeed * Time.deltaTime; //y축으로 이동할양
        transform.Translate(new Vector3(xMove, yMove, 0));  //이동

        //rigidBody.velocity = ;
        //transform.Translate(Vector3.forward*speed);
    }
}
