using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LeaderController : MonoBehaviour, IPointerClickHandler
{
    Leader leaderData;
    float health;

    float maxHealth;

    [SerializeField] Image leaderImage;
    [SerializeField] Image healthBar;

    ArenaManager arenaManager;

    void Start()
    {
        arenaManager = FindObjectOfType<ArenaManager>();
    }

    public void SetLeaderData(Leader leader)
    {
        leaderData = leader;
        health = leader.health;
        maxHealth = leader.health;
    }

    public void HilightLeader()
    {
        Color color = HexToColor("#FFB1B1");
        leaderImage.color = color;
    }

    public void RemoveHiglight()
    {
        leaderImage.color = Color.white;
    }

    Color HexToColor(string hex)
    {
        Color color = Color.white;
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (arenaManager.selectedAttacker)
        {
            if (!arenaManager.selectedAttacker.hasAttacked)
            {
                if (arenaManager.selectedAttacker.tag == "PlayerCard")
                {
                    if (arenaManager.EnemySlotsAreEmpty())
                    {
                        arenaManager.selectedAttacker.hasAttacked = true;
                        TakeDamage(arenaManager.selectedAttacker.attack);


                    }
                }
                else
                {
                    if (arenaManager.PlayerSlotsAreEmpty())
                    {
                        arenaManager.selectedAttacker.hasAttacked = true;
                        TakeDamage(arenaManager.selectedAttacker.attack);

                    }
                }
            }
            else
            {
                Debug.Log("Card already attacked this turn");
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        RemoveHiglight();
        arenaManager.selectedAttacker.DeselectCard();
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = health / maxHealth;
    }
}
