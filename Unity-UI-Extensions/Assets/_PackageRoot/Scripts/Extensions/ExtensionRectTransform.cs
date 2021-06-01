using UnityEngine;

public static class ExtensionRectTransform
{
	public static float		Width						(this RectTransform rt)	=> rt.rect.width;
	public static float		Height						(this RectTransform rt)	=> rt.rect.height;

	public static void		SetWidth					(this RectTransform rt, float x)	=> rt.sizeDelta = rt.sizeDelta.SetX(x);
	public static void		SetHeight					(this RectTransform rt, float y)	=> rt.sizeDelta = rt.sizeDelta.SetY(y);
	
	//public static Vector2	Left						(this RectTransform rt) => new Vector3(rt.offsetMin.x,						rt.offsetMin.y + rt.sizeDelta.y / 2);
	//public static Vector2	Right						(this RectTransform rt) => new Vector3(rt.offsetMax.x,						rt.offsetMin.y + rt.sizeDelta.y / 2);
	//public static Vector2	Bottom						(this RectTransform rt) => new Vector3(rt.offsetMin.x + rt.sizeDelta.x / 2,	rt.offsetMin.y);
	//public static Vector2	Top							(this RectTransform rt) => new Vector3(rt.offsetMin.x + rt.sizeDelta.x / 2,	rt.offsetMax.y);
	
	public static Vector2	WorldRight					(this RectTransform rt) => rt.position.ToVector2XY() + rt.LocalRight();
	public static Vector2	WorldBottom					(this RectTransform rt) => rt.position.ToVector2XY() + rt.LocalBottom();
	public static Vector2	WorldLeft					(this RectTransform rt) => rt.position.ToVector2XY() + rt.LocalLeft();
	public static Vector2	WorldTop					(this RectTransform rt) => rt.position.ToVector2XY() + rt.LocalTop();

	public static Vector2	Right						(this RectTransform rt) => rt.localPosition.ToVector2XY() + rt.LocalRight();
	public static Vector2	Bottom						(this RectTransform rt) => rt.localPosition.ToVector2XY() + rt.LocalBottom();
	public static Vector2	Left						(this RectTransform rt) => rt.localPosition.ToVector2XY() + rt.LocalLeft();
	public static Vector2	Top							(this RectTransform rt) => rt.localPosition.ToVector2XY() + rt.LocalTop();

	// return value in local space of the object		
	public static Vector2	LocalRight					(this RectTransform rt) => new Vector2(rt.rect.xMax,						rt.rect.yMin + rt.rect.height / 2);
	public static Vector2	LocalBottom					(this RectTransform rt) => new Vector2(rt.rect.xMin + rt.rect.width / 2,	rt.rect.yMin);
	public static Vector2	LocalLeft					(this RectTransform rt) => new Vector2(rt.rect.xMin,						rt.rect.yMin + rt.rect.height / 2);
	public static Vector2	LocalTop					(this RectTransform rt) => new Vector2(rt.rect.xMin + rt.rect.width / 2,	rt.rect.yMax);

	public static float		LocalRightX					(this RectTransform rt) => rt.offsetMax.x;
	public static float		LocalBottomY				(this RectTransform rt) => rt.offsetMin.y;
	public static float		LocalLeftX					(this RectTransform rt) => rt.offsetMin.x;
	public static float		LocalTopY					(this RectTransform rt) => rt.offsetMax.y;

	
	//public static void		SetRight					(this RectTransform rt, Vector2 newPos, bool move = true) { if (move) rt.position += (rt.Right()	- newPos).ToVector3(); /* TODO: implement move==false (resize) */ }
	//public static void		SetBottom					(this RectTransform rt, Vector2 newPos, bool move = true) { if (move) rt.position += (rt.Bottom()	- newPos).ToVector3(); /* TODO: implement move==false (resize) */ }
	//public static void		SetLeft						(this RectTransform rt, Vector2 newPos, bool move = true) { if (move) rt.position += (rt.Left()		- newPos).ToVector3(); /* TODO: implement move==false (resize) */ }
	//public static void		SetTop						(this RectTransform rt, Vector2 newPos, bool move = true) { if (move) rt.position += (rt.Top()		- newPos).ToVector3(); /* TODO: implement move==false (resize) */ }
	
	public static void		SetRight					(this RectTransform rt, Vector2 newPos) { rt.localPosition -= (rt.Right()	- newPos).ToVector3(); }
	public static void		SetBottom					(this RectTransform rt, Vector2 newPos) { rt.localPosition -= (rt.Bottom()	- newPos).ToVector3(); }
	public static void		SetLeft						(this RectTransform rt, Vector2 newPos) { rt.localPosition -= (rt.Left()	- newPos).ToVector3(); }
	public static void		SetTop						(this RectTransform rt, Vector2 newPos) { rt.localPosition -= (rt.Top()		- newPos).ToVector3(); }

	public static void		SetLocalRight				(this RectTransform rt, float x)		{ rt.offsetMax = new Vector2(x, rt.offsetMax.y); }
	public static void		SetLocalBottom				(this RectTransform rt, float y)		{ rt.offsetMin = new Vector2(rt.offsetMin.x, y); }
	public static void		SetLocalLeft				(this RectTransform rt, float x)		{ rt.offsetMin = new Vector2(x, rt.offsetMin.y); }
	public static void		SetLocalTop					(this RectTransform rt, float y)		{ rt.offsetMax = new Vector2(rt.offsetMax.x, y); }

	public static void		SetWorldRight				(this RectTransform rt, Vector2 newPos) { rt.position -= (rt.Right()	- newPos).ToVector3(); }
	public static void		SetWorldBottom				(this RectTransform rt, Vector2 newPos) { rt.position -= (rt.Bottom()	- newPos).ToVector3(); }
	public static void		SetWorldLeft				(this RectTransform rt, Vector2 newPos) { rt.position -= (rt.Left()		- newPos).ToVector3(); }
	public static void		SetWorldTop					(this RectTransform rt, Vector2 newPos) { rt.position -= (rt.Top()		- newPos).ToVector3(); }


	public static void		SetAspectRatioFromHeight	(this RectTransform rt, float aspectRatio)					=> rt.SetWidth(rt.Height() * aspectRatio);
	public static void		SetAspectRatioFromWidth		(this RectTransform rt, float aspectRatio)					=> rt.SetHeight(rt.Width() / aspectRatio);
}
