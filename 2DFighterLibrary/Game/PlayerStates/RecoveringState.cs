using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class RecoveringState : GameEventListenerState
{
    public RecoveringState(GameEventListenerId listenerId): base(listenerId) {

    }

    public override void Begin() {
        base.Begin();
        Debug.Log("Recovering");
    }
}