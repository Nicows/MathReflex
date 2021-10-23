using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AvatarManager : MonoBehaviour
{

    [Header("Prefabs and Spawns")]
    public GameObject spawnAvatarsFlying;
    public GameObject spawnMoveSpotsFlying;
    public GameObject prefabAvatar;
    public GameObject prefabMoveSpot;

    [Header("Avatars in shop/inventaire")]
    public GameObject groupOfAvatarsShop;
    public RewardedAdsButton rewardedAdsButton;

    [Header("Clicked Buy")]
    private static Transform avatarToBuy = null;
    public static bool needToRefresh = false;

    [Header("Menu")]
    public Image currentAvatar;
    public Languages languages;

    private void Start()
    {
        GetAvatarWhenFirstStart();
        RefreshAvatar();
    }
    private void GetAvatarWhenFirstStart()
    {
        //Début du jeu lorsqu'on a aucun avatar
        if (PlayerPrefs.GetString("AvatarUsed", "") == "")
        {
            PlayerPrefs.SetInt("Avatar_carre", 1);
            PlayerPrefs.SetString("AvatarUsed", "carre");
        }
    }
    private void Update()
    {
        NeedToRefresh();
    }

    private void NeedToRefresh()
    {
        if (needToRefresh)
        {
            RefreshAvatar();
            needToRefresh = false;
        }
    }
    public void DisplayAd(Button button)
    {
        avatarToBuy = button.transform.parent;
        rewardedAdsButton.ShowAd(button);
    }
    public static void BuyAvatar()
    {
        PlayerPrefs.SetInt("Avatar_" + avatarToBuy.name, 1);
        needToRefresh = true;
    }
    private void RefreshAvatar()
    {
        CheckAvatarBought();
        //Check all Avatars bought and change button to select
        foreach (Transform avatar in groupOfAvatarsShop.GetComponentsInChildren<Transform>(true))
        {
            int avatarBought = PlayerPrefs.GetInt("Avatar_" + avatar.name, 0);

            foreach (Button button in avatar.GetComponentsInChildren<Button>(true))
            {
                if (button.name == "ButtonBuy")
                {
                    button.GetComponentInChildren<TMP_Text>().text = languages.GetBuy();
                }

                if (avatarBought == 1)
                {
                    if (button.name == "ButtonBuy")
                    {
                        button.gameObject.SetActive(false);
                    }
                    else if (button.name == "ButtonSelect")
                    {
                        button.gameObject.SetActive(true);
                        if (PlayerPrefs.GetString("AvatarUsed", "") == avatar.name)
                        {
                            button.interactable = false;
                            button.GetComponentInChildren<TMP_Text>().text = languages.GetSelected();
                            currentAvatar.sprite = Resources.Load<Sprite>(path: "Images/Reflexion/" + avatar.name);
                        }
                        else
                        {
                            button.interactable = true;
                            button.GetComponentInChildren<TMP_Text>().text = languages.GetSelect();
                        }
                    }
                }
            }

        }

    }
    private void CheckAvatarBought()
    {
        //Check all Avatars in the CharactersFlying and set Active all avatars bought(GetInt = 1)
        foreach (Transform avatar in spawnAvatarsFlying.GetComponentInChildren<Transform>())
        {
            int avatarBought = PlayerPrefs.GetInt("Avatar_" + avatar.name, 0);
            if (avatarBought == 0) avatar.gameObject.SetActive(false);
            else if (avatarBought == 1) avatar.gameObject.SetActive(true);
        }
    }
    public void SelectAvatar(GameObject avatar)
    {
        PlayerPrefs.SetString("AvatarUsed", avatar.name);
        RefreshAvatar();
    }

}
