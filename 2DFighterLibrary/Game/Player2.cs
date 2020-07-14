using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class Player2 : Player {
    
    public override void Begin() {
        base.Begin();
        this.SetPlayerNumber(2);
        this.SetDirection(Player.LEFT);
    }

    public override void Tick() {
        base.Tick();
    }
}
