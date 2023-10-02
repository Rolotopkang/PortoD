using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Portal : MonoBehaviour
{
    public EnumTool.PortalGunBulletType PortalGunBulletType;
    public Sprite UnconnectedPortal;
    public Sprite ConnectedPortal;


    private BoxCollider2D _boxCollider2D;
    private Tilemap _tilemap;
    private Vector3Int _baseTile;

    public void Init(Tilemap tilemap, Vector3Int baseTile)
    {
        _baseTile = baseTile;
        _tilemap = tilemap;
        PortalSystem.GetInstance().PortalRegister(this);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag.Equals("Player") || other.gameObject.tag.Equals("Box")) && PortalSystem.GetInstance().isConnectionBuilt())
        {
            PortalSystem.GetInstance().TP(other.gameObject,PortalGunBulletType);
        }
    }

    public void RefreshSprite()
    {
        GetComponent<SpriteRenderer>().sprite = PortalSystem.GetInstance().isConnectionBuilt()? ConnectedPortal : UnconnectedPortal;
        _tilemap.SetColliderType(_baseTile, PortalSystem.GetInstance().isConnectionBuilt()? Tile.ColliderType.None : Tile.ColliderType.Grid);
    }
    public void DestroySelf()
    {
        _tilemap.SetColliderType(_baseTile, Tile.ColliderType.Grid);
        Destroy(gameObject);
    }
}
