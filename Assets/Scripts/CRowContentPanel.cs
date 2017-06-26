using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class CRowContentPanel : MonoBehaviour
{
    public Image itemImage;
    public Text nameText;
    public Text detailText;
    public float price;
    public Button buyButton;
    public Text buyText;
    public GameObject Invisible;

    public abstract void OnBuyButtonClick();
    public void SetVisible()
    {
        Invisible.SetActive(false);
    }

    IEnumerator ChangeColorCorountine(int num)
    {
        if (num == 0)
        {
            buyButton.image.color = new Color32(118, 113, 113, 255);
        }
        else if (num == 1)
        {
            buyButton.image.color = new Color32(82, 136, 196, 255);
        }

        yield return new WaitForSeconds(0.2f);

        buyButton.image.color = new Color32(255, 255, 255, 255);
    }
}
