using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class Box : MonoBehaviour
{
    public EnumTool.BoxColor BoxColor;

    public Sprite Blue;
    public Sprite Red;
    public Sprite Green;

    private DissolveController DissolveController;
    
    private void Start()
    {
        DissolveController = GetComponent<DissolveController>();
        ChangeColor(BoxColor);
    }

    public void ChangeColor(EnumTool.BoxColor _boxColor)
    {
        BoxColor = _boxColor;
        GetComponent<SpriteRenderer>().sprite = GetBoxSpriteByColor(BoxColor);
    }

    public void DestroySelf()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        DissolveController.Hide(() => { Destroy(gameObject);});
    }

    private Sprite GetBoxSpriteByColor(EnumTool.BoxColor boxColor)
    {
        switch (boxColor)
        {
            case EnumTool.BoxColor.Blue:
                return Blue;
            case EnumTool.BoxColor.Red:
                return Red;
            case EnumTool.BoxColor.Green:
                return Green;
        }

        Debug.Log("找不到颜色！");
        return Blue;
    }
}
