using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIConditions : MonoBehaviour
{
    public Conditions hp;
    // Start is called before the first frame update
    private void Awake()
    {
       // if (CharacterManager.Instance.Player != null && CharacterManager.Instance.Player.conditions != null)
            //CharacterManager.Instance.Player.conditions.uiCon = this;
        Debug.Log("캐릭터매니저연결");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
