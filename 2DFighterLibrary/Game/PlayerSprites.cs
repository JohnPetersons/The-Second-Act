using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class PlayerSprites : GameSpriteStateMachine
{
    public override void Begin() {
        base.Begin();

        GameSpriteState defaultSprite = new GameSpriteState(this.listenerId, "DefaultTempPlayer");
        GameSpriteState chargeSprite = new GameSpriteState(this.listenerId, "DefaultTempPlayerCharge");
        GameSpriteState winCollisionSprite = new GameSpriteState(this.listenerId, "DefaultTempPlayerWinCollision");
        GameSpriteState loseCollisionSprite = new GameSpriteState(this.listenerId, "DefaultTempPlayerLoseCollision");

        defaultSprite.AddStateChange("charge", chargeSprite);
        defaultSprite.AddStateChange("collisionWin", winCollisionSprite);
        defaultSprite.AddStateChange("collisionLoss", loseCollisionSprite);
        chargeSprite.AddStateChange("stop", loseCollisionSprite);
        chargeSprite.AddStateChange("collisionWin", winCollisionSprite);
        chargeSprite.AddStateChange("collisionLoss", loseCollisionSprite);
        winCollisionSprite.AddStateChange("recover", defaultSprite);
        loseCollisionSprite.AddStateChange("recover", defaultSprite);
        
        this.AddCurrentState(defaultSprite);
    }
}
