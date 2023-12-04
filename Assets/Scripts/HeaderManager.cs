using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HeaderManager : MonoBehaviour
{
    public TextMeshProUGUI ArenaText;
    public TextMeshProUGUI HistoryText;
    public TextMeshProUGUI LeaderboardText;
    public TextMeshProUGUI MyProfileText;
    public TextMeshProUGUI playerAddressText;

    // Start is called before the first frame update
    void Start()
    {
        string playerAddress = PlayerPrefs.GetString("playerAddress");
        // make the address uppercase
        playerAddress = playerAddress.ToUpper();

        // truncate the address
        string truncatedPlayerAddress = playerAddress.Substring(0, 6) + "..." + playerAddress.Substring(playerAddress.Length - 4, 4);
        playerAddressText.text = truncatedPlayerAddress;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the current scene name
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Change the colour of the active scene's text to blue
        if (currentSceneName == "Arena")
        {
            ArenaText.color = new Color32(0, 0, 255, 255);
        }
        else if (currentSceneName == "History")
        {
            HistoryText.color = new Color32(0, 0, 255, 255);
        }
        else if (currentSceneName == "Leaderboard")
        {
            LeaderboardText.color = new Color32(0, 0, 255, 255);
        }
        else if (currentSceneName == "MyProfile")
        {
            MyProfileText.color = new Color32(0, 0, 255, 255);
        }
    }

    public void Arena()
    {
        SceneManager.LoadScene("Arena");
    }

    public void History()
    {
        SceneManager.LoadScene("History");
    }

    public void Leaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    public void MyProfile()
    {
        SceneManager.LoadScene("MyProfile");
    }
}
