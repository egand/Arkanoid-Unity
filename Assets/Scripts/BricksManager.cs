using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BricksManager : MonoBehaviour
{
    #region Singleton
    private static BricksManager _instance;

    public static BricksManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    public Sprite[] sprites;
    public List<int[,]> LevelsData { get; set; }
    public Brick brickPrefab;
    public Color[] brickColors;
    public int InitialBricksCount { get; set; }

    private int _maxRows = 17;
    private int _maxCols = 12;
    private GameObject _bricksContainer;
    public List<Brick> RemainingBricks { get; set; }
    public int CurrentLevel;
    private float _initBrickSpawnPosX = -1.96f;
    private float _initBrickSpawnPosY = 3.325f;
    private float _shiftAmount = 0.365f;

    // Start is called before the first frame update
    void Start()
    {
        this.LevelsData = this.LoadLevelsData();
        this._bricksContainer = new GameObject("BricksContainer");
        this.RemainingBricks = new List<Brick>();
        this.GenerateBricks();
    }

    private void GenerateBricks()
    {
        int[,] currentLevelData = this.LevelsData[this.CurrentLevel];
        float currentSpawnX = _initBrickSpawnPosX;
        float currentSpawnY = _initBrickSpawnPosY;

        for (int row = 0; row < this._maxRows; row++)
        {
            for(int col = 0; col < this._maxCols; col++)
            {
                int brickType = currentLevelData[row, col];
                if (brickType > 0)
                {
                    Brick newBrick = Instantiate(brickPrefab, new Vector2(currentSpawnX, currentSpawnY), Quaternion.identity) as Brick;
                    newBrick.Init(_bricksContainer.transform, brickType);
                    this.RemainingBricks.Add(newBrick);
                }
                currentSpawnX += _shiftAmount;
                if (col == (this._maxCols - 1))
                {
                    currentSpawnX = _initBrickSpawnPosX;
                }
            }
            currentSpawnY -= _shiftAmount;
        }
        this.InitialBricksCount = this.RemainingBricks.Count;
    }

    private List<int[,]> LoadLevelsData()
    {
        TextAsset text = Resources.Load("levels") as TextAsset;
        string[] rows = text.text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

        List<int[,]> levelsData = new List<int[,]>();
        int[,] currentLevel = new int[_maxRows, _maxCols];
        int currentRow = 0;
        for (int i = 0; i < rows.Length; i++)
        {
            string line = rows[i];
            if (line.IndexOf("--") == -1)
            {
                string[] bricks = line.Split(',');
                for (int j = 0; j < bricks.Length; j++)
                {
                    currentLevel[currentRow, j] = int.Parse(bricks[j]);
                }
                currentRow++;
            }
            else
            {
                // end of current level
                currentRow = 0;
                levelsData.Add(currentLevel);
                currentLevel = new int[_maxRows, _maxCols];
            }
        }
        return levelsData;
    }
}
