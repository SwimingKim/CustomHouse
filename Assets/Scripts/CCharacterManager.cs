using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;

public class CCharacterManager : MonoBehaviour
{
    public int index = 0;
    public static int price = 1200;
    public static int availableCount = 2;

    public List<CItem> characterList = new List<CItem>();
    public List<GameObject> characterObj;

    public TextAsset characterText;

    // Use this for initialization
    void Start()
    {
        Init();
    }

    void Init()
    {
        int i = 0;

        Dictionary<string, object> dataObject = MiniJSON.jsonDecode(characterText.text) as Dictionary<string, object>;
        Dictionary<string, object> characteData = dataObject["character"] as Dictionary<string, object>;
        foreach (var item in characteData)
        {
            // Debug.Log(item.Key + " : " + item.Value);
            characterList.Add(new Character(item.Value.ToString(), characterObj[i], Resources.Load<Sprite>("Sprites/Characters/" + item.Key)));
            i++;
        }

        // 첫번째 아이템은 보이게
        (characterList[index] as Character).isVisible = true;
    }

    public int sumTapValue()
    {
        int sum = 0;
        foreach (Character item in characterList)
        {
            sum += item.count;
        }
        return sum;
    }

    public List<CItem> GetCharacterList()
    {
        return characterList;
    }

    public void CountUp()
    {
        // if (sumTapValue() % 3 == 1) availableCount++;
        (characterList[++index] as Character).isVisible = true;

        CListManager.instance.ShowList(1);
    }

}
