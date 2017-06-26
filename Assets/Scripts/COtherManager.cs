using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;

public class COtherManager : MonoBehaviour
{
    public int index = 0;

    public List<CItem> otherList = new List<CItem>();
    public List<GameObject> otherObj;

    public TextAsset otherText;

    // Use this for initialization
    void Start()
    {
        Init();
    }

    void Init()
    {
        int i = 0;

        Dictionary<string, object> dataObject = MiniJSON.jsonDecode(otherText.text) as Dictionary<string, object>;
        // 기타
        Dictionary<string, object> otherData = dataObject["other"] as Dictionary<string, object>;
        foreach (var item in otherData)
        {
            // Debug.Log(item.Key);
            Dictionary<string, object> dicData = item.Value as Dictionary<string, object>;

            string name = dicData["name"].ToString();
            int secondValue = int.Parse(dicData["secondValue"].ToString());
            int price = int.Parse(dicData["price"].ToString());

            otherList.Add(new Other(name, otherObj[i], Resources.Load<Sprite>("Sprites/Others/" + item.Key), secondValue, price));
            i++;
        }

        (otherList[index] as Other).isVisible = true;
        (otherList[0] as Other).level = 1;
    }

    public List<CItem> GetHouseList()
    {
        return otherList;
    }

    public void IndexUp()
    {
        (otherList[++index] as Other).isVisible = true;
        CListManager.instance.ShowList(2);
    }

    public int SumSecondValue()
    {
        int sum = 0;
        foreach (Other other in otherList)
        {
            if (other.level != 0)
                sum += other.nowSecondValue;
        }
        return sum;
    }

}

public class Other : CItem
{
    public int level = 0;
    public float upValue = 1.07f;
    public int secondValue;
    public int nowSecondValue;

    public Other(string name, GameObject prefab, Sprite img, int secondValue, int price)
    {
        this.name = name;
        this.prefab = prefab;
        this.img = img;
        this.secondValue = secondValue;
        this.price = price;
        this.nowSecondValue = secondValue;
    }
}