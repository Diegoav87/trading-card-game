using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderDisplay : MonoBehaviour
{
    public TMP_Text nameText;

    public Image imageSprite;

    public Image flag;

    public Image border;


    public void LoadLeader(Leader leader)
    {
        nameText.text = leader.name;

        imageSprite.sprite = leader.image;
        flag.sprite = leader.flag;
        border.sprite = leader.border;
    }


}