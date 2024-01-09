using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoSingleton<CursorManager>
{
    [SerializeField]
    private Texture2D _baseCursorTexture = null;

	private Texture2D curCursorTexture;

	[Header("Base")]
	[SerializeField] private float baseScale = 1;	
	[SerializeField] private Color baseColor = Color.white;	


	[Header("Animate")]
	[SerializeField] private float animTime = 0.25f;
	[SerializeField] private float TargetAngle = 720f;
	[SerializeField] private float TargetScale = 1;
	[SerializeField] private Color AttackTargetColor = Color.red;
	[SerializeField] private Color InteractTargetColor = Color.blue;

	private float curScale;
	private float curAngle;
	private float baseAngle;
	private Color curColor;

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
		Cursor.visible = false;
		Init();    
    }

    public override void Init()
    {
		//Cursor.visible = false;
        SetCursorIcon(_baseCursorTexture);
	}

    private void SetCursorIcon(Texture2D texture)
    {
        Texture2D copyTexture = new Texture2D(
			(int)((float)texture.width * baseScale),
			(int)((float)(texture.height * baseScale)));
        copyTexture.SetPixels(texture.GetPixels());
        copyTexture.Apply();

        curCursorTexture = copyTexture;

		pixelsHotSpot = new Vector2(
			curCursorTexture.width * curScale / 2
			, curCursorTexture.height * curScale / 2
		);
		// pixelsHotSpot = new Vector2(curCursorTexture.width / 2f, curCursorTexture.height / 2f);
		//Cursor.SetCursor(_baseCursorTexture,
		//    new Vector2(_baseCursorTexture.width / 2f, _baseCursorTexture.height / 2f),
		//    CursorMode.Auto);

		//Cursor.SetCursor(curCursorTexture,
		//    new Vector2(pixelsHotSpot.x, pixelsHotSpot.y),
		//    CursorMode.ForceSoftware);

		Cursor.SetCursor(curCursorTexture,
			pixelsHotSpot,
            CursorMode.Auto);

        SetTextureColor(baseColor);
        curScale = baseScale;
		curColor = baseColor;
	}

	private void SetTextureColor(Color color)
    {
        var fillColor = color;
        var fillColorArray = _baseCursorTexture.GetPixels();

		Color initColor = Color.white;
		Color initalphaColor = Color.white;
		initalphaColor.a = 0;


        for (var i = 0; i < fillColorArray.Length; ++i)
        {
            if (fillColorArray[i].a == 0)
            {
                fillColorArray[i] = initalphaColor;
            }
            else
                fillColorArray[i] = initColor;

            fillColorArray[i] *= fillColor;
        }

		//      for (int y = 0; y < cursorTexture.height; y++)
		//{
		//	for (int x = 0; x < cursorTexture.width; x++)
		//	{
		//		cursorTexture.SetPixel(x, y, color);
		//	}
		//}
		curCursorTexture.SetPixels(fillColorArray);
		curCursorTexture.Apply();
	}

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
			OnAttack();
		}

		if (Input.GetKeyDown(KeyCode.LeftShift))
        {
			OnInteract();
		}

		if(Input.GetKeyDown(KeyCode.RightShift))
        {
			OnBase();
		}

	}

	public void OnBase()
	{
		DOTween.To(() => curColor, color =>
		{
			this.curColor = color;
			SetTextureColor(this.curColor);
		}, baseColor, animTime);
	}

	public void OnInteract()
    {
		DOTween.To(() => curColor, color =>
		{
			this.curColor = color;
			SetTextureColor(this.curColor);
		}, InteractTargetColor, animTime);
	}

	public void OnAttack()
    {
		DOTween.To(() => curColor, color =>
		{
			this.curColor = color;
			SetTextureColor(this.curColor);
		}, AttackTargetColor, animTime);
	}

	public void Shoot()
    {

    }

    void OnGUI()
	{
        updateTexture();

        Matrix4x4 back = GUI.matrix;
        GUIUtility.RotateAroundPivot(curAngle, pos);
        GUI.DrawTexture(rect, curCursorTexture);
        GUI.matrix = back;
    }

	void updateTexture()
	{
		pos = Input.mousePosition;
		pos.y *= -1f;
		//pos.y = Screen.height - pos.y;

		//float last_angle = angle;
		//	float new_angle = Mathf.Atan2(pos.y - prev_position.y, pos.x - prev_position.x) * Mathf.Rad2Deg;
		//	angle = Mathf.MoveTowardsAngle(
		//		last_angle,
		//		Mathf.LerpAngle(last_angle, new_angle, 1f - smoothness),
		//		10f
		//		);
		

		//prev_position = pos;

		pixelsHotSpot = new Vector2(
			curCursorTexture.width * curScale / 2
			,curCursorTexture.height * curScale / 2
		);

		rect = 
			new Rect(pos.x - pixelsHotSpot.x, pos.y - pixelsHotSpot.y, curCursorTexture.width * curScale, curCursorTexture.height * curScale);
	}
}
