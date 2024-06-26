using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class Register : MonoBehaviour
{

    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public Button registerButton;
    public Button loginButton;

    string apiURL = "http://localhost:5000/api/";


    void Start()
    {
        registerButton.onClick.AddListener(RegisterPlayer);
        loginButton.onClick.AddListener(GoToLogin);
    }

    void CreatePlayer(string endpoint, object playerData)
    {
        StartCoroutine(SendPostRequest(endpoint, playerData));
    }

    IEnumerator SendPostRequest(string endpoint, object data)
    {
        string jsonData = JsonConvert.SerializeObject(data);

        using (UnityWebRequest www = new UnityWebRequest(apiURL + endpoint, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Post request failed: {www.error}");
            }
            else
            {
                Debug.Log($"Post request successful!");
                SceneManager.LoadScene("Login1");
            }
        }
    }


    void RegisterPlayer()
    {
        CreatePlayer("players/", new { username = usernameInput.text, password = passwordInput.text });
    }

    void GoToLogin()
    {
        SceneManager.LoadScene("Login1");
    }


}




