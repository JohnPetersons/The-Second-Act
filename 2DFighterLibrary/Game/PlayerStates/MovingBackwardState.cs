﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class MovingBackwardState : GameEventListenerState
{
    private Player player;
    public MovingBackwardState(GameEventListenerId listenerId): base(listenerId) {
        this.player = this.gameObject.GetComponent<Player>();
    }

    public override void Begin() {
        base.Begin();
        Debug.Log("Moving Forward");
    }
    
    public override void Tick() {
        base.Tick();
        this.gameObject.transform.Translate(new Vector3(-5.0f * (float)GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime) * this.player.GetDirection(), 0.0f, 0.0f));
    }
}
