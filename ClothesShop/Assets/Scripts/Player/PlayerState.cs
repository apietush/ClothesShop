using System.Collections.Generic;

public class PlayerState
{
    public int GoldAmount = 400;
    public List<Offer> OwnedOffers = new List<Offer>(6);
    public List<Offer> EquippedOffers { get; set; } = new List<Offer>(3);

    public void AddClothes(Offer offer)
    {
        OwnedOffers.Add(offer);
    }

    public void RemoveClothes(Offer offer)
    {
        OwnedOffers.Add(offer);

        if (EquippedOffers.Contains(offer))
        {
            EquippedOffers.Remove(offer);
        }
    }
}
