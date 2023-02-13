using UnityEngine;
using UnityEngine.UI; // needed when accessing text elements
using System;

#if UNITY_WEBGL

// This script has been moved from the VerifyExmaple.cs example in the EVM scripts folder to show you how to make additional changes
public class VerifyMenu : MonoBehaviour
{
    public Text responseText;
    public string message = "hello";

    async public void OnHashMessage()
    {
        try
        {
            string hashedMessage = await Web3GL.Sha3(message);
            string signHashed = await Web3GL.Sign(hashedMessage);
            Debug.Log("Signed Hashed: " + signHashed);
            ParseSignatureFunction(signHashed);
            string verify = await Web3GL.EcRecover(hashedMessage, signHashed);
            responseText.text = verify;
            Debug.Log("Verify Address: " + responseText.text);
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }
    public void ParseSignatureFunction(string sig)
    {
        string signature = sig;
        string r = signature.Substring(0, 66);
        Debug.Log("R:" + r);
        string s = "0x" + signature.Substring(66, 64);
        Debug.Log("S: " + s);
        int v = int.Parse(signature.Substring(130, 2), System.Globalization.NumberStyles.HexNumber);
        Debug.Log("V: " + v);
    }
}

#endif