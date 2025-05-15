using UnityEngine;
using System.Collections;

public class BG_Scroll : MonoBehaviour {

    public float scrollSpeed;
    private Vector2 originalOffset;

    void Start()
    {
        originalOffset = GetComponent<Renderer>().sharedMaterial.GetTextureOffset("_MainTex");
    }

    void Update()
    {
        float offsetY = Mathf.Repeat(Time.time * scrollSpeed, 1);
        Vector2 offset = new Vector2(originalOffset.x, offsetY);
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
    }

    void OnDisable()
    {
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", originalOffset);
    }
}
