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

public class Login : MonoBehaviour
{

    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public Button loginButton;
    public Button goToRegisterButton;
    string apiURL = "http://localhost:5000/api/";
    string data;



    // Start is called before the first frame update
    void Start()
    {
        loginButton.onClick.AddListener(login);
        // goToRegisterButton.onClick.AddListener(moveToRegister);

       

    }



    // Update is called once per frame
    void login()
    {
        Debug.Log($"Logging in '{usernameInput.text}'");
        // loadWelcomeScreen();

        StartCoroutine(LoginPlayer("login/", new { username = usernameInput.text , password = passwordInput.text }));
        
    }
    

    void moveToRegister()
    {
        SceneManager.LoadScene("Register");
    }


    IEnumerator LoginPlayer(string endpoint, object data)
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

                Debug.Log("Login successful");

                string playerData = www.downloadHandler.text;
                PlayerData player = JsonConvert.DeserializeObject<PlayerData>(playerData);
                Debug.Log(player.username);

                SceneManager.LoadScene("Menu");

            }
        }
    }
}
