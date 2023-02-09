using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Ready : MonoBehaviour
{
    public void OnClickPlayButton() // Bind Button Event.
    {
        Managers.Game.GameState = GameState.Play;
    }
}
