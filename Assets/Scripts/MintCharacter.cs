using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using Utils;
using System.Numerics;

public class MintCharacter : MonoBehaviour
{
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI strategyClassHash;
    public TextMeshProUGUI strength;
    public TextMeshProUGUI agility;
    public TextMeshProUGUI vitality;
    public TextMeshProUGUI stamina;

    [DllImport("__Internal")]
    private static extern void PasteFromClipboard(string callbackObjectName, string callbackMethodName);
    [DllImport("__Internal")]
    private static extern void CopyToClipboard(string text);

    public void Mint()
    {
        string characterNameString = HexConverter.StringToFelt(characterName.text);
        string strategyClassHashString = strategyClassHash.text;
        string strengthString = strength.text;
        string agilityString = agility.text;
        string vitalityString = vitality.text;
        string staminaString = stamina.text;

        // Ensure the sum of strength, agility, vitality, and stamina is 5
        int sum = int.Parse(strengthString) + int.Parse(agilityString) + int.Parse(vitalityString) + int.Parse(staminaString);
        if (sum != 5)
        {
            Debug.LogError("The sum of strength, agility, vitality, and stamina must be 5.");
            return;
        }

        MintCharacterToken(characterNameString, strengthString, agilityString, vitalityString, staminaString, strategyClassHashString);
    }

    void MintCharacterToken(string characterName, string strength, string agility, string vitality, string stamina, string strategyClassHash)
    {
        string[] calldata = new string[] {
            characterName,
            strength,
            agility,
            vitality,
            stamina,
            "0x0057dfdc7b7f813288e54f6a2ea0d2077f7c54a643620ef2b8074a58589c959d"
        };
        string calldataString = JsonUtility.ToJson(new ArrayWrapper { array = calldata });
        string walletType = PlayerPrefs.GetString("walletType");
        Debug.Log(calldataString);
        if (walletType == "ArgentX")
        {
            JSInteropManager.SendTransactionArgentX("0x47c92218dfdaac465ad724f028f0f075b1c05c9ff9555d0e426c025e45c035", "createCharacter", calldataString, "MintPanel", "MintCharacterCallback");
        }
        else if (walletType == "Braavos")
        {
            JSInteropManager.SendTransactionBraavos("0x47c92218dfdaac465ad724f028f0f075b1c05c9ff9555d0e426c025e45c035", "createCharacter", calldataString, "MintPanel", "MintCharacterCallback");
        }
        else
        {
            JSInteropManager.SendTransaction("0x47c92218dfdaac465ad724f028f0f075b1c05c9ff9555d0e426c025e45c035", "createCharacter", calldataString, "MintPanel", "MintCharacterCallback");
        }
    }

    void MintCharacterCallback(string transactionHash)
    {
        Debug.Log("https://goerli.voyager.online/tx/" + transactionHash);
    }

    public void Cancel()
    {
        SceneManager.LoadScene("Arena");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Copy from input field to system clipboard
            TMP_InputField inputField = GetCurrentInputField();
            if (inputField != null)
            {
                CopyToClipboard(inputField.text);
                GUIUtility.systemCopyBuffer = inputField.text;
            }
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            PasteFromClipboard("MintPanel", "PasteCallback");
        }
    }

    TMP_InputField GetCurrentInputField()
    {
        GameObject selectedObject = EventSystem.current.currentSelectedGameObject;
        if (selectedObject != null)
        {
            return selectedObject.GetComponent<TMP_InputField>();
        }

        return null;
    }

    private void PasteCallback(string clipboardText)
    {
        TMP_InputField inputField = GetCurrentInputField();
        if (clipboardText != null && clipboardText != "")
        {
            GUIUtility.systemCopyBuffer = clipboardText;
            inputField.text += clipboardText;
        }
        else
        {
            Debug.Log("Clipboard is empty");
            // Paste from system clipboard to input field
            if (inputField != null)
            {
                inputField.text += GUIUtility.systemCopyBuffer;
            }
        }
    }
}
