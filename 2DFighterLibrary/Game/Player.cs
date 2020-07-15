using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;
/*
Player prefab component list:
- GameEventListenerId
- Player1 or Player2
- GameOnTriggerHandler
- RigidBody with kinematic setting
- Collider of some kind
- ChargeStatus
*/
public class Player : GameStateMachine {
    
    private GameInputState input;
    private int direction;

    public const int LEFT = -1;
    public const int RIGHT = 1;
    public override void Begin() {
        base.Begin();

        GameSystem.SetTimeMultiplier("gameplay", 1.0);

        IdleState Idle = new IdleState(this.listenerId);
        MovingForwardState MovingForward = new MovingForwardState(this.listenerId);
        MovingBackwardState MovingBackward = new MovingBackwardState(this.listenerId);
        ChargingState Charging = new ChargingState(this.listenerId);
        SpecialActivateState SpecialActivate = new SpecialActivateState(this.listenerId);
        SpecialChargingState SpecialCharging = new SpecialChargingState(this.listenerId);
        CollisionWinCondition CollisionWinCond = new CollisionWinCondition(this.listenerId); 
        CollisionLossCondition CollisionLossCond = new CollisionLossCondition(this.listenerId); 
        CollisionWinState CollisionWin = new CollisionWinState(this.listenerId);
        CollisionLossState CollisionLoss = new CollisionLossState(this.listenerId);

        GameState Ready = new GameState();
        GameState Playing = new GameState();
        GameState Paused = new GameState();
        VictoryState Victory = new VictoryState(this.listenerId);
        DefeatState Defeat = new DefeatState(this.listenerId);

        SpecialActivateCondition SpecialActivateCond = new SpecialActivateCondition(this.listenerId);

        Idle.AddStateChange("moveForward", MovingForward);
        Idle.AddStateChange("moveBackward", MovingBackward);
        Idle.AddStateChange("charge", Charging);
        Idle.AddStateChange("collisionWin", CollisionWin);
        Idle.AddStateChange("collisionLoss", CollisionLoss);
        Idle.AddStateChange("specialActivate", SpecialActivate);
        Idle.AddGameStateCondition(CollisionWinCond);
        Idle.AddGameStateCondition(CollisionLossCond);
        Idle.AddGameStateCondition(SpecialActivateCond);
        MovingForward.AddStateChange("stop", Idle);
        MovingForward.AddStateChange("moveBackward", MovingBackward);
        MovingForward.AddStateChange("charge", Charging);
        MovingForward.AddStateChange("collisionWin", CollisionWin);
        MovingForward.AddStateChange("collisionLoss", CollisionLoss);
        MovingForward.AddStateChange("specialActivate", SpecialActivate);
        MovingForward.AddGameStateCondition(CollisionWinCond);
        MovingForward.AddGameStateCondition(CollisionLossCond);
        MovingForward.AddGameStateCondition(SpecialActivateCond);
        MovingBackward.AddStateChange("stop", Idle);
        MovingBackward.AddStateChange("moveForward", MovingForward);
        MovingBackward.AddStateChange("charge", Charging);
        MovingBackward.AddStateChange("collisionWin", CollisionWin);
        MovingBackward.AddStateChange("collisionLoss", CollisionLoss);
        MovingBackward.AddStateChange("specialActivate", SpecialActivate);
        MovingBackward.AddGameStateCondition(CollisionWinCond);
        MovingBackward.AddGameStateCondition(CollisionLossCond);
        MovingBackward.AddGameStateCondition(SpecialActivateCond);
        Charging.AddStateChange("stop", Idle);
        Charging.AddStateChange("collisionWin", CollisionWin);
        Charging.AddStateChange("collisionLoss", CollisionLoss);
        Charging.AddGameStateCondition(CollisionWinCond);
        Charging.AddGameStateCondition(CollisionLossCond);
        SpecialActivate.AddStateChange("specialCharge", SpecialCharging);
        SpecialCharging.AddStateChange("stop", Idle);
        SpecialCharging.AddStateChange("collisionWin", CollisionWin);
        SpecialCharging.AddStateChange("collisionLoss", CollisionLoss);
        SpecialCharging.AddGameStateCondition(CollisionWinCond);
        SpecialCharging.AddGameStateCondition(CollisionLossCond);
        CollisionWin.AddStateChange("recover", Idle);
        CollisionLoss.AddStateChange("recover", Idle);

        Ready.AddStateChange("play", Playing);
        Playing.AddStateChange("pause", Paused);
        Paused.AddStateChange("play", Playing);
        Playing.AddStateChange("victory", Victory);
        Playing.AddStateChange("defeat", Defeat);

        this.input = new GameInputState(this.listenerId, 0);
        this.input.SetInputMapping(GameInputState.LEFT_STICK_LEFT_RIGHT, "moveStick");
        this.input.SetInputMapping(GameInputState.A, "chargeButton");
        this.input.SetInputMapping(GameInputState.B, "specialButton");

        this.AddCurrentState(this.input);
        this.AddCurrentState(Idle);
        this.AddCurrentState(Ready);
    }

    public override void Tick() {
        if (GameSystem.GetTimeMultiplier(GameSystem.GAMEPLAY) > 0) {
            base.Tick();
        }
    }

    public void SetPlayerNumber(int i) {
        this.input.SetPlayerNumber(i);
    }

    public void SetDirection(int i) {
        this.direction = i;
    }

    public int GetDirection() {
        return this.direction;
    }

    public override void HandleGameEvent(GameEvent gameEvent) {
        if (GameSystem.GetTimeMultiplier(GameSystem.GAMEPLAY) > 0) {
            base.HandleGameEvent(gameEvent);
            if (gameEvent.GetName().Equals("moveStick")) {
                double val = gameEvent.GetGameData<double>() * this.direction;
                if (val > 0) {
                    new TypedGameEvent<bool>(this.GetListenerId(), "moveForward", true);
                } else if (val < 0) {
                    new TypedGameEvent<bool>(this.GetListenerId(), "moveBackward", true);
                }
            } else if (gameEvent.GetName().Equals("chargeButton") && gameEvent.GetGameData<string>().Equals(GameInputState.KEY_DOWN)) {
                new TypedGameEvent<bool>(this.GetListenerId(), "charge", true);
            } else if (gameEvent.GetName().Equals("specialButton") && gameEvent.GetGameData<string>().Equals(GameInputState.KEY_DOWN)) {
                // commented out until I put in specials
                //new TypedGameEvent<bool>(this.GetListenerId(), "special", true);
            } else if (gameEvent.GetName().Equals("triggerEnter")) {
                new TypedGameEvent<GameObject>(this.GetListenerId(), "collision", gameEvent.GetGameData<GameObject>());
            } else {
                new TypedGameEvent<bool>(this.GetListenerId(), "stop", true);
            }
        }
    }
}
