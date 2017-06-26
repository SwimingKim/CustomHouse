using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CItemGameObject : MonoBehaviour
{
    public GameObject effect;
    public Transform genPos;

    public void ShowEffect()
    {
        GameObject go = Instantiate(effect, genPos.position, Quaternion.identity);
        go.transform.SetParent(transform);
    }

}
