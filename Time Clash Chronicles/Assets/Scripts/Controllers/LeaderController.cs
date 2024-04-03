using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderController : MonoBehaviour
{
    Leader leaderData;
    int health;

    public void SetLeaderData(Leader leader)
    {
        leaderData = leader;
        health = leader.health;
    }
}
