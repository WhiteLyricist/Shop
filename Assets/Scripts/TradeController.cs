using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TradeController : MonoBehaviour, IDropHandler
{
    public static Action<GameObject> Trade = delegate { };

    public void OnDrop(PointerEventData eventData)
    {
        Trade(gameObject);
    }
}
