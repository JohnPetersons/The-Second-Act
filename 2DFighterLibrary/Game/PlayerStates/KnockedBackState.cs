using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class KnockedBackState : GameEventListenerState
{
    public KnockedBackState(GameEventListenerId listenerId): base(listenerId) {

    }

    public override void Begin() {
        base.Begin();
        Debug.Log("Knocked back");
    }
}