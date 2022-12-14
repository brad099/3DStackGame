using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("General Panel")]
    [SerializeField] GameObject GeneralPanel;

    [Header("Home Panel")]
    [SerializeField] GameObject HomePanel;
    [SerializeField] TMP_Text Gold_Amount;
    [SerializeField] private int GoldAmount;
    [SerializeField] TMP_Text Gem_Amount;
    [SerializeField] private int GemAmount;

    [Header("Settings Panel")]
    [SerializeField] GameObject SettingsPanel;
    [SerializeField] GameObject SettingsUIPanel;

    [Header("Prize Panel")]
    [SerializeField] GameObject PrizePanel;
    [SerializeField] GameObject PrizeUIPanel;


    [Header("Shop Panel")]
    [SerializeField] GameObject ShopPanel;
    [SerializeField] GameObject ShopUIPanel;

    [Header("Win Panel")]
    [SerializeField] GameObject WinPanel;
    [SerializeField] TMP_Text WinCoinTxt;
    [SerializeField] GameObject WinUIPanel;

    [Header("Fail Panel")]
    [SerializeField] GameObject FailPanel;
    [SerializeField] GameObject FailUIPanel;



    [Header("Fly Panel")]
    [SerializeField] GameObject flycoin;
    int maxCoins;
    public Transform stayinline;
    List<GameObject> coinsQueue = new List<GameObject>();
    public Transform goldpos1000;
    public Transform goldpos500;
    public float minDuration;
    public Ease easemode;
    public float maxDuration;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        //demo value for start
        // GoldAmount = 1670;
        // PlayerPrefs.SetInt("gold",GoldAmount);
        // GemAmount = 650;
        // PlayerPrefs.SetInt("gem",GemAmount);

        FlyingCoins();

        // Getting Values on the Beginning
        GemAmount = PlayerPrefs.GetInt("gem");
        Gem_Amount.text  = "" + PlayerPrefs.GetInt("gem");
    }

    private void FlyingCoins()
    {
        for (int i = 0; i < 20; i++)
        {
        GameObject coin;
        coin = Instantiate(flycoin);
        coin.transform.parent = stayinline.transform;
        coin.SetActive(false);
        coinsQueue.Add(coin);   
        }  
    }

    private void Start()
    {
        OpenHomePanel();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            OpenWinPanel();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            OpenFailPanel();
        }

         if (Input.GetKeyDown(KeyCode.Q))
        {
         
       }
    }

    //////// General Panel ///////
    public void OpenGeneralPanel()
    {
        GeneralPanel.SetActive(true);
    }   
    public void CloseGeneralPanel()
    {
        GeneralPanel.SetActive(false);
    }

    //////// Home Panel ///////
    public void OpenHomePanel()
    {
        HomePanel.SetActive(true);
    }
    public void CloseHomePanel()
    {
        HomePanel.SetActive(false);
    }


    //////// Settings Panel ///////
    public void OpenSettingsPanel()
    {
        SettingsPanel.SetActive(true);
        SettingsUIPanel.SetActive(true);
        SettingsUIPanel.transform.localScale = Vector3.zero;
        Image panelImg = SettingsPanel.GetComponent<Image>();
        panelImg.color = new Color(0, 0, 0, 0);
        DOTween.To(() => panelImg.color, x => panelImg.color = x, new Color32(32, 32, 32, 180), 0.2f);
        SettingsUIPanel.transform.DOScale(0.7f, 0.2f);
    }
    public void CloseSettingsPanel()
    {
        Image panelImg = SettingsPanel.GetComponent<Image>();
        DOTween.To(() => panelImg.color, x => panelImg.color = x, new Color32(32, 32, 32, 0), 0.2f);
        SettingsUIPanel.transform.DOScale(0f, 0.2f).OnComplete(() =>
        {
            SettingsPanel.SetActive(false);
            SettingsUIPanel.SetActive(false);
        });
    }


    //////// Win Panel ///////
    public void OpenWinPanel()
    {
        WinPanel.SetActive(true);
        Image panelImg = WinPanel.GetComponent<Image>();
        DOTween.To(() => panelImg.color, x => panelImg.color = x, new Color32(32, 32, 32, 0), 0.2f);
        WinUIPanel.transform.DOScale(0.7f, 0.5f);
    }
    public void CloseWinPanel()
    {
        Image panelImg = WinPanel.GetComponent<Image>();
        DOTween.To(() => panelImg.color, x => panelImg.color = x, new Color32(32, 32, 32, 0), 0.2f);
        WinUIPanel.transform.DOScale(0f, 0.2f).OnComplete(() =>
        {
            GoldAmount += 76;
            PlayerPrefs.SetInt("gold",GoldAmount);
            DOTween.To(() => int.Parse(Gold_Amount.text), x => Gold_Amount.text = x.ToString(), GoldAmount, 1);
            //Gold_Amount.text = GoldAmount.ToString();
            GemAmount += 25;
            PlayerPrefs.SetInt("gem",GemAmount);
            DOTween.To(() => int.Parse(Gem_Amount.text), x => Gem_Amount.text = x.ToString(), GemAmount, 1);
            //Gem_Amount.text = GemAmount.ToString();
        });
        WinPanel.SetActive(false);
    }



    //////// Fail Panel ///////
    public void OpenFailPanel()
    {
        FailPanel.SetActive(true);
        Image panelImg = FailPanel.GetComponent<Image>();
        DOTween.To(() => panelImg.color, x => panelImg.color = x, new Color32(32, 32, 32, 0), 0.2f);
        FailUIPanel.transform.DOScale(0.7f, 0.5f);
    }
    public void CloseFailPanel()
    {
        Image panelImg = FailPanel.GetComponent<Image>();
        DOTween.To(() => panelImg.color, x => panelImg.color = x, new Color32(32, 32, 32, 0), 0.2f);
        FailUIPanel.transform.DOScale(0f, 0.2f).OnComplete(() =>
        {
            GoldAmount += 23;
            PlayerPrefs.SetInt("gold",GoldAmount);
            DOTween.To(() => int.Parse(Gold_Amount.text), x => Gold_Amount.text = x.ToString(), GoldAmount, 1);
            //Gold_Amount.text = GoldAmount.ToString();
            GemAmount += 3;
            PlayerPrefs.SetInt("gem",GemAmount);
            DOTween.To(() => int.Parse(Gem_Amount.text), x => Gem_Amount.text = x.ToString(), GemAmount, 1);
            //Gem_Amount.text = GemAmount.ToString();
        });
        FailPanel.SetActive(false);
    }



    //////// Prize Panel ///////
    public void OpenPrizePanel()
    {
        PrizePanel.SetActive(true);
        PrizeUIPanel.SetActive(true);
        PrizeUIPanel.transform.localScale = Vector3.zero;
        Image panelImg = PrizePanel.GetComponent<Image>();
        panelImg.color = new Color(0, 0, 0, 0);
        DOTween.To(() => panelImg.color, x => panelImg.color = x, new Color32(32, 32, 32, 180), 0.2f);
        PrizeUIPanel.transform.DOScale(0.7f, 0.2f);
    }
    public void ClosePrizePanel()
    {
        Image panelImg = PrizePanel.GetComponent<Image>();
        DOTween.To(() => panelImg.color, x => panelImg.color = x, new Color32(32, 32, 32, 0), 0.2f);
        PrizeUIPanel.transform.DOScale(0f, 0.2f).OnComplete(() =>
        {
            PrizePanel.SetActive(false);
            PrizeUIPanel.SetActive(false);
            GoldAmount += 214;
            PlayerPrefs.SetInt("gold",GoldAmount);
            DOTween.To(() => int.Parse(Gold_Amount.text), x => Gold_Amount.text = x.ToString(), GoldAmount, 1);
            //Gold_Amount.text = GoldAmount.ToString();
            GemAmount += 13;
            PlayerPrefs.SetInt("gem",GemAmount);
            DOTween.To(() => int.Parse(Gem_Amount.text), x => Gem_Amount.text = x.ToString(), GemAmount, 1);
            //Gem_Amount.text = GemAmount.ToString();
        });
    }




    //Shop Panel
    public void OpenShopPanel()
    {
        ShopPanel.SetActive(true);
        ShopUIPanel.SetActive(true);
        ShopUIPanel.transform.localScale = Vector3.zero;
        Image panelImg = PrizePanel.GetComponent<Image>();
        panelImg.color = new Color(0, 0, 0, 0);
        DOTween.To(() => panelImg.color, x => panelImg.color = x, new Color32(32, 32, 32, 180), 0.2f);
        ShopUIPanel.transform.DOScale(0.7f, 0.2f);
    }

    //Gold 1000 Gem-300
    public void goldpurchase1000()
    {
        if (GemAmount>= 300)
        {  
         GoldAmount += 1000;
         PlayerPrefs.SetInt("gold",GoldAmount);
         //Gold_Amount.text = GoldAmount.ToString();
         DOTween.To(() => int.Parse(Gold_Amount.text), x => Gold_Amount.text = x.ToString(), GoldAmount, 1);
         GemAmount -= 300;
         PlayerPrefs.SetInt("gem",GemAmount);
         //Gem_Amount.text = GemAmount.ToString();
         DOTween.To(() => int.Parse(Gem_Amount.text), x => Gem_Amount.text = x.ToString(), GemAmount, 1);
           foreach (var coin in coinsQueue)
         {
            coin.SetActive(true);
            coin.transform.position = goldpos1000.position + new Vector3(Random.Range(-0.07f,0.07f),0,0);
            float duration = Random.Range (minDuration,maxDuration);
            coin.transform.DOMove (Gold_Amount.rectTransform.position,duration)
            .SetEase(easemode)
            .OnComplete(()=>{
            coin.SetActive(false);
            });   
       }
       }
        else
        return;
    }

    //Gold 500 Gem-100
    public void goldpurchase500()
    {
        if (GemAmount >= 100)
        {  
        GoldAmount += 500;
        PlayerPrefs.SetInt("gold",GoldAmount);
        DOTween.To(() => int.Parse(Gold_Amount.text), x => Gold_Amount.text = x.ToString(), GoldAmount, 1);
        //Gold_Amount.text = GoldAmount.ToString();
        GemAmount -= 100;
        PlayerPrefs.SetInt("gem",GemAmount);
        //Gem_Amount.text = GemAmount.ToString();
        DOTween.To(() => int.Parse(Gem_Amount.text), x => Gem_Amount.text = x.ToString(), GemAmount, 1);
           foreach (var coin in coinsQueue)
         {
            coin.SetActive(true);
            coin.transform.position = goldpos500.position + new Vector3(Random.Range(-0.07f,0.07f),0,0);
            float duration = Random.Range (minDuration,maxDuration);
            coin.transform.DOMove (Gold_Amount.rectTransform.position,duration)
            .SetEase(easemode)
            .OnComplete(()=>{
            coin.SetActive(false);
            });   
       }
        }
        else
        return;
    }


    public void CloseShopPanel()
    {
        Image panelImg = ShopPanel.GetComponent<Image>();
        DOTween.To(() => panelImg.color, x => panelImg.color = x, new Color32(32, 32, 32, 0), 0.2f);
        ShopUIPanel.transform.DOScale(0f, 0.2f).OnComplete(() =>
        {
            ShopPanel.SetActive(false);
            ShopUIPanel.SetActive(false);
        });
    }
}