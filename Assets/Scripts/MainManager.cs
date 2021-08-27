using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    public int maxPoints;
    public Text BestScoreText;

    public static MainManager Instance;
    public GameObject inputField;
    public string userName;


    // Start is called before the first frame update
    void Start()
    {
        //Added: To load maxPoints from saved ScenesData
        maxPoints = ScenesData.Instance.LoadPlayerData();
        //Debug.Log("MainManager.maxPoints: " + ScenesData.Instance.LoadPlayerData());


        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        //Added: To load username from ScenesData
        userName = ScenesData.Instance.userName;
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";

        //Added: To display userName and maxPoints on top of the screen
        BestScoreText.text = $"The Best Score: {userName} : {maxPoints}";
    }

    public void GameOver()
    {
        //Added: To set maxPoints to ScenesData
        ScenesData.Instance.maxPoints = MaxPoints();
        //Added: To save data to ScenesData
        ScenesData.Instance.SavePlayerData();

        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    //Added: To update the maxPoints
    public int MaxPoints()
    {
        if (m_Points > maxPoints)
        {
            maxPoints = m_Points;
        }
        return maxPoints;
    }


}
