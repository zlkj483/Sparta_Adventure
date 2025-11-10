using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Conditions : MonoBehaviour
{
    public float curHp;
    public float maxHp;
    public float startHp;
    public float tickDamage;
    public Image uiBar;
    // Start is called before the first frame update
    void Start()
    {
        curHp = startHp;
    }

    // Update is called once per frame
    void Update()
    {
        curHp -= Time.deltaTime;
        uiBar.fillAmount = GetPercentage();
    }

    float GetPercentage()
    {
        return curHp / maxHp;
    }

    public void Heal(float heal)
    {
        curHp = Mathf.Min(curHp + heal, maxHp);
    }

    public void Takedamage(float damage)
    {
        curHp = Mathf.Max(curHp - damage, 0);
    }
}
