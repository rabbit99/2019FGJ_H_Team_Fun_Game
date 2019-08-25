using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThankMessage : MonoBehaviour
{
    public  string[] creatorsName = { "阿空", "安安", "李奇", "威廉", "波卡", "GMF", };
    public  string[] thanksFor = { "遊玩", "參與", "嘗試", "被動贊助" };

    public Text txtThank;

    private void Start()
    {
        txtThank = GetComponent<Text>();
    }

    public  void GetRandomThankMessage()
    {
        txtThank.text = creatorsName[Random.Range(0, creatorsName.Length)] + "感謝你的" + thanksFor[Random.Range(0, thanksFor.Length)];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            GetRandomThankMessage();
    }
}
