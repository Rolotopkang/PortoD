using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PortalGun_Bullet : MonoBehaviour
{
    public EnumTool.PortalGunBulletType PortalGunBulletType;
    public GameObject PortalPrefab;
    public TrailRenderer TrailRenderer;

    //子弹速度
    public float sp = 100;

    public float dis;

    public Ray ray;

    public RaycastHit hit;

    private Vector3 posRecord;
    private Vector2 normalVector2;

    private void FixedUpdate()
    {
        BulletMovement();
    }

    private void Update()
    {
        
    }

    public void BulletMovement()
    {
        //记录位置
        posRecord = transform.position;
        //子弹开始移动
        transform.position += transform.right * sp * Time.deltaTime;
        //计算当前位置与记录位置的距离
        dis = (posRecord - transform.position).magnitude;
        normalVector2 = (posRecord - transform.position).normalized;
        //大于0说明子弹移动了
        
        if (dis > 0)
        {
            // ContactFilter2D contact = new ContactFilter2D();
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y),
                transform.right, dis);
            //从记录的位置向子弹飞行的方向发出射线
            if(hit)
            {
                // Debug.Log("1");
                // Debug.Log(hit.transform.name);
                if (hit.transform .gameObject.tag.Equals("LightWall"))
                {
                    DestroySelf();
                }
                
                if (hit.transform.gameObject.GetComponent<TilemapCollider2D>())
                {
                    TilemapCollider2D tilemapCollider = hit.transform.gameObject.GetComponent<TilemapCollider2D>();
                    if (tilemapCollider != null)
                    {
                        if (hit.transform .gameObject.tag.Equals("Ground"))
                        {
                            DestroySelf();
                        }
                        
                        if (hit.transform .gameObject.tag.Equals("PortalAbleGround"))
                        {
                            Vector3 hitPoint = hit.point;
                            Tilemap tilemap = tilemapCollider.gameObject.GetComponent<Tilemap>();
                            bool isdirect = false;
                            Vector3Int old = Vector3Int.zero;
                            if (tilemap != null)
                            {
                                // 获取击中点的Tile坐标
                                Vector3Int cellPosition = tilemap.WorldToCell(new Vector3(hitPoint.x + normalVector2.x/5,hitPoint.y + normalVector2.y/5));
                                // 获取击中点的Tile信息
                                TileBase tileBase = tilemap.GetTile(cellPosition);
                                
                                if (tileBase == null)
                                {
                                    old = cellPosition;
                                    cellPosition = tilemap.WorldToCell(new Vector3(hitPoint.x - normalVector2.x/5,hitPoint.y - normalVector2.y/5));
                                    tileBase = tilemap.GetTile(cellPosition);
                                    isdirect = false;
                                }
                                else
                                {
                                    isdirect = true;
                                    old = tilemap.WorldToCell(new Vector3(hitPoint.x - normalVector2.x/5,hitPoint.y - normalVector2.y/5));
                                }
                                // Debug.Log(cellPosition);
                                if (tileBase != null)
                                {
                                    // Debug.Log("进入5" +cellPosition+old);
                                    Vector3Int  normalTileVector2 = cellPosition - old;
                                    if (Mathf.Abs(normalTileVector2.x) == (Mathf.Abs(normalTileVector2.y)))
                                    {
                                        Debug.Log("错误的传送门生成位置");
                                        DestroySelf();
                                        return;
                                    }
                                    GameObject tmp_portal = Instantiate(PortalPrefab, tilemap.GetCellCenterWorld(isdirect? cellPosition : old),GetTileDirection(normalTileVector2));
                                    tmp_portal.GetComponent<Portal>().Init(tilemap,isdirect?  old : cellPosition);
                                }
                            }
                            
                            DestroySelf();
                        }
                    }
                }
            }
        }
    }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.transform.gameObject.GetComponent<TilemapCollider2D>())
    //     {
    //         TilemapCollider2D tilemapCollider = collision.transform.gameObject.GetComponent<TilemapCollider2D>();
    //         // 获取碰撞点的世界坐标
    //         Vector3 hitPosition = collision.contacts[0].point;
    //         
    //         if (tilemapCollider != null)
    //         {
    //             if (collision.transform.gameObject.tag.Equals("Ground"))
    //             {
    //                 DestroySelf();
    //             }
    //
    //             if (collision.transform.gameObject.tag.Equals("PortalAbleGround"))
    //             {
    //                 foreach (ContactPoint2D contact in collision.contacts)
    //                 {
    //                     //绘制线
    //                     Debug.DrawLine ( contact.point, transform.position, Color.red , 10f);
    //                     Vector3 direction = transform.InverseTransformPoint (contact.point);
    //                     if(direction.x > 0f){ 
    //                         print( "右碰撞");
    //                     }
    //                     if(direction.x < 0f){
    //                         print("左碰撞");}
    //                     if(direction.y > 0f){
    //                         print ("上碰撞");
    //                     }
    //                     if(direction.y < 0f){
    //                         print ("下碰撞");
    //                     }
    //                 }
    //                 Tilemap tilemap = tilemapCollider.gameObject.GetComponent<Tilemap>();
    //                 // 将世界坐标转换为 Tilemap 的本地坐标
    //                 Vector3Int cellPosition = tilemap.WorldToCell(hitPosition);
    //                 // 获取碰撞信息中的法线向量
    //                 Vector2 collisionNormal = collision.contacts[0].normal;
    //
    //                 Debug.Log("击中点"+ hitPosition);
    //                 Debug.Log(collisionNormal+"/"+cellPosition);
    //
    //                 Instantiate(PortalPrefab, tilemap.GetCellCenterWorld(new Vector3Int(cellPosition.x -(int)normalVector2.x,cellPosition.y - (int)normalVector2.y)),
    //                     GetTileDirection(collisionNormal));
    //                 Debug.Log((int)normalVector2.x);
    //                 Debug.Log((int)normalVector2.y);
    //                 DestroySelf();
    //             }
    //         }
    //
    //     }
    // }

    // private Quaternion GetTileDirection(Vector2 vector2)
    // {
    //     vector2 = vector2.normalized;
    //     Debug.Log("二元数向量" + vector2);
    //     if (vector2.x == 1 && vector2.y == 0)
    //     {
    //         return Quaternion.Euler(0, 0, 180);
    //     }
    //
    //     if (vector2.x == -1 && vector2.y == 0)
    //     {
    //         return Quaternion.Euler(0, 0, 0);
    //     }
    //
    //     if (vector2.y == 1 && vector2.x == 0)
    //     {
    //         return Quaternion.Euler(0, 0, 90);
    //     }
    //
    //     if (vector2.y == -1 && vector2.x == 0)
    //     {
    //         return Quaternion.Euler(0, 0, -90);
    //     }
    //
    //     return Quaternion.identity;
    // }
    
    private Quaternion GetTileDirection(Vector3Int vector2)
    {
        if (vector2.x == 1 && vector2.y == 0)
        {
            return Quaternion.Euler(0, 0, 90);
        }

        if (vector2.x == -1 && vector2.y == 0)
        {
            return Quaternion.Euler(0, 0, -90);
        }

        if (vector2.y == 1 && vector2.x == 0)
        {
            return Quaternion.Euler(0, 0, 180);
        }

        if (vector2.y == -1 && vector2.x == 0)
        {
            return Quaternion.Euler(0, 0, 0);
        }

        return Quaternion.identity;
    }

    private void DestroySelf()
    {
        StartCoroutine(EndTrail());
    }

    private IEnumerator EndTrail()
    {
        GetComponent<Collider2D>().enabled = false;
        sp = 0;
        GetComponent<SpriteRenderer>().enabled = false;
        //特效
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}