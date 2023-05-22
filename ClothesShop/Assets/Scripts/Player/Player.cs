using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    [Header("PhysicsSetting")]
    [SerializeField] private float speed;

    [Header("MenuRoughDependencies")]

    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject hintPanel;

    [Header("PlayerView")]

    [SerializeField] private SpriteRenderer BodyPreviewSprite;
    [SerializeField] private SpriteRenderer LeftHandPreviewSprite;
    [SerializeField] private SpriteRenderer RightHandPreviewSprite;

    [SerializeField] private SpriteRenderer LeftLegPreviewSprite;
    [SerializeField] private SpriteRenderer RightLegPreviewSprite;

    [SerializeField] private SpriteRenderer LeftFootPreviewSprite;
    [SerializeField] private SpriteRenderer RightFootPreviewSprite;

    [Header("DefaultSprites")]

    [SerializeField] private Sprite BodySprite;
    [SerializeField] private Sprite LeftHandSprite;
    [SerializeField] private Sprite RightHandSprite;

    [SerializeField] private Sprite LeftLegSprite;
    [SerializeField] private Sprite RightLegSprite;

    [SerializeField] private Sprite LeftFootSprite;
    [SerializeField] private Sprite RightFootSprite;

    private float _xVelocity;
    private float _yVelocity;
    private Collider2D _interactable;
    private Rigidbody2D rigidbody;
    private Animator _animator;
    public PlayerState PlayerState { get; } = new PlayerState();

    public void Equip(Offer offer)
    {
        var checkedEquipped = PlayerState.EquippedOffers.FirstOrDefault(piece => piece.ClothesType == offer.ClothesType);

        if (checkedEquipped != null)
        {
            PlayerState.EquippedOffers.Remove(checkedEquipped);
        }

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

        PlayerState.EquippedOffers.Add(offer);
    }

    public void OnClothesSelled(Offer offer)
    {
        PlayerState.RemoveClothes(offer);
        SetDefaultSprites(offer);
    }

    public void OnClothesBought(Offer offer)
    {
        PlayerState.AddClothes(offer);
    }

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(_xVelocity, _yVelocity) * speed;

        if (rigidbody.velocity != Vector2.zero)
        {
            _animator.speed = 2;
            _animator.Play("GoDown");
        }
        else
        {
            _animator.speed = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        _interactable = collider;
        hintPanel.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        _interactable = null;
        hintPanel.SetActive(false);
    }

    public void TryInteract()
    {
        if (_interactable?.tag == "ShopTrigger")
        {
            menu.SetActive(true);
            hintPanel.SetActive(false);
        }
    }

    public void MoveHorizontal(float direction) => _xVelocity = direction;

    public void MoveVertical(float direction) => _yVelocity = direction;

    private void SetDefaultSprites(Offer offer)
    {
        if (offer.ClothesType == ClothesType.Boots)
        {
            LeftFootPreviewSprite.sprite = LeftFootSprite;
            RightFootPreviewSprite.sprite = RightFootSprite;
        }
        else if (offer.ClothesType == ClothesType.Pants)
        {
            LeftLegPreviewSprite.sprite = LeftLegSprite;
            RightLegPreviewSprite.sprite = RightLegSprite;
        }
        else if (offer.ClothesType == ClothesType.Shirt)
        {
            LeftHandPreviewSprite.sprite = LeftHandSprite;
            RightHandPreviewSprite.sprite = RightHandSprite;
            BodyPreviewSprite.sprite = BodySprite;
        }
    }

}
