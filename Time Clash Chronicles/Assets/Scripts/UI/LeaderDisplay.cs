using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderDisplay : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public TMP_Text healthText;
    public Image imageSprite;


    public void LoadLeader(Leader leader)
    {
        // nameText.text = leader.name;
        // descriptionText.text = leader.description;
        // healthText.text = leader.health.ToString();
        // imageSprite.sprite = leader.image;
    }


}