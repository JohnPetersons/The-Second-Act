using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class ExitGameFocusState : GameState
{
    public override void Begin() {
        base.Begin();
        Debug.Log("Exit Game Focus");
    }
}
