using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderDisplay : MonoBehaviour
{
    public TMP_Text nameText;

    public Image imageSprite;


    public void LoadLeader(Leader leader)
    {
        nameText.text = leader.name;

        imageSprite.sprite = leader.image;
    }


}