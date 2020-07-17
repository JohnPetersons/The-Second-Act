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

        PlayerSpriteState idleSprite1 = new PlayerSpriteState(this.listenerId, "PlayerIdle1");
        PlayerSpriteState idleSprite2 = new PlayerSpriteState(this.listenerId, "PlayerIdle2");
        PlayerSpriteState walkingSprite;
        if (dir > 0) {
            walkingSprite = new PlayerSpriteState(this.listenerId, "Player/LookingRight/Walking/walking1", 0.15, walkingSprite1ColliderRight, dir);
            walkingSprite.AddSequencedSprite("Player/LookingRight/Walking/walking3", 0.15);
        } else {
            walkingSprite = new PlayerSpriteState(this.listenerId, "Player/LookingLeft/Walking/walking1", 0.15, walkingSprite1ColliderLeft, dir);
            walkingSprite.AddSequencedSprite("Player/LookingLeft/Walking/walking3", 0.15);
        }
        PlayerSpriteState chargeSprite = new PlayerSpriteState(this.listenerId, "DefaultTempPlayerCharge");
        PlayerSpriteState winCollisionSprite = new PlayerSpriteState(this.listenerId, "DefaultTempPlayerWinCollision");
        PlayerSpriteState loseCollisionSprite = new PlayerSpriteState(this.listenerId, "DefaultTempPlayerLoseCollision");

        idleSprite1.SetTimedSpriteStateChange(idleSprite2, 0.75);
        idleSprite2.SetTimedSpriteStateChange(idleSprite1, 0.75);

        idleSprite1.AddStateChange("charge", chargeSprite);
        idleSprite2.AddStateChange("charge", chargeSprite);
        idleSprite1.AddStateChange("moveForward", walkingSprite);
        idleSprite2.AddStateChange("moveForward", walkingSprite);
        walkingSprite.AddStateChange("stop", idleSprite1);
        walkingSprite.AddStateChange("moveBackward", idleSprite1);
        walkingSprite.AddStateChange("charge", chargeSprite);
        walkingSprite.AddStateChange("collisionWin", winCollisionSprite);
        walkingSprite.AddStateChange("collisionLoss", loseCollisionSprite);
        idleSprite1.AddStateChange("collisionWin", winCollisionSprite);
        idleSprite2.AddStateChange("collisionWin", winCollisionSprite);
        idleSprite1.AddStateChange("collisionLoss", loseCollisionSprite);
        idleSprite2.AddStateChange("collisionLoss", loseCollisionSprite);
        chargeSprite.AddStateChange("stop", loseCollisionSprite);
        chargeSprite.AddStateChange("collisionWin", winCollisionSprite);
        chargeSprite.AddStateChange("collisionLoss", loseCollisionSprite);
        winCollisionSprite.AddStateChange("recover", idleSprite1);
        loseCollisionSprite.AddStateChange("recover", idleSprite1);
        
        this.AddCurrentState(idleSprite1);
    }
}
