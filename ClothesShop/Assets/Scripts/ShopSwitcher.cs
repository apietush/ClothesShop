using System;
using TMPro;
using UnityEngine;

public class ShopSwitcher : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI shopTitle;
    private string _type;

    public event Action<string> Switched;

    public void Initialize(string type)
    {
        _type = type;
        shopTitle.text = type.ToString();
    }

    public void OnButtonClicked()
    {
        Switched?.Invoke(_type);
    }
}