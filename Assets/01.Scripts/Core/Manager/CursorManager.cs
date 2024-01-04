using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoSingleton<CursorManager>
{
    [SerializeField]
    private Texture2D _baseCursorTexture = null;

	private Texture2D cursorTexture;

	[Header("Base")]
	[SerializeField] private float baseScale = 1;	
	[SerializeField] private Color baseColor = Color.red;	


	[Header("Animate")]
	[SerializeField] private float animTime = 0.25f;
	[SerializeField] private float TargetAngle = 720f;
	[SerializeField] private float TargetScale = 1;
	[SerializeField] private Color TargetColor = Color.blue;

	private float curScale;
	private float curAngle;
	private float baseAngle;

	public Vector2 pixelsHotSpot;

	Vector2 pos;
	Rect rect;

	private bool IsAttack = false;
	public void SetAttackState(bool value) => IsAttack = value;

	private bool CursorIsOnInteract = false;
	public void SetInteract(bool value) => CursorIsOnInteract = value;

	private bool IsAttackAnimating = false;

    private void Awake()
    {
		Init();    
    }

    public override void Init()
    {
		//Cursor.visible = false;
        SetCursorIcon(_baseCursorTexture);
	}

    private void SetCursorIcon(Texture2D texture)
    {
		//Texture2D copyTexture = new Texture2D(texture.width, texture.height);
		//copyTexture.SetPixels(texture.GetPixels());
		//copyTexture.Apply();

		//cursorTexture = copyTexture;

		//SetTextureColor(baseColor);

		pixelsHotSpot = Vector2.zero;
		//Cursor.SetCursor(_baseCursorTexture,
  //          new Vector2(_baseCursorTexture.width / 2f, _baseCursorTexture.height / 2f),
  //          CursorMode.Auto);	
		Cursor.SetCursor(_baseCursorTexture,
            new Vector2(pixelsHotSpot.x, pixelsHotSpot.y),
            CursorMode.ForceSoftware);

		baseScale = curScale = baseScale;
	}

	private void SetTextureColor(Color color)
    {
		//var fillColor = color;
		//var fillColorArray = cursorTexture.GetPixels();

		//for (var i = 0; i < fillColorArray.Length; ++i)
		//{
		//	fillColorArray[i] = fillColor;
		//}

		for (int y = 0; y < cursorTexture.height; y++)
		{
			for (int x = 0; x < cursorTexture.width; x++)
			{
				cursorTexture.SetPixel(x, y, color);
			}
		}

		cursorTexture.Apply();
	}

	void OnGUI()
	{
		//updateTexture();

  //      Matrix4x4 back = GUI.matrix;
  //      GUIUtility.RotateAroundPivot(curAngle, pos);
  //      GUI.DrawTexture(rect, _baseCursorTexture);
  //      GUI.matrix = back;
    }

	void updateTexture()
	{
		pos = Input.mousePosition;
		pos.y = Screen.height - pos.y;
		
		if (IsAttack)
		{
			IsAttackAnimating = true;

			DOTween.To(() => curAngle, angle => this.curAngle = angle, TargetAngle, animTime);
			DOTween.To(() => curScale, scale => this.curScale = scale, TargetScale, animTime);
			//DOTween.To(() => curScale, scale => this.curScale = scale, TargetScale, animTime);
		}
		else
        {

        }

		if (!IsAttack && CursorIsOnInteract)
		{
				
		}

		//float last_angle = angle;
		//	float new_angle = Mathf.Atan2(pos.y - prev_position.y, pos.x - prev_position.x) * Mathf.Rad2Deg;
		//	angle = Mathf.MoveTowardsAngle(
		//		last_angle,
		//		Mathf.LerpAngle(last_angle, new_angle, 1f - smoothness),
		//		10f
		//		);
		

		//prev_position = pos;

		rect = 
			new Rect(pos.x - pixelsHotSpot.x, pos.y - pixelsHotSpot.y, _baseCursorTexture.width * curScale, _baseCursorTexture.height * curScale);
	}
}