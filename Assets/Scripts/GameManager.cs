using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Deck deck;

    int resetMoney = 1000;
    //Buttons

    public Button dealButton;
    public Button standButton;
    public Button hitButton;
    public Button betButton;

    public Text standButtonText;
    public Text dealerScoreText;
    public Text betsText;
    public Text cashText;
    public Text playerScoreText;
    public Text resultText;


    public GameObject dealerHideCard;

    // value of the bet
    int pot = 0;

    public Player player;
   public Player dealer;

    int standClicks = 0;


    
    public static GameManager instance;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        dealButton.onClick.AddListener(() => DealClicked());
        hitButton.onClick.AddListener(() => HitClicked());
        standButton.onClick.AddListener(() => StandClicked());
        betButton.onClick.AddListener(() => BetClicked());
        dealButton.gameObject.SetActive(true);
        hitButton.gameObject.SetActive(false);
        standButton.gameObject.SetActive(false);
        resultText.gameObject.SetActive(false);
        dealerScoreText.gameObject.SetActive(false);
        if (PlayerPrefs.HasKey("Cash")) {
           
            player.Money = int.Parse(PlayerPrefs.GetString("Cash").ToString());
            if (player.Money <= 0) {
                player.Money = resetMoney;
            }
            cashText.text = "Cash : $" + player.Money.ToString();
            Debug.Log(PlayerPrefs.GetString("Cash"));
        }
        else {
            PlayerPrefs.SetString("Cash", player.Money.ToString());
            cashText.text = "Cash : $" + player.Money.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BetClicked() {
        Text newBet = betButton.GetComponentInChildren(typeof(Text)) as Text;
        int intBet = int.Parse(newBet.text.ToString().Split(' ')[2].Remove(0,1));
        player.Money -= intBet;
        cashText.text = "Cash : $" +player.Money.ToString();
        pot += intBet * 2;
        betsText.text = "Bet : $" + pot.ToString();
    }

    void DealClicked() {
        if (player.Money <= 0)
        {
            player.Money = resetMoney;
        }

        player.ResetHand();
        dealer.ResetHand();
        resultText.gameObject.SetActive(false);
        dealerScoreText.gameObject.SetActive(false);
        dealerHideCard.GetComponent<Image>().enabled = true;
        deck.Shuffle();
        player.SetHandForThisPlayer();
        dealer.SetHandForThisPlayer();
        playerScoreText.text = "Hand : " + player.handValue.ToString();
        dealerScoreText.text = "Dealer Hand : " + dealer.handValue.ToString();
        dealButton.gameObject.SetActive(false);
        hitButton.gameObject.SetActive(true);
        standButton.gameObject.SetActive(true);
        standButtonText.text = "Stand";

        // Set Size of bet
        pot = 40;
        betsText.text = "Bet : $" + pot.ToString();
        player.Money -= 20;
        cashText.text = "Cash : $" + player.Money.ToString();
    }

    void HitClicked()
    {
        if (player.GetCardForPlayer() <= 10) {
            player.GetCardForPlayer();
            playerScoreText.text = "Hand : " + player.handValue.ToString();
           
        }
        if (player.handValue > 20)
            RoundOver();
    }


    void StandClicked()
    {
        standClicks++;
        if (standClicks > 1)
            RoundOver();

        HitDealer();
        standButtonText.text = "Call";

    }

    void HitDealer() {
        while (dealer.handValue< 16 && dealer.nextCardIndex < 10) {
            dealer.GetCardForPlayer();
            dealerScoreText.text = " Dealer Hand : " + dealer.handValue.ToString();
            if (dealer.handValue > 20)
                RoundOver();
            // update dealer score after this
        }

    }

    void RoundOver() {
        bool playerBust = player.handValue > 21;
        bool dealerBust = dealer.handValue > 21;
        bool player21 = player.handValue == 21;
        bool dealer21 = dealer.handValue == 21;
        if (standClicks < 2 && !playerBust && !dealerBust && !player21 && !dealer21)
            return;


        bool roundOver = true;

        // if both, player and dealer are busted.
        if (playerBust && dealerBust)
        {
            resultText.text = " All bust. Bets Returned ";
            player.Money += pot / 2;

        }
        // if player is busted
        else if (playerBust || (!dealerBust &&  dealer.handValue > player.handValue))
        {

            resultText.text = " Dealer Wins !!!";
        }
        // if dealer is busted
        else if (dealerBust || (!playerBust && player.handValue > dealer.handValue))
        {
            resultText.text = " You Win !!!";
            player.Money += pot;
        }
        // if both scores tie
        else if (player.handValue == dealer.handValue)
        {
            resultText.text = "Its a Tie!! Bets Returned";
            player.Money += pot / 2;
        }
        // if nothing of the above is true, its round over
        else {
            roundOver = false;
        }

        if (roundOver) {

            dealButton.gameObject.SetActive(true);
            hitButton.gameObject.SetActive(false);
            standButton.gameObject.SetActive(false);
            resultText.gameObject.SetActive(true);
            dealerScoreText.gameObject.SetActive(true);
            dealerHideCard.GetComponent<Image>().enabled = false;
            cashText.text = "Cash : $" + player.Money.ToString();
            standClicks = 0;
            
        }
    }
}
