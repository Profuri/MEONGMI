using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoSingleton<CursorManager>
{
    [SerializeField]
    private Texture2D _baseCursorTexture = null;
	//private Texture2D curCursorTexture;

	[SerializeField]
	private RectTransform transform_cursor;
	private Image _cursorImage;


	[Header("Base")]
	[SerializeField] private float baseScale = 1;	
	[SerializeField] private Color baseColor = Color.white;	


	[Header("Animate")]
	[SerializeField] private float animTime = 0.25f;
	[SerializeField] private float TargetAngle = 720f;
	[SerializeField] private float TargetScale = 1;
	[SerializeField] private Color AttackTargetColor = Color.red;
	[SerializeField] private Color InteractTargetColor = Color.blue;
	[SerializeField] private Ease animEase;

	private float curScale;
	private float curAngle;
	private float baseAngle;
	private Color curColor;

	//public Vector2 pixelsHotSpot;

	Vector2 pos;
	Rect rect;

	private bool IsAttack = false;
	public void SetAttackState(bool value) => IsAttack = value;

	private bool CursorIsOnInteract = false;
	public void SetInteract(bool value) => CursorIsOnInteract = value;

	private bool IsAttackAnimating = false;

    private void Awake()
    {
		Cursor.visible = true;
		//Cursor.lockState = CursorLockMode.Confined;
		Init();    
    }

    public override void Init()
    {
		SettingCursor();
		SetCursorIcon(_baseCursorTexture);
	}

    private void SetCursorIcon(Texture2D texture)
    {
		//      Texture2D copyTexture = new Texture2D(
		//	(int)((float)texture.width * baseScale),
		//	(int)((float)(texture.height * baseScale)));
		//      copyTexture.SetPixels(texture.GetPixels());
		//      copyTexture.Apply();

		//      curCursorTexture = copyTexture;

		//pixelsHotSpot = new Vector2(
		//	curCursorTexture.width * curScale / 2
		//	, curCursorTexture.height * curScale / 2
		//);
		// pixelsHotSpot = new Vector2(curCursorTexture.width / 2f, curCursorTexture.height / 2f);
		//Cursor.SetCursor(_baseCursorTexture,
		//    new Vector2(_baseCursorTexture.width / 2f, _baseCursorTexture.height / 2f),
		//    CursorMode.Auto);

		//     Cursor.SetCursor(curCursorTexture,
		//pixelsHotSpot,
		//         CursorMode.ForceSoftware);

		//     Cursor.SetCursor(curCursorTexture,
		//pixelsHotSpot,
		//         CursorMode.Auto);

		//SetScaleTexture(2f);
		//SetTextureColor(baseColor);
	}

	private void SettingCursor()
    {
		Cursor.visible = false;
		//transform_cursor.pivot = Vector2.up;

		if (transform_cursor.GetComponent<Graphic>())
			transform_cursor.GetComponent<Graphic>().raycastTarget = false;

		_cursorImage = transform_cursor.GetComponent<Image>();
		curScale = baseScale;
		curColor = baseColor;
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

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            OnBase();
        }

        UpdateCursorPosition();
    }

    private void UpdateCursorPosition()
    {
        Vector2 mousePos = Input.mousePosition;
        transform_cursor.position = mousePos;
    }

    public void OnBase()
	{
		_cursorImage.DOKill();
		transform_cursor.DOKill();
		_cursorImage.DOColor(baseColor, animTime).SetEase(animEase); ;
		transform_cursor.DOScale(baseScale, animTime).SetEase(animEase); ;
		//Vector3 rotateValue = new Vector3(0, 0, 0);
		//Quaternion quaternion = Quaternion.Euler(0, 0, 0);
		//transform_cursor.DORotateQuaternion(quaternion, animTime).SetEase(animEase);
		//DOTween.To(() => curColor, color =>
		//{
		//	this.curColor = color;
		//	SetTextureColor(this.curColor);
		//}, baseColor, animTime);
		DOTween.To(() => curAngle, angle =>
		{
			this.curAngle = angle;
			transform_cursor.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
		}, baseAngle, animTime).SetEase(animEase);
	}

	public void OnInteract()
    {
		_cursorImage.DOKill();
		_cursorImage.rectTransform.DOKill();
		_cursorImage.DOColor(InteractTargetColor, animTime).SetEase(animEase); ;
		_cursorImage.rectTransform.DOScale(baseScale, animTime).SetEase(animEase); ;
		//Vector3 rotateValue = new Vector3(0, 0, 0);
		//Quaternion quaternion = Quaternion.Euler(0, 0, 0);
		//transform_cursor.DORotateQuaternion(quaternion, animTime).SetEase(animEase);
		//DOTween.To(() => curColor, color =>
		//{
		//	this.curColor = color;
		//	SetTextureColor(this.curColor);
		//}, InteractTargetColor, animTime);
		DOTween.To(() => curAngle, angle =>
		{
			this.curAngle = angle;
			transform_cursor.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
		}, baseAngle, animTime).SetEase(animEase);
	}

	public void OnAttack()
    {
		_cursorImage.DOKill();
		_cursorImage.rectTransform.DOKill();
		_cursorImage.DOColor(AttackTargetColor,animTime).SetEase(animEase); ;
		_cursorImage.rectTransform.DOScale(TargetScale, animTime).SetEase(animEase); ;
		//Vector3 rotateValue = new Vector3(0, 0, TargetAngle);
		//Quaternion quaternion = Quaternion.Euler(0, 0, TargetAngle);
		//transform_cursor.DORotate(quaternion, animTime).SetEase(animEase);
        //DOTween.To(() => curColor, color =>
        //{
        //	this.curColor = color;
        //	SetTextureColor(this.curColor);
        //}, AttackTargetColor, animTime);

        DOTween.To(() => curAngle, angle =>
        {
            this.curAngle = angle;
			transform_cursor.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
		}, TargetAngle, animTime).SetEase(animEase);
    }

	public void Shoot()
	{

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

		//curCursorTexture.SetPixels(fillColorArray);
		//curCursorTexture.Apply();
	}
    void OnGUI()
	{
        //updateTexture();

        //Matrix4x4 back = GUI.matrix;
        //GUIUtility.RotateAroundPivot(curAngle, pos);
        //GUI.DrawTexture(rect, curCursorTexture);
        //GUI.matrix = back;
    }

	void updateTexture()
	{
		//pos = Input.mousePosition;
		//pos.y *= -1f;
		////pos.y = Screen.height - pos.y;

		////float last_angle = angle;
		////	float new_angle = Mathf.Atan2(pos.y - prev_position.y, pos.x - prev_position.x) * Mathf.Rad2Deg;
		////	angle = Mathf.MoveTowardsAngle(
		////		last_angle,
		////		Mathf.LerpAngle(last_angle, new_angle, 1f - smoothness),
		////		10f
		////		);
		

		////prev_position = pos;

		//pixelsHotSpot = new Vector2(
		//	curCursorTexture.width * curScale / 2
		//	,curCursorTexture.height * curScale / 2
		//);


		//rect = 
		//	new Rect(pos.x - pixelsHotSpot.x, pos.y - pixelsHotSpot.y, curCursorTexture.width * curScale, curCursorTexture.height * curScale);
	}

	public void SetScaleTexture(float _scaleFactor)
	{
		//int _newWidth = Mathf.RoundToInt(curCursorTexture.width * _scaleFactor);
		//int _newHeight = Mathf.RoundToInt(curCursorTexture.height * _scaleFactor);

		//Color[] _scaledTexPixels = new Color[_newWidth * _newHeight];

		//for (int _yCord = 0; _yCord < _newHeight; _yCord++)
		//{
		//	float _vCord = _yCord / (_newHeight * 1f);
		//	int _scanLineIndex = _yCord * _newWidth;

		//	for (int _xCord = 0; _xCord < _newWidth; _xCord++)
		//	{
		//		float _uCord = _xCord / (_newWidth * 1f);

		//		_scaledTexPixels[_scanLineIndex + _xCord] = curCursorTexture.GetPixelBilinear(_uCord, _vCord);
		//	}
		//}

		//// Create Scaled Texture
		//curCursorTexture.SetPixels(_scaledTexPixels, 0);
		//curCursorTexture.Apply();
	}
}
