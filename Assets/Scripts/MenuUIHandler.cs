using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Added: To handle the data input on menu between scene
public class MenuUIHandler : MonoBehaviour
{
    public GameObject inputField;
    //public string userName;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UserNameInput()
    {
        string userName = inputField.GetComponent<TMP_InputField>().text;
        ScenesData.Instance.userName = userName;
        Debug.Log("userName in Menu: " + userName);

    }

    public void StartNew()
    {
        UserNameInput();

        SceneManager.LoadScene(1);
        
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
