using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float healAmount = 30f;
    public float resTime = 20f;
    private Collider col;
    private MeshRenderer meshRen;
    UIConditions uiCon;
    Conditions hp { get { return uiCon.hp; } }

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider>();
        meshRen = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerConditions playerConditions = other.GetComponent<PlayerConditions>();
            if (playerConditions != null)
            {
                playerConditions.Heal(healAmount);

                StartCoroutine(ResTimeCoroutine());
            }
            else
            {
                Debug.LogError("컴퍼넌트 로드 실패");
            }

        }
    }

    private IEnumerator ResTimeCoroutine()
    {
        col.enabled = false;
        meshRen.enabled = false;
        yield return new WaitForSeconds(resTime);
        col.enabled = true;
        meshRen.enabled = true;
    }
}
