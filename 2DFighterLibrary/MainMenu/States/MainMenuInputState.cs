using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class MainMenuInputState : GameEventListenerState
{
    private double focusChangeTimerDefault, focusChangeTimer;
    public MainMenuInputState(GameEventListenerId listenerId): base(listenerId) {
        this.focusChangeTimer = this.focusChangeTimerDefault = 0.2;
    }

    public override void Begin() {
        base.Begin();
    }

    public override GameState GetNextState(GameEvent gameEvent) {
        GameState result = base.GetNextState(gameEvent);
        if (gameEvent.GetName().Equals("select") && gameEvent.GetGameData<string>().Equals(GameInputState.KEY_DOWN)) {
            new TypedGameEvent<bool>(this.GetListenerId(), "selected", true);
        } else if (this.focusChangeTimer <= 0) {
            if (gameEvent.GetName().Equals("leftStick")) {
                if (gameEvent.GetGameData<double>() == -1) {
                    this.focusChangeTimer = this.focusChangeTimerDefault;
                    new TypedGameEvent<bool>(this.GetListenerId(), "up", true);
                } else if (gameEvent.GetGameData<double>() == 1) {
                    this.focusChangeTimer = this.focusChangeTimerDefault;
                    new TypedGameEvent<bool>(this.GetListenerId(), "down", true);
                }
            }
        } else {
            this.focusChangeTimer -= GameSystem.GetDeltaTime(MainMenu.TAG, Time.deltaTime);
        }
        return result;
    }
}
