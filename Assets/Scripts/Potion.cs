using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float healAmount = 30f;
    public float resTime = 20f;
    private Collider col;
    private MeshRenderer meshRen;

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
            Conditions playerCondition = other.GetComponent<Conditions>();
            if (playerCondition != null)
            {
                playerCondition.Add(healAmount);
            }
            StartCoroutine(ResTimeCoroutine());

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
