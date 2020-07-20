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

        Vector2[] walkingSprite1ColliderRight = {new Vector2(-0.0625f, 0.5f),
            new Vector2(-0.0625f, -0.5f),
            new Vector2(0.5f, -0.5f),
            new Vector2(0.5f, 0.5f)};

        Vector2[] walkingSprite1ColliderLeft = {new Vector2(0.0625f, 0.5f),
            new Vector2(0.0625f, -0.5f),
            new Vector2(-0.5f, -0.5f),
            new Vector2(-0.5f, 0.5f)};

        PlayerSpriteState idleSprite;
        PlayerSpriteState walkingSprite;
        if (dir > 0) {
            idleSprite = new PlayerSpriteState(this.listenerId, "DefaultTempPlayer");
            walkingSprite = new PlayerSpriteState(this.listenerId, "DefaultTempPlayer");
           /* idleSprite = new PlayerSpriteState(this.listenerId, "Player/LookingRight/Idle/idle1", 0.15, walkingSprite1ColliderRight, dir);
            idleSprite.AddSequencedSprite("Player/LookingRight/Idle/idle2", 0.15);
            idleSprite.AddSequencedSprite("Player/LookingRight/Idle/idle3", 0.15);
            idleSprite.AddSequencedSprite("Player/LookingRight/Idle/idle4", 0.15);
            idleSprite.AddSequencedSprite("Player/LookingRight/Idle/idle5", 0.15);
            idleSprite.AddSequencedSprite("Player/LookingRight/Idle/idle6", 0.15);
            walkingSprite = new PlayerSpriteState(this.listenerId, "Player/LookingRight/Walking/walking1", 0.15, walkingSprite1ColliderRight, dir);
            walkingSprite.AddSequencedSprite("Player/LookingRight/Walking/walking3", 0.15);*/
        } else {
            idleSprite = new PlayerSpriteState(this.listenerId, "DefaultTempPlayer");
            walkingSprite = new PlayerSpriteState(this.listenerId, "DefaultTempPlayer");
            /*idleSprite = new PlayerSpriteState(this.listenerId, "Player/LookingLeft/Idle/idle1", 0.15, walkingSprite1ColliderLeft, dir);
            idleSprite.AddSequencedSprite("Player/LookingLeft/Idle/idle2", 0.15);
            idleSprite.AddSequencedSprite("Player/LookingLeft/Idle/idle3", 0.15);
            idleSprite.AddSequencedSprite("Player/LookingLeft/Idle/idle4", 0.15);
            idleSprite.AddSequencedSprite("Player/LookingLeft/Idle/idle5", 0.15);
            idleSprite.AddSequencedSprite("Player/LookingLeft/Idle/idle6", 0.15);
            walkingSprite = new PlayerSpriteState(this.listenerId, "Player/LookingLeft/Walking/walking1", 0.15, walkingSprite1ColliderLeft, dir);
            walkingSprite.AddSequencedSprite("Player/LookingLeft/Walking/walking3", 0.15);*/
        }
        PlayerSpriteState chargeSprite = new PlayerSpriteState(this.listenerId, "DefaultTempPlayerCharge");
        PlayerSpriteState dashChargingSprite  = new PlayerSpriteState(this.listenerId, "PlayerDashCharging");
        PlayerSpriteState feintSprite  = new PlayerSpriteState(this.listenerId, "PlayerDashCharging");
        PlayerSpriteState winCollisionSprite = new PlayerSpriteState(this.listenerId, "DefaultTempPlayerWinCollision");
        PlayerSpriteState loseCollisionSprite = new PlayerSpriteState(this.listenerId, "DefaultTempPlayerLoseCollision");

        idleSprite.AddStateChange("charge", chargeSprite);
        idleSprite.AddStateChange("dashCharge", dashChargingSprite);
        idleSprite.AddStateChange("feint", feintSprite);
        idleSprite.AddStateChange("moveForward", walkingSprite);
        walkingSprite.AddStateChange("stop", idleSprite);
        walkingSprite.AddStateChange("moveBackward", idleSprite);
        walkingSprite.AddStateChange("charge", chargeSprite);
        walkingSprite.AddStateChange("dashCharge", dashChargingSprite);
        walkingSprite.AddStateChange("feint", feintSprite);
        walkingSprite.AddStateChange("collisionWin", winCollisionSprite);
        walkingSprite.AddStateChange("collisionLoss", loseCollisionSprite);
        idleSprite.AddStateChange("collisionWin", winCollisionSprite);
        idleSprite.AddStateChange("collisionLoss", loseCollisionSprite);
        chargeSprite.AddStateChange("stop", loseCollisionSprite);
        chargeSprite.AddStateChange("collisionWin", winCollisionSprite);
        chargeSprite.AddStateChange("collisionLoss", loseCollisionSprite);
        dashChargingSprite.AddStateChange("dash", chargeSprite);
        dashChargingSprite.AddStateChange("collisionWin", winCollisionSprite);
        dashChargingSprite.AddStateChange("collisionLoss", loseCollisionSprite);
        feintSprite.AddStateChange("stop", idleSprite);
        feintSprite.AddStateChange("charge", chargeSprite);
        feintSprite.AddStateChange("dashCharge", dashChargingSprite);
        feintSprite.AddStateChange("collisionWin", winCollisionSprite);
        feintSprite.AddStateChange("collisionLoss", loseCollisionSprite);
        winCollisionSprite.AddStateChange("recover", idleSprite);
        loseCollisionSprite.AddStateChange("recover", idleSprite);
        
        this.AddCurrentState(idleSprite);
    }
}
