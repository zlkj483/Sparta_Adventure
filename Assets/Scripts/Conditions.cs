using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Conditions : MonoBehaviour
{
    public float curValue;
    public float maxValue;
    public float startValue;
    public float tickDamage;
    public Image uiBar;
    // Start is called before the first frame update
    void Start()
    {
        curValue = startValue;
    }
    void UpdateUI()
    {
        uiBar.fillAmount = GetPercentage();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    float GetPercentage()
    {
        return curValue / maxValue;
    }

    public void Add(float amount)
    {
        curValue = Mathf.Min(curValue + amount, maxValue);
        UpdateUI(); 
        Debug.Log($"{amount}만큼 회복함");
    }

    public void Subtract(float amount)
    {
       
        curValue = Mathf.Max(curValue - amount, 0);
        UpdateUI();
    }
}
