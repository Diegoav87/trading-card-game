using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject cardPreviewPrefab;
    [SerializeField] float hoverDuration = 1f;

    [SerializeField] Image selectionHighlight;


    GameObject gameCanvas;
    public GameObject cardPreview;
    Coroutine hoverCoroutine;

    public Card cardData;

    private void Start()
    {
        gameCanvas = GameObject.FindGameObjectWithTag("GameCanvas");

        cardPreview = Instantiate(cardPreviewPrefab, gameCanvas.transform);
        SetCardPosition();
        cardPreview.transform.localScale = new Vector3(0f, 0f, 0f);
        cardPreview.GetComponent<CardDisplay>().LoadCard(cardData);
    }

    void SetCardPosition()
    {
        cardPreview.transform.position = transform.position;
        cardPreview.transform.position = new Vector3(transform.position.x + 210, cardPreview.transform.position.y, 0f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!GetComponent<CardController>().isFlipped)
        {
            hoverCoroutine = StartCoroutine(ShowCardPreviewAfterDelay());

        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverCoroutine != null)
        {
            StopCoroutine(hoverCoroutine);
        }
        cardPreview.transform.localScale = new Vector3(0f, 0f, 0f);
    }

    IEnumerator ShowCardPreviewAfterDelay()
    {
        yield return new WaitForSeconds(hoverDuration);

        SetCardPosition();
        cardPreview.transform.localScale = new Vector3(2.3f, 2.3f, 2.3f);
    }
}