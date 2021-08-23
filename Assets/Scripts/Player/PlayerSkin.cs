using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _playerRenderer;

    private Sprite[] _currentSkin = null;
    private int skinID;

    private void LateUpdate()
    {
        if(_currentSkin!= null)
        {
            ReplaceSprite();
        }
    }
    private void ReplaceSprite()
    {
        string index = "";
        if (_playerRenderer.sprite.name[_playerRenderer.sprite.name.Length - 2] != '_')
        {
            if (_playerRenderer.sprite.name[_playerRenderer.sprite.name.Length - 3] != '_')
                index += _playerRenderer.sprite.name[_playerRenderer.sprite.name.Length - 3];
            index += _playerRenderer.sprite.name[_playerRenderer.sprite.name.Length - 2];
        }
        index += _playerRenderer.sprite.name[_playerRenderer.sprite.name.Length - 1];

        if (int.TryParse(index, out int j))
        {
            if (j >= _currentSkin.Length)
            {
                _playerRenderer.sprite = null;
            }
            else
            {
                _playerRenderer.sprite = _currentSkin[j];
            }
        }
    }

    public void SetSkin(Sprite[] sprites, int index)
    {
        if (sprites != null)
        {
            skinID = index;
            if (index != 0)
                _currentSkin = sprites;
            else
                _currentSkin = null;
        }
    }
    public int GetID()
    {
        return skinID;
    }
    public Sprite GetCurrentSkin()
    {
        if (_currentSkin != null)
            return _currentSkin[0];
        else
            return ItemsDatabase.Instance.GetSkinAnimation(0)[0];
    }
}
