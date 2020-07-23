using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class QuickStepForwardState : GameEventListenerState
{
    private Player player;
    private double timer1, timer2, timer;
    private bool spriteChanged;
    public QuickStepForwardState(GameEventListenerId listenerId): base(listenerId) {
        this.player = this.gameObject.GetComponent<Player>();
        Settings settings = GameSystem.GetGameData<Settings>("Settings");
        this.timer1 = settings.GetSetting("quickStepForwardTimer1") + settings.GetSetting("quickStepForwardTimer2");
        this.timer2 = settings.GetSetting("quickStepForwardTimer2");
    }

    public override void Begin() {
        base.Begin();
        this.timer = this.timer1;
        this.spriteChanged = false;
    }
    
    public override void Tick() {
        base.Tick();
        if (this.timer > timer2) {
            float quickStepForward = GameSystem.GetGameData<Settings>("Settings").GetSetting("quickStepForward");
            this.gameObject.transform.Translate(new Vector3(quickStepForward * (float)GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime) * this.player.GetDirection(), 0.0f, 0.0f));
        } else if (this.timer <= timer2) {
            if (!this.spriteChanged) {
                new TypedGameEvent<bool>(this.GetListenerId() + GameSpriteStateMachine.SPRITE_LISTENER_SUFFIX, "stop", true);
                this.spriteChanged = true;
            }
            if (this.timer <= 0) {
                new TypedGameEvent<bool>(this.GetListenerId(), "stop", true);
            }
        }
        this.timer -= GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime);
    }
}
