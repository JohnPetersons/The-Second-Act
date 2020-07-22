using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class ControlsFocusState : GameState
{
    public override void Begin() {
        base.Begin();
        GameSystem.GetGameData<GameObject>(Selected.TAG).GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        GameSystem.GetGameData<GameObject>(Focus.TAG).GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        Vector3 newPos = GameSystem.GetGameData<GameObject>(ControlsButton.TAG).transform.position;
        GameSystem.GetGameData<GameObject>(Focus.TAG).transform.position = new Vector3(newPos.x, newPos.y, 1);
    }
}
