using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class StartMatchSelectedState : GameState
{
    public override void Begin() {
        base.Begin();
        new TypedGameEvent<string>(LoadScreen.TAG, "fadeOutGame", "startMatch");
    }
}
