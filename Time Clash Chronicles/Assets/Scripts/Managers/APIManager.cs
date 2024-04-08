using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class APIConnection : MonoBehaviour
{
    string apiURL = "http://localhost:5000/api/";
    string data;

    void Start()
    {
        StartCoroutine(LoginPlayer("login/", new { username = "Diego", password = "123" }));
    }

    IEnumerator SendGetRequest(string endpoint)
    {
        UnityWebRequest www = UnityWebRequest.Get(apiURL + endpoint);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log($"Request failed: {www.error}");

            if (!string.IsNullOrEmpty(www.downloadHandler.text))
            {
                Debug.LogError($"Error message: {www.downloadHandler.text}");
            }
        }
        else
        {
            data = www.downloadHandler.text;
        }
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
            }
        }
    }

    IEnumerator GetCards(string endpoint)
    {
        yield return StartCoroutine(SendGetRequest(endpoint));

        if (data != null)
        {
            CardData[] cards = JsonConvert.DeserializeObject<CardData[]>(data);

            foreach (CardData card in cards)
            {
                Debug.Log(card.name);
            }
        }
    }

    IEnumerator GetCard(string endpoint)
    {
        yield return StartCoroutine(SendGetRequest(endpoint));

        if (data != null)
        {
            CardData card = JsonConvert.DeserializeObject<CardData>(data);
            Debug.Log(card.name);
        }

    }

    IEnumerator GetPlayer(string endpoint)
    {

        yield return StartCoroutine(SendGetRequest(endpoint));

        if (data != null)
        {
            PlayerData player = JsonConvert.DeserializeObject<PlayerData>(data);
            Debug.Log("Player Name: " + player.username);
        }

    }

    IEnumerator GetDecks(string endpoint)
    {
        yield return StartCoroutine(SendGetRequest(endpoint));

        if (data != null)
        {
            DeckData[] decks = JsonConvert.DeserializeObject<DeckData[]>(data);

            foreach (DeckData deck in decks)
            {
                Debug.Log(deck.name);
            }
        }


    }

    IEnumerator GetDeck(string endpoint)
    {

        yield return StartCoroutine(SendGetRequest(endpoint));

        if (data != null)
        {
            DeckData deck = JsonConvert.DeserializeObject<DeckData>(data);
            Debug.Log("Deck Name: " + deck.name);
        }

    }

    IEnumerator GetDeckCards(string endpoint)
    {
        yield return StartCoroutine(SendGetRequest(endpoint));

        if (data != null)
        {
            CardData[] cards = JsonConvert.DeserializeObject<CardData[]>(data);

            foreach (CardData card in cards)
            {
                Debug.Log(card.name);
            }
        }


    }

    IEnumerator GetGame(string endpoint)
    {

        yield return StartCoroutine(SendGetRequest(endpoint));

        if (data != null)
        {
            GameData game = JsonConvert.DeserializeObject<GameData>(data);
            Debug.Log("game duration: " + game.duration);
        }

    }

    IEnumerator CreatePlayer(string endpoint, object playerData)
    {
        yield return StartCoroutine(SendPostRequest(endpoint, playerData));
    }

    IEnumerator CreateGame(string endpoint, object gameData)
    {
        yield return StartCoroutine(SendPostRequest(endpoint, gameData));
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
            }
        }
    }
}


