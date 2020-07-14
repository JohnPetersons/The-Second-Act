using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class UsingSpecialState : GameEventListenerState
{
    public UsingSpecialState(GameEventListenerId listenerId): base(listenerId) {

    }

    public override void Begin() {
        base.Begin();
        Debug.Log("Using special");
    }
}