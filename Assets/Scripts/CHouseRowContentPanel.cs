using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CHouseRowContentPanel : CRowContentPanel
{
    House house;

    public void Init(CItem item)
    {
        house = item as House;

        if (house.isVisible == true) SetVisible();

        if (house.level == 0)
        {
            ShowInitInfo("Buy");
            return;
        }
        else if (house.level == house.maxlevel)
        {
            buyButton.enabled = false;
            ShowInitInfo("Max Level");
            return;
        }
        else
        {
            ShowInitInfo(null);
        }
    }

    public override void OnBuyButtonClick()
    {
        if (CClickManager.instance.totalClick >= house.price)
        {
            StartCoroutine("ChangeColorCorountine", 1);

            int upValue = Mathf.RoundToInt(house.level * house.upValue);
            house.tapValue += upValue;
            house.nowTapValue = house.tapValue;
            house.price += upValue;
            house.level++;

            CClickManager.instance.CalculateTabValue(CDataManager.instance.sumHouseTapValue(), CDataManager.instance.sumCharacterTapValue());

            if (house.level == house.maxlevel)
            {
                house.level = house.maxlevel;
                CDataManager.instance.houseManager.IndexUp();
            }
            else if (house.level == 1)
            {
                CDataManager.instance.houseManager.RefreshHouse();
            }
            else
            {
                house.prefab.SendMessage("ShowEffect");
            }
            ChangeInfo(null);
            CClickManager.instance.ClickCountDown(house.price);
        }
        else
        {
            StartCoroutine("ChangeColorCorountine", 0);
            Debug.Log("가격 부족");
        }

    }

    public void ShowInitInfo(string str)
    {
        itemImage.sprite = house.img;
        ChangeInfo(str);
    }

    public void ChangeInfo(string str)
    {
        if (house.level == 0)
        {
            nameText.text = house.name;
            detailText.text = "아이템을 구매해보세요";
        }
        else
        {
            nameText.text = "레벨 " + house.level + " " + house.name;
            detailText.text = "♥" + house.tapValue + " / 탭";
        }

        if (str != null) buyText.text = str;
        else buyText.text = "+ " + Mathf.RoundToInt(house.level * house.upValue) + "\n♥" + house.price;
    }

}


