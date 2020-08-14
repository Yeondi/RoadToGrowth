using UnityEngine;
using System.Collections;


public class Room : MonoBehaviour {
	public Rect pos;
	public bool mainRoom = false;
	public bool isHall = false;
	public bool disable = false;
	public bool Shop = false;

	public bool isHorizontal = false;
	public bool isVertical = false;

	//private static Texture2D _staticRectTexture;
	//private static GUIStyle _staticRectStyle;

	private bool onSignal;

	public void Update()
	{
		if (disable)
			return;

		transform.position = new Vector2 (pos.x, pos.y);
		if (isHall) {
			DebugX.DrawRect (pos, new Color(0.5f, 1.0f, 0.3f, 0.7f));
			return;
		}

		if (!mainRoom) {
			DebugX.DrawRect (pos, Color.blue);
		} else {
			DebugX.DrawRect (pos, Color.red);
		}
	}

	public float GetRight()
	{
		return pos.x + pos.width;
	}

	public float GetLeft()
	{
		return this.pos.x;
	}

	public float GetTop()
	{
		return this.pos.y + this.pos.height;
	}

	public float GetBottom()
	{
		return this.pos.y;
	}

	public Vector2 GetBottomCenter()
    {
		float x = this.pos.center.x;
		float y = this.GetBottom() + 2.0f;
		return new Vector2(x, y);
    }

}