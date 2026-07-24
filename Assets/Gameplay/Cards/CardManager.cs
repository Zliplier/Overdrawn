using DG.Tweening;
using UnityEngine;
using Zlipacket.CoreZlipacket.Tools;

namespace Gameplay.Cards
{
    public class CardManager : Singleton<CardManager>
    {
        [Header("Components")]
        [SerializeField] private HandManager handManager;
        
        [Header("Prefabs")]
        [SerializeField] private CardView cardViewPrefab;

        public CardView CreateCardView(Card card, RectTransform cardParent)
        {
            CardView cardView = Instantiate(cardViewPrefab, cardParent);
            cardView.name = "Card";
            RectTransform cardRt = cardView.GetComponent<RectTransform>();
            cardRt.localScale = Vector3.one;
            cardRt.anchoredPosition = Vector2.zero;
            cardView.Setup(card);
            cardRt.DOPunchScale(new Vector3(0.2f, 0.1f, 0f), 0.25f);
            
            return cardView;
        }

        public void DrawCard()
        {
            if (handManager.IsHandFull)
                return;
            
            CardView cardView = CreateCardView(null, handManager.cardParent);
            StartCoroutine(handManager.AddCard(cardView));
        }
    }
}