﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class StartMatchSelectedState : GameState
{
    public override void Begin() {
        base.Begin();
        GameSystem.GetGameData<GameObject>(Selected.TAG).GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        GameSystem.GetGameData<GameObject>(Focus.TAG).GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        Vector3 newPos = GameSystem.GetGameData<GameObject>(StartMatchButton.TAG).transform.position;
        GameSystem.GetGameData<GameObject>(Selected.TAG).transform.position = new Vector3(newPos.x, newPos.y, 1);
        new TypedGameEvent<string>(LoadScreen.TAG, "fadeOutGame", "startMatch");
    }
}
