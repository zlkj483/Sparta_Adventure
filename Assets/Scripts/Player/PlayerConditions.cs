using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConditions : MonoBehaviour
{
    public UIConditions uiCon;
    Conditions Hp { get { return uiCon.hp; } }

    // Update is called once per frame
    void Update()
    {
        if (uiCon == null || uiCon.hp == null)
        {
            Debug.LogWarning("PlayerConditions: uiCon is NULL. 틱 데미지 미적용.");
            return;
        }
        Hp.Subtract(Hp.tickDamage * Time.deltaTime);
        if (Hp.curValue <= 0f)
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
        if (Hp != null)
        {
            Hp.Add(amount);
        }
    }


}
