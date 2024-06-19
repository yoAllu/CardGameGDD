using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<GameObject> cardPrefabs; // Assign all card prefabs in the inspector
    private List<Card> cards = new List<Card>();

    void Start()
    {
        InitializeDeck();
        ShuffleDeck();
        DistributeCards();
    }

    public void InitializeDeck()
    {
        foreach (GameObject cardPrefab in cardPrefabs)
        {
            GameObject cardObject = Instantiate(cardPrefab, transform);
            Card card = cardObject.AddComponent<Card>();

            // Set the suit and rank based on the prefab name or another method
            string cardName = cardPrefab.name; // Assume the prefab name contains the suit and rank

            // Extract rank and suit from the card name format "PlayingCards_RankSuit"
            string prefix = "PlayingCards_";
            if (cardName.StartsWith(prefix))
            {
                string cardDetails = cardName.Substring(prefix.Length); // Remove prefix
                int lastIndex = cardDetails.LastIndexOfAny(new char[] { 'C', 'D', 'H', 'S' });
                if (lastIndex > 0)
                {
                    card.rank = cardDetails.Substring(0, lastIndex);
                    card.suit = cardDetails.Substring(lastIndex);
                }
                else
                {
                    Debug.LogError("Card details format is incorrect: " + cardDetails);
                }
            }
            else
            {
                Debug.LogError("Card prefab name format is incorrect: " + cardName);
            }

            cards.Add(card);
        }
    }

    public void ShuffleDeck()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            Card temp = cards[i];
            int randomIndex = Random.Range(0, cards.Count);
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
    }

    public List<List<Card>> DistributeCards()
    {
        int playerCount = 4;
        List<List<Card>> players = new List<List<Card>>();

        for (int i = 0; i < playerCount; i++)
        {
            players.Add(new List<Card>());
        }

        for (int i = 0; i < cards.Count; i++)
        {
            players[i % playerCount].Add(cards[i]);
        }

        return players;
    }
}
