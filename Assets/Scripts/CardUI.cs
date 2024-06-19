using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public Text rankText;
    public Text suitText;
    private Card card;
    private SevenOfClubsGame gameController;

    public void Initialize(Card card, SevenOfClubsGame gameController)
    {
        this.card = card;
        this.gameController = gameController;
        rankText.text = card.rank;
        suitText.text = card.suit;
    }

    public void OnCardClicked()
    {
        gameController.PlayCard(card);
    }
}
