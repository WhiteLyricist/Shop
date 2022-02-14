using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemController : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public static Action<float> Transfer = delegate { };

    public float price;

    private Vector2 _startPosition;

    private RectTransform _rectTransform;

    private bool _item = false;

    private CanvasGroup _canvasGroup;

    private void Start()
    {
        TradeController.Trade += OnTrade;
        BackgroundItemHandler.ToBackground += OnToBackground;

        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnTrade(GameObject parent) 
    {
        if (transform.parent.gameObject.name == "Shop" && parent.name == "Inventory" && _item)
        {
            if (Wallet.Amount - price >= 0)
            {
                Transfer(-1.0f * price);
            }
            else 
            {
                OnToBackground();
                return;
            }
        }

        if (transform.parent.gameObject.name == "Inventory" && parent.name == "Shop" && _item)
        {
            Transfer(+0.7f * price);
        }

        if (_item) { transform.parent = null; transform.parent = parent.transform; }
        _item = false;
    }

    private void OnToBackground() 
    {
        if (_item)
        {
            _rectTransform.anchoredPosition = _startPosition;
            _item = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPosition = _rectTransform.anchoredPosition;
        _canvasGroup.blocksRaycasts = false;
        _item = true;
    }

    private void OnNoMoneyTransfer() 
    {
        if (_item)
        {
            _rectTransform.anchoredPosition = _startPosition;
            _item = false;
        }
    }

    private void OnDestroy()
    {
        TradeController.Trade -= OnTrade;
        BackgroundItemHandler.ToBackground -= OnToBackground;
    }
}
