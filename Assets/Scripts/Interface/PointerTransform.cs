using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerTransform : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new(1f, 1f, 1f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new(1.1f, 1.1f, 1.1f);
    }
}
