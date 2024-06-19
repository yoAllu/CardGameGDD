using System.Collections.Generic;
using UnityEngine;

public class SevenOfClubsGame : MonoBehaviour
{
    public Deck deck; // Reference to the deck
    public Transform[] playerCardAreas; // UI areas for player cards

    private List<List<Card>> players;
    private int currentPlayer;
    private Dictionary<string, List<Card>> tableCards; // Cards on the table, organized by suit

    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        deck.InitializeDeck();
        deck.ShuffleDeck();
        players = deck.DistributeCards();
        currentPlayer = 0;
        tableCards = new Dictionary<string, List<Card>>()
        {
            { "C", new List<Card>() },
            { "D", new List<Card>() },
            { "H", new List<Card>() },
            { "S", new List<Card>() }
        };

        // Place the Seven of Clubs on the table
        Card sevenOfClubs = FindCard("7", "C");
        if (sevenOfClubs != null)
        {
            tableCards["C"].Add(sevenOfClubs);
            players[GetPlayerWithCard(sevenOfClubs)].Remove(sevenOfClubs);
        }

        // Initialize player cards visually
        for (int i = 0; i < players.Count; i++)
        {
            // Activate player card area parent
            playerCardAreas[i].gameObject.SetActive(true);

            // Calculate card spacing based on the number of cards (example spacing)
            float cardSpacing = 2.7f; // Adjust as needed

            for (int j = 0; j < players[i].Count; j++)
            {
                Card card = players[i][j];

                // Add BoxCollider2D component to card if it doesn't already have one
                if (card.GetComponent<BoxCollider2D>() == null)
                {
                    card.gameObject.AddComponent<BoxCollider2D>();
                }

                // Add the Card script to the card
                if (card.GetComponent<Card>() == null)
                {
                    card.gameObject.AddComponent<Card>();
                }

                // Move card object to player's card area
                card.transform.SetParent(playerCardAreas[i]);
                card.transform.localPosition = new Vector3(j * cardSpacing, 0f, 0f); // Example side-by-side spacing

                // Set desired rotation and scale
                switch (i)
                {
                    case 0: // Player 1
                        card.transform.localRotation = Quaternion.Euler(-180f, 0f, 0f);
                        break;
                    case 1: // Player 2
                        card.transform.localRotation = Quaternion.Euler(-180f, 0f, 0f);
                        break;
                    case 2: // Player 3
                        card.transform.localRotation = Quaternion.Euler(-180f, 0f, 0f);
                        break;
                    case 3: // Player 4
                        card.transform.localRotation = Quaternion.Euler(-180f, 0f, 0f);
                        break;
                }

                // Set desired scale
                card.transform.localScale = new Vector3(40, 50, 1); // Example scaling

                card.gameObject.SetActive(true); // Ensure card is active
                card.Initialize(this); // Ensure card is initialized with game controller reference
            }
        }

        // Start the first turn
        StartTurn();
    }

    Card FindCard(string rank, string suit)
    {
        foreach (List<Card> playerCards in players)
        {
            foreach (Card card in playerCards)
            {
                if (card.rank == rank && card.suit == suit)
                {
                    return card;
                }
            }
        }
        return null;
    }

    int GetPlayerWithCard(Card card)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].Contains(card))
            {
                return i;
            }
        }
        return -1;
    }

    void StartTurn()
    {
        Debug.Log("Player " + (currentPlayer + 1) + "'s turn.");
    }

    public void PlayCard(Card card)
    {
        if (currentPlayer != GetPlayerWithCard(card))
        {
            Debug.Log("It's not your turn!");
            return;
        }

        if (IsValidMove(card))
        {
            players[currentPlayer].Remove(card);
            tableCards[card.suit].Add(card);
            Debug.Log("Player " + (currentPlayer + 1) + " played " + card.rank + " of " + card.suit);

            // Check for game end condition
            if (IsGameOver())
            {
                Debug.Log("Game Over! Player " + (currentPlayer + 1) + " wins!");
                return;
            }

            // Move to the next player
            currentPlayer = (currentPlayer + 1) % players.Count;
            StartTurn();
        }
        else
        {
            Debug.Log("Invalid move! Player " + (currentPlayer + 1) + ", try again.");
        }
    }

    bool IsValidMove(Card card)
    {
        // Check if the card can be legally played according to the game's rules (specific to your game logic)
        return true; // Implement your own game rules here
    }

    bool IsGameOver()
    {
        // Check if the game is over (specific to your game logic)
        return false; // Implement your own game end condition here
    }
}
