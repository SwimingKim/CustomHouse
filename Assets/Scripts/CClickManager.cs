using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CClickManager : MonoBehaviour
{
    public static CClickManager instance = null;

    public Text totalClickText;
    public Text tapClickText;
    public Text secondClickText;

    public int totalClick { get; set; }
    public int tapValue;
    public int secondValue;

    float delayTime = 0.4f;
    float timer;

    public GameObject tapEffect { get; set; }
    public GameObject secondEffect;

    AudioSource source;

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

        source = GetComponent<AudioSource>();
        totalClick = 10000000;
    }

    void Start()
    {
        ShowTapValue();
        ShowSecondValue();

        InvokeRepeating("SecondClickUp", 1f, 1f);
    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    public void TapClickUp()
    {
        if (timer >= delayTime)
        {
            totalClick += tapValue;
            totalClickText.text = totalClick.ToString();

            StartCoroutine("TapEffectCoroutine");

            timer = 0f;

            source.Play();
        }
    }

    public void SecondClickUp()
    {
        totalClick += secondValue;
        totalClickText.text = totalClick.ToString();

        StartCoroutine("SecondEffectCoroutine");
    }

    public void ClickCountDown(int price)
    {
        Debug.LogFormat("total = {0} price = {1}", totalClick, price);
        totalClick -= price;
        totalClickText.text = totalClick.ToString();

        CalculateTabValue(CDataManager.instance.sumHouseTapValue(), CDataManager.instance.sumCharacterTapValue());
        CClickManager.instance.CalculateSecondValue(CDataManager.instance.sumOtherSecondValue());
        source.Play();
        Debug.Log("클릭 아이템 구매");
    }

    IEnumerator TapEffectCoroutine()
    {
        tapEffect.SetActive(true);

        yield return new WaitForSeconds(delayTime);

        tapEffect.SetActive(false);
    }

    IEnumerator SecondEffectCoroutine()
    {
        secondEffect.SetActive(true);

        yield return new WaitForSeconds(delayTime);

        secondEffect.SetActive(false);
    }

    public void CalculateSecondValue(int plus)
    {
        secondValue = 0;
        secondValue += plus;

        ShowSecondValue();
    }

    public void CalculateTabValue(int plus, int mul)
    {
        tapValue = 0;
        tapValue += plus;

        if (tapValue == 0 && mul != 0) tapValue = 1;
        if (mul != 0) tapValue *= 2 * mul;

        ShowTapValue();
    }

    public void TapValuePlus(int num)
    {
        tapValue += num;

        ShowTapValue();
    }

    public void TapValueMul(int num)
    {
        tapValue *= 2;

        ShowTapValue();
    }

    public void ShowTapValue()
    {
        tapEffect.GetComponent<Text>().text = "♥ " + tapValue;
        tapClickText.text = tapValue.ToString();
    }

    public void ShowSecondValue()
    {
        secondEffect.GetComponent<Text>().text = secondValue + " UP";
        secondClickText.text = secondValue.ToString();
    }

}
