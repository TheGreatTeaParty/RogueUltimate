using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSkinCreator : MonoBehaviour
{
    [SerializeField]
    private Image avatar;

    private int _currentIndex = 0;
    private ItemsDatabase database;

    // Start is called before the first frame update
    void Start()
    {
        database = ItemsDatabase.Instance;
        avatar.sprite = database.GetSkinAnimation(_currentIndex)[0];
    }

    
    public void NextSkin()
    {
        if(database.GetSkinLength()-1 > _currentIndex)
        {
            _currentIndex++;
            avatar.sprite = database.GetSkinAnimation(_currentIndex)[0];
        }
        else
        {
            _currentIndex = 0;
            avatar.sprite = database.GetSkinAnimation(_currentIndex)[0];
        }
    }
    public void PrevSkin()
    {
        if (_currentIndex > 0)
        {
            _currentIndex--;
            avatar.sprite = database.GetSkinAnimation(_currentIndex)[0];
        }
        else
        {
            _currentIndex = database.GetSkinLength() - 1;
            avatar.sprite = database.GetSkinAnimation(_currentIndex)[0];
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
}
