using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Wallet : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _amountText;

    private static float _amount = 100;
    public static float Amount => _amount;

    void Start()
    {
        _amountText.text = _amount.ToString();
        ItemController.Transfer += OnTransfer;
    }

    private void OnTransfer(float price) 
    {
        _amount += price;
        _amountText.text = _amount.ToString();
    }

    private void OnDestroy()
    {
        ItemController.Transfer -= OnTransfer;
    }
}
