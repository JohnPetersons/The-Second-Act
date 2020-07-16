using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class PlayerSpriteState : GamePolygonColliderSpriteState
{
    private CollisionDistanceFixer fixer;
    private double edge;

    public PlayerSpriteState(GameEventListenerId listenerId, string spriteName): base(listenerId, spriteName) {
        this.edge = 0.5;
        this.fixer = this.gameObject.GetComponent<CollisionDistanceFixer>();
    }

    public PlayerSpriteState(GameEventListenerId listenerId, string spriteName, Vector2[] colliderPoints, int direction): base(listenerId, spriteName, colliderPoints) {
        this.fixer = this.gameObject.GetComponent<CollisionDistanceFixer>();
        this.edge = 0;
        foreach(Vector2 point in colliderPoints) {
            if (Math.Sign(point.x) == Math.Sign(direction) && Math.Abs(point.x) > this.edge) {
                this.edge = Math.Abs(point.x);
            }
        }
    }

    public override void Begin() {
        base.Begin();
        if (this.fixer != null) {
            this.fixer.SetEdge(this.edge);
        }
    }
}
