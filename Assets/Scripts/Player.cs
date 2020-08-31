using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    // total value of cards in player hand
    public int handValue = 0;
    // money for betting 
    int money = 1000;

    public List<GameObject> hand = new List<GameObject>();
    public int nextCardIndex = 0;

    // for converting aces to value of 1 or 11
    List<Card> aceList = new List<Card>();

    // Start is called before the first frame update
    public int Money {
        get {

            return money;

        }

        set {
            money += value;
        }
    }
    private void Start()
    {
        
    }
    // set hand for this player
    public void SetHandForThisPlayer()
    {
        GetCardForPlayer();
        GetCardForPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetCardForPlayer() {
        // get a card and deal it to the player
      //  if (nextCardIndex <= 8)
        int cardValue = GameManager.instance.deck.DealCard(hand[nextCardIndex].GetComponent<Card>());
        hand[nextCardIndex].GetComponent<Image>().enabled = true;
        handValue += cardValue;
        if (cardValue == 1) {
            aceList.Add(hand[nextCardIndex].GetComponent<Card>());

        }
        CheckForAce();
        nextCardIndex++;
        return handValue;

    }

    // to check whether to assign 1 or 11 to an ace
    public void CheckForAce() {

        foreach (Card card in aceList)
        {
            if (handValue + 10 < 22 && card.ValueOfThisCard == 1)
            {
                card.ValueOfThisCard = 11;
                handValue += 10;


            }
            else if (handValue > 21 && card.ValueOfThisCard == 11) {
                card.ValueOfThisCard = 1;
                handValue -= 10;
            }
        }

    }
}
