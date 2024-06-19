using UnityEngine;

public class Card : MonoBehaviour
{
    public string rank;
    public string suit;
    private SevenOfClubsGame gameController;

    public void Initialize(SevenOfClubsGame controller)
    {
        gameController = controller;
    }

    void OnMouseDown()
    {
        if (gameController != null)
        {
            gameController.PlayCard(this);
        }
    }
}
