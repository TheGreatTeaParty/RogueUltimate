using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;

public class CharacterSkinCreator : MonoBehaviour
{
    [SerializeField]
    private Image avatar;
    [SerializeField]
    private Button _nextBtn;
    [SerializeField]
    private Button _unlockBtn;
    private int _currentIndex = 0;
    private ItemsDatabase database;

    // Start is called before the first frame update
    void Start()
    {
        database = ItemsDatabase.Instance;
        avatar.sprite = database.GetSkinAnimation(_currentIndex)[0];
        database.OnSkinUnlocked += GetAvatarInfo;
    }


    public void NextSkin()
    {
        if(database.GetSkinLength()-1 > _currentIndex)
        {
            _currentIndex++;
            GetAvatarInfo();
        }
        else
        {
            _currentIndex = 0;
            GetAvatarInfo();
        }
    }
    public void PrevSkin()
    {
        if (_currentIndex > 0)
        {
            _currentIndex--;
            GetAvatarInfo();
        }
        else
        {
            _currentIndex = database.GetSkinLength() - 1;
            GetAvatarInfo();
        }
    }

    public int GetSkinID()
    {
        return _currentIndex;
    }
    public Sprite[] GetSkin()
    {
        return database.GetSkinAnimation(_currentIndex);
    }
    void GetAvatarInfo()
    {
        avatar.sprite = database.GetSkinAnimation(_currentIndex)[0];
        if (database.IsSkinLocked(_currentIndex))
        {
            _nextBtn.interactable = false;
            _unlockBtn.gameObject.SetActive(true);
        }
        else
        {
            _nextBtn.interactable = true;
            _unlockBtn.gameObject.SetActive(false);
        }
    }
    public void UnlockAvatar()
    {
        database.BuyAvatarByIndex(_currentIndex);
    }
}
