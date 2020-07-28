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
    public override void Begin() {
        base.Begin();

        GameSystem.SetTimeMultiplier(GameSystem.GAMEPLAY, 1.0);

        IdleState idle = new IdleState(this.listenerId);
        MovingForwardState movingForward = new MovingForwardState(this.listenerId);
        MovingBackwardState movingBackward = new MovingBackwardState(this.listenerId);
        QuickStepForwardState quickStepForward = new QuickStepForwardState(this.listenerId);
        QuickStepBackwardState quickStepBackward = new QuickStepBackwardState(this.listenerId);
        DashChargingState dashCharging = new DashChargingState(this.listenerId);
        DashState dash = new DashState(this.listenerId);
        FeintState feint = new FeintState(this.listenerId);
        ChargingState charging = new ChargingState(this.listenerId);
        ChargeRecoveryState chargeRecovery = new ChargeRecoveryState(this.listenerId);
        DashRecoveryState dashRecovery = new DashRecoveryState(this.listenerId);
        //SpecialActivateState specialActivate = new SpecialActivateState(this.listenerId);
        //SpecialChargingState specialCharging = new SpecialChargingState(this.listenerId);
        CollisionWinCondition collisionWinCond = new CollisionWinCondition(this.listenerId); 
        CollisionLossCondition collisionLossCond = new CollisionLossCondition(this.listenerId); 
        CollisionWinState collisionWin = new CollisionWinState(this.listenerId);
        CollisionLossState collisionLoss = new CollisionLossState(this.listenerId);

        PlayerReadyState ready = new PlayerReadyState(this.listenerId);
        PlayerPlayingState playing = new PlayerPlayingState(this.listenerId);
        PlayerPausedState paused = new PlayerPausedState(this.listenerId);
        VictoryState victory = new VictoryState(this.listenerId);
        DefeatState defeat = new DefeatState(this.listenerId);

        //SpecialActivateCondition specialActivateCond = new SpecialActivateCondition(this.listenerId);
        DefeatCondition defeatCond = new DefeatCondition(this.listenerId);

        idle.AddStateChange("moveForward", movingForward);
        idle.AddStateChange("moveBackward", movingBackward);
        idle.AddStateChange("charge", charging);
        idle.AddStateChange("dashCharge", dashCharging);
        idle.AddStateChange("feint", feint);
        idle.AddStateChange("collisionWin", collisionWin);
        idle.AddStateChange("collisionLoss", collisionLoss);
        //idle.AddStateChange("specialActivate", specialActivate);
        idle.AddGameStateCondition(collisionWinCond);
        idle.AddGameStateCondition(collisionLossCond);
        //idle.AddGameStateCondition(specialActivateCond);
        movingForward.AddStateChange("stop", idle);
        movingForward.AddStateChange("quickStep", quickStepForward);
        movingForward.AddStateChange("moveBackward", movingBackward);
        movingForward.AddStateChange("charge", charging);
        movingForward.AddStateChange("dashCharge", dashCharging);
        movingForward.AddStateChange("feint", feint);
        movingForward.AddStateChange("collisionWin", collisionWin);
        movingForward.AddStateChange("collisionLoss", collisionLoss);
        //movingForward.AddStateChange("specialActivate", specialActivate);
        movingForward.AddGameStateCondition(collisionWinCond);
        movingForward.AddGameStateCondition(collisionLossCond);
        //movingForward.AddGameStateCondition(specialActivateCond);
        quickStepForward.AddStateChange("stop", idle);
        quickStepForward.AddStateChange("charge", charging);
        quickStepForward.AddStateChange("dashCharge", dashCharging);
        quickStepForward.AddStateChange("feint", feint);
        quickStepForward.AddStateChange("collisionWin", collisionWin);
        quickStepForward.AddStateChange("collisionLoss", collisionLoss);
        //quickStepForward.AddStateChange("specialActivate", specialActivate);
        quickStepForward.AddGameStateCondition(collisionWinCond);
        quickStepForward.AddGameStateCondition(collisionLossCond);
        //quickStepForward.AddGameStateCondition(specialActivateCond);
        movingBackward.AddStateChange("stop", idle);
        movingBackward.AddStateChange("quickStep", quickStepBackward);
        movingBackward.AddStateChange("moveForward", movingForward);
        movingBackward.AddStateChange("charge", charging);
        movingBackward.AddStateChange("dashCharge", dashCharging);
        movingBackward.AddStateChange("feint", feint);
        movingBackward.AddStateChange("collisionWin", collisionWin);
        movingBackward.AddStateChange("collisionLoss", collisionLoss);
        //movingBackward.AddStateChange("specialActivate", specialActivate);
        movingBackward.AddGameStateCondition(collisionWinCond);
        movingBackward.AddGameStateCondition(collisionLossCond);
        //movingBackward.AddGameStateCondition(specialActivateCond);
        quickStepBackward.AddStateChange("stop", idle);
        quickStepBackward.AddStateChange("charge", charging);
        quickStepBackward.AddStateChange("dashCharge", dashCharging);
        quickStepBackward.AddStateChange("feint", feint);
        quickStepBackward.AddStateChange("collisionWin", collisionWin);
        quickStepBackward.AddStateChange("collisionLoss", collisionLoss);
        //quickStepBackward.AddStateChange("specialActivate", specialActivate);
        quickStepBackward.AddGameStateCondition(collisionWinCond);
        quickStepBackward.AddGameStateCondition(collisionLossCond);
        //quickStepBackward.AddGameStateCondition(specialActivateCond);
        charging.AddStateChange("stop", chargeRecovery);
        charging.AddStateChange("collisionWin", collisionWin);
        charging.AddStateChange("collisionLoss", collisionLoss);
        charging.AddGameStateCondition(collisionWinCond);
        charging.AddGameStateCondition(collisionLossCond);
        dashCharging.AddStateChange("dash", dash);
        dashCharging.AddStateChange("collisionWin", collisionWin);
        dashCharging.AddStateChange("collisionLoss", collisionLoss);
        dashCharging.AddGameStateCondition(collisionWinCond);
        dashCharging.AddGameStateCondition(collisionLossCond);
        dash.AddStateChange("stop", dashRecovery);
        dash.AddStateChange("collisionWin", collisionWin);
        dash.AddStateChange("collisionLoss", collisionLoss);
        dash.AddGameStateCondition(collisionWinCond);
        dash.AddGameStateCondition(collisionLossCond);
        feint.AddStateChange("stop", idle);
        feint.AddStateChange("charge", charging);
        feint.AddStateChange("dashCharge", dashCharging);
        feint.AddStateChange("feint", feint);
        feint.AddStateChange("collisionWin", collisionWin);
        feint.AddStateChange("collisionLoss", collisionLoss);
        feint.AddGameStateCondition(collisionWinCond);
        feint.AddGameStateCondition(collisionLossCond);
        chargeRecovery.AddStateChange("recover", idle);
        chargeRecovery.AddStateChange("collisionWin", collisionWin);
        chargeRecovery.AddStateChange("collisionLoss", collisionLoss);
        chargeRecovery.AddGameStateCondition(collisionWinCond);
        chargeRecovery.AddGameStateCondition(collisionLossCond);
        dashRecovery.AddStateChange("recover", idle);
        dashRecovery.AddStateChange("collisionWin", collisionWin);
        dashRecovery.AddStateChange("collisionLoss", collisionLoss);
        dashRecovery.AddGameStateCondition(collisionWinCond);
        dashRecovery.AddGameStateCondition(collisionLossCond);
        /*specialActivate.AddStateChange("specialCharge", specialCharging);
        specialActivate.AddStateChange("collisionWin", collisionWin);
        specialActivate.AddStateChange("collisionLoss", collisionLoss);
        specialActivate.AddGameStateCondition(collisionWinCond);
        specialActivate.AddGameStateCondition(collisionLossCond);
        specialCharging.AddStateChange("stop", idle);
        specialCharging.AddStateChange("collisionWin", collisionWin);
        specialCharging.AddStateChange("collisionLoss", collisionLoss);
        specialCharging.AddGameStateCondition(collisionWinCond);
        specialCharging.AddGameStateCondition(collisionLossCond);*/
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
        this.input.SetInputMapping(GameInputState.D_PAD_LEFT_RIGHT, "moveStick");
        this.input.SetInputMapping(GameInputState.A, "chargeButton");
        this.input.SetInputMapping(GameInputState.B, "quickStepButton");
        this.input.SetInputMapping(GameInputState.X, "dashButton");
        this.input.SetInputMapping(GameInputState.Y, "feintButton");
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
        return this.direction * GameSystem.GetGameData<Settings>("Settings").GetSetting("gameSpeed");
    }
}
