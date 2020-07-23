﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class MovingBackwardState : GameEventListenerState
{
    private Player player;
    private int stop;
    public MovingBackwardState(GameEventListenerId listenerId): base(listenerId) {
        this.player = this.gameObject.GetComponent<Player>();
    }

    public override void Begin() {
        base.Begin();
        this.stop = 0;
    }
    
    public override void Tick() {
        base.Tick();
        float moveBackward = GameSystem.GetGameData<Settings>("Settings").GetSetting("moveBackward");
        this.gameObject.transform.Translate(new Vector3(moveBackward * (float)GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime) * this.player.GetDirection(), 0.0f, 0.0f));
    }

    public override GameState GetNextState(GameEvent gameEvent) {
        GameState result = base.GetNextState(gameEvent);
        if (gameEvent.GetName().Equals("moveStick") && gameEvent.GetGameData<double>() == 0) {
            if (this.stop > 1) {
                new TypedGameEvent<bool>(this.GetListenerId(), "stop", true, true);
            } else {
                this.stop++;
            }
        } else if (gameEvent.GetName().Equals("moveStick") && gameEvent.GetGameData<double>() != 0) {
            this.stop = 0;
        }
        return result;
    }
}
