using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{

    int valueOfThisCard = 0;

    //  value of this card
    public int ValueOfThisCard
    {
        get { return valueOfThisCard; }
        set {
            valueOfThisCard = value;
        }

    }


    public void ResetCard()
    {
        Sprite backSprite = Deck.instance.backSprite;
        gameObject.GetComponent<Image>().sprite = backSprite;
        ValueOfThisCard = 0;
    }


    public void SetSprite( Sprite newSprite) {
        gameObject.GetComponent<Image>().sprite = newSprite;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
