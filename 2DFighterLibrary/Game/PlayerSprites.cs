using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class PlayerSprites : GameSpriteStateMachine
{
    public override void Begin() {
        base.Begin();

        PlayerSpriteState idleSprite1 = new PlayerSpriteState(this.listenerId, "PlayerIdle1");
        PlayerSpriteState idleSprite2 = new PlayerSpriteState(this.listenerId, "PlayerIdle2");
        PlayerSpriteState chargeSprite = new PlayerSpriteState(this.listenerId, "DefaultTempPlayerCharge");
        PlayerSpriteState winCollisionSprite = new PlayerSpriteState(this.listenerId, "DefaultTempPlayerWinCollision");
        PlayerSpriteState loseCollisionSprite = new PlayerSpriteState(this.listenerId, "DefaultTempPlayerLoseCollision");

        idleSprite1.SetTimedSpriteStateChange(idleSprite2, 0.75);
        idleSprite2.SetTimedSpriteStateChange(idleSprite1, 0.75);

        idleSprite1.AddStateChange("charge", chargeSprite);
        idleSprite2.AddStateChange("charge", chargeSprite);
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
