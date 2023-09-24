﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ListController : MonoBehaviour
{
    private List<Character> monsters;
    private List<GameObject> ListItems;
    private int money;
    private Transform scrollViewContent;

    public GameObject btnCrown, btnTreasure;
    public TextMeshProUGUI countCrown, countTreasure;

    void Start()
    {
        ListItems = new List<GameObject>();
        scrollViewContent = GameObject.Find("Content").transform;
        InstantiateItems();
        UpdateUI();

    }

    public void UpdateUI()
    {
        foreach (GameObject obj in ListItems)
            obj.GetComponent<ListItemController>().SetButton();
        SetItemButton();
        GameObject.Find("Market Manager").GetComponent<MarketManager>().UpdateUI();
    }

    public void ReSpawnMonster(Character monster)
    {
        GameObject.Find("Market Manager").GetComponent<MarketManager>().SpawnMonster(monster);
    }

    public void SetData(List<Character> monsters)
    {
        this.monsters = monsters;
    }

    public void InstantiateItems()
    {
        while (ListItems.Count > 0)
            Destroy(ListItems[ListItems.Count - 1]);
        for (int i = 0; i < monsters.Count; i++)
        {
            GameObject listItemObject = Instantiate(Resources.Load<GameObject>("UI/listItem"), scrollViewContent);
            listItemObject.GetComponent<ListItemController>().SetText(monsters[i]);
            ListItems.Add(listItemObject);
        }

    }

    public void SetItemButton()
    {

        if (500 > GameManager.s_Instance.player.Gold)
        {

            btnTreasure.GetComponent<Image>().sprite = Resources.Load("UI/btn_color_red", typeof(Sprite)) as Sprite;
        }
        else
            btnTreasure.GetComponent<Image>().sprite = Resources.Load("UI/btn_color_green", typeof(Sprite)) as Sprite;

        countTreasure.text = GameManager.s_Instance.player.GetItem(Item.TREASURE).ToString();

        if (1000 > GameManager.s_Instance.player.Gold)
        {

            btnCrown.GetComponent<Image>().sprite = Resources.Load("UI/btn_color_red", typeof(Sprite)) as Sprite;
        }
        else
            btnCrown.GetComponent<Image>().sprite = Resources.Load("UI/btn_color_green", typeof(Sprite)) as Sprite;

        countCrown.text = GameManager.s_Instance.player.GetItem(Item.CROWN).ToString();
    }

    public void BuyCrown()
    {
        if (1000 > GameManager.s_Instance.player.Gold)
            return;

        GameManager.s_Instance.player.AddItem(Item.CROWN, 1);
        GameManager.s_Instance.player.AddGold(-1 * 1000);
        GetComponentInParent<ListController>().UpdateUI();
    }

    public void BuyTreasure()
    {
        if (500 > GameManager.s_Instance.player.Gold)
            return;

        GameManager.s_Instance.player.AddItem(Item.TREASURE, 1);
        GameManager.s_Instance.player.AddGold(-1 * 500);
        GetComponentInParent<ListController>().UpdateUI();
    }

}
