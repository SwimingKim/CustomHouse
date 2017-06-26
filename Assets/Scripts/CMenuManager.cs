using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CMenuManager : MonoBehaviour
{
    public GameObject panel;
    public GameObject hideMenuPanel;

    public Button[] menuList;
    public GameObject[] upPanel;

    CListManager listManager;
    public CMenuMovement menuMovement;

    CMainCameraMovement mainCameraMovement;

    bool isClick = false;

    void Awake()
    {
        listManager = GetComponent<CListManager>();
        menuList[0].image.color = menuList[0].colors.disabledColor;
    }

    //130 126 112

    void Start()
    {
        mainCameraMovement = Camera.main.gameObject.GetComponent<CMainCameraMovement>();
    }

    // show, hide, camera
    public void OnChangeModeButtonClick(int num)
    {
        if (!isClick)
        {
            menuMovement.StopAllCoroutines();
            isClick = true;
            Invoke("ChangeClickState", 1.6f);
            switch (num)
            {
                case 0: // show menu
                    OnItemMenuButtonClick(0); // 집메뉴

                    hideMenuPanel.SetActive(false);
                    CGameManager.instance.ChangeDirection(CGameManager.DIRECTION.SHOW);
                    menuMovement.StartCoroutine("MenuMoveCoroutine", num);
                    break;
                case 1: // hide menu
                    hideMenuPanel.SetActive(true);
                    CGameManager.instance.ChangeDirection(CGameManager.DIRECTION.HIDE);
                    menuMovement.StartCoroutine("MenuMoveCoroutine", num);
                    break;
                case 2: // camera mode
                    CGameManager.instance.ChangeTopDirection(CGameManager.DIRECTION.CAMERA);
                    menuMovement.StartCoroutine("MenuMoveCoroutine", 2);
                    StartCoroutine("ChangeModeCoroutine", num);
                    break;
                case 3: // close camera
                    CGameManager.instance.ChangeTopDirection(CGameManager.DIRECTION.HIDE);
                    menuMovement.StartCoroutine("MenuMoveCoroutine", 2);
                    StartCoroutine("ChangeModeCoroutine", num);
                    break;
            }
            mainCameraMovement.StartCoroutine("CameraMoveCoroutine");
        }
    }

    void ChangeClickState()
    {
        isClick = false;
    }

    IEnumerator ChangeModeCoroutine(int num)
    {
        yield return new WaitForSeconds(1f);
        if (num == 2) // 카메라 모드
        {
            CGameManager.instance.ChangeDirection(CGameManager.DIRECTION.CAMERA);
            mainCameraMovement.ChangeCameraMode(true);
        }
        else if (num == 3) // 게임 모드
        {
            CGameManager.instance.ChangeDirection(CGameManager.DIRECTION.HIDE);
            mainCameraMovement.ChangeCameraMode(false);
            // mainCameraMovement.StartCoroutine("CameraMoveCoroutine");
        }
        foreach (GameObject item in upPanel)
        {
            item.SetActive(!item.activeSelf);
        }

        yield return new WaitForFixedUpdate();
        menuMovement.StartCoroutine("MenuMoveCoroutine", 3);
    }

    public void OnItemMenuButtonClick(int num)
    {
        if (menuMovement.isFinish)
        {
            foreach (Button item in menuList)
            {
                item.image.color = item.colors.normalColor;
            }

            listManager.ShowList(num);
            menuList[num].image.color = menuList[num].colors.disabledColor;
        }
    }

}
