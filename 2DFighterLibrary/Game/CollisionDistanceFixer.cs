using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

/*
Only for the player prefabs, fixes the collision distance between them so they stop when they would have collided.
No overlap
*/
public class CollisionDistanceFixer : GameEventListener
{
    private double previousX, previousX2, edge;
    private double timer, effectTimer;
    private GameLoader loader;
    public override void Begin() {
        base.Begin();
        this.previousX = this.previousX2 = this.gameObject.transform.position.x;
        this.edge = 0.5;
        this.timer = 0.025;
        this.effectTimer = 0.025;
        this.loader = new GameLoader();
    }

    public override void Tick() {
        base.Tick();
        previousX2 = previousX;
        previousX = this.gameObject.transform.position.x;
        if (this.timer <= 0) {
            //GameSystem.GetGameData<GameLoader>("GameLoader").Load("DustEffect," + this.transform.position.x + ",0,-3");
            this.timer = this.effectTimer;
        }
        this.timer -= Math.Abs(previousX - previousX2) * (Math.Abs(previousX - previousX2) / 2);
    }

    public double GetVelocity() {
        if (this.previousX == this.gameObject.transform.position.x) {
            return this.gameObject.transform.position.x - this.previousX2;
        } else {
            return this.gameObject.transform.position.x - this.previousX;
        }
    }

    public void SetEdge(double d) {
        this.edge = d;
    }

    public double GetEdge() {
        return this.edge;
    }

    public override void HandleGameEvent(GameEvent gameEvent) {
        base.Tick();
        if (gameEvent.GetName().Equals("collision")) {
            GameObject go = gameEvent.GetGameData<GameObject>();
            CollisionDistanceFixer fixer = go.GetComponent<CollisionDistanceFixer>();
            if (fixer != null) {
                double total = Math.Abs(fixer.GetVelocity()) + Math.Abs(this.GetVelocity());
                float distance = (float)(this.GetEdge() + fixer.GetEdge()) - Vector3.Distance(this.transform.position, go.transform.position);
                if (total != 0) {
                    new TypedGameEvent<double>(this.GetListenerId(), "fixCollision", -1 * distance * ((this.GetVelocity() / total)));
                }
            }
        } else if (gameEvent.GetName().Equals("fixCollision")) {
            this.transform.Translate(new Vector3((float)gameEvent.GetGameData<double>(), 0, 0));
        }
    }

    public override void OnDestroy() {
        base.OnDestroy();
        this.loader.RemoveLoaded();
    }
}
