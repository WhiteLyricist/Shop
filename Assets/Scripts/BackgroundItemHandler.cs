using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackgroundItemHandler : MonoBehaviour, IDropHandler
{
    public static Action ToBackground = delegate { };

    public void OnDrop(PointerEventData eventData)
    {
        ToBackground();
    }
}
