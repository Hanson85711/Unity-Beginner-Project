using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 5.0f);
    }
    // Update is called once per frame
    void Update()
    {
        transform.right = GetComponent<Rigidbody2D>().velocity;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Target")
        {
            GameUI.uiSingleton.AddScore();
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    } 
}
