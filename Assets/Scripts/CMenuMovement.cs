using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CMenuMovement : MonoBehaviour
{
    Vector3 upVector = new Vector3(540, 0);
    Vector3 downVector = new Vector3(540, -675);
    Vector3 topVector = new Vector3(540, -760);

    public GameObject[] upPanel;

    float dist = 100;

    public bool isFinish = true;

    void Start()
    {
        transform.position = downVector;
    }

    IEnumerator HideMenuCoroutine()
    {
        yield return new WaitForFixedUpdate();
        dist = Vector3.Distance(transform.position, downVector);
        if (dist > 0.1)
        {
            PosMove(downVector);
            StartCoroutine("HideMenuCoroutine");
        }
        else
        {
            dist = 100;

            Invoke("InvokeTime", 1f);
        }
    }

    void InvokeTime()
    {
        StopAllCoroutines();
        StartCoroutine("MenuMoveCoroutine", 0);
    }

    IEnumerator MenuMoveCoroutine(int num)
    {
        yield return new WaitForFixedUpdate();
        if (dist > 0.1)
        {
            isFinish = false;
            MenuMove(num);
            StartCoroutine("MenuMoveCoroutine", num);
        }
        else
        {
            MenuMove(num);
            StopCoroutine("MenuMoveCoroutine");
            dist = 100;
            isFinish = true;
        }
    }

    void MenuMove(int num)
    {
        Vector2 nowPos = transform.position;
        switch (num)
        {
            case 0 :
                PosMove(upVector);
                dist = Vector2.Distance(nowPos, upVector);
                break;
            case 1 :
                PosMove(downVector);
                dist = Vector2.Distance(nowPos, downVector);
                break;
            case 2 :
                PosMove(topVector);
                dist = Vector2.Distance(nowPos, topVector);
                break;
            case 3 :
                PosMove(downVector);
                dist = Vector2.Distance(nowPos, downVector);
                break;
        }
    }

    void PosMove(Vector3 pos)
    {
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime + 0.2f);
    }
}
