using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConditions : MonoBehaviour
{
    public UIConditions uiCon;
    Conditions hp { get { return uiCon.hp; } }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (uiCon == null || uiCon.hp == null)
        {
            Debug.LogWarning("PlayerConditions: uiCon is NULL. 틱 데미지 미적용.");
            return;
        }
        hp.Subtract(hp.tickDamage * Time.deltaTime);
        if (hp.curValue <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Player Die");
    }

    public void Heal(float amount)
    {
        if (hp != null)
        {
            hp.Add(amount);
        }
    }
}
