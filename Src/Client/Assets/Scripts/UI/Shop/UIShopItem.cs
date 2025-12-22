using Common.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIShopItem : MonoBehaviour, ISelectHandler
{
    public Text title;
    public Text count;
    public Text price;
    public Text limitClass;
    public Image icon;

    public Image background;
    public Sprite normalBG;
    public Sprite selectedBG;

    private bool selected;
    public bool Selected
    {
        get { return selected; }
        set
        {
            selected = value;
            this.background.overrideSprite = selected ? selectedBG : normalBG;
        }
    }

    // Use this for initialization
    public int ShopItemID { get; set; }

    private UIShop shop;
    private ItemDefine item;
    private ShopItemDefine ShopItem { get; set; }

    public void SetShopItem(int id, ShopItemDefine shopItem, UIShop owner)
    {
        this.shop = owner;
        this.ShopItemID = id;
        this.ShopItem = shopItem;
        this.item = DataManager.Instance.Items[this.ShopItem.ItemID];

        this.title.text = this.item.Name;
        this.count.text = "x" + ShopItem.Count.ToString();
        this.price.text = ShopItem.Price.ToString();
        this.limitClass.text = this.item.LimitClass.ToString();
        this.icon.overrideSprite = Resloader.Load<Sprite>(item.Icon);
    }


    public void OnSelect(BaseEventData eventData)
    {
        this.Selected = true;
        this.shop.SelectShopItem(this);
    }
}