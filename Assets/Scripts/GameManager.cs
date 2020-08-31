using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Deck deck;


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
        dealButton.onClick.AddListener(() => DealClicked());
        hitButton.onClick.AddListener(() => HitClicked());
        standButton.onClick.AddListener(() => StandClicked());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DealClicked() {
        dealerScoreText.gameObject.SetActive(false);
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
        cashText.text = "Cash : $" + player.Money.ToString();
    }

    void HitClicked()
    {
        if (player.GetCardForPlayer() <= 10) {
            player.GetCardForPlayer();
        }
    }


    void StandClicked()
    {
        standClicks++;
        if (standClicks > 1)

            Debug.Log("End");

        HitDealer();
        standButtonText.text = "Call";

    }

    void HitDealer() {
        while (dealer.handValue< 16 && dealer.nextCardIndex < 10) {
            dealer.GetCardForPlayer();
            
            // update dealer score after this
        }

    }
}
