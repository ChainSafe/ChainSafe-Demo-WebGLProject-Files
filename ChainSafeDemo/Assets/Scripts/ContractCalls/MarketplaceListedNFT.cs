using System.Collections;
using System.Collections.Generic;
using Models;
using Newtonsoft.Json;
using UnityEngine.Networking;
using UnityEngine.UI;
using Web3Unity.Scripts.Library.ETHEREUEM.Connect;
using System;
using UnityEngine;


#if UNITY_WEBGL
public class MarketplaceListedNFT : MonoBehaviour
{
    // This script has been moved from the GetListedNFTWebGL.cs example in the Minter scripts folder to show you how to make additional changes.
    private string chain = "ethereum";
    public Renderer textureObject;
    private string network = "moonbeam";
    public Text price;
    public Text seller;
    public Text description;
    public Text listPercentage;
    public Text contractAddr;
    public Text tokenId;
    public Text itemId;
    private string _itemPrice = "";
    private string _tokenType = "";
    private string _itemID = "";
    private int NFTNumber = 0;


    public void Awake()
    {
        
        price.text = "";
        seller.text = "";
        description.text = "";
        listPercentage.text = "";
        tokenId.text = "";
        itemId.text = "";
        contractAddr.text = "";
    }

    // Start is called before the first frame update
    async void Start()
    {
        List<GetNftListModel.Response> response = await EVM.GetNftMarket(chain, network);
        price.text = response[NFTNumber].price;
        seller.text = response[NFTNumber].seller;
        if (response[NFTNumber].uri.StartsWith("ipfs://"))
        {
            response[NFTNumber].uri = response[NFTNumber].uri.Replace("ipfs://", "https://ipfs.io/ipfs/");
            Debug.Log("Response URI" + response[NFTNumber].uri);
        }

        UnityWebRequest webRequest = UnityWebRequest.Get(response[NFTNumber].uri);
        await webRequest.SendWebRequest();
        RootGetNFT data =
            JsonConvert.DeserializeObject<RootGetNFT>(
                System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data)); 
        description.text = data.description;
        // parse json to get image uri
        string imageUri = data.image;
        if (imageUri.StartsWith("ipfs://"))
        {
            imageUri = imageUri.Replace("ipfs://", "https://ipfs.io/ipfs/");
            StartCoroutine(DownloadImage(imageUri));
        }

        if (data.properties != null)
        {
            foreach (var prop in data.properties.additionalFiles)
            {
                if (prop.StartsWith("ipfs://"))
                {
                    var additionalURi = prop.Replace("ipfs://", "https://ipfs.io/ipfs/");
                }
            }
        }
        listPercentage.text = response[NFTNumber].listedPercentage;
        contractAddr.text = response[NFTNumber].nftContract;
        itemId.text = response[NFTNumber].itemId;
        _itemID = response[NFTNumber].itemId;
        _itemPrice = response[NFTNumber].price;
        _tokenType = response[NFTNumber].tokenType;
        tokenId.text = response[NFTNumber].tokenId;
    }

    async public void NextNFT()
    {
        NFTNumber++;
        List<GetNftListModel.Response> response = await EVM.GetNftMarket(chain, network);
        price.text = response[NFTNumber].price;
        seller.text = response[NFTNumber].seller;
        if (response[NFTNumber].uri.StartsWith("ipfs://"))
        {
            response[NFTNumber].uri = response[NFTNumber].uri.Replace("ipfs://", "https://ipfs.io/ipfs/");
            Debug.Log("Response URI" + response[NFTNumber].uri);
        }

        UnityWebRequest webRequest = UnityWebRequest.Get(response[NFTNumber].uri);
        await webRequest.SendWebRequest();
        RootGetNFT data =
            JsonConvert.DeserializeObject<RootGetNFT>(
                System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data)); 
        description.text = data.description;
        // parse json to get image uri
        string imageUri = data.image;
        if (imageUri.StartsWith("ipfs://"))
        {
            imageUri = imageUri.Replace("ipfs://", "https://ipfs.io/ipfs/");
            StartCoroutine(DownloadImage(imageUri));
        }

        if (data.properties != null)
        {
            foreach (var prop in data.properties.additionalFiles)
            {
                if (prop.StartsWith("ipfs://"))
                {
                    var additionalURi = prop.Replace("ipfs://", "https://ipfs.io/ipfs/");
                }
            }
        }
        listPercentage.text = response[NFTNumber].listedPercentage;
        contractAddr.text = response[NFTNumber].nftContract;
        itemId.text = response[NFTNumber].itemId;
        _itemID = response[NFTNumber].itemId;
        _itemPrice = response[NFTNumber].price;
        _tokenType = response[NFTNumber].tokenType;
        tokenId.text = response[NFTNumber].tokenId;
    }

    async public void PreviousNFT()
    {
        if (NFTNumber != 0){
        NFTNumber--;}
        List<GetNftListModel.Response> response = await EVM.GetNftMarket(chain, network);
        price.text = response[NFTNumber].price;
        seller.text = response[NFTNumber].seller;
        if (response[NFTNumber].uri.StartsWith("ipfs://"))
        {
            response[NFTNumber].uri = response[NFTNumber].uri.Replace("ipfs://", "https://ipfs.io/ipfs/");
            Debug.Log("Response URI" + response[NFTNumber].uri);
        }

        UnityWebRequest webRequest = UnityWebRequest.Get(response[NFTNumber].uri);
        await webRequest.SendWebRequest();
        RootGetNFT data =
            JsonConvert.DeserializeObject<RootGetNFT>(
                System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data)); 
        description.text = data.description;
        // parse json to get image uri
        string imageUri = data.image;
        if (imageUri.StartsWith("ipfs://"))
        {
            imageUri = imageUri.Replace("ipfs://", "https://ipfs.io/ipfs/");
            StartCoroutine(DownloadImage(imageUri));
        }

        if (data.properties != null)
        {
            foreach (var prop in data.properties.additionalFiles)
            {
                if (prop.StartsWith("ipfs://"))
                {
                    var additionalURi = prop.Replace("ipfs://", "https://ipfs.io/ipfs/");
                }
            }
        }
        listPercentage.text = response[NFTNumber].listedPercentage;
        contractAddr.text = response[NFTNumber].nftContract;
        itemId.text = response[NFTNumber].itemId;
        _itemID = response[NFTNumber].itemId;
        _itemPrice = response[NFTNumber].price;
        _tokenType = response[NFTNumber].tokenType;
        tokenId.text = response[NFTNumber].tokenId;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator DownloadImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ProtocolError)
            Debug.Log(request.error);
        else
        {
            Texture2D webTexture = ((DownloadHandlerTexture) request.downloadHandler).texture as Texture2D;
            Sprite webSprite = SpriteFromTexture2D(webTexture);
            textureObject.GetComponent<Image>().sprite = webSprite;
        }
    }

    Sprite SpriteFromTexture2D(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f),
            100.0f);
    }

    public async void PurchaseItem()
    {
        BuyNFT.Response response = await EVM.CreatePurchaseNftTransaction(chain, network,
        PlayerPrefs.GetString("Account"), _itemID, _itemPrice, _tokenType);
        Debug.Log("Account: " + response.tx.account);
        Debug.Log("To : " + response.tx.to);
        Debug.Log("Value : " + response.tx.value);
        Debug.Log("Data : " + response.tx.data);
        Debug.Log("Gas Price : " + response.tx.gasPrice);
        Debug.Log("Gas Limit : " + response.tx.gasLimit);

        try
        { 
            string responseNft = await Web3GL.SendTransaction( response.tx.to, response.tx.value, response.tx.gasLimit, response.tx.gasLimit);
            if (responseNft == null)
            {
                Debug.Log("Empty Response Object:");
            }
            print(responseNft);
            Debug.Log(responseNft);
        }
        catch (Exception e)
        {
            Debug.LogError(e, this);
        }
    }
}
#endif