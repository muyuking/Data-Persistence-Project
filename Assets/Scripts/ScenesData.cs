using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Added: To handle the persistence data between scene
public class ScenesData : MonoBehaviour
{
    public static ScenesData Instance;

    public string userName;
    public int maxPoints;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {

    }

    [System.Serializable]
    class SaveData
    {
        //public string userName;
        public int maxPoints;

    }

    public void SavePlayerData()
    {
        SaveData data = new SaveData();
        //data.userName = userName;
        data.maxPoints = maxPoints;
        Debug.Log("maxPoints in ScenesData.SavePlayerData: " + maxPoints);

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public int LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            //userName = data.userName;
            maxPoints = data.maxPoints;

            Debug.Log("maxPoints in ScenesData.LoadPlayerData: " + maxPoints);

            return maxPoints;
        }
        else
        {
            return 0;
        }


    }


}
