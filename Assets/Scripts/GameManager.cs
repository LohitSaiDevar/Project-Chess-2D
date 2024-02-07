using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject chessPiece;
    [SerializeField] GameObject winnerText;
    [SerializeField] GameObject restartText;

    private GameObject[,] positions = new GameObject[8, 8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];


    private string currentPlayer = "White";
    private bool gameOver;
    private void Start()
    {
        //Arranges White ChessPieces to PlayerWhite array and black chess pieces to PlayerBlack array
        playerWhite = new GameObject[]
        {
            Create("white_rook", 0, 0), Create("white_knight", 1, 0), Create("white_bishop", 2, 0),
            Create("white_king", 3, 0), Create("white_queen", 4, 0), Create("white_bishop", 5, 0),
            Create("white_knight", 6, 0), Create("white_rook", 7, 0), Create("white_pawn", 0, 1),
            Create("white_pawn", 1, 1), Create("white_pawn", 2, 1), Create("white_pawn", 3, 1),
            Create("white_pawn", 4, 1), Create("white_pawn", 5, 1), Create("white_pawn", 6, 1),
            Create("white_pawn", 7, 1),
        };
        playerBlack = new GameObject[]
        {
            Create("black_rook", 0, 7), Create("black_knight", 1, 7), Create("black_bishop", 2, 7),
            Create("black_king", 3, 7), Create("black_queen", 4, 7), Create("black_bishop", 5, 7),
            Create("black_knight", 6, 7), Create("black_rook", 7, 7), Create("black_pawn", 0, 6),
            Create("black_pawn", 1, 6), Create("black_pawn", 2, 6), Create("black_pawn", 3, 6),
            Create("black_pawn", 4, 6), Create("black_pawn", 5, 6), Create("black_pawn", 6, 6),
            Create("black_pawn", 7, 6),
        };
        for (int i = 0; i < playerBlack.Length; i++)
        {
            SetPositions(playerWhite[i]);
            SetPositions(playerBlack[i]);
        }
    }

    //Creates a chess piece.
    private GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(chessPiece, new Vector3(0, 0, 0), Quaternion.identity);
        ChessMan cm = obj.GetComponent<ChessMan>();
        cm.name = name;
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate();
        return obj;
    }

    //Setting positions for each of the chesspieces
    public void SetPositions(GameObject obj)
    {
        ChessMan cm = obj.GetComponent<ChessMan>();
        positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }


    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }

    public GameObject GetPositions(int x, int y)
    {
        return positions[x, y];
    }
    public bool PositionsOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1))
        {
            return false;
        }
        else
        {
            return true;
        } 
    }

    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    //Getting the bool gameOver in other scripts
    public bool IsGameOver()
    {
        return gameOver;
    }

    //If the player's turn is over, switch current player
    public void NextTurn()
    {
        if (currentPlayer == "black")
        {
            currentPlayer = "white";
        }
        else
        {
            currentPlayer = "black";
        }
    }
    public void Update()
    {
        if (gameOver == true && Input.GetMouseButtonDown(0))
        {
            gameOver = false;
            SceneManager.LoadScene(0);
        }
    }

    //Script for announcing winner
    public void Winner(string playerWinner)
    {
        gameOver = true;

        winnerText.SetActive(true);
        winnerText.GetComponent<TextMeshProUGUI>().text = playerWinner + " is the winner";
        restartText.SetActive(true);
    }
}
