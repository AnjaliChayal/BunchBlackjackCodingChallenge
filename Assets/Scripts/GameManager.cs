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

   public Player player;
   public Player dealer;

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
        player.SetHandForThisPlayer();
    }
    void HitClicked()
    {

    }
    void StandClicked()
    {


    }
}
