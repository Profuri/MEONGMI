using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoSingleton<CursorManager>
{
    [SerializeField]
    private Texture2D _cursorTexture = null;

    public override void Init()
    {
        SetCursorIcon(_cursorTexture);
    }

    private void SetCursorIcon(Texture2D texture)
    {
        Cursor.SetCursor(texture,
            new Vector2(texture.width / 2f, texture.height / 2f),
            CursorMode.Auto);
    }
}
