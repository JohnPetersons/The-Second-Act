using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class StartMatchSelectedState : GameState
{
    public override void Begin() {
        base.Begin();
        Debug.Log("Start Match Selected");
    }
}
