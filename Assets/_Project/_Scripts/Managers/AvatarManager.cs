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

    [Header("Menu")]
    [SerializeField] private Image _currentAvatar;

    private void Start()
    {
        GetAvatarWhenFirstStart();
        RefreshAvatar();
    }
    private void OnEnable() => Languages.OnLanguageChanged += RefreshAvatar;
    private void OnDisable() => Languages.OnLanguageChanged -= RefreshAvatar;

    private void GetAvatarWhenFirstStart()
    {
        
        var avatar = PlayerPrefs.GetString("AvatarUsed", "");
        //Début du jeu lorsqu'on a aucun avatar
        if (avatar == "")
            PlayerPrefs.SetString("AvatarUsed", "carre");
        else
            _currentAvatar.sprite = Resources.Load<Sprite>(path: "Avatars/" + avatar);

    }

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
                button.GetComponentInChildren<TextMeshProUGUI>().text = Languages.Instance.GetPropriety("selected");
            }
            else
            {
                button.interactable = true;
                button.GetComponentInChildren<TextMeshProUGUI>().text = Languages.Instance.GetPropriety("select");
            }
        }
        DisplayAvailableAvatars();
    }
    private void DisplayAvailableAvatars()
    {
        foreach (Transform avatar in _spawnAvatarsFlying.GetComponentInChildren<Transform>())
        {
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
