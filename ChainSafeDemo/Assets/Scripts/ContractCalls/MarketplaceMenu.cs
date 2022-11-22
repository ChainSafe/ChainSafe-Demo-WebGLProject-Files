using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketplaceMenu : MonoBehaviour
{
    public void OpenMarketplace()
    {
        Application.OpenURL("https://marketplace.chainsafe.io/");
    }
}
