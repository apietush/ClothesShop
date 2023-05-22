using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    // used for placeholder formula for economy
    private const int SellTax = 5;

    [Header("PrefabUtilities")]
    [SerializeField] private GameObject SwitherButtonPrefab;
    [SerializeField] private GameObject SwitherRoot;

    [Header("Configs")]
    [SerializeField] private OffersReference OffersList;

    [Header("Dependencies")]
    [SerializeField] private Player player;

    [Header("ChildComponents")]
    [SerializeField] private TextMeshProUGUI CurrentItemPrice;
    [SerializeField] private TextMeshProUGUI CurrentItemId;

    [SerializeField] private TextMeshProUGUI BuyButtonText;
    [SerializeField] private TextMeshProUGUI GoldAmount;
    [SerializeField] private Button BuyButton;
    [SerializeField] private Button SellButton;

    [Header("Preview sprites")]
    [SerializeField] private Image BodyPreviewSprite;
    [SerializeField] private Image HeadPreviewSprite;
    [SerializeField] private Image LeftHandPreviewSprite;
    [SerializeField] private Image RightHandPreviewSprite;

    [SerializeField] private Image LeftLegPreviewSprite;
    [SerializeField] private Image RightLegPreviewSprite;

    [SerializeField] private Image LeftFootPreviewSprite;
    [SerializeField] private Image RightFootPreviewSprite;


    private LinkedList<Offer> _currentList;
    private Offer _currentOffer;
    private List<ShopSwitcher> _switcherButtons = new List<ShopSwitcher>(3);
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

            _switcherButtons.Add(switcher);

            _currentList = new LinkedList<Offer>(OffersList.Offers.Where(x => x.ClothesType.ToString() == clothesTypes.First()));
            ChangeCurrentItem();
        }
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
        if (player.PlayerState.OwnedOffers.Contains(_currentOffer))
        {
            player.Equip(_currentOffer);
            return;
        }

        player.PlayerState.GoldAmount -= _currentOffer.Price;
        player.OnClothesBought(_currentOffer);

    }

    public void OnSellPressed()
    {
        player.PlayerState.GoldAmount += _currentOffer.Price - SellTax;
        player.OnClothesSelled(_currentOffer);
    }

    public void OnCloseClicked()
    {
        gameObject.SetActive(false);
    }

    private void ChangeCurrentItem()
    {
        var offer = _currentList.ElementAt(_index);
        _currentOffer = offer;

        CurrentItemId.text = offer.Id;
        CurrentItemPrice.text = offer.Price.ToString();


        if (offer.ClothesType == ClothesType.Boots)
        {
            LeftFootPreviewSprite.sprite = offer.LeftFoot;
            RightFootPreviewSprite.sprite = offer.RightFoot;
        }
        else if (offer.ClothesType == ClothesType.Pants)
        {
            LeftLegPreviewSprite.sprite = offer.LeftLeg;
            RightLegPreviewSprite.sprite = offer.RightLeg;
        }
        else if (offer.ClothesType == ClothesType.Shirt)
        {
            LeftHandPreviewSprite.sprite = offer.LeftHand;
            RightHandPreviewSprite.sprite = offer.RightHand;
            BodyPreviewSprite.sprite = offer.Body;
        }
    }


    // Update is called once per frame
    public void Update()
    {
        BuyButtonText.text = player.PlayerState.OwnedOffers.Contains(_currentOffer) ? "Equip" : "Buy";

        GoldAmount.text = player.PlayerState.GoldAmount.ToString();


        if (_currentOffer.Price > player.PlayerState.GoldAmount || player.PlayerState.EquippedOffers.Contains(_currentOffer))
        {
            BuyButton.interactable = false;
        }
        else
        {
            BuyButton.interactable = true;
        }

        SellButton.interactable = player.PlayerState.OwnedOffers.Contains(_currentOffer);
    }

    protected void OnDestroy()
    {
        foreach (var switcher in _switcherButtons)
        {
            switcher.Switched -= OnShopSwitched;
        }
    }
}