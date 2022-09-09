using System;
using Models;
using UnityEngine;
using System.Collections;
using Newtonsoft.Json; // used for json serialization with args
using UnityEngine.Networking; // needed for web requests

#if UNITY_WEBGL
public class VoucherMenu : MonoBehaviour
{
    // This script has been moved from the GetVoucherWebGL1155.cs example in the Minter scripts folder to show you how to make additional changes
    public GameObject SuccessPopup;
    public GameObject IPFSImageHolder;
    public GameObject IPFSImage;
    string gateway = "https://game-api-stg.chainsafe.io/ipfs/";
    string metadataUri;
    string downloadURI;

    public async void Get1155VoucherButton()
    {
        var voucherResponse1155 = await EVM.Get1155Voucher();
        Debug.Log("Voucher Response 1155 Signature: " + voucherResponse1155.signature);
        Debug.Log("Voucher Response 1155 Min Price: " + voucherResponse1155.minPrice);
        Debug.Log("Voucher Response 1155 Token ID: " + voucherResponse1155.tokenId);
        Debug.Log("Voucher Response 1155 Nonce: " + voucherResponse1155.nonce);
        Debug.Log("Voucher Response 1155 Signer: " + voucherResponse1155.signer);
        // saves the voucher to player prefs, you can change this if you like to fit your system 
        PlayerPrefs.SetString("WebGLVoucher1155Sig", voucherResponse1155.signature);
        PlayerPrefs.SetString("WebGLVoucher1155TokenID", voucherResponse1155.tokenId);
        PlayerPrefs.SetString("WebGLVoucher1155Signer", voucherResponse1155.signer);
        PlayerPrefs.SetString("WebGLVoucher1155Nonce", voucherResponse1155.nonce.ToString());
        PlayerPrefs.SetString("WebGLVoucher1155MinPrice", voucherResponse1155.minPrice.ToString());
        string newURL = voucherResponse1155.tokenId.Replace("0x","f");
        // initialize gateway and uri
        metadataUri = gateway + newURL;
        // start json web request
        StartCoroutine(GetURIData());
    }

    IEnumerator GetURIData() {
        // json web request
        UnityWebRequest www = UnityWebRequest.Get(metadataUri);
        yield return www.SendWebRequest();
        // error or display result
        if (www.result != UnityWebRequest.Result.Success) {
        Debug.Log(www.error);
        }
        else 
        {
        // Show results as text
        downloadURI = www.downloadHandler.text;
        // gets ipfs image from voucher uri
        GetIPFSImage(downloadURI);
        }
    }

    public async void GetIPFSImage(string downloadURI)
    {
        // map key values to model and deserialize
        URIData deserializedProduct = JsonConvert.DeserializeObject<URIData>(downloadURI);
        Debug.Log("Updating Image...Please Wait!");
        // get ipfs image texture via uri substring (substring removes x amount of chars from the start of the string, we remove 7 here to get rid of the ipfs://)
        UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(gateway + deserializedProduct.image.Substring(7));
        await textureRequest.SendWebRequest();
        IPFSImage.GetComponent<Renderer>().material.mainTexture = (DownloadHandlerTexture.GetContent(textureRequest));
        IPFSImageHolder.SetActive(true);
        Debug.Log("Image Updated!");
        SuccessPopup.SetActive(true);
    }
}
#endif