using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightPlus;
using System;

[Serializable]
public class PartType
{
    public HighlightEffect repairedHighlightEffect;
    public HighlightEffect damagedHighlightEffect;
    public bool needRepair;

    public void DamagedView(bool x)
    {
        if (x)
        {
            repairedHighlightEffect.gameObject.SetActive(false);
            damagedHighlightEffect.gameObject.SetActive(true);
        }
        else
        {
            damagedHighlightEffect.highlighted = false;
            damagedHighlightEffect.gameObject.SetActive(false);
            repairedHighlightEffect.gameObject.SetActive(true);
        }

    }
}

public enum PartTypeEnum { WINGS, CAMERA, BATTERY };
