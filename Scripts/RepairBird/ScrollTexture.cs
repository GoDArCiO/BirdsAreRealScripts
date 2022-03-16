using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTexture : MonoBehaviour
{
    public Vector2 scroll;
    public Renderer rendererToScroll;

    // Update is called once per frame
    void Update()
    {
        rendererToScroll.material.mainTextureOffset = scroll;
    }
}
