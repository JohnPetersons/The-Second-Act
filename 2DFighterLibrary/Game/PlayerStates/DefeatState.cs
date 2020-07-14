using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class DefeatState : GameEventListenerState
{
    public DefeatState(GameEventListenerId listenerId): base(listenerId) {

    }

    public override void Begin() {
        base.Begin();
        Debug.Log("Defeat");
    }
}