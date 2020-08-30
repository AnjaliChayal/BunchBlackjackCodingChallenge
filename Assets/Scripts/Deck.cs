using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{

    public List<Sprite> sprites = new List<Sprite>();
    int[] cardValues = new int[53];
    public Sprite backSprite;
    int currentIndexInDeck = 0;


    public static Deck instance;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GetValuesForCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetValuesForCards() {
        int num = 0;

        for (int i = 0; i < sprites.Count; i++)
        {
            num = i;
           // Debug.Log(i);
            num  %=  13;
            // if remainder of x/13 is greater than 10, use as 10 or else use as remainder value
            if (num > 10 || num == 0) {
                num = 10;

            }
            cardValues[i] = num++;

            currentIndexInDeck = 1;

        }

       

    }

    void Shuffle()
    {
        for (int i = sprites.Count - 1; i > 0 ; --i)
        {
            int j = Mathf.FloorToInt(Random.Range(0.0f, 1.0f) * sprites.Count - 1) + 1;

            Sprite face = sprites[i];
            sprites[i] = sprites[j];
            sprites[j] = face;

            int value = cardValues[i];
            cardValues[i] = cardValues[j];
            cardValues[j] = value;
        }

    }


    public int DealCard(Card card) {
        card.SetSprite(sprites[currentIndexInDeck]);
        card.ValueOfThisCard = cardValues[currentIndexInDeck];
        currentIndexInDeck++;
        return card.ValueOfThisCard;
    }

 
}
