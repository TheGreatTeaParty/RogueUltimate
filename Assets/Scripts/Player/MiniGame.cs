using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiniGame : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject miniGame;
    [SerializeField]
    private Image[] fieldImages;
    [SerializeField]
    private Sprite[] coinSprites;
    [SerializeField]
    private TextMeshProUGUI amountUI;
    private int[] _fields = new int[9] {0, 0, 0, 0, 0, 0, 0, 0, 0};
    private int _cube1;
    private int _cube2;
    private int _playerMoney = 0;

    public string GetActionName()
    {
        return "Play";
    }

    public void Interact()
    {
        if (HasEnoughMoney())
        {
            CharacterManager.Instance.Inventory.Gold -= 20;
            _playerMoney = 20;
            OpenMiniGame();
        }
        else
        {
            _playerMoney = 0;
            /*ShowToast();*/
            InterfaceManager.Instance.ShowFaceElements();
        }
    }

    public void ThrowCubes()
    {
        _cube1 = Random.Range(1, 7);
        _cube2 = Random.Range(1, 7);
        int cubeSum = _cube1 + _cube2;
        amountUI.SetText(cubeSum.ToString());

        // PlayAnimation(_cube1, _cube2);

        switch (cubeSum)
        {
             // "Pig" field takes all coins from the usual fields, besides "Wedding" field
            case 2:
                Pig();
                Debug.Log("-- Pig field --");
                break;
             // Usual fields, wich can't store more than 1 coin
            case 3:
            case 4:
            case 5:
            case 6:
            case 8:
            case 9:
            case 10:
            case 11:
                if (FieldIsEmpty(_fields[cubeSum - 3]))
                    PutCoin(cubeSum);
                else
                    TakeCoin(cubeSum);
                    
                break;
             // "Wedding" field can store more than 1 coin
            case 7:
                Wedding(cubeSum);
                Debug.Log("-- Wedding field --");
                break;
             // "King's field takes all coins from the desk, including "Wedding" field
            case 12:
                King();
                Debug.Log("-- King's field --");
                break;

        }

         // Check has player enough money to continue game
        if (!CanContinuePlay())
            CloseMiniGame();

    }

    private void Pig()
    {
        int sum = 0;

        for (ushort i = 0; i < _fields.Length; i++)
        {
            if (i == 4) continue;

            sum += _fields[i];
            _fields[i] = 0;
            fieldImages[i].sprite = coinSprites[0];
        }

        _playerMoney += sum;
    }

    private void Wedding(int cubeSum)
    {
        _playerMoney--;
        _fields[4]++;
        fieldImages[cubeSum - 3].sprite = coinSprites[_fields[4]];
    }

    private void King()
    {
        int sum = 0;

        for (ushort i = 0; i < _fields.Length; i++)
        {
            sum += _fields[i];
            _fields[i] = 0;
            fieldImages[i].sprite = coinSprites[0];
        }

        _playerMoney += sum;
    }

    private void PutCoin(int cubeSum)
    {
        _playerMoney--;
        _fields[cubeSum - 3] = 1;
        fieldImages[cubeSum - 3].sprite = coinSprites[1];

    }

    private void TakeCoin(int cubeSum)
    {
        _playerMoney++;
        _fields[cubeSum - 3] = 0;
        fieldImages[cubeSum - 3].sprite = coinSprites[0];
    }

    private void OpenMiniGame()
    {
        InterfaceManager.Instance.HideFaceElements();
        InterfaceManager.Instance.HideTavernUI();
        miniGame.gameObject.SetActive(true);
    }

    public void CloseMiniGame()
    {
        CalculateGold();
        miniGame.gameObject.SetActive(false);
        InterfaceManager.Instance.ShowFaceElements();
        InterfaceManager.Instance.ShowTavernUI();
    }

    private void CalculateGold()
    {
        CharacterManager.Instance.Inventory.Gold += _playerMoney;
    }

    private bool FieldIsEmpty(int field)
    {
        return field == 0;
    }

    private bool HasEnoughMoney()
    {
        return CharacterManager.Instance.Inventory.Gold >= 20;
    }

    private bool CanContinuePlay()
    {
        return _playerMoney > 0;
    }
}
