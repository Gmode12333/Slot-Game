using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : GlobalReference<GameManager>
{
    [Header("Game Setting")]
    public float MusicVolume = 0.5f;
    public float SoundVolume = 0.5f;
    public int ItemMinSpawn = 50;
    public int ItemMaxSpawn = 100;
    public int Win5Line = 4;
    public int Win4Line = 2;
    public int Win3Line = 1;
    [Header("User UI And Money Data")]
    public int UserGold;
    public TextMeshProUGUI UserGoldText;
    public TextMeshProUGUI BetGoldText;
    public TextMeshProUGUI WinGoldText;
    //Popup Text
    public GameObject PopupText;
    public Transform PopupTextSpawn;
    public ParticleSystem SpinEffect;
    public GameObject AutoEffect;
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
    // Default = 0 Less Object More Chance To Win
    [Range(0, 5)]
    public int WinRate = 0;
    [Range(10f, 20f)]
    // Start Spin Speed && Spin Time
    public float SpinTime = 0.1f;
    [Range(1f, 10f)]
    public float SpinSpeed = 1f;
    // Stack Win Num
    public int WinNum;
    public int MinBet;
    public int MaxBet;
    public int betAmount = 100;
    [Header("Debug Checking Slot && HasCheck Win Slot")]
    public bool HasChecked;
    public CheckBox Checkbox;
    public string A1, A2, A3, B1, B2, B3, C1, C2, C3, D1, D2, D3, E1, E2, E3;
    public bool A1b, A2b, A3b, B1b, B2b, B3b, C1b, C2b, C3b, D1b, D2b, D3b, E1b, E2b, E3b;
    public int PayLine;
    void Start()
    {
        //Load Data
        LoadUserData();
        //Set Random Item Amount
        Spin1._itemAmount = Random.Range(ItemMinSpawn, ItemMaxSpawn);
        Spin2._itemAmount = Random.Range(ItemMinSpawn, ItemMaxSpawn);
        Spin3._itemAmount = Random.Range(ItemMinSpawn, ItemMaxSpawn);
        Spin4._itemAmount = Random.Range(ItemMinSpawn, ItemMaxSpawn);
        Spin5._itemAmount = Random.Range(ItemMinSpawn, ItemMaxSpawn + 50);
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
            CanSpin = true;
            foreach (var item in Buttons)
            {
                item.interactable = true;
            }
        }
        UserGoldText.text = UserGold.ToString();
        BetGoldText.text = betAmount.ToString();
    }
    public IEnumerator WaitForCheckWin()
    {
        yield return new WaitForSeconds(0.5f);
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
    public void TurnOnAutoSpinEffect()
    {
        if (isAutoSpin)
        {
            AutoEffect.gameObject.SetActive(false);
        }
        else
        {
            AutoEffect.gameObject.SetActive(true);
        }
    }
    IEnumerator AutoSpin()
    {
        while (isAutoSpin)
        {
            Spin();
            yield return new WaitUntil(() => CanSpin == true);
            yield return new WaitUntil(() => isSpin == false);
            yield return new WaitForSeconds(6f);
        }
    }
    public void Spin()
    {
        if (UserGold < 0 || UserGold < betAmount)
        {
            return;
        }
        SoundManager.Instance.StopSound();
        BetMoney();
        SetDefaultBool();
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
        SoundManager.Instance.PlaySound("OnSpin");
        SaveUserData();
    }
    public void CheckWin()
    {
        //Check All Slot Position
        CheckFiveLine();
        CheckThreeLine();
        CheckFourLine();
        CanSpin = true;
        HasChecked = true;
        if (WinNum > 0)
        {
            AddGold(WinNum);
            PayLine = WinNum;
            WinNum = 0;
            SaveUserData();
            SoundManager.Instance.PlaySound("Win");
        }
        else
        {
            var text = Instantiate(PopupText, PopupTextSpawn);
            text.GetComponent<PopupText>().amount = -betAmount;
            SoundManager.Instance.PlaySound("Click");
        }
    }
    public void CheckThreeLine()
    {
        if (A1 == B1 && B1 == C1)
        {
            WinNum += Win3Line;
            A1b = true;
            B1b = true;
            C1b = true;
        }
        if (A2 == B2 && B2 == C2)
        {
            WinNum += Win3Line;
            A2b = true;
            B2b = true;
            C2b = true;
        }
        if (A3 == B3 && B3 == C3)
        {
            WinNum += Win3Line;
            A3b = true;
            B3b = true;
            C3b = true;
        }
        if (B1 == C1 && C1 == D1)
        {
            WinNum += Win3Line;
            B1b = true;
            C1b = true;
            D1b = true;
        }
        if (B2 == C2 && C2 == D2)
        {
            WinNum += Win3Line;
            B2b = true;
            C2b = true;
            D2b = true;
        }
        if (B3 == C3 && C3 == D3)
        {
            WinNum += Win3Line;
            B3b = true;
            C3b = true;
            D3b = true;
        }
        if (C1 == D1 && D1 == E1)
        {
            WinNum += Win3Line;
            C1b = true;
            D1b = true;
            E1b = true;
        }
        if (C2 == D2 && D2 == E2)
        {
            WinNum += Win3Line;
            C2b = true;
            D2b = true;
            E2b = true;
        }
        if (C3 == D3 && D3 == E3)
        {
            WinNum += Win3Line;
            C3b = true;
            D3b = true;
            E3b = true;
        }
        if (A1 == B2 && B2 == C3)
        {
            WinNum += Win3Line;
            A1b = true;
            B2b = true;
            C3b = true;
        }
        if (A3 == B2 && B2 == C1)
        {
            WinNum += Win3Line;
            A3b = true;
            B2b = true;
            C1b = true;
        }
        if (C1 == D2 && D2 == E3)
        {
            WinNum += Win3Line;
            C1b = true;
            D2b = true;
            E3b = true;
        }
        if (C3 == D2 && D2 == E1)
        {
            WinNum += Win3Line;
            C3b = true;
            D2b = true;
            E1b = true;
        }
        if (B1 == C2 && C2 == D3)
        {
            WinNum += Win3Line;
            B1b = true;
            C2b = true;
            D3b = true;
        }
        if (B3 == C2 && C2 == D1)
        {
            WinNum += Win3Line;
            B3b = true;
            C2b = true;
            D1b = true;
        }
        if (D1 == C2 && C2 == B3)
        {
            WinNum += Win3Line;
            B1b = true;
            C2b = true;
            D3b = true;
        }
        if (D3 == C2 && C2 == B1)
        {
            WinNum += Win3Line;
            B3b = true;
            C2b = true;
            D1b = true;
        }
        if (A1 == B2 && B2 == C1)
        {
            WinNum += Win3Line;
            A1b = true;
            B2b = true;
            C1b = true;
        }
        if (A2 == B3 && B3 == C2)
        {
            WinNum += Win3Line;
            A2b = true;
            B3b = true;
            C2b = true;
        }
        if (B1 == C2 && C2 == D1)
        {
            WinNum += Win3Line;
            B1b = true;
            C2b = true;
            D1b = true;
        }
        if (B2 == C3 && C3 == D2)
        {
            WinNum += Win3Line;
            B2b = true;
            C3b = true;
            D2b = true;
        }
        if (C1 == D2 && D2 == E1)
        {
            WinNum += Win3Line;
            C1b = true;
            D2b = true;
            E1b = true;
        }
        if (C2 == D3 && D3 == E2)
        {
            WinNum += Win3Line;
            C2b = true;
            D3b = true;
            E2b = true;
        }
    }
    public void CheckFourLine()
    {
        if (A1 == B1 && B1 == C1 && C1 == D1)
        {
            WinNum += Win4Line;
            A1b = true;
            B1b = true;
            C1b = true;
            D1b = true;
        }
        if (A2 == B2 && B2 == C2 && C2 == D2)
        {
            WinNum += Win4Line;
            A2b = true;
            B2b = true;
            C2b = true;
            D2b = true;
        }
        if (A3 == B3 && B3 == C3 && C3 == D3)
        {
            WinNum += Win4Line;
            A3b = true;
            B3b = true;
            C3b = true;
            D3b = true;
        }
        if (B1 == C1 && C1 == D1 && D1 == E1)
        {
            WinNum += Win4Line;
            B1b = true;
            C1b = true;
            D1b = true;
            E1b = true;
        }
        if (B2 == C2 && C2 == D2 && D2 == E2)
        {
            WinNum += Win4Line;
            B2b = true;
            C2b = true;
            D2b = true;
            E2b = true;
        }
        if (B3 == C3 && C3 == D3 && D3 == E3)
        {
            WinNum += Win4Line;
            B3b = true;
            C3b = true;
            D3b = true;
            E3b = true;
        }
    }
    public void CheckFiveLine()
    {
        if (A1 == B1 && B1 == C1 && C1 == D1 && D1 == E1)
        {
            WinNum += Win5Line;
            A1b = true;
            B1b = true;
            C1b = true;
            D1b = true;
            E1b = true;
        }
        if (A2 == B2 && B2 == C2 && C2 == D2 && D2 == E2)
        {
            WinNum += Win5Line;
            A2b = true;
            B2b = true;
            C2b = true;
            D2b = true;
            E2b = true;
        }
        if (A3 == B3 && B3 == C3 && C3 == D3 && D3 == E3)
        {
            WinNum += Win5Line;
            A3b = true;
            B3b = true;
            C3b = true;
            D3b = true;
            E3b = true;
        }
        if (A1 == B2 && B2 == C3 && C3 == D2 && D2 == E1)
        {
            WinNum += Win5Line;
            A1b = true;
            B2b = true;
            C3b = true;
            D2b = true;
            E1b = true;
        }
        if (A3 == B2 && B2 == C1 && C1 == D2 && D2 == E3)
        {
            WinNum += Win5Line;
            A3b = true;
            B2b = true;
            C1b = true;
            D2b = true;
            E3b = true;
        }
        if (A2 == B1 && B1 == C2 && C2 == D1 && D1 == E2)
        {
            WinNum += Win5Line;
            A2b = true;
            B1b = true;
            C2b = true;
            D1b = true;
            E2b = true;
        }
        if (A2 == B3 && B3 == C2 && C2 == D3 && D3 == E2)
        {
            WinNum += Win5Line;
            A2b = true;
            B3b = true;
            C2b = true;
            D3b = true;
            E2b = true;
        }
        if (A1 == B1 && B1 == C2 && C2 == D3 && D3 == E3)
        {
            WinNum += Win5Line;
            A1b = true;
            B1b = true;
            C2b = true;
            D3b = true;
            E3b = true;
        }
        if (A3 == B3 && B3 == C2 && C2 == D1 && D1 == E1)
        {
            WinNum += Win5Line;
            A3b = true;
            B3b = true;
            C2b = true;
            D1b = true;
            E1b = true;
        }
        if (A2 == B3 && B3 == C2 && C2 == D1 && D1 == E2)
        {
            WinNum += Win5Line;
            A2b = true;
            B3b = true;
            C2b = true;
            D1b = true;
            E2b = true;
        }
        if (A2 == B1 && B1 == C2 && C2 == D3 && D3 == E2)
        {
            WinNum += Win5Line;
            A2b = true;
            B1b = true;
            C2b = true;
            D3b = true;
            E2b = true;
        }
        if (A1 == B2 && B2 == C2 && C2 == D2 && D2 == E1)
        {
            WinNum += Win5Line;
            A1b = true;
            B2b = true;
            C2b = true;
            D2b = true;
            E1b = true;
        }
        if (A3 == B2 && B2 == C2 && C2 == D2 && D2 == E3)
        {
            WinNum += Win5Line;
            A3b = true;
            B2b = true;
            C2b = true;
            D2b = true;
            E3b = true;
        }
        if (A1 == B2 && B2 == C1 && C1 == D2 && D2 == E1)
        {
            WinNum += Win5Line;
            A1b = true;
            B2b = true;
            C1b = true;
            D2b = true;
            E1b = true;
        }
        if (A3 == B2 && B2 == C3 && C3 == D2 && D2 == E3)
        {
            WinNum += Win5Line;
            A3b = true;
            B2b = true;
            C3b = true;
            D2b = true;
            E3b = true;
        }
        if (A2 == B2 && B2 == C1 && C1 == D2 && D2 == E2)
        {
            WinNum += Win5Line;
            A2b = true;
            B2b = true;
            C1b = true;
            D2b = true;
            E2b = true;
        }
        if (A2 == B2 && B2 == C3 && C3 == D2 && D2 == E2)
        {
            WinNum += Win5Line;
            A2b = true;
            B2b = true;
            C3b = true;
            D2b = true;
            E2b = true;
        }
        if (A1 == B1 && B1 == C3 && C3 == D1 && D1 == E1)
        {
            WinNum += Win5Line;
            A1b = true;
            B1b = true;
            C3b = true;
            D1b = true;
            E1b = true;
        }
        if (A3 == B3 && B3 == C1 && C1 == D3 && D3 == E3)
        {
            WinNum += Win5Line;
            A3b = true;
            B3b = true;
            C1b = true;
            D3b = true;
            E3b = true;
        }
        if (A1 == B1 && B1 == C2 && C2 == D1 && D1 == E1)
        {
            WinNum += Win5Line;
            A1b = true;
            B1b = true;
            C2b = true;
            D1b = true;
            E1b = true;
        }
        if (A3 == B3 && B3 == C2 && C2 == D3 && D3 == E3)
        {
            WinNum += Win5Line;
            A3b = true;
            B3b = true;
            C2b = true;
            D3b = true;
            E3b = true;
        }
        if (A1 == B3 && B3 == C3 && C3 == D3 && D3 == E1)
        {
            WinNum += Win5Line;
            A1b = true;
            B3b = true;
            C3b = true;
            D3b = true;
            E1b = true;
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
    public void SetDefaultBool()
    {
        A1b = false;
        A2b = false;
        A3b = false;
        B1b = false;
        B2b = false;
        B3b = false;
        C1b = false;
        C2b = false;
        C3b = false;
        D1b = false;
        D2b = false;
        D3b = false;
        E1b = false;
        E2b = false;
        E3b = false;
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
            betAmount = MinBet;
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
[Serializable]
public class CheckSlotWin
{
    public string SlotName;
    public bool isWin;
}
