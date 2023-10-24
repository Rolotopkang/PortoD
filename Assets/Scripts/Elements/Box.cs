using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class Box : MonoBehaviour, ConvertLaser
{
    public EnumTool.BoxColor BoxColor;

    public Sprite Blue;
    public Sprite Red;
    public Sprite Green;
    public Sprite LaserBlue;
    public Sprite LaserRed;
    public Sprite LaserGreen;
    
    public bool CanbePick = false;
    public bool bePicked;
    public BoxCollider2D collision;

    protected DissolveController DissolveController;


    protected void OnEnable()
    {
        if (BoxSystem.IsInitialized)
        {
            BoxSystem.GetInstance().Register(DestroySelf);
        }
    }
    
    

    protected virtual void Start()
    {
        DissolveController = GetComponent<DissolveController>();
        ChangeColor(BoxColor);
    }

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && CanbePick && !PlayerController.GetInstance().isHolding && !bePicked)
        {
            PlayerController.GetInstance().TryPickUp(this);
        }
        else if(PlayerController.GetInstance().isHolding && Input.GetKeyDown(KeyCode.E) && bePicked)
        {
            PlayerController.GetInstance().TryRlease();
        }
        if (bePicked)
        {
            transform.localPosition = Vector3.zero;
        }
    }

    public void ChangeColor(EnumTool.BoxColor _boxColor)
    {
        BoxColor = _boxColor;
        GetComponent<SpriteRenderer>().sprite = GetBoxSpriteByColor(BoxColor);
    }

    public virtual void Convert(bool set, Vector3 hitPosition)
    {
        
    }

    public void DestroySelf()
    {
        BoxSystem.GetInstance().UnRegister(DestroySelf);
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Rigidbody2D>().velocity *= 0.05f;
        DissolveController.Hide(() => { Destroy(gameObject);});
    }

    public void ShowHint(bool set)
    {
        transform.GetChild(0).GetChild(0).gameObject.SetActive(set);
        CanbePick = set;
    }

    public void PickUp(bool set)
    {
        collision.enabled = !set;
        collision.
        GetComponent<Rigidbody2D>().bodyType = set ? RigidbodyType2D.Static : RigidbodyType2D.Dynamic;
        ShowHint(!set);
        bePicked = set;
        gameObject.layer = set ? 0 : 9;
    }

    protected Sprite GetBoxSpriteByColor(EnumTool.BoxColor boxColor)
    {
        switch (boxColor)
        {
            case EnumTool.BoxColor.Blue:
                return Blue;
            case EnumTool.BoxColor.Red:
                return Red;
            case EnumTool.BoxColor.Green:
                return Green;
            case EnumTool.BoxColor.LaserBlue:
                return LaserBlue;
            case EnumTool.BoxColor.LaserRed:
                return LaserRed;
            case EnumTool.BoxColor.LaserGreen:
                return LaserGreen;
        }

        Debug.Log("找不到颜色！");
        return Blue;
    }
}
