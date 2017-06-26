using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;

public class CDataManager : MonoBehaviour
{
    public static CDataManager instance = null;

    public CHouseManager houseManager;
    public CCharacterManager characterManager;
    public COtherManager otherManager;

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

        houseManager = GetComponent<CHouseManager>();
        characterManager = GetComponent<CCharacterManager>();
        otherManager = GetComponent<COtherManager>();
    }

    public List<CItem> GetHouseList()
    {
        return houseManager.houseList;
    }
    public List<CItem> GetCharacteList()
    {
        return characterManager.characterList;
    }
    public List<CItem> GetOtherList()
    {
        return otherManager.otherList;
    }

    public int sumHouseTapValue()
    {
        return houseManager.sumTapValue();
    }
    public int sumCharacterTapValue()
    {
        return characterManager.sumTapValue();
    }
    public int sumOtherSecondValue()
    {
        return otherManager.SumSecondValue();
    }

}
