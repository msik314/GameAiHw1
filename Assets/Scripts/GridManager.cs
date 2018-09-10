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
    private List<GameObject> pellets;
    private Vector2Int size;
    private Vector2Int offset;
    void Awake()
    {
        size = new Vector2Int();
        List<string> rows = new List<string>();
        StreamReader sr = new StreamReader(Application.dataPath + "/" + mapFile);
        string line = sr.ReadLine();
        while(line != null)
        {
            rows.Add(sr);
            if(sr.Length > size.x)
            {
                size.x = sr.Length;
            }
            line = sr.ReadLine();
        }
        
        size.y = rows.Count;
        
        offset = new Vector2Int{size.x/2, size.y/2};
        
        grid = new byte[size.y][];
        
        for(int i = 0; i < size.y; ++i)
        {
            grid[i] = new byte[size.x];
            for(int j = 0; j < rows[i].Length; ++j)
            {
                byte val = translateChar(rows[i][j]);
                grid[i][j] = val;
                Vector3 ip = new Vector3{j + offset.x, i + offset.y, 0};
                Instantiate(lookup[val], ip, Quaternion.identity, transform);
                
            }
            for(int j = rows[i].Length; j < size.x; ++j)
            {
                grid[i][j] = 0;
            }
        }
    }
    
    public bool walk(Vector2Int last, Vector2Int next)
    {
        return get(next.x, next.y) == 1;
    }
    
    public bool isIntersection(Vector2Int pos)
    {
        return getVertical(pos) != 0 && getHorizontal(pos) != 0;
    }
    
    public byte getVertical(Vector2Int pos)
    {
        Vector2Int index = pos - offset;
        byte res = get(index.x, index.y + 1);
        res |= (byte)(get(index.x, index.y - 1) << 1);
        return res;
    }
    
    public byte getHorizontal(Vector2Int pos)
    {
        Vector2Int index = pos - offset;
        byte res = get(index.x + 1, index.y);
        res |= (byte)(get(index.x + 1, index.y) << 1);
        return res;
    }
    
    private byte get(int x, int y)
    {
        if(x > 0 && x < size.x && y > 0 && y < size.y)
        {
            return (byte)(grid[y][x] < 6 ? 1:0);
        }
        
        return 1;
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
