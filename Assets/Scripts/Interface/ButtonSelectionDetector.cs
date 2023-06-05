using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelectionDetector : MonoBehaviour, ISelectHandler, IDeselectHandler {
    public void OnDeselect(BaseEventData eventData)
    {
        transform.localScale = new(1f, 1f, 1f);
    }

    public void OnSelect(BaseEventData eventData)
    {
        transform.localScale = new(1.1f, 1.1f, 1.1f);
    }
}