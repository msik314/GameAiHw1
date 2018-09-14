using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenerator : MonoBehaviour
{
	[SerializeField] private Sprite[] wallSprites;

	// Keeps tracks of which tiles are walls.
	Wall[,] walls;
	int rows, columns;

	public void SetUp(Vector2Int size)
	{
		walls = new Wall[size.y, size.x];
		rows = size.y;
		columns = size.x;
		for (int i = 0; i < size.y; ++i)
		{
			for (int j = 0; j < size.x; ++j)
				walls[i,j] = null;
		}
	}

	public void AddWall(int i, int j, Wall wall)
	{
		walls[i, j] = wall;
	}

	public void Build()
	{
		for (int i = 0; i < rows; ++i)
		{
			for (int j = 0; j < columns; ++j)
			{
				if (walls[i,j])
				{
					bool up = (i + 1 < rows && walls[i + 1, j] != null);
					bool left = (j - 1 >= 0 && walls[i, j - 1] != null);
					bool down = (i - 1 >= 0 && walls[i - 1, j] != null);
					bool right = (j + 1 < columns && walls[i, j + 1] != null);
					int wallFlags = ((up ? 1 : 0) << 3) + ((right ? 1 : 0) << 2) + ((down ? 1 : 0) << 1) + (left ? 1 : 0);
					walls[i, j].SetSprite(wallSprites[wallFlags]);
				}
			}
		}
	}
}
