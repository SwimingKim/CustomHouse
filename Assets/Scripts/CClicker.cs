using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CClicker : MonoBehaviour
{
    public Ray ray;
    public RaycastHit hitInfo;

    Quaternion quaternion;
    float value;

    void Start()
    {
        quaternion = Quaternion.identity;
        value = 10f;
    }

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hitInfo))
            {
    			CClickManager.instance.TapClickUp();
                ClickEvent();
                Debug.Log("캐릭터 클릭");
            }
        }
    }

    void ClickEvent()
    {
        quaternion.eulerAngles = new Vector3(0, 180, value);

        transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, Time.deltaTime * 5f);

        value *= -1;
    }

}
