using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerTransform : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale *= 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale /= 1.1f;
    }

    
}
