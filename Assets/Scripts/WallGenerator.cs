using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum WallTypes
{
	OuterUpperRight,
	OuterUpperLeft,
	OuterRight,
	OuterLeft,
	OuterLowerRight,
	OuterLowerLeft
}

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
					if (up && left && down && right)
					{
						//give up.
						walls[i, j].SetSprite(null);
					}
					if (up && left && down)
					{
						if (walls[i + 1, j - 1] == null)
							walls[i, j].SetSprite(wallSprites[37]);
						else
							walls[i, j].SetSprite(wallSprites[35]);
					}
					else if (up && right && down)
					{
						if (walls[i + 1, j + 1] == null)
							walls[i, j].SetSprite(wallSprites[36]);
						if (walls[i - 1, j + 1] == null)
							walls[i, j].SetSprite(wallSprites[34]);
					}
					else if (up && left && right)
					{

					}
				}
			}
		}
	}
}
