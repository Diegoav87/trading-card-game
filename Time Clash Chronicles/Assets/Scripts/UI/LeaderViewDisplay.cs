using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderViewDisplay : MonoBehaviour
{
    public TMP_Text nameText;
    public Image image;
    public Image flag;

    public void LoadLeader(int id, string name)
    {
        nameText.text = name;
        flag.sprite = Resources.Load<Sprite>("Images/Leaders/Flags/" + id);
        image.sprite = Resources.Load<Sprite>("Images/Leaders/" + id);
    }
}
