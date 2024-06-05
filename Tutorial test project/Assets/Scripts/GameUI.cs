using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public static GameUI uiSingleton {get; private set;}
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI bulletText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOver; 
    private float score;


    private void Awake()
    {
        if (uiSingleton != null && uiSingleton != this)
        {
            Destroy(this);
        }
        else
        {
            uiSingleton = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = Time.timeSinceLevelLoad.ToString("#,#");
    }

    public void AddScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void UpdateBullet(float currentAmmo, float maxAmmo)
    {
        bulletText.text = currentAmmo.ToString() + "/" + maxAmmo.ToString();
    }

    public void ShowGameOver()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0;
    }

    public void TryAgain()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene("Level1");
    }

}
