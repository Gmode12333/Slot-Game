using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : GlobalReference<GameManager>
{
    [Header("Game Setting")]
    public int targetFrameRate = 200;
    public float MusicVolume = 0.5f;
    public float SoundVolume = 0.5f;
    public int ItemMinSpawn = 50;
    public int ItemMaxSpawn = 100;
    [Header("User UI And Money Data")]
    public int UserGold;
    public TextMeshProUGUI UserGoldText;
    public TextMeshProUGUI BetGoldText;
    public TextMeshProUGUI WinGoldText;
    //Popup Text
    public GameObject PopupText;
    public Transform PopupTextSpawn;
    public ParticleSystem SpinEffect;
    //Button That Set Uninteractable When Spin
    public List<Button> Buttons;
    //Item Slot Prefab
    public List<GameObject> ItemSlot;
    [Header("Check Bool Spin State")]
    public bool CanSpin = true;
    public bool isSpin = false;
    public bool isAutoSpin = false;
    public bool isBeginSpawn = false;
    [Header("ItemAmountspawn && SpinTime")]
    public Auto Spin1;
    public Auto Spin2;
    public Auto Spin3;
    public Auto Spin4;
    public Auto Spin5;
    // Start Spawn Amount && WinRate)
    public int ItemAmount = 50;
    // Default = 0 Higher WinRate = Higher Chance To Win
    [Range(0, 5)]
    public int WinRate = 0;
    [Range(1f, 10f)]
    // Start Spin Speed && Spin Time
    public float SpinTime = 0.1f;
    // Stack Win Num
    public int WinNum;
    public int MinBet;
    public int MaxBet;
    public int betAmount = 100;
    [Header("Debug Checking Slot && HasCheck Win")]
    public bool HasChecked;
    public string A1, A2, A3, B1, B2, B3, C1, C2, C3, D1, D2, D3, E1, E2, E3;
    void Start()
    {
        //targetFrameRate = 200 (Default);
        Application.targetFrameRate = targetFrameRate;
        //Load Data
        LoadUserData();
        //Set Random Item Amount
        Spin1._itemAmount = Random.Range(ItemMinSpawn, ItemMaxSpawn);
        Spin2._itemAmount = Random.Range(ItemMinSpawn, ItemMaxSpawn);
        Spin3._itemAmount = Random.Range(ItemMinSpawn, ItemMaxSpawn);
        Spin4._itemAmount = Random.Range(ItemMinSpawn, ItemMaxSpawn);
        Spin5._itemAmount = Random.Range(ItemMinSpawn, ItemMaxSpawn);
        //Game Volume
        SoundManager.Instance.MusicSource.volume = MusicVolume;
        SoundManager.Instance.SoundSource.volume = SoundVolume;

        //Test APK comment this if you want to build
        UserGold += 10000;
        isBeginSpawn = true;
    }

    void Update()
    {
        if (isSpin)
        {
            foreach (var item in Buttons)
            {
                item.interactable = false;
            }
            CanSpin = false;
        }
        else if(HasChecked && !isSpin)
        {
            foreach (var item in Buttons)
            {
                item.interactable = true;
            }
            CanSpin = true;
        }
        UserGoldText.text = UserGold.ToString();
        BetGoldText.text = betAmount.ToString();
    }
    public IEnumerator WaitForCheckWin()
    {
        yield return new WaitForSeconds(1f);
        if (!HasChecked)
        {
            CheckWin();
        }
    }
    public void SetAutoSpin()
    {
        if (!isAutoSpin)
        {
            isAutoSpin = true;
            StartCoroutine(AutoSpin());
        }
        else
        {
            isAutoSpin = false;
            StopCoroutine(AutoSpin());
        }
    }
    IEnumerator AutoSpin()
    {
        while (isAutoSpin && !isSpin)
        {
            Spin();
            yield return new WaitForSeconds(6f);
        }
    }
    public void Spin()
    {
        if (UserGold < 0 || UserGold < betAmount)
        {
            return;
        }
        BetMoney();
        isBeginSpawn = false;
        CanSpin = false;
        isSpin = true;
        SpinEffect.Play();
        WinGoldText.text = "0";
        Spin1.OnSpin();
        Spin2.OnSpin();
        Spin3.OnSpin();
        Spin4.OnSpin();
        Spin5.OnSpin();
        SoundManager.Instance.PlaySound("Spin");
        SaveUserData();
    }
    public void CheckWin()
    {
        //Check All Slot Position
        CheckEasy();
        CheckNormal();
        CanSpin = true;
        HasChecked = true;
        if (WinNum > 0)
        {
            AddGold(WinNum);
            WinNum = 0;
            SaveUserData();
            SoundManager.Instance.PlaySound("Win");
        }
    }
    public void CheckEasy()
    {
        if (A1 == B1 && B1 == C1 && C1 == D1 && D1 == E1)
        {
            WinNum++;
        }
        if (A2 == B2 && B2 == C2 && C2 == D2 && D2 == E2)
        {
            WinNum++;
        }
        if (A3 == B3 && B3 == C3 && C3 == D3 && D3 == E3)
        {
            WinNum++;
        }
        if (A1 == B2 && B2 == C3 && C3 == D2 && D2 == E1)
        {
            WinNum++;
        }
        if (A3 == B2 && B2 == C1 && C1 == D2 && D2 == E3)
        {
            WinNum++;
        }
    }
    public void CheckNormal()
    {
        if (A2 == B1 && B1 == C2 && C2 == D1 && D1 == E2)
        {
            WinNum++;
        }
        if (A2 == B3 && B3 == C2 && C2 == D3 && D3 == E2)
        {
            WinNum++;
        }
        if (A1 == B1 && B1 == C2 && C2 == D3 && D3 == E3)
        {
            WinNum++;
        }
        if (A3 == B3 && B3 == C2 && C2 == D1 && D1 == E1)
        {
            WinNum++;
        }
        if (A2 == B3 && B3 == C2 && C2 == D1 && D1 == E2)
        {
            WinNum++;
        }
        if (A2 == B1 && B1 == C2 && C2 == D3 && D3 == E2)
        {
            WinNum++;
        }
        if (A1 == B2 && B2 == C2 && C2 == D2 && D2 == E1)
        {
            WinNum++;
        }
        if (A3 == B2 && B2 == C2 && C2 == D2 && D2 == E3)
        {
            WinNum++;
        }
        if(A1 == B2 && B2 == C1 && C1 == D2 && D2 == E1)
        {
            WinNum++;
        }
        if (A3 == B2 && B2 == C3 && C3 == D2 && D2 == E3)
        {
            WinNum++;
        }
        if(A2 == B2 && B2 == C1 && C1 == D2 && D2 == E2)
        {
            WinNum++;
        }
        if (A2 == B2 && B2 == C3 && C3 == D2 && D2 == E2)
        {
            WinNum++;
        }
        if (A1 == B1 && B1 == C3 && C3 == D1 && D1 == E1)
        {
            WinNum++;
        }
        if (A3 == B3 && B3 == C1 && C1 == D3 && D3 == E3)
        {
            WinNum++;
        }
        if (A1 == B3 && B3 == C3 && C3 == D3 && D3 == E1)
        {
            WinNum++;
        }
    }
    public void SetMin()
    {
        betAmount = MinBet;
    }
    public void SetMax()
    {
        betAmount = MaxBet;
    }
    public void AddBetAmount()
    {
        betAmount += 50;
        if (betAmount >= MaxBet)
        {
            betAmount = MaxBet;
            return;
        }
    }
    public void MinusBetAmount()
    {
        betAmount -= 50;
        if (betAmount <= MinBet)
        {
            betAmount = 1;
            return;
        }
    }
    public void BetMoney()
    {
        UserGold -= betAmount;
    }
    public void AddGold(int amount)
    {
        // amount == WinNum;
        int money = (betAmount * amount) * 2;
        var text = Instantiate(PopupText, PopupTextSpawn);
        text.GetComponent<PopupText>().amount = money;
        UserGold += money;
        WinGoldText.text = money.ToString();
    }
    public void GetRandomItemSlot(RectTransform spawn, int itemcount)
    {
        for (int i = 0; i < itemcount; i++)
        {
            int num = Random.Range(WinRate, ItemSlot.Count);
            Instantiate(ItemSlot[num], spawn);
        }
    }
    public void SaveUserData()
    {
        PlayerPrefs.SetInt("UserGold", UserGold);
    }
    public void LoadUserData()
    {
        UserGold = PlayerPrefs.GetInt("UserGold");
    }
}
