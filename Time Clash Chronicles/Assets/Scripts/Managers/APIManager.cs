using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class APIHandler : MonoBehaviour
{
    private string URL = "http://localhost:5000/api/cards";

    void Start()
    {
        StartCoroutine(FetchCards());
    }

    IEnumerator FetchCards()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(URL))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Failed to fetch cards: " + webRequest.error);
                yield break;
            }

            string jsonResponse = webRequest.downloadHandler.text;
            CardData[] cards = JsonConvert.DeserializeObject<CardData[]>(jsonResponse);

            foreach (CardData cardData in cards)
            {
                Debug.Log(cardData.name);
            }

        }
    }
}
