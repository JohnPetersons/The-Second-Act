using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class MovingForwardState : GameEventListenerState
{
    public MovingForwardState(GameEventListenerId listenerId): base(listenerId) {

    }

    public override void Begin() {
        base.Begin();
        Debug.Log("Moving Forward");
    }
}
