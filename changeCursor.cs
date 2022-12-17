using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeCursor : MonoBehaviour
{
    [SerializeField] Texture2D cursorImage;
    void Start()
    {
        Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.ForceSoftware);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
