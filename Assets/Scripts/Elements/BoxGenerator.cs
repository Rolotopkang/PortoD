using System;
using System.Collections;
using Tools;
using UnityEngine;

public class BoxGenerator : ReceiverMono
{
    public SpriteRenderer BoxGeneratorSpriteRenderer;
    public SpriteRenderer root;
    public Transform GeneeratePoint;
    public float ResetTime = 0.5f;
    public GameObject BoxPrefab;
    [HideInInspector]
    public EnumTool.BoxColor BoxColor;
    public Sprite Blue;
    public Sprite Red;
    public Sprite Green;

    
    private Box currentBox;
    private bool CoolingDown = false;
    private Coroutine ResetBoxGenerator;

    private void Start()
    {
        root.color = Color.red;
        ChangeColor();
    }

    public void ChangeColor()
    {
        BoxGeneratorSpriteRenderer.sprite = GetBoxSpriteByColor(BoxColor);
    }

    public override void Receive(bool isTriggered)
    {
        if (!CoolingDown && isTriggered)
        {
            if (currentBox)
            {
                currentBox.DestroySelf();
            }

            currentBox = Instantiate(BoxPrefab, GeneeratePoint.position, Quaternion.identity, transform)
                .GetComponent<Box>();
            currentBox.ChangeColor(BoxColor);

            if (ResetBoxGenerator != null)
            {
                StopCoroutine(ResetBoxGenerator);
                ResetBoxGenerator = null;
            }
            ResetBoxGenerator = StartCoroutine(StartResetGenerator());
        }
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
    
    private IEnumerator StartResetGenerator()
    {
        CoolingDown = true;
        root.color = Color.green;
        yield return new WaitForSeconds(ResetTime);
        CoolingDown = false;
        root.color = Color.red;
    }
}
