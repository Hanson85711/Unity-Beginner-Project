using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Sprite[] sushiSprites; 
    [SerializeField] private float spawnCooldown;
    [SerializeField] private float sushiMilestone;
    private float milestoneProgress; 
    private float timer;
    private float xPos;
    private BoxCollider2D col;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        timer = spawnCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (milestoneProgress == sushiMilestone && spawnCooldown > 0.5f)
        {
            milestoneProgress = 0; 
            spawnCooldown -= 0.5f; 
        }

        if (timer < 0)
        {
            xPos = Random.Range(col.bounds.min.x, col.bounds.max.x);
            GameObject sushiTarget = Instantiate(target, new Vector3(xPos, transform.position.y, transform.position.z), Quaternion.identity);
            sushiTarget.GetComponent<SpriteRenderer>().sprite = sushiSprites[Random.Range(0, sushiSprites.Length)];
            milestoneProgress++; 
            timer = spawnCooldown;
        }
    }
}
