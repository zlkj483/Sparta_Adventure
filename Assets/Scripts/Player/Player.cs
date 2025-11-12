using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerConditions conditions;

   public ItemData itemData;
   public Action addItem;


    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        conditions = GetComponent<PlayerConditions>();
    }

    private void Start()
    {

    }
    private void Update()
    {
    }


}