using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "New Item/Potion")]
public class Potion : MonoBehaviour
{
    public float healAmount = 30f;
    public float resTime = 20f;
    private Collider col;
    private MeshRenderer meshRen;
    UIConditions uiCon;
    Conditions Hp { get { return uiCon.hp; } }

    void Start()
    {
        col = GetComponent<Collider>();
        meshRen = GetComponent<MeshRenderer>();
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
