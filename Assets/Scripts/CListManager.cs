using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CListManager : MonoBehaviour
{
    public static CListManager instance = null;

    public GameObject[] _rowContentPanelPrefab; // 행 패널 프리팹
    public Transform _contentTransform;

    public List<CItem> houseList = new List<CItem>();
    List<CItem> characterList = new List<CItem>();
    List<CItem> otherList = new List<CItem>();

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    void Start()
    {
        houseList = CDataManager.instance.GetHouseList();
        characterList = CDataManager.instance.GetCharacteList();
        otherList = CDataManager.instance.GetOtherList();

        ShowList(0);
    }

    public void ShowList(int type)
    {
        List<CItem> list = new List<CItem>();
        switch (type)
        {
            case 0:
                list = houseList;
                break;
            case 1:
                list = characterList;
                break;
            case 2:
                list = otherList;
                break;
        }

        RemoveList();

        CClickManager.instance.CalculateTabValue(CDataManager.instance.sumHouseTapValue(), CDataManager.instance.sumCharacterTapValue());
        CClickManager.instance.CalculateSecondValue(CDataManager.instance.otherManager.SumSecondValue());

        float posY = 0f; // 행 패널 y위치

        for (int i = 0; i < list.Count; i++)
        {
            // Dictionary<string, object> itemData = itemList[i] as Dictionary<string, object>;

            GameObject rowPanel = Instantiate(_rowContentPanelPrefab[type], Vector2.zero, Quaternion.identity, _contentTransform);

            rowPanel.SendMessage("Init", list[i]);

            rowPanel.GetComponent<RectTransform>().localPosition = new Vector2(0f, posY);

            posY -= 250f;

            _contentTransform.GetComponent<RectTransform>().sizeDelta = new Vector2(_contentTransform.GetComponent<RectTransform>().sizeDelta.x, Mathf.Abs(posY));
        }
    }

    public void RemoveList()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("ItemRowPanel");

        foreach (GameObject item in items)
        {
            Destroy(item);
        }
    }


}
