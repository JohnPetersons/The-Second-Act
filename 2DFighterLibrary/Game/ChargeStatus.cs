using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class ChargeStatus : GameEventListener
{
    private int turnNumber;
    private int myTurnNumber;
    private bool active, setActive;
    public override void Begin() {
        base.Begin();
        this.myTurnNumber = 0;
        this.turnNumber = 0;
        this.active = false;
        this.setActive = false;
    }

    public override void Tick() {
        base.Tick();
        this.turnNumber++;
        if (this.active != this.setActive) {
            this.active = this.setActive;
        }
    }

    public void SetActive() {
        this.setActive = true;
        this.active = true;
        this.myTurnNumber = this.turnNumber;
    }

    public void SetInactive() {
        this.setActive = false;
    }

    public bool IsActive() {
        return this.active;
    }

    public int GetTurnNumber() {
        if (!this.IsActive()) {
            return this.turnNumber;
        }
        return this.myTurnNumber;
    }
}
