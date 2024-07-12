using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    public List<KeyCardAccess> KeyCards;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void AddKeycard(KeyCardAccess card)
    {
        if (!KeyCards.Contains(card))
        {
            KeyCards.Add(card);
        }
    }
}
