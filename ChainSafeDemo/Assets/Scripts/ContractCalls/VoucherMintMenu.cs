using System;
using System.Collections;
using System.Collections.Generic;
using Models;
using UnityEditor;
using UnityEngine.UI; // needed when accessing text elements
using UnityEngine;

#if UNITY_WEBGL
public class VoucherMintMenu : MonoBehaviour
{
    // This script has been moved from the MintWebGL721.cs example in the Minter scripts folder to show you how to make additional changes
    public GameObject SuccessPopup;
    public Text responseText;
    // used for speed bonus on successful mint
    public GameObject Player;
    public GameObject SpeedClouds;
    // abi for reddem contract in json format
    string abi = "[ { \"anonymous\": false, \"inputs\": [ { \"indexed\": false, \"internalType\": \"address\", \"name\": \"previousAdmin\", \"type\": \"address\" }, { \"indexed\": false, \"internalType\": \"address\", \"name\": \"newAdmin\", \"type\": \"address\" } ], \"name\": \"AdminChanged\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"operator\", \"type\": \"address\" }, { \"indexed\": false, \"internalType\": \"bool\", \"name\": \"approved\", \"type\": \"bool\" } ], \"name\": \"ApprovalForAll\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"beacon\", \"type\": \"address\" } ], \"name\": \"BeaconUpgraded\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"setBy\", \"type\": \"address\" }, { \"indexed\": false, \"internalType\": \"uint256\", \"name\": \"mintingFee\", \"type\": \"uint256\" } ], \"name\": \"MintingFeeSet\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": false, \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" } ], \"name\": \"Paused\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"indexed\": true, \"internalType\": \"bytes32\", \"name\": \"previousAdminRole\", \"type\": \"bytes32\" }, { \"indexed\": true, \"internalType\": \"bytes32\", \"name\": \"newAdminRole\", \"type\": \"bytes32\" } ], \"name\": \"RoleAdminChanged\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"sender\", \"type\": \"address\" } ], \"name\": \"RoleGranted\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"sender\", \"type\": \"address\" } ], \"name\": \"RoleRevoked\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"operator\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"from\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"to\", \"type\": \"address\" }, { \"indexed\": false, \"internalType\": \"uint256[]\", \"name\": \"ids\", \"type\": \"uint256[]\" }, { \"indexed\": false, \"internalType\": \"uint256[]\", \"name\": \"values\", \"type\": \"uint256[]\" } ], \"name\": \"TransferBatch\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"operator\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"from\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"to\", \"type\": \"address\" }, { \"indexed\": false, \"internalType\": \"uint256\", \"name\": \"id\", \"type\": \"uint256\" }, { \"indexed\": false, \"internalType\": \"uint256\", \"name\": \"value\", \"type\": \"uint256\" } ], \"name\": \"TransferSingle\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": false, \"internalType\": \"string\", \"name\": \"value\", \"type\": \"string\" }, { \"indexed\": true, \"internalType\": \"uint256\", \"name\": \"id\", \"type\": \"uint256\" } ], \"name\": \"URI\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": false, \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" } ], \"name\": \"Unpaused\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"implementation\", \"type\": \"address\" } ], \"name\": \"Upgraded\", \"type\": \"event\" }, { \"inputs\": [], \"name\": \"DEFAULT_ADMIN_ROLE\", \"outputs\": [ { \"internalType\": \"bytes32\", \"name\": \"\", \"type\": \"bytes32\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"PAUSER_ROLE\", \"outputs\": [ { \"internalType\": \"bytes32\", \"name\": \"\", \"type\": \"bytes32\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"UPGRADER_ROLE\", \"outputs\": [ { \"internalType\": \"bytes32\", \"name\": \"\", \"type\": \"bytes32\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"URI_SETTER_ROLE\", \"outputs\": [ { \"internalType\": \"bytes32\", \"name\": \"\", \"type\": \"bytes32\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"id\", \"type\": \"uint256\" } ], \"name\": \"balanceOf\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address[]\", \"name\": \"accounts\", \"type\": \"address[]\" }, { \"internalType\": \"uint256[]\", \"name\": \"ids\", \"type\": \"uint256[]\" } ], \"name\": \"balanceOfBatch\", \"outputs\": [ { \"internalType\": \"uint256[]\", \"name\": \"\", \"type\": \"uint256[]\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"id\", \"type\": \"uint256\" }, { \"internalType\": \"uint256\", \"name\": \"value\", \"type\": \"uint256\" } ], \"name\": \"burn\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"internalType\": \"uint256[]\", \"name\": \"ids\", \"type\": \"uint256[]\" }, { \"internalType\": \"uint256[]\", \"name\": \"values\", \"type\": \"uint256[]\" } ], \"name\": \"burnBatch\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"uint256\", \"name\": \"id\", \"type\": \"uint256\" } ], \"name\": \"exists\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"getChainID\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"getMintingFee\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" } ], \"name\": \"getRoleAdmin\", \"outputs\": [ { \"internalType\": \"bytes32\", \"name\": \"\", \"type\": \"bytes32\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"internalType\": \"uint256\", \"name\": \"index\", \"type\": \"uint256\" } ], \"name\": \"getRoleMember\", \"outputs\": [ { \"internalType\": \"address\", \"name\": \"\", \"type\": \"address\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" } ], \"name\": \"getRoleMemberCount\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" } ], \"name\": \"grantRole\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" } ], \"name\": \"hasRole\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"string\", \"name\": \"baseTokenURI\", \"type\": \"string\" }, { \"internalType\": \"address\", \"name\": \"owner\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"mintingFee\", \"type\": \"uint256\" } ], \"name\": \"initialize\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"internalType\": \"address\", \"name\": \"operator\", \"type\": \"address\" } ], \"name\": \"isApprovedForAll\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"to\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"id\", \"type\": \"uint256\" }, { \"internalType\": \"uint256\", \"name\": \"amount\", \"type\": \"uint256\" }, { \"internalType\": \"bytes\", \"name\": \"data\", \"type\": \"bytes\" } ], \"name\": \"mint\", \"outputs\": [], \"stateMutability\": \"payable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"to\", \"type\": \"address\" }, { \"internalType\": \"uint256[]\", \"name\": \"ids\", \"type\": \"uint256[]\" }, { \"internalType\": \"uint256[]\", \"name\": \"amounts\", \"type\": \"uint256[]\" }, { \"internalType\": \"bytes\", \"name\": \"data\", \"type\": \"bytes\" } ], \"name\": \"mintBatch\", \"outputs\": [], \"stateMutability\": \"payable\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"pause\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"paused\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"proxiableUUID\", \"outputs\": [ { \"internalType\": \"bytes32\", \"name\": \"\", \"type\": \"bytes32\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"components\": [ { \"internalType\": \"uint256\", \"name\": \"tokenId\", \"type\": \"uint256\" }, { \"internalType\": \"uint256\", \"name\": \"minPrice\", \"type\": \"uint256\" }, { \"internalType\": \"address\", \"name\": \"signer\", \"type\": \"address\" }, { \"internalType\": \"address\", \"name\": \"receiver\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"amount\", \"type\": \"uint256\" }, { \"internalType\": \"uint256\", \"name\": \"nonce\", \"type\": \"uint256\" }, { \"internalType\": \"bytes\", \"name\": \"signature\", \"type\": \"bytes\" } ], \"internalType\": \"struct GeneralERC1155.NFTVoucher\", \"name\": \"voucher\", \"type\": \"tuple\" } ], \"name\": \"redeem\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"payable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" } ], \"name\": \"renounceRole\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" } ], \"name\": \"revokeRole\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"from\", \"type\": \"address\" }, { \"internalType\": \"address\", \"name\": \"to\", \"type\": \"address\" }, { \"internalType\": \"uint256[]\", \"name\": \"ids\", \"type\": \"uint256[]\" }, { \"internalType\": \"uint256[]\", \"name\": \"amounts\", \"type\": \"uint256[]\" }, { \"internalType\": \"bytes\", \"name\": \"data\", \"type\": \"bytes\" } ], \"name\": \"safeBatchTransferFrom\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"from\", \"type\": \"address\" }, { \"internalType\": \"address\", \"name\": \"to\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"id\", \"type\": \"uint256\" }, { \"internalType\": \"uint256\", \"name\": \"amount\", \"type\": \"uint256\" }, { \"internalType\": \"bytes\", \"name\": \"data\", \"type\": \"bytes\" } ], \"name\": \"safeTransferFrom\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"operator\", \"type\": \"address\" }, { \"internalType\": \"bool\", \"name\": \"approved\", \"type\": \"bool\" } ], \"name\": \"setApprovalForAll\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"newTarget\", \"type\": \"address\" } ], \"name\": \"setFeeTarget\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"uint256\", \"name\": \"newMintingFee\", \"type\": \"uint256\" } ], \"name\": \"setMintingFee\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"string\", \"name\": \"newuri\", \"type\": \"string\" } ], \"name\": \"setURI\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"bytes4\", \"name\": \"interfaceId\", \"type\": \"bytes4\" } ], \"name\": \"supportsInterface\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"uint256\", \"name\": \"id\", \"type\": \"uint256\" } ], \"name\": \"totalSupply\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"unpause\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"newImplementation\", \"type\": \"address\" } ], \"name\": \"upgradeTo\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"newImplementation\", \"type\": \"address\" }, { \"internalType\": \"bytes\", \"name\": \"data\", \"type\": \"bytes\" } ], \"name\": \"upgradeToAndCall\", \"outputs\": [], \"stateMutability\": \"payable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"name\": \"uri\", \"outputs\": [ { \"internalType\": \"string\", \"name\": \"\", \"type\": \"string\" } ], \"stateMutability\": \"view\", \"type\": \"function\" } ]"; 

    async public void VoucherMintNFT()
    {
        string account = PlayerPrefs.GetString("Account");
        string contract = "0x2c1867bc3026178a47a677513746dcc6822a137a";
        string method = "redeem";
        // value in wei
        string value = "0";
        // gas limit OPTIONAL
        string gasLimit = "";
        // gas price OPTIONAL
        string gasPrice = "";
        string amount = "1";
        (string, string, string, string, string, string, string) tuple = ('"' + PlayerPrefs.GetString("WebGLVoucher1155TokenID") + '"', '"' + PlayerPrefs.GetString("WebGLVoucher1155MinPrice") + '"', '"' + PlayerPrefs.GetString("WebGLVoucher1155Signer") + '"', '"' + account + '"', '"' + amount + '"', '"' + PlayerPrefs.GetString("WebGLVoucher1155Nonce") + '"', '"' + PlayerPrefs.GetString("WebGLVoucher1155Sig") + '"');
        string v = tuple.ToString();
        string v1 = v.Replace("(", string.Empty);
        string v2 = v1.Replace(")", string.Empty);
        string args = "[[" + v2 + "]]";
        // authentication prefab if statement, you can change this to match your auth wallet if your system is set up for it
        if (PlayerPrefs.GetString("WebGLVoucher1155Signer") == "0x1372199B632bd6090581A0588b2f4F08985ba2d4")
        {
        Debug.Log("Args Sent : " + args);
        try {
            string response = await Web3GL.SendContract(method, abi, contract, args, value, gasLimit, gasPrice);
            Debug.Log(response);
            responseText.text = "Success! TXHash: " + response;
            PlayerPrefs.SetString("WebGLVoucher1155", "");
            SuccessPopup.SetActive(true);
            // add speed bonus and activate speed clouds
            SpeedClouds.SetActive(true);
            Player.GetComponent<PlayerController>().speed = 35;
            
        } catch (Exception e) {
            Debug.LogException(e, this);
        }
        }
        else
        {
            responseText.text = "Voucher Invalid";
            Debug.Log("Voucher Invalid");
        }
    }
}
#endif