using System;
using System.Collections;
using Tools;
using UnityEngine;

public class BoxGenerator : ReceiverMono
{
    public bool isAUTO = false;
    public SpriteRenderer BoxGeneratorSpriteRenderer;
    public SpriteRenderer root;
    public Transform GeneeratePoint;
    public float ResetTime = 0.5f;
    public GameObject BoxPrefab;
    public GameObject LaserBoxPrefab;
    [HideInInspector]
    public EnumTool.BoxColor BoxColor;
    public Sprite Blue;
    public Sprite Red;
    public Sprite Green;
    public Sprite LaserBlue;
    public Sprite LaserRed;
    public Sprite LaserGreen;
    

    
    private Box currentBox;
    private bool CoolingDown = false;
    private Coroutine ResetBoxGenerator;

    private void Start()
    {
        root.color = Color.red;
        ChangeColor();
    }

    private void Update()
    {
        if (isAUTO)
        {
            if (currentBox == null)
            {
                GameObject tmp_Boxprefab;
                if (BoxColor is EnumTool.BoxColor.LaserBlue or EnumTool.BoxColor.LaserRed or EnumTool.BoxColor.LaserGreen)
                {
                    tmp_Boxprefab =  LaserBoxPrefab;
                }
                else
                {
                    tmp_Boxprefab =  BoxPrefab;
                }
                currentBox = Instantiate(tmp_Boxprefab, GeneeratePoint.position, Quaternion.identity, transform)
                                .GetComponent<Box>();
                            currentBox.ChangeColor(BoxColor);
            }
        }
    }

    public void ChangeColor()
    {
        BoxGeneratorSpriteRenderer.sprite = GetBoxSpriteByColor(BoxColor);
    }

    public override void Receive(bool isTriggered ,TriggerMono triggerMono)
    {
        if (isAUTO)
        {
            if (currentBox && isTriggered)
            {
                currentBox.DestroySelf();
            }
            return;
        }
        
        if (!CoolingDown && isTriggered)
        {
            if (currentBox)
            {
                currentBox.DestroySelf();
            }

            GameObject tmp_Boxprefab;
            if (BoxColor is EnumTool.BoxColor.LaserBlue or EnumTool.BoxColor.LaserRed or EnumTool.BoxColor.LaserGreen)
            {
                 tmp_Boxprefab =  LaserBoxPrefab;
            }
            else
            {
                tmp_Boxprefab =  BoxPrefab;
            }
            
            
            currentBox = Instantiate(tmp_Boxprefab, GeneeratePoint.position, Quaternion.identity, transform)
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
    
    private IEnumerator StartResetGenerator()
    {
        CoolingDown = true;
        root.color = Color.green;
        yield return new WaitForSeconds(ResetTime);
        CoolingDown = false;
        root.color = Color.red;
    }
}
