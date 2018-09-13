using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Wall : MonoBehaviour
{
	[HideInInspector] public bool isOuterWall = false;
	
	public void SetSprite(Sprite sprite)
	{
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		if (sprite == null)
			sr.enabled = false;
		else
			sr.sprite = sprite;
	}
}
