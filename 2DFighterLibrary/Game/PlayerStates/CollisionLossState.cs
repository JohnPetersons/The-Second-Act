﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class CollisionLossState : GameEventListenerState
{
    public CollisionLossState(GameEventListenerId listenerId): base(listenerId) {

    }

    public override void Begin() {
        base.Begin();
        Debug.Log("Collision loss");
    }
}