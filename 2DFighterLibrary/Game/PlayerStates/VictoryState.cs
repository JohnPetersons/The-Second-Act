using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class VictoryState : GameEventListenerState
{
    public VictoryState(GameEventListenerId listenerId): base(listenerId) {

    }

    public override void Begin() {
        base.Begin();
        Debug.Log("Victory");
    }
}