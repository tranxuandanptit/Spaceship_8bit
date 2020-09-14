using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditUI : UIController
{
    public void OnBackButtonClicked()
    {
        GameManager.Instance.CurrentGameState = GameState.GameMenu;
    }
}
