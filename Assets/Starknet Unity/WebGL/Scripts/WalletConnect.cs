using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WalletConnect : MonoBehaviour
{
    public static string playerAddress;
    public GameObject walletSelector;
    public GameObject connectButton;
    private IEnumerator ConnectWalletAsync(Action connectWalletFunction)
    {
        // Call the JavaScript method to connect the wallet
        connectWalletFunction();

        // Wait for the connection to be established
        yield return new WaitUntil(() => JSInteropManager.IsConnected());

        playerAddress = JSInteropManager.GetAccount();
        PlayerPrefs.SetString("playerAddress", playerAddress);
        Debug.Log("Connected to wallet: " + playerAddress);

        // Load the MintCharacter scene
        SceneManager.LoadScene("MintCharacter");
    }

    public void ConnectWallet()
    {
        Debug.Log("Connecting wallet...");
        connectButton.SetActive(false);
        walletSelector.SetActive(true);
    }

    public void OnButtonConnectWalletArgentX()
    {
        PlayerPrefs.SetString("walletType", "ArgentX");
        StartCoroutine(ConnectWalletAsync(JSInteropManager.ConnectWalletArgentX));
    }

    public void OnButtonConnectWalletBraavos()
    {
        PlayerPrefs.SetString("walletType", "Braavos");
        StartCoroutine(ConnectWalletAsync(JSInteropManager.ConnectWalletBraavos));
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("playerAddress"))
        {
            playerAddress = PlayerPrefs.GetString("playerAddress");
            Debug.Log("Connected to wallet: " + playerAddress);
        }
        bool available = JSInteropManager.IsWalletAvailable();
        if (!available)
        {
            JSInteropManager.AskToInstallWallet();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
