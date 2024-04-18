using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderViewDisplay : MonoBehaviour
{
    public TMP_Text nameText;

    public void LoadLeader(LeaderData leaderData)
    {
        nameText.text = leaderData.name;
    }
}
