using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

/*
Components:
- PlayerNetworkController
- NetworkIdentity
*/
public class PlayerNetworkController : GameStateMachine
{

    private static int playerNumber = 1;
    private int player;
    private NetworkAdapter adapter;
    
    public override void Begin() {
        base.Begin();
        this.player = PlayerNetworkController.playerNumber;
        this.adapter = this.gameObject.GetComponent<NetworkAdapter>();

        GameInputState input = new GameInputState(this.listenerId, 1);
        input.SetInputMapping(GameInputState.LEFT_STICK_LEFT_RIGHT, "moveStick");
        input.SetInputMapping(GameInputState.A, "chargeButton");
        input.SetInputMapping(GameInputState.B, "specialButton");
        input.SetInputMapping(GameInputState.START, "start");

        this.AddCurrentState(input);

        PlayerNetworkController.playerNumber++;
    }

    public override void Tick() {
        base.Tick();
    }

    private void DuplicateInputEvent(string tag, GameEvent gameEvent) {
        if (gameEvent.GetName().Equals("moveStick")) {
            new TypedGameEvent<double>(tag, gameEvent.GetName(), gameEvent.GetGameData<double>());
        } else {
            new TypedGameEvent<string>(tag, gameEvent.GetName(), gameEvent.GetGameData<string>());
        }
    }

    public override void HandleGameEvent(GameEvent gameEvent) {
        base.HandleGameEvent(gameEvent);
        if (this.adapter.IsLocal()) {
            if (this.player == 1) {
                this.DuplicateInputEvent(Player1.TAG, gameEvent);
            } else if (this.player == 2) {
                this.DuplicateInputEvent(Player2.TAG, gameEvent);
            }
        }
    }
}
