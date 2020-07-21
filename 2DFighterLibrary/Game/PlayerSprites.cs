using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class PlayerSprites : GameSpriteStateMachine
{
    public override void Begin() {
        base.Begin();

        Player player = this.gameObject.GetComponent<Player>();
        int dir = (int)player.GetDirection();

        Vector2[] walkingSprite1Collider = {new Vector2(-0.5f, 0.5f),
            new Vector2(-0.5f, -0.5f),
            new Vector2(0.5f, -0.5f),
            new Vector2(0.5f, 0.5f)};

        PlayerSpriteState idleSprite = new PlayerSpriteState(this.listenerId, "DefaultTempPlayer");
        PlayerSpriteState walkingSpriteForward = new PlayerSpriteState(this.listenerId, "DefaultTempPlayer");
        PlayerSpriteState walkingSpriteBackward = new PlayerSpriteState(this.listenerId, "DefaultTempPlayer");
        PlayerSpriteState chargeSprite;
        PlayerSpriteState dashSprite;
        PlayerSpriteState quickStepSpriteForward;
        PlayerSpriteState quickStepSpriteBackward;
        PlayerSpriteState quickStepSpritePause = new PlayerSpriteState(this.listenerId, "DefaultTempPlayer");
        if (dir > 0) {
            chargeSprite = new PlayerSpriteState(this.listenerId, "Player/LookingRight/Attacking/Attacking1", walkingSprite1Collider, dir);
            dashSprite = new PlayerSpriteState(this.listenerId, "Player/LookingRight/Attacking/Attacking2", walkingSprite1Collider, dir);
            quickStepSpriteForward = new PlayerSpriteState(this.listenerId, "Player/LookingRight/QuickStep/QuickStep1", walkingSprite1Collider, dir);
            quickStepSpriteBackward = new PlayerSpriteState(this.listenerId, "Player/LookingLeft/QuickStep/QuickStep1", walkingSprite1Collider, dir);
        } else {
            chargeSprite = new PlayerSpriteState(this.listenerId, "Player/LookingLeft/Attacking/Attacking1", walkingSprite1Collider, dir);
            dashSprite = new PlayerSpriteState(this.listenerId, "Player/LookingLeft/Attacking/Attacking2", walkingSprite1Collider, dir);
            quickStepSpriteForward = new PlayerSpriteState(this.listenerId, "Player/LookingLeft/QuickStep/QuickStep1", walkingSprite1Collider, dir);
            quickStepSpriteBackward = new PlayerSpriteState(this.listenerId, "Player/LookingRight/QuickStep/QuickStep1", walkingSprite1Collider, dir);
        }
        /*  
        The below is stuff from when I was working on basic animation
        if (dir > 0) {
            idleSprite = new PlayerSpriteState(this.listenerId, "Player/LookingRight/Idle/idle1", 0.15, walkingSprite1ColliderRight, dir);
            idleSprite.AddSequencedSprite("Player/LookingRight/Idle/idle2", 0.15);
            idleSprite.AddSequencedSprite("Player/LookingRight/Idle/idle3", 0.15);
            idleSprite.AddSequencedSprite("Player/LookingRight/Idle/idle4", 0.15);
            idleSprite.AddSequencedSprite("Player/LookingRight/Idle/idle5", 0.15);
            idleSprite.AddSequencedSprite("Player/LookingRight/Idle/idle6", 0.15);
            walkingSprite = new PlayerSpriteState(this.listenerId, "Player/LookingRight/Walking/walking1", 0.15, walkingSprite1ColliderRight, dir);
            walkingSprite.AddSequencedSprite("Player/LookingRight/Walking/walking3", 0.15);
        } else {
            idleSprite = new PlayerSpriteState(this.listenerId, "Player/LookingLeft/Idle/idle1", 0.15, walkingSprite1ColliderLeft, dir);
            idleSprite.AddSequencedSprite("Player/LookingLeft/Idle/idle2", 0.15);
            idleSprite.AddSequencedSprite("Player/LookingLeft/Idle/idle3", 0.15);
            idleSprite.AddSequencedSprite("Player/LookingLeft/Idle/idle4", 0.15);
            idleSprite.AddSequencedSprite("Player/LookingLeft/Idle/idle5", 0.15);
            idleSprite.AddSequencedSprite("Player/LookingLeft/Idle/idle6", 0.15);
            walkingSprite = new PlayerSpriteState(this.listenerId, "Player/LookingLeft/Walking/walking1", 0.15, walkingSprite1ColliderLeft, dir);
            walkingSprite.AddSequencedSprite("Player/LookingLeft/Walking/walking3", 0.15);
        }*/
        PlayerSpriteState dashChargingSprite  = new PlayerSpriteState(this.listenerId, "PlayerDashCharging");
        PlayerSpriteState feintSprite  = new PlayerSpriteState(this.listenerId, "PlayerDashCharging");
        PlayerSpriteState winCollisionSprite = new PlayerSpriteState(this.listenerId, "DefaultTempPlayerWinCollision");
        PlayerSpriteState loseCollisionSprite = new PlayerSpriteState(this.listenerId, "DefaultTempPlayerLoseCollision");

        idleSprite.AddStateChange("charge", chargeSprite);
        idleSprite.AddStateChange("dashCharge", dashChargingSprite);
        idleSprite.AddStateChange("feint", feintSprite);
        idleSprite.AddStateChange("moveForward", walkingSpriteForward);
        idleSprite.AddStateChange("moveBackward", walkingSpriteBackward);
        walkingSpriteForward.AddStateChange("stop", idleSprite);
        walkingSpriteForward.AddStateChange("quickStep", quickStepSpriteForward);
        walkingSpriteForward.AddStateChange("moveBackward", idleSprite);
        walkingSpriteForward.AddStateChange("charge", chargeSprite);
        walkingSpriteForward.AddStateChange("dashCharge", dashChargingSprite);
        walkingSpriteForward.AddStateChange("feint", feintSprite);
        walkingSpriteForward.AddStateChange("collisionWin", winCollisionSprite);
        walkingSpriteForward.AddStateChange("collisionLoss", loseCollisionSprite);
        walkingSpriteBackward.AddStateChange("stop", idleSprite);
        walkingSpriteBackward.AddStateChange("quickStep", quickStepSpriteBackward);
        walkingSpriteBackward.AddStateChange("moveForward", idleSprite);
        walkingSpriteBackward.AddStateChange("charge", chargeSprite);
        walkingSpriteBackward.AddStateChange("dashCharge", dashChargingSprite);
        walkingSpriteBackward.AddStateChange("feint", feintSprite);
        walkingSpriteBackward.AddStateChange("collisionWin", winCollisionSprite);
        walkingSpriteBackward.AddStateChange("collisionLoss", loseCollisionSprite);

        
        quickStepSpriteForward.AddStateChange("stop", quickStepSpritePause);
        quickStepSpriteForward.AddStateChange("charge", chargeSprite);
        quickStepSpriteForward.AddStateChange("dashCharge", dashChargingSprite);
        quickStepSpriteForward.AddStateChange("feint", feintSprite);
        quickStepSpriteForward.AddStateChange("collisionWin", winCollisionSprite);
        quickStepSpriteForward.AddStateChange("collisionLoss", loseCollisionSprite);

        
        quickStepSpriteBackward.AddStateChange("stop", quickStepSpritePause);
        quickStepSpriteBackward.AddStateChange("charge", chargeSprite);
        quickStepSpriteBackward.AddStateChange("dashCharge", dashChargingSprite);
        quickStepSpriteBackward.AddStateChange("feint", feintSprite);
        quickStepSpriteBackward.AddStateChange("collisionWin", winCollisionSprite);
        quickStepSpriteBackward.AddStateChange("collisionLoss", loseCollisionSprite);

        quickStepSpritePause.AddStateChange("stop", idleSprite);
        quickStepSpritePause.AddStateChange("charge", chargeSprite);
        quickStepSpritePause.AddStateChange("dashCharge", dashChargingSprite);
        quickStepSpritePause.AddStateChange("feint", feintSprite);
        quickStepSpritePause.AddStateChange("collisionWin", winCollisionSprite);
        quickStepSpritePause.AddStateChange("collisionLoss", loseCollisionSprite);

        idleSprite.AddStateChange("collisionWin", winCollisionSprite);
        idleSprite.AddStateChange("collisionLoss", loseCollisionSprite);
        chargeSprite.AddStateChange("stop", loseCollisionSprite);
        chargeSprite.AddStateChange("collisionWin", winCollisionSprite);
        chargeSprite.AddStateChange("collisionLoss", loseCollisionSprite);
        dashChargingSprite.AddStateChange("dash", dashSprite);
        dashChargingSprite.AddStateChange("collisionWin", winCollisionSprite);
        dashChargingSprite.AddStateChange("collisionLoss", loseCollisionSprite);
        dashSprite.AddStateChange("stop", loseCollisionSprite);
        dashSprite.AddStateChange("collisionWin", winCollisionSprite);
        dashSprite.AddStateChange("collisionLoss", loseCollisionSprite);
        feintSprite.AddStateChange("stop", idleSprite);
        feintSprite.AddStateChange("charge", chargeSprite);
        feintSprite.AddStateChange("dashCharge", dashChargingSprite);
        feintSprite.AddStateChange("collisionWin", winCollisionSprite);
        feintSprite.AddStateChange("collisionLoss", loseCollisionSprite);
        winCollisionSprite.AddStateChange("recover", idleSprite);
        winCollisionSprite.AddStateChange("collisionLoss", loseCollisionSprite);
        loseCollisionSprite.AddStateChange("recover", idleSprite);
        loseCollisionSprite.AddStateChange("collisionWin", winCollisionSprite);
        
        this.AddCurrentState(idleSprite);
    }
}
