using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.Callbacks;
using UnityEngine;

public class HandGun : MonoBehaviour
{

    [SerializeField] private Transform dino;
    [SerializeField] private float handDistance;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float maxBullets;
    private float currentBullets; 
    private Animator myAnimator;
    private Vector3 handPos; 
    private Vector3 cursorPos; 
    private Vector3 direction; 

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        currentBullets = maxBullets;
        GameUI.uiSingleton.UpdateBullet(currentBullets, maxBullets); 
    }

    // Update is called once per frame
    void Update()
    {
        cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPos.z = 0f;
        //Grabs worldspace mouse position 
        direction = cursorPos - dino.position;
        //Getting direction is target position - original position

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));
        
        transform.position = dino.position + Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) * new Vector3(handDistance, 0, 0);
        
        Shoot();
        Reload();

    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && hasBullets())
        {
            myAnimator.SetTrigger("Shoot");
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * bulletSpeed;
        }
    }

    void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentBullets = maxBullets; 
            GameUI.uiSingleton.UpdateBullet(currentBullets, maxBullets);
        }
    }

    bool hasBullets()
    {
        if (currentBullets <= 0)
        {
            return false;
        }

        currentBullets--;
        GameUI.uiSingleton.UpdateBullet(currentBullets, maxBullets); 
        return true; 
    }
}
