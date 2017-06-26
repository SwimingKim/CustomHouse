using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COtherRowContentPanel : CRowContentPanel
{
    Other other;

    CMainCameraMovement mainCamera;
    CMenuMovement menuMovement;

    void Start()
    {
        mainCamera = Camera.main.gameObject.GetComponent<CMainCameraMovement>();
        menuMovement = GameObject.Find("UIMenuPanel").GetComponent<CMenuMovement>();
    }

    public void Init(CItem item)
    {
        other = item as Other;

        if (other.isVisible == true) SetVisible();

        ShowInitInfo();
    }

    public override void OnBuyButtonClick()
    {
        if (menuMovement.isFinish)
        {
            if (CClickManager.instance.totalClick >= other.price)
            {
                StartCoroutine("ChangeColorCorountine", 1);

                int upValue = Mathf.RoundToInt(other.level * other.upValue);
                other.secondValue += upValue;
                other.nowSecondValue = other.secondValue;
                other.level++;
                other.price += upValue;
                
                if (other.level == 1)
                {
                    other.prefab.SetActive(true);
                    mainCamera.StartCoroutine("FollowPrefabCoroutine",
                    other.prefab.transform.position);
                    menuMovement.StartCoroutine("HideMenuCoroutine");
                }
                if (other.level == 3)
                {
                    CDataManager.instance.otherManager.IndexUp();
                }
                ChangeInfo();
                CClickManager.instance.ClickCountDown(other.price);
            }
            else
            {
                StartCoroutine("ChangeColorCorountine", 0);
                Debug.Log("가격부족");
            }
        }

    }

    public void ShowInitInfo()
    {
        itemImage.sprite = other.img;
        ChangeInfo();
    }

    public void ChangeInfo()
    {
        if (other.level == 0)
        {
            buyText.text = "Buy";
            nameText.text = other.name;
            detailText.text = "아이템을 구매해주세요";
        }
        else
        {
            nameText.text = "레벨 " + other.level + " " + other.name;
            detailText.text = "♥" + other.secondValue + " / 초";
            buyText.text = Mathf.RoundToInt(other.level * other.upValue) + "\n♥" + other.price;
        }

    }
}


