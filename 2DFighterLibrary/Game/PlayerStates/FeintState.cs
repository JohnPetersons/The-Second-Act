using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class FeintState : GameEventListenerState
{
    private Player player;
    private double timer1, timer2, timer;
    private bool canMove;
    public FeintState(GameEventListenerId listenerId): base(listenerId) {
        this.player = this.gameObject.GetComponent<Player>();
        this.timer1 = 1;
        this.timer2 = 0.9;
    }

    public override void Begin() {
        base.Begin();
        this.timer = this.timer1;
        this.canMove = false;
    }
    
    public override void Tick() {
        base.Tick();
        if (this.timer <= 0) {
            new TypedGameEvent<bool>(this.GetListenerId(), "stop", true);
        }
        this.timer -= GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime);
    }

    public override GameState GetNextState(GameEvent gameEvent) {
        GameState result = base.GetNextState(gameEvent);
        if (gameEvent.GetName().Equals("moveStick") && gameEvent.GetGameData<double>() == 0) {
            this.canMove = true;
        } else if (this.canMove && gameEvent.GetName().Equals("moveStick") && gameEvent.GetGameData<double>() != 0) {
            new TypedGameEvent<bool>(this.GetListenerId(), "stop", true);
        }
        return result;
    }
}
