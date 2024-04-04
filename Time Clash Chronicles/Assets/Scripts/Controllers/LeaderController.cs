using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LeaderController : MonoBehaviour, IPointerClickHandler
{
    Leader leaderData;
    int health;

    DeckManager deckManager;
    ArenaManager arenaManager;

    void Start()
    {
        deckManager = FindObjectOfType<DeckManager>();
        arenaManager = FindObjectOfType<ArenaManager>();
    }

    public void SetLeaderData(Leader leader)
    {
        leaderData = leader;
        health = leader.health;
    }

    public void HilightLeader()
    {
        Color color = HexToColor("#FFB1B1");
        GetComponent<Image>().color = color;
    }

    void RemoveHiglight()
    {
        GetComponent<Image>().color = Color.white;
    }

    Color HexToColor(string hex)
    {
        Color color = Color.white;
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (arenaManager.selectedAttacker && arenaManager.EnemySlotsAreEmpty())
        {
            TakeDamage(arenaManager.selectedAttacker.cardData.attack);
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        RemoveHiglight();
    }
}
