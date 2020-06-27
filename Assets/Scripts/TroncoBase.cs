using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroncoBase : MonoBehaviour
{
    public static TroncoBase instance;
    public SpriteRenderer spriteRenderer;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }
}
