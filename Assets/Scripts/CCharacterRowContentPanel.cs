using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCharacterRowContentPanel : CRowContentPanel
{
    Character character;

    CMainCameraMovement mainCamera;
    CMenuMovement menuMovement;

    void Start()
    {
        mainCamera = Camera.main.gameObject.GetComponent<CMainCameraMovement>();
        menuMovement = GameObject.Find("UIMenuPanel").GetComponent<CMenuMovement>();
    }

    public void Init(CItem item)
    {
        character = item as Character;

        if (character.isVisible == true) SetVisible();

        ShowInitInfo(null);
    }

    public override void OnBuyButtonClick()
    {
        if (!menuMovement.isFinish) return;
        if (CCharacterManager.availableCount > character.count && CClickManager.instance.totalClick >= CCharacterManager.price)
        {
            StartCoroutine("ChangeColorCorountine", 1);

            character.count++;
            CCharacterManager.price *= 2;
            ChangeInfo();

            MakePrefab();

            if (character.count == 1) CDataManager.instance.characterManager.CountUp();
            CClickManager.instance.ClickCountDown(CCharacterManager.price);
        }
        else
        {
            StartCoroutine("ChangeColorCorountine", 0);
            Debug.Log("구매실패");
        }
    }

    void MakePrefab()
    {
        GameObject go = Instantiate(character.prefab, character.prefab.transform.position, character.prefab.transform.rotation);
        StartCoroutine("ChangeColorCorountine", 1);

        mainCamera.StartCoroutine("FollowPrefabCoroutine", go.transform.position);
        menuMovement.StartCoroutine("HideMenuCoroutine");

    }

    public void ShowInitInfo(string str)
    {
        itemImage.sprite = character.img;
        nameText.text = character.name;
        buyText.text = "♥" + character.price;
        detailText.text = "구매해주세요";
        ChangeInfo();
    }

    public void ChangeInfo()
    {
        detailText.text = character.count + "/" + CCharacterManager.availableCount + "보유";
        buyText.text = "♥" + CCharacterManager.price;
    }
}

public class Character : CItem
{
    public float upValue = 2.5f;
    public int count = 0;

    public Character(string name, GameObject prefab, Sprite img)
    {
        this.name = name;
        this.prefab = prefab;
        this.img = img;
        this.price = CCharacterManager.price;
    }
}
