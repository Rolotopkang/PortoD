using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Portal : MonoBehaviour, ConvertLaser
{
    public EnumTool.PortalGunBulletType PortalGunBulletType;
    public Sprite UnconnectedPortal;
    public Sprite ConnectedPortal;
    public float resetTime = 0.1f;
    

    private Coroutine ResetBool;
    private BoxCollider2D _boxCollider2D;
    private Tilemap _tilemap;
    private Vector3Int _baseTile;

    private void Start()
    {
        SetLaser(false);
    }

    public void Init(Tilemap tilemap, Vector3Int baseTile)
    {
        _baseTile = baseTile;
        _tilemap = tilemap;
        PortalSystem.GetInstance().PortalRegister(this);
    }

    public void SetLaser(bool set)
    {
        transform.GetChild(0).gameObject.SetActive(set);
    }

    public void Convert(bool set , Vector3 hitPos)
    {
        if (set)
        {
            if (ResetBool != null)
            {
                StopCoroutine(ResetBool);
                ResetBool = null;
            }
            ResetBool = StartCoroutine(ResetBoolFunc());
        }
        PortalSystem.GetInstance().OnLaserAttach( PortalGunBulletType, set);
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
    
    private IEnumerator ResetBoolFunc()
    {
        yield return new WaitForSeconds(resetTime);
        Convert(false, Vector3.zero);
    }
}
