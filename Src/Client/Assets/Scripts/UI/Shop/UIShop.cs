using Common.Data;
using Managers;
using Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : UIWindow
{
    public Text title;
    public Text money;

    public GameObject ShopItem;
    public Transform[] itemRoot;
    private ShopDefine shop;

    // Use this for initialization
    private void Start()
    {
        StartCoroutine(InitItems());
    }

    IEnumerator InitItems()
    {
        foreach (var kv in DataManager.Instance.ShopItems[shop.Id])
        {
            if (kv.Value.Status > 0)
            {
                GameObject go = Instantiate(ShopItem, itemRoot[0]);
                UIShopItem ui = go.GetComponent<UIShopItem>();
                ui.SetShopItem(kv.Key, kv.Value, this);
            }
        }
        yield return null;
    }


    public void SetShop(ShopDefine shop)
    {
        this.shop = shop;
        this.title.text = shop.Name;
        SetMoney();
    }

    public void SetMoney() 
    {
        this.money.text = User.Instance.CurrentCharacter.Gold.ToString();
    }


    private UIShopItem selectedItem;

    public void SelectShopItem(UIShopItem item)
    {
        if (selectedItem != null)
        {
            selectedItem.Selected = false;
        }
        selectedItem = item;
    }

    public void OnClickBuy() 
    {
        if(this.selectedItem == null) 
        {
            MessageBox.Show("请选择购买的道具","购买提示");
            return;
        }
        if (!ShopManager.Instance.BuyItem(this.shop.Id, this.selectedItem.ShopItemID)) 
        {

        }
        
    }
}