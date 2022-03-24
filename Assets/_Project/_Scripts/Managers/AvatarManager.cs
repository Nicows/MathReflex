using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AvatarManager : MonoBehaviour
{
    [Header("Prefabs and Spawns")]
    [SerializeField] private GameObject _spawnAvatarsFlying;
    [SerializeField] private GameObject _spawnMoveSpotsFlying;
    [SerializeField] private GameObject _prefabAvatar;
    [SerializeField] private GameObject _prefabMoveSpot;

    [Header("Avatars in shop/inventaire")]
    [SerializeField] private GameObject _groupOfAvatarsShop;
    // public RewardedAdsButton rewardedAdsButton;

    [Header("Menu")]
    [SerializeField] private Image _currentAvatar;

    private void Start()
    {
        GetAvatarWhenFirstStart();
        RefreshAvatar();
        // rewardedAdsButton.LoadAd();
    }
    private void GetAvatarWhenFirstStart()
    {
        var avatar = PlayerPrefs.GetString("AvatarUsed", "");
        //Début du jeu lorsqu'on a aucun avatar
        if (avatar == "")
        {
            // PlayerPrefs.SetInt("Avatar_carre", 1);
            PlayerPrefs.SetString("AvatarUsed", "carre");
        }
        else
        {
            _currentAvatar.sprite = Resources.Load<Sprite>(path: "Avatars/" + avatar);
        }
    }


    // public void DisplayAd(Button button)
    // {
    // rewardedAdsButton.LoadAd();
    // rewardedAdsButton.ShowAd(button);
    //     BuyAvatar(button.transform.parent.gameObject);
    // }
    // public static void BuyAvatar(GameObject avatar)
    // {
    //     PlayerPrefs.SetInt("Avatar_" + avatar.name, 1);
    //     needToRefresh = true;
    // }
    private void RefreshAvatar()
    {
        var avatarUsed = PlayerPrefs.GetString("AvatarUsed", "");
        foreach (Transform avatar in _groupOfAvatarsShop.transform)
        {
            if (!avatar.CompareTag("Avatar")) return;
            var button = avatar.GetComponentInChildren<Button>();
            if (avatarUsed == avatar.name)
            {
                button.interactable = false;
                button.GetComponentInChildren<TMP_Text>().text = Languages.Instance.GetPropriety("selected");
            }
            else
            {
                button.interactable = true;
                button.GetComponentInChildren<TMP_Text>().text = Languages.Instance.GetPropriety("select");
            }

        }
        DisplayAvailableAvatars();
    }
    private void DisplayAvailableAvatars()
    {
        //Check all Avatars in the CharactersFlying and set Active all avatars bought(GetInt = 1)
        foreach (Transform avatar in _spawnAvatarsFlying.GetComponentInChildren<Transform>())
        {
            // int avatarBought = PlayerPrefs.GetInt("Avatar_" + avatar.name, 0);
            // if (avatarBought == 0) avatar.gameObject.SetActive(false);
            // else if (avatarBought == 1) avatar.gameObject.SetActive(true);
            avatar.gameObject.SetActive(true);
        }
    }
    public void SelectAvatar(GameObject avatar)
    {
        PlayerPrefs.SetString("AvatarUsed", avatar.name);
        _currentAvatar.sprite = Resources.Load<Sprite>(path: "Avatars/" + avatar.name);
        RefreshAvatar();
    }

}
