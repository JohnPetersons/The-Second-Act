using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class SpecialUsedState : GameEventListenerState
{
    public SpecialUsedState(GameEventListenerId listenerId): base(listenerId) {

    }

    public override void Begin() {
        base.Begin();
        Debug.Log("Special used");
    }
}