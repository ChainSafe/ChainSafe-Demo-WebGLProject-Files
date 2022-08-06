using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.UI; // needed when accessing text elements
using UnityEngine;

public class ContractMenu : MonoBehaviour
{
    // This script has been moved from the CustomCallExample.cs example in the EVM scripts folder to show you how to make additional changes
    string chain = "ethereum";
    // set network mainnet, testnet
    string network = "goerli";
    // abi in json format
    string abi = "[ { \"inputs\": [ { \"internalType\": \"uint8\", \"name\": \"_myArg\", \"type\": \"uint8\" } ], \"name\": \"addTotal\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"myTotal\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" } ]";
    // address of contract
    string contract = "0x741C3F3146304Aaf5200317cbEc0265aB728FE07";
    public GameObject SuccessPopup;
    public Text responseText;

    /*
        //Solidity Contract
        // SPDX-License-Identifier: MIT
        pragma solidity ^0.8.0;

        contract AddTotal {
            uint256 public myTotal = 0;

            function addTotal(uint8 _myArg) public {
                myTotal = myTotal + _myArg;
            }
        }
        */

    async public void CheckVariable()
    {
        string method = "myTotal";
        // array of arguments for contract
        string args = "[]";
        // connects to user's browser wallet to call a transaction
        string response = await EVM.Call(chain, network, contract, abi, method, args);
        // display response in game
        print("Contract Variable Total: " + response);
        responseText.text = "Contract Variable Total: " + response;
        SuccessPopup.SetActive(true);
    }

    async public void AddOneToVariable()
    {
        string method = "addTotal";
        string amount = "1";
        // array of arguments for contract
        string[] obj = {amount};
        string args = JsonConvert.SerializeObject(obj);
        // connects to user's browser wallet to call a transaction
        string response = await Web3GL.SendContract(method, abi, contract, args, "0", "", "");
        // display response in game
        print("Please check the contract variable again in a few seconds once the chain has processed the request!");
        responseText.text = "Contract Variable Total: " + response;
        SuccessPopup.SetActive(true);
    }
}
