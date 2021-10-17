using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AvatarManager : MonoBehaviour
{
    public GameObject charactersFlying;
    public GameObject moveSpotsFlying;
    public GameObject prefabAvatar;
    public GameObject prefabMoveSpot;

    public GameObject groupAvatars;
    public RewardedAdsButton rewardedAdsButton;


    private void Start()
    {

        //Début du jeu lorsqu'on a aucun avatar
        if (PlayerPrefs.GetString("AvatarUsed", "") == "")
        {
            PlayerPrefs.SetInt("Avatar_carre", 1);
            PlayerPrefs.SetString("AvatarUsed", "carre");
        }

        RefreshAvatar();
    }
    public void BuyAd(Button button){
        rewardedAdsButton.LoadAd();
        rewardedAdsButton.ShowAd(button);
    }
    public void BuyAvatar(GameObject avatar)
    {
        //RefreshAvatar ? SetInt avatar(name) 1(Bought)
        PlayerPrefs.SetInt("Avatar_" + avatar.name, 1);
        RefreshAvatar();
    }
    private void RefreshAvatar()
    {
        //Check all Avatars in the CharactersFlying and set Active all avatars bought(GetInt = 1)
        foreach (Transform avatar in charactersFlying.GetComponentInChildren<Transform>())
        {
            int avatarBought = PlayerPrefs.GetInt("Avatar_" + avatar.name, 0);
            if (avatarBought == 0) avatar.gameObject.SetActive(false);
            else if (avatarBought == 1) avatar.gameObject.SetActive(true);
        }

        //Check all Avatars bought and change button to select
        foreach (Transform avatar in groupAvatars.GetComponentsInChildren<Transform>())
        {
            int avatarBought = PlayerPrefs.GetInt("Avatar_" + avatar.name, 0);
            if (avatarBought == 1)
            {
                foreach (Button button in avatar.GetComponentsInChildren<Button>(true))
                {
                    if (button.name == "ButtonBuy")
                    {
                        button.gameObject.SetActive(false);
                    }
                    else if (button.name == "ButtonSelect")
                    {
                        button.gameObject.SetActive(true);
                        if(PlayerPrefs.GetString("AvatarUsed","") == avatar.name){
                            button.interactable = false;
                            button.GetComponentInChildren<TMP_Text>().text = "Selected";
                        }else{
                            button.interactable = true;
                            button.GetComponentInChildren<TMP_Text>().text = "Select";
                        }
                    }
                }
            }
        }

    }
    public void SelectAvatar(GameObject avatar)
    {
        PlayerPrefs.SetString("AvatarUsed", avatar.name);
        RefreshAvatar();
    }
}
