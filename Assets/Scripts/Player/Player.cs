using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerConditions conditions;

    public ItemData itemData;
    public Action addItem;

    //public Transform rayStartPoint;
    public float interDistance = 3f;
    public LayerMask itemLayer;
    public GameObject potionInfoUI;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        conditions = GetComponent<PlayerConditions>();
    }

    private void Start()
    {
        if(potionInfoUI != null)
        {
            potionInfoUI.SetActive(false);
        }
    }
    private void Update()
    {
       CheckForPotion();
    }

    void CheckForPotion()
    {
        Camera mainCam = Camera.main;
        if(potionInfoUI == null || mainCam == null) return;

        RaycastHit rayHit;
        Vector3 origin = mainCam.transform.position;    
        Vector3 direction = mainCam.transform.forward;
        Vector3 boxExtent = new Vector3(0.7f, 0.7f, 0.7f);
        Quaternion orientation = mainCam.transform.rotation;
        if (Physics.BoxCast(origin, boxExtent, direction, out rayHit, orientation, interDistance, itemLayer)) // BoxCast 실행: 상자(boxExtents)를 방향(direction)으로 쏘기
        {
            if(rayHit.collider.GetComponent<Potion>() != null)
            {
                if(potionInfoUI != null)
                {
                  potionInfoUI.SetActive(true);
                }
            }
        }
        else
        {
                potionInfoUI.SetActive(false) ;
        }
        Debug.DrawRay(origin, direction * interDistance, Color.yellow);
    }
}