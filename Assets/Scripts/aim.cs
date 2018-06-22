using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aim : MonoBehaviour {


    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;


    void Start()
    {
        OnMouseEnter();
        
    }
    void OnMouseEnter() 
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        
    }

}
