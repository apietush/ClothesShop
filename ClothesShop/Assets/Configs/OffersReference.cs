using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/OffersReferenceScriptableObject", order = 1)]
public class OffersReference : ScriptableObject
{
    public Offer[] Offers;
}