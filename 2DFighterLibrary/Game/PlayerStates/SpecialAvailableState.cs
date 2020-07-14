using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class SpecialAvailableState : GameEventListenerState
{
    public SpecialAvailableState(GameEventListenerId listenerId): base(listenerId) {

    }

    public override void Begin() {
        base.Begin();
        Debug.Log("Special available");
    }
}