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
- PolygonCollider2D
- ChargeStatus
- SpriteRenderer
- PlayerSprites
- CollisionDistanceFixer
- NetworkIdentity
- NetworkTransform
*/
public class Player : GameStateMachine {
    
    private GameInputState input;
    private int direction;

    public const int LEFT = -1;
    public const int RIGHT = 1;

    public const float SPEED = 1.0f;
    public override void Begin() {
        base.Begin();

        GameSystem.SetTimeMultiplier(GameSystem.GAMEPLAY, 1.0);

        IdleState idle = new IdleState(this.listenerId);
        MovingForwardState movingForward = new MovingForwardState(this.listenerId);
        MovingBackwardState movingBackward = new MovingBackwardState(this.listenerId);
        ChargingState charging = new ChargingState(this.listenerId);
        ChargeRecoveryState chargeRecovery = new ChargeRecoveryState(this.listenerId);
        SpecialActivateState specialActivate = new SpecialActivateState(this.listenerId);
        SpecialChargingState specialCharging = new SpecialChargingState(this.listenerId);
        CollisionWinCondition collisionWinCond = new CollisionWinCondition(this.listenerId); 
        CollisionLossCondition collisionLossCond = new CollisionLossCondition(this.listenerId); 
        CollisionWinState collisionWin = new CollisionWinState(this.listenerId);
        CollisionLossState collisionLoss = new CollisionLossState(this.listenerId);

        PlayerReadyState ready = new PlayerReadyState(this.listenerId);
        PlayerPlayingState playing = new PlayerPlayingState(this.listenerId);
        PlayerPausedState paused = new PlayerPausedState(this.listenerId);
        VictoryState victory = new VictoryState(this.listenerId);
        DefeatState defeat = new DefeatState(this.listenerId);

        SpecialActivateCondition specialActivateCond = new SpecialActivateCondition(this.listenerId);
        DefeatCondition defeatCond = new DefeatCondition(this.listenerId);

        idle.AddStateChange("moveForward", movingForward);
        idle.AddStateChange("moveBackward", movingBackward);
        idle.AddStateChange("charge", charging);
        idle.AddStateChange("collisionWin", collisionWin);
        idle.AddStateChange("collisionLoss", collisionLoss);
        idle.AddStateChange("specialActivate", specialActivate);
        idle.AddGameStateCondition(collisionWinCond);
        idle.AddGameStateCondition(collisionLossCond);
        idle.AddGameStateCondition(specialActivateCond);
        movingForward.AddStateChange("stop", idle);
        movingForward.AddStateChange("moveBackward", movingBackward);
        movingForward.AddStateChange("charge", charging);
        movingForward.AddStateChange("collisionWin", collisionWin);
        movingForward.AddStateChange("collisionLoss", collisionLoss);
        movingForward.AddStateChange("specialActivate", specialActivate);
        movingForward.AddGameStateCondition(collisionWinCond);
        movingForward.AddGameStateCondition(collisionLossCond);
        movingForward.AddGameStateCondition(specialActivateCond);
        movingBackward.AddStateChange("stop", idle);
        movingBackward.AddStateChange("moveForward", movingForward);
        movingBackward.AddStateChange("charge", charging);
        movingBackward.AddStateChange("collisionWin", collisionWin);
        movingBackward.AddStateChange("collisionLoss", collisionLoss);
        movingBackward.AddStateChange("specialActivate", specialActivate);
        movingBackward.AddGameStateCondition(collisionWinCond);
        movingBackward.AddGameStateCondition(collisionLossCond);
        movingBackward.AddGameStateCondition(specialActivateCond);
        charging.AddStateChange("stop", chargeRecovery);
        charging.AddStateChange("collisionWin", collisionWin);
        charging.AddStateChange("collisionLoss", collisionLoss);
        charging.AddGameStateCondition(collisionWinCond);
        charging.AddGameStateCondition(collisionLossCond);
        chargeRecovery.AddStateChange("recover", idle);
        chargeRecovery.AddStateChange("collisionWin", collisionWin);
        chargeRecovery.AddStateChange("collisionLoss", collisionLoss);
        chargeRecovery.AddGameStateCondition(collisionWinCond);
        chargeRecovery.AddGameStateCondition(collisionLossCond);
        specialActivate.AddStateChange("specialCharge", specialCharging);
        specialActivate.AddStateChange("collisionWin", collisionWin);
        specialActivate.AddStateChange("collisionLoss", collisionLoss);
        specialActivate.AddGameStateCondition(collisionWinCond);
        specialActivate.AddGameStateCondition(collisionLossCond);
        specialCharging.AddStateChange("stop", idle);
        specialCharging.AddStateChange("collisionWin", collisionWin);
        specialCharging.AddStateChange("collisionLoss", collisionLoss);
        specialCharging.AddGameStateCondition(collisionWinCond);
        specialCharging.AddGameStateCondition(collisionLossCond);
        collisionWin.AddStateChange("recover", idle);
        collisionLoss.AddStateChange("recover", idle);

        ready.AddStateChange("play", playing);
        playing.AddStateChange("pause", paused);
        playing.AddGameStateCondition(defeatCond);
        paused.AddStateChange("play", playing);
        playing.AddStateChange("victory", victory);
        playing.AddStateChange("defeat", defeat);

        this.input = new GameInputState(this.listenerId, 0);
        this.input.SetInputMapping(GameInputState.LEFT_STICK_LEFT_RIGHT, "moveStick");
        this.input.SetInputMapping(GameInputState.A, "chargeButton");
        this.input.SetInputMapping(GameInputState.B, "specialButton");
        this.input.SetInputMapping(GameInputState.START, "start");

        this.AddCurrentState(this.input);
        this.AddCurrentState(idle);
        this.AddCurrentState(ready);
    }

    public override void Tick() {
        base.Tick();
    }

    public void SetGamepadNumber(int i) {
        this.input.SetGamepadNumber(i);
    }

    public void SetDirection(int i) {
        this.direction = i;
    }

    public float GetDirection() {
        return this.direction * Player.SPEED;
    }
}
