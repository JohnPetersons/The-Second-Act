using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class ExitGameSelectedState : GameState
{
    public override void Begin() {
        base.Begin();
        Debug.Log("Exit Game Selected");
    }
}
