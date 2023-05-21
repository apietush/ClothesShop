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

    private void ChangeCurrentItem()
    {
        var offer = _currentList.ElementAt(_index);

        CurrentItemSprite.sprite = offer.Icon;
        CurrentItemPrice.text = offer.Price.ToString();
    }


    // Update is called once per frame
    public void Update()
    {
    }
}