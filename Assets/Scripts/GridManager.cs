using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    //First 6 tiles in the list should be walkable
    [SerializeField] private List<char> translate; 
    [SerializeField] private List<GameObject>lookup;
    [SerializeField] private string mapFile;
    private byte[][] grid;
    private GameObject[][] pellets;
    private Vector2Int size;
    private Vector2Int offset;
    
    private List<Vector2Int>bluePos;
    private List<Vector2Int>orangePos;
    
    void Awake()
    {
        bluePos = new List<Vector2Int>();
        orangePos = new List<Vector2Int>();
        size = new Vector2Int();
        List<string> rows = new List<string>();
        StreamReader sr = new StreamReader(Application.dataPath + "/" + mapFile);
        string line = sr.ReadLine();
        while(line != null)
        {
            rows.Add(line);
            if(line.Length > size.x)
            {
                size.x = line.Length;
            }
            line = sr.ReadLine();
        }
        
        rows.Reverse();
        
        size.y = rows.Count;
        
        offset = new Vector2Int(-size.x/2, -size.y/2);
        
        grid = new byte[size.y][];
        pellets = new GameObject[size.y][];
        
        for(int i = 0; i < size.y; ++i)
        {
            grid[i] = new byte[size.x];
            pellets[i] = new GameObject[size.x];
            for(int j = 0; j < rows[i].Length; ++j)
            {
                byte val = translateChar(rows[i][j]);
                grid[i][j] = val;
                
                Vector3 ip = new Vector3(j + offset.x, i + offset.y, 0);
                GameObject obj = (GameObject)Instantiate(lookup[val], ip, Quaternion.identity, transform);
                
                switch(val)
                {
                case 1:
                    pellets[i][j] = obj;
                    break;
                case 2:
                    pellets[i][j] = obj;
                    break;
                case 3:
                    bluePos.Add(new Vector2Int(j, i));
                    break;
                case 4:
                    orangePos.Add(new Vector2Int(j, i));
                    break;
                }
                
            }
            for(int j = rows[i].Length; j < size.x; ++j)
            {
                grid[i][j] = 0;
            }
        }
    }
    
    public bool walk(Vector2Int next)
    {
        Vector2Int index = next - offset;
        return get(index.x, index.y) == 1;
    }
    
    public bool eat(Vector2Int position)
    {
        Vector2Int index = position - offset;
        byte val =  getVal(index.x, index.y);
        
        if(val == 1 || val == 2)
        {
            set(index.x, index.y, 0);
            GameObject obj = pellets[index.y][index.x];
            pellets[index.y][index.x] = null;
            Destroy(obj);
        }
        
        return val == 1 || val == 2;
    }
    
    public Vector2Int teleport(Vector2Int position)
    {
        Vector2Int index = position - offset;
        byte val = getVal(index.x, index.y);
        
        if(val == 3)
        {
            for(int i = 0; i < bluePos.Count; ++i)
            {
                if(bluePos[i] == index)
                {
                    return orangePos[i] + offset;
                }
            }
        }
        else if(val == 4)
        {
            for(int i = 0; i < orangePos.Count; ++i)
            {
                if(orangePos[i] == index)
                {
                    return bluePos[i] + offset;
                }
            }
        }
        
        return position;
    }
    
    private byte get(int x, int y)
    {
        if(x >= 0 && x < size.x && y >= 0 && y < size.y)
        {
            return (byte)(grid[y][x] < 5 ? 1:0);
        }
        
        return 0;
    }
    
    private byte getVal(int x, int y)
    {
        if(x >= 0 && x < size.x && y >= 0 && y < size.y)
        {
            return grid[y][x];
        }
        return 0;
    }
    
    private void set(int x, int y, byte val)
    {
        if(x >= 0 && x < size.x && y >= 0 && y < size.y)
        {
             grid[y][x] = val;
        }
    }
    
    private byte translateChar(char value)
    {
        for(byte i = 0; i < translate.Count; ++i)
        {
            if(translate[i] == value)
            {
                return i;
            }
        }
        return 0;
    }
}
