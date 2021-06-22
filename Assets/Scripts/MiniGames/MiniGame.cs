using UnityEngine;

public class MiniGame : MonoBehaviour, IInteractable
{
    [SerializeField]
    private MiniGameUI UI;
    private short[] _fields = new short[9] {0, 0, 0, 0, 0, 0, 0, 0, 0};
    private int _cube1;
    private int _cube2;
    private short _moneyPlayer = 0;
    private short _moneyAI1 = 10;
    private short _moneyAI2 = 10;
    private bool _isPlayerTurn = false;
    private bool _isAI1Turn = false;
    private bool _isAI2Turn = false;


    public string GetActionName()
    {
        return "Play";
    }

    public void Interact()
    {
        if (HasEnoughMoney())
        {
            CharacterManager.Instance.Inventory.Gold -= 10;
            _moneyPlayer = 10;
            _isPlayerTurn = true;
            UI.OpenMiniGame();
            UI.turnUI.SetText("");
            UI.amountUI.SetText("");
            UI.ChangeButtonText("Throw");
        }
        else
        {
            _moneyPlayer = 0;
            _isPlayerTurn = false;
            /*ShowToast();*/
            InterfaceManager.Instance.ShowFaceElements();
        }
    }

    public void ThrowCubes()
    {
        _cube1 = Random.Range(1, 7);
        _cube2 = Random.Range(1, 7);
        int cubeSum = _cube1 + _cube2;
        short money = 0;


        if (_isPlayerTurn)
        {
            money = _moneyPlayer;
        }

        else if (_isAI1Turn)
        {
            money = _moneyAI1;
        }

        else if (_isAI2Turn)
        {
            money = _moneyAI2;
        }

        // PlayAnimation(_cube1, _cube2);

        switch (cubeSum)
        {
             // "Pig" field takes all coins from the usual fields, besides "Wedding" field
            case 2:
                money += Pig();
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
                {
                    money--;
                    PutCoin(cubeSum);
                }

                else
                {
                    money++;
                    TakeCoin(cubeSum);
                }
                break;
             // "Wedding" field can store more than 1 coin
            case 7:
                money--;
                Wedding();
                Debug.Log("-- Wedding field --");
                break;
             // "King's field takes all coins from the desk, including "Wedding" field
            case 12:
                money += King();
                Debug.Log("-- King's field --");
                break;

        }

         // Check has player enough money to continue game and is it player's turn
        if (_isPlayerTurn)
        {
            _moneyPlayer = money;
            
            if (!CanContinuePlay(_moneyPlayer))
            {
                Debug.Log("U lose");
                UI.Defeat();
                CloseGame();
            }

            Debug.Log("Player's Turn");
            Debug.Log(" -- Player's money : " + _moneyPlayer + " -- ");

            _isPlayerTurn = false;  
            _isAI1Turn = CanContinuePlay(_moneyAI1);
            _isAI2Turn = CanContinuePlay(_moneyAI2);

            UI.turnUI.SetText("You thrown");
            UI.amountUI.SetText(cubeSum.ToString());
            UI.ChangeButtonText("Next");
        }

        else if (_isAI1Turn)
        {
            _moneyAI1 = money;
            _isAI1Turn = false;
            Debug.Log("AI #1 Turn");
            Debug.Log(" -- AI #1 money : "  + _moneyAI1 + " -- ");

            if (!CanContinuePlay(_moneyAI2))
            {
                _isPlayerTurn = true;
                UI.ChangeButtonText("Throw");
            }

            UI.turnUI.SetText("First Khajiit thrown");
            UI.amountUI.SetText(cubeSum.ToString());

        }

        else if (_isAI2Turn)
        {
            _moneyAI2 = money;
            _isPlayerTurn = CanContinuePlay(_moneyPlayer);

            UI.turnUI.SetText("Second Khajiit thrown");
            UI.amountUI.SetText(cubeSum.ToString());
            UI.ChangeButtonText("Throw");

            Debug.Log("AI #2 Turn");
            Debug.Log(" -- AI #2 money : " + _moneyAI2 + " -- ");
        }

        if (CanContinuePlay(_moneyPlayer) && !CanContinuePlay(_moneyAI1) && !CanContinuePlay(_moneyAI2))
        {
            Victory();
            UI.Victory();
        }

    }

    private short Pig()
    {
        short sum = 0;

        for (ushort i = 0; i < _fields.Length; i++)
        {
            if (i == 4) continue;

            sum += _fields[i];
            _fields[i] = 0;
            UI.fieldImages[i].sprite = UI.coinSprites[0];
        }

        return sum;
    }

    private void Wedding()
    {
        _fields[4]++;
        UI.fieldImages[4].sprite = UI.coinSprites[_fields[4]];
        if (_fields[4] > 3) UI.fieldImages[4].SetNativeSize();
    }

    private short King()
    {
        short sum = 0;

        for (ushort i = 0; i < _fields.Length; i++)
        {
            sum += _fields[i];
            _fields[i] = 0;
            UI.fieldImages[i].sprite = UI.coinSprites[0];
            UI.fieldImages[i].SetNativeSize();
        }

        return sum;
    }

    private void PutCoin(int cubeSum)
    {
        _fields[cubeSum - 3] = 1;
        UI.fieldImages[cubeSum - 3].sprite = UI.coinSprites[1];

    }

    private void TakeCoin(int cubeSum)
    {
        _fields[cubeSum - 3] = 0;
        UI.fieldImages[cubeSum - 3].sprite = UI.coinSprites[0];
    }

    private bool FieldIsEmpty(int field)
    {
        return field == 0;
    }

    private bool HasEnoughMoney()
    {
        return CharacterManager.Instance.Inventory.Gold >= 10;
    }

    private bool CanContinuePlay(int money)
    {
        return money > 0;
    }

    private void CloseGame()
    {

        King();

        CharacterManager.Instance.Inventory.Gold += _moneyPlayer;
        CharacterManager.Instance.Inventory.UpdateGold();

        _moneyPlayer = 0;
        _moneyAI1 = 10;
        _moneyAI2 = 10;
        _isPlayerTurn = false;
        _isAI1Turn = false;
        _isAI2Turn = false;
    }

    private void Victory()
    {

        _moneyPlayer += King();
        Debug.Log("U won & earned " + _moneyPlayer);
        CharacterManager.Instance.Inventory.Gold += _moneyPlayer;
        CharacterManager.Instance.Inventory.UpdateGold();
        CloseGame();
    }


}
