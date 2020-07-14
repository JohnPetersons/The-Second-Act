using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class StartMatchSelectedState : GameState
{
    public override void Begin() {
        base.Begin();
        new TypedGameEvent<string>(GameMaster.TAG, "startMatch", "Match Started");
        Debug.Log("Start Match Selected");
    }
}
