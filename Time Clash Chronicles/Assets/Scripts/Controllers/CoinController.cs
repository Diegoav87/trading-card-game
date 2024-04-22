using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;



public class CoinController : MonoBehaviour
{

    public int coins;

    [SerializeField] TextMeshProUGUI coinText;

    public void UpdateCoinText()
    {
        coinText.text = coins.ToString();
    }


}
