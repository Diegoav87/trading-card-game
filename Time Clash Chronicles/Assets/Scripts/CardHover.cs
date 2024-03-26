using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;

public class CardHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float verticalMoveAmount = 30f;
    public float moveTime = 0.1f;
    public float scaleAmount = 2f;

    private Vector3 startPos;
    private Vector3 startScale;

    void Start()
    {
        startPos = transform.localPosition;
        startScale = transform.localScale;
    }

    public IEnumerator MoveCard(bool startingAnimation)
    {
        Vector3 endPosition;
        Vector3 endScale;

        float elapsedTime = 0f;
        while (elapsedTime < moveTime)
        {
            elapsedTime += Time.deltaTime;

            if (startingAnimation)
            {
                endPosition = startPos + new Vector3(0f, verticalMoveAmount, 0f);
                endScale = startScale * scaleAmount;
            }
            else
            {
                endPosition = startPos;
                endScale = startScale;
            }

            Vector3 lerpedPos = Vector3.Lerp(transform.localPosition, endPosition, (elapsedTime / moveTime));
            Vector3 lerpedScale = Vector3.Lerp(transform.localScale, endScale, (elapsedTime / moveTime));

            transform.localPosition = lerpedPos;
            transform.localScale = lerpedScale;
            yield return null;
        }


    }

    public void ResetScale()
    {
        transform.localScale = startScale;
    }

    bool IsInArena()
    {
        return transform.parent.GetComponent<ArenaSlot>() != null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!IsInArena())
        {
            StartCoroutine(MoveCard(true));

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!IsInArena() && !GetComponent<CardDragDrop>().dragging)
        {
            StartCoroutine(MoveCard(false));

        }

    }
}
