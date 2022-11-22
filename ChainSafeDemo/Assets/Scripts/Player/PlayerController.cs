using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject GlobalManager;
    public GameObject MintNFTMenu;
    public GameObject VoucherMintNFTMenu;
    public GameObject VerifyMenu;
    public GameObject SignMenu;
    public GameObject TransferMenu;
    public GameObject ContractMenu;
    public GameObject VoucherMenu;
    public GameObject MarketplaceMenu;
    public GameObject WelcomeMenu;
    public GameObject AchievementText;
    public Text WalletText;
    public Text CoinsText;
    public Text LivesText;
    private Rigidbody rb;
    private PlayerInputActions playerInput;
    private Animator animator;
    CharacterController characterController;
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool isMovementPressed;
    public float speed = 20;
    public float gravity = -50;
    private float groundedGravity = -20f;
    public float rotationFactorPerFrame = 15.0f;

    void Awake()
    {
        // finds global object
        GlobalManager = GameObject.FindGameObjectWithTag("Global");
        // sets texts
        WalletText.text = PlayerPrefs.GetString("Account");
        CoinsText.text = "Coins: " + GlobalManager.GetComponent<Global>().globalCoins.ToString();
        LivesText.text = "Lives: " + GlobalManager.GetComponent<Global>().globalLives.ToString();
        rb = GetComponent<Rigidbody>();
        //initialize player input actions and animator
        playerInput = new PlayerInputActions();
        playerInput.Game.Move.started += onMovementInput;
        playerInput.Game.Move.canceled += onMovementInput;
        playerInput.Game.Move.performed += onMovementInput;
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        // pops up welcome menu at start of game
        if (GlobalManager.GetComponent<Global>().globalLives == 5){
            WelcomeMenu.SetActive(true);
        }
    }

    // used when player collides with tagged objects
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            // checks if player has 0 lives on hit, if greater than 0, subract 1
            if (GlobalManager.GetComponent<Global>().globalLives > 0)
            {
            GlobalManager.GetComponent<Global>().globalLives -= 1;
            LivesText.text = "Lives: " + GlobalManager.GetComponent<Global>().globalLives.ToString();
            FindObjectOfType<AudioManager>().Play("Cluck");
            SceneManager.LoadScene("Game");
            }
            else
            {
            FindObjectOfType<AudioManager>().Play("Cluck");
            SceneManager.LoadScene("Game");
            }
        }
    }

    // used when player collides with tagged objects
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        // adds 1 to coin score, displays it and destroys the coin
        if (hit.transform.tag == "Coin")
        {
            FindObjectOfType<AudioManager>().Play("Coin");
            FindObjectOfType<AudioManager>().Play("Cluck");
            GlobalManager.GetComponent<Global>().globalCoins += 1;
            CoinsText.text = "Coins: " + GlobalManager.GetComponent<Global>().globalCoins.ToString();
            Destroy(hit.gameObject);
        }

        // makes menus pop up
        if (hit.transform.tag == "MintNFT")
        {
            FindObjectOfType<AudioManager>().Play("Pop");
            MintNFTMenu.SetActive(true);
        }

        if (hit.transform.tag == "VoucherMintNFT")
        {
            FindObjectOfType<AudioManager>().Play("Pop");
            VoucherMintNFTMenu.SetActive(true);
        }

        if (hit.transform.tag == "Verify")
        {
            FindObjectOfType<AudioManager>().Play("Pop");
            VerifyMenu.SetActive(true);
        }

        if (hit.transform.tag == "Sign")
        {
            FindObjectOfType<AudioManager>().Play("Pop");
            SignMenu.SetActive(true);
        }

        if (hit.transform.tag == "Transfer")
        {
            FindObjectOfType<AudioManager>().Play("Pop");
            TransferMenu.SetActive(true);
        }

        if (hit.transform.tag == "Contract")
        {
            FindObjectOfType<AudioManager>().Play("Pop");
            ContractMenu.SetActive(true);
        }

        if (hit.transform.tag == "Voucher")
        {
            if (GlobalManager.GetComponent<Global>().globalCoins > 0)
            {
            FindObjectOfType<AudioManager>().Play("Pop");
            VoucherMenu.SetActive(true);
            }
            else
            {
                Debug.Log("Get More Coins!");
            }
        }

        if (hit.transform.tag == "Marketplace")
        {
            FindObjectOfType<AudioManager>().Play("Pop");
            MarketplaceMenu.SetActive(true);
        }
    }

    // menu close buttons, usually you would subtract a coin once the blockchain call has suceeded, I've just done it here to show you how in the voucher script

    public void CloseVoucherMenu()
    {
        FindObjectOfType<AudioManager>().Play("Pop");
        if (GlobalManager.GetComponent<Global>().globalCoins > 0){
        GlobalManager.GetComponent<Global>().globalCoins -= 1;
        CoinsText.text = "Coins: " + GlobalManager.GetComponent<Global>().globalCoins.ToString();
        }
        VoucherMenu.SetActive(false);
    }

    public void CloseMintNFTMenu()
    {
        FindObjectOfType<AudioManager>().Play("Pop");
        CoinsText.text = "Coins: " + GlobalManager.GetComponent<Global>().globalCoins.ToString();
        MintNFTMenu.SetActive(false);
    }

    async public void CloseVoucherMintNFTMenu()
    {
        FindObjectOfType<AudioManager>().Play("Pop");
        CoinsText.text = "Coins: " + GlobalManager.GetComponent<Global>().globalCoins.ToString();
        VoucherMintNFTMenu.SetActive(false);
        AchievementText.SetActive(true);
        await new WaitForSeconds(5);
        AchievementText.SetActive(false);
    }

    public void CloseVerifyMenu()
    {
        FindObjectOfType<AudioManager>().Play("Pop");
        CoinsText.text = "Coins: " + GlobalManager.GetComponent<Global>().globalCoins.ToString();
        VerifyMenu.SetActive(false);
    }

    public void CloseSignMenu()
    {
        FindObjectOfType<AudioManager>().Play("Pop");
        CoinsText.text = "Coins: " + GlobalManager.GetComponent<Global>().globalCoins.ToString();
        SignMenu.SetActive(false);
    }

    public void CloseTransferMenu()
    {
        FindObjectOfType<AudioManager>().Play("Pop");
        CoinsText.text = "Coins: " + GlobalManager.GetComponent<Global>().globalCoins.ToString();
        TransferMenu.SetActive(false);
    }

    public void CloseContractMenu()
    {
        FindObjectOfType<AudioManager>().Play("Pop");
        CoinsText.text = "Coins: " + GlobalManager.GetComponent<Global>().globalCoins.ToString();
        ContractMenu.SetActive(false);
    }

    public void CloseMarketplaceMenu()
    {
        FindObjectOfType<AudioManager>().Play("Pop");
        MarketplaceMenu.SetActive(false);
    }

    public void CloseWelcomeMenu()
    {
        FindObjectOfType<AudioManager>().Play("Pop");
        WelcomeMenu.SetActive(false);
    }

    public void OpenMarketplace()
    {
        Application.OpenURL("https://marketplace-ui.onrender.com/");
    }

    // used for player movement, call this to enable or disable player input detection
    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    // gravity
     void handleGravity()
    {
        if (characterController.isGrounded){
            currentMovement.y = groundedGravity;
            }
            else
            {
                currentMovement.y += gravity * Time.deltaTime;    
            }
    }
     
    // movement
    void onMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
        if (isMovementPressed)
        {
            FindObjectOfType<AudioManager>().Play("Walk");
            animator.SetBool("isRunning", true);
        }
        else
        {
            FindObjectOfType<AudioManager>().Pause("Walk");
            animator.SetBool("isRunning", false);
        }
    }

    // rotation
    void handleRotation()
    {
        Vector3 positionToLookAt;
        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;
        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }

    }

    // player move functions here as they constantly need to be updated
    void Update()
    {
        // all movement is calulated over Time.deltaTime to unsure uniform speeds across devices
        characterController.Move(currentMovement * speed * Time.deltaTime);
        handleGravity();
        handleRotation();
    }
}
