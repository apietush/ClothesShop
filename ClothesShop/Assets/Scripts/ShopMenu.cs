using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] private GameObject SwitherButtonPrefab;
    [SerializeField] private GameObject SwitherRoot;
    [SerializeField] private OffersReference OffersList;

    [SerializeField] private Image CurrentItemSprite;
    [SerializeField] private TextMeshProUGUI CurrentItemPrice;

    [SerializeField] private TextMeshProUGUI BuyButtonText;
    [SerializeField] private TextMeshProUGUI GoldAmount;
    [SerializeField] private Button BuyButton;
    [SerializeField] private Button SellButton;

    [SerializeField] private Player player;

    private LinkedList<Offer> _currentList;
    private int _index;


    // Start is called before the first frame update
    public void Start()
    {
        var clothesTypes = Enum.GetNames(typeof(ClothesType));

        foreach (var type in clothesTypes)
        {
            var switcherButton = Instantiate(SwitherButtonPrefab, SwitherRoot.transform);
            var switcher = switcherButton.GetComponent<ShopSwitcher>();
            switcher.Initialize(type);
            switcher.Switched += OnShopSwitched;
        }

        _currentList = new LinkedList<Offer>(OffersList.Offers.Where(x => x.ClothesType.ToString() == clothesTypes.First()));
        ChangeCurrentItem();
    }

    private void OnShopSwitched(string currentType)
    {
        _currentList = new LinkedList<Offer>(OffersList.Offers.Where(x => x.ClothesType.ToString() == currentType));
        _index = 0;

        ChangeCurrentItem();
    }

    public void OnNextPressed()
    {
        _index = _index >= _currentList.Count - 1 ? 0 : _index + 1;
        ChangeCurrentItem();
    }

    public void OnPreviousPressed()
    {
        _index = _index > 0 ? _index - 1 : _currentList.Count - 1;
        ChangeCurrentItem();
    }

    public void OnBuyPressed()
    {
        var offer = _currentList.ElementAt(_index);
        if (player.PlayerState.OwnedOffers.Contains(offer) )
        {
            return;
        }
        player.PlayerState.OwnedOffers.Add(offer);
        player.PlayerState.GoldAmount -=offer.Price;

        BuyButtonText.text = player.PlayerState.OwnedOffers.Contains(offer) ? "Equip" : "Buy";
    }

    public void OnSellPressed()
    {
        var offer = _currentList.ElementAt(_index);
        if (player.PlayerState.OwnedOffers.Contains(offer) == false)
        {
            return;
        }

        player.PlayerState.OwnedOffers.Remove(offer);
        player.PlayerState.GoldAmount += offer.Price - 5;
    }

    public void OnCloseClicked()
    {
        gameObject.SetActive(false);
    }

    private void ChangeCurrentItem()
    {
        var offer = _currentList.ElementAt(_index);

        CurrentItemSprite.sprite = offer.Icon;
        CurrentItemPrice.text = offer.Price.ToString();

        BuyButtonText.text = player.PlayerState.OwnedOffers.Contains(offer) ? "Equip" : "Buy";
    }


    // Update is called once per frame
    public void Update()
    {
        var offer = _currentList.ElementAt(_index);

        GoldAmount.text = $"Gold: {player.PlayerState.GoldAmount.ToString()}";
        BuyButton.interactable = offer.Price <= player.PlayerState.GoldAmount;
        SellButton.interactable = player.PlayerState.OwnedOffers.Contains(offer);
    }
}