using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Box_LaserConvert : Box
{
    
    public float resetTime = 0.1f;
    public GameObject Laser;
    private bool isFlip;
    private Coroutine ResetBool;

    protected override void Start()
    {
        base.Start();
        Laser.SetActive(false);
    }

    public override void Convert(bool set, Vector3 hitPosition)
    {
        if (set)
        {
            if (ResetBool != null)
            {
                StopCoroutine(ResetBool);
                ResetBool = null;
            }
            ResetBool = StartCoroutine(ResetBoolFunc());
            Laser.SetActive(true);
            
            
            Vector2 localCollisionPoint =  transform.GetChild(0).InverseTransformPoint(hitPosition);
            // Vector2 tmp_Vec2 = new Vector2(localCollisionPoint.x, localCollisionPoint.y);
        
            float xDiff = Mathf.Abs(localCollisionPoint.x);
            float yDiff = Mathf.Abs(localCollisionPoint.y);
            if (xDiff > yDiff)
            {
                // X坐标的差值更大，表示碰撞点更可能在左侧或右侧
                if (localCollisionPoint.x > 0)
                {
                    // 碰撞点在碰撞体的右侧
                    Laser.transform.localRotation = Quaternion.Euler(CheckIsFlip() ? new Vector3(0,0,-90): new Vector3(0,0,90) );
                }
                else
                {
                    // 碰撞点在碰撞体的左侧
                    Laser.transform.localRotation  = Quaternion.Euler(CheckIsFlip() ? new Vector3(0,0,90): new Vector3(0,0,-90) );
                }
            }
            else
            {
                // Y坐标的差值更大，表示碰撞点更可能在上侧或下侧
                if (localCollisionPoint.y > 0)
                {
                    // 碰撞点在碰撞体的上侧
                    Laser.transform.localRotation  = Quaternion.Euler(CheckIsFlip() ? new Vector3(0,0,180): new Vector3(0,0,0));
                }
                else
                {
                    // 碰撞点在碰撞体的下侧
                    Laser.transform.localRotation  = Quaternion.Euler(CheckIsFlip() ? new Vector3(0,0,0): new Vector3(0,0,180));
                }
            }
        }
        else
        {
            Laser.SetActive(false);
        }
    }

    public bool CheckIsFlip()
    {
        float z = transform.eulerAngles.z;
        if (z is <= 181f and > 135f)
        {
            return false;
        }
        if (z is <= 45f and > -45f)
        {
            return false;
        }
        if (z is >= -181f and < -135f)
        {
            return false;
        }
        return true;
    }

    private IEnumerator ResetBoolFunc()
    {
        yield return new WaitForSeconds(resetTime);
        Convert(false, Vector3.zero);
    }
}
