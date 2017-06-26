using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameData;

public class CHouseManager : MonoBehaviour
{
    public int index = 0;

    public List<CItem> houseList = new List<CItem>();
    public List<GameObject> houseObj;

    public GameObject[] tapEffects;

    public TextAsset houseText;

    void Start()
    {
        CClickManager.instance.tapEffect = tapEffects[0];

        Init();
    }

    void Init()
    {
        Dictionary<string, object> dataObject = MiniJSON.jsonDecode(houseText.text) as Dictionary<string, object>;

        // 집
        int i = 0;
        Dictionary<string, object> houseData = dataObject["house"] as Dictionary<string, object>;
        foreach (var item in houseData)
        {
            // Debug.Log(item.Key);
            Dictionary<string, object> dicData = item.Value as Dictionary<string, object>;

            string name = dicData["name"].ToString();
            int maxlevel = int.Parse(dicData["maxlevel"].ToString());
            int tapValue = int.Parse(dicData["tapValue"].ToString());
            int price = int.Parse(dicData["price"].ToString());

            houseList.Add(new House(name, houseObj[i], maxlevel, Resources.Load<Sprite>("Sprites/Houses/"+item.Key), tapValue, price));
            i++;
        }

        // 첫번째 아이템은 보이게
        (houseList[index] as House).isVisible = true;
        (houseList[0] as House).level = 1;
    }

    public int sumTapValue()
    {
        int sum = 0;
        foreach (House item in houseList)
        {
            if (item.level != 0)
                sum += item.nowTapValue;
        }
        return sum;
    }

    public List<CItem> GetHouseList()
    {
        return houseList;
    }

    public void IndexUp()
    {
        (houseList[++index] as House).isVisible = true;
        CListManager.instance.ShowList(0);
    }

    public void RefreshHouse()
    {
        foreach (GameObject item in houseObj)
        {
            item.SetActive(false);
        }
        houseObj[index].SetActive(true);

        CClickManager.instance.tapEffect = tapEffects[index];
    }

}


public class House : CItem
{
    public int maxlevel;
    public int level = 0;
    public float upValue = 1.64f;
    public int tapValue;
    public int nowTapValue;

    public House(string name, GameObject prefab, int maxlevel, Sprite img, int tapValue, int price)
    {
        this.name = name;
        this.prefab = prefab;
        this.maxlevel = maxlevel;
        this.img = img;
        this.tapValue = tapValue;
        this.price = price;
        this.nowTapValue = tapValue;
    }

}
