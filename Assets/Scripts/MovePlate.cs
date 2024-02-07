using UnityEngine;

public class MovePlate : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;
    [SerializeField] private SpriteRenderer spRenderer;
    [SerializeField] private GameObject reference = null;

    [SerializeField] private int matrixX;
    [SerializeField] private int matrixY;

    public bool attack = false;


    private void Awake()
    {
        
        spRenderer = GetComponent<SpriteRenderer>();
        if (attack)
        {
            spRenderer.color = new Color(1, 0, 0, 1);
        }
    }

    public void OnMouseUp()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        if (attack)
        {
            GameObject cp = gameManager.GetComponent<GameManager>().GetPositions(matrixX,matrixY);

            if (cp.CompareTag("white_king")) gameManager.GetComponent<GameManager>().Winner("black");
            if (cp.CompareTag("black_king")) gameManager.GetComponent<GameManager>().Winner("white");

            Destroy(cp);
        }
        gameManager.GetComponent<GameManager>().SetPositionEmpty(reference.GetComponent<ChessMan>().GetXBoard(), reference.GetComponent<ChessMan>().GetYBoard());
        reference.GetComponent<ChessMan>().SetXBoard(matrixX);
        reference.GetComponent<ChessMan>().SetYBoard(matrixY);
        reference.GetComponent<ChessMan>().SetCoords();

        gameManager.GetComponent<GameManager>().SetPositions(reference);
        gameManager.GetComponent<GameManager>().NextTurn();
        reference.GetComponent<ChessMan>().DestroyMovePlates();
        Debug.Log("Working");
    }
    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }
    public void SetReference(GameObject obj)
    {
        reference = obj;
    }
    public GameObject GetReference()
    {
        return reference;
    }
}
