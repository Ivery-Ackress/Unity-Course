using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameLogic game;
    [SerializeField] private TMP_Text score;
    [SerializeField] private TMP_InputField scoreToWin;
    [SerializeField] private Color winColor = Color.green;
    [SerializeField] private Color drawColor = Color.grey;
    [SerializeField] private Color loseColor = Color.red;
    [SerializeField] private TMP_InputField diceCount;
    [SerializeField] private Button startButton;
    [SerializeField] private Button resetButton;

    private int winScore = 0;

    public int WinScore
    {
        get { return winScore; }
        private set { winScore = value; }
    }

    private void Awake()
    {
        
        game.ReDraw.AddListener(Draw);
        scoreToWin.onEndEdit.AddListener(SetWinScore);
        diceCount.onEndEdit.AddListener(SetDiceCount);
        startButton.onClick.AddListener(StartGame);
        resetButton.onClick.AddListener(ResetGame);
    }

    private void Draw()
    {
        var currentScore = game.CurrentValue;
        var color = ColorUtility.ToHtmlStringRGB(currentScore switch
        {
            var n when n > WinScore => winColor,
            var n when n == WinScore => drawColor,
            _ => loseColor,
        });

        score.text = $"<color=#{color}>{currentScore.ToString()}</color> / {game.DiceCount * 6}";
    }

    private void ResetGame()
    {
        diceCount.text = "0";
        SetDiceCount("0");
        scoreToWin.text = "0";
        SetWinScore("0");
    }

    private void SetDiceCount(string value)
    {
        game.DiceCount = int.Parse(value);
    }

    private void SetWinScore(string value)
    {
        winScore = int.Parse(value);
    }

    private void StartGame()
    {
        game.DropDices();
    } 
}
