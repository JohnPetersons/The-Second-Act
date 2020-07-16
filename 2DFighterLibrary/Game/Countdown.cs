using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class Countdown : GameSpriteStateMachine
{

    public const string TAG = "countdown";
    public override void Begin() {
        base.Begin();
        this.listenerId.SetListenerId(Countdown.TAG);

        GameSpriteState defaultSprite = new GameSpriteState(this.listenerId, "DefaultTempPlayer");
        GameSpriteState sprite3 = new GameSpriteState(this.listenerId, "Countdown3");
        GameSpriteState sprite2 = new GameSpriteState(this.listenerId, "Countdown2");
        GameSpriteState sprite1 = new GameSpriteState(this.listenerId, "Countdown1");
        GameSpriteState spriteGO = new GameSpriteState(this.listenerId, "CountdownGO");

        defaultSprite.AddStateChange("3", sprite3);
        sprite3.AddStateChange("2", sprite2);
        sprite2.AddStateChange("1", sprite1);
        sprite1.AddStateChange("GO", spriteGO);
        
        this.AddCurrentState(defaultSprite);
    }
}
