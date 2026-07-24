using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Gameplay.GameActions;
using UnityEngine;
using Zlipacket.CoreZlipacket.ActionSystem;
using Zlipacket.CoreZlipacket.Tools;
using Zlipacket.CoreZlipacket.Tools.Extensions;

namespace Gameplay.Cards
{
    public class CardManager : Singleton<CardManager>
    {
        [Header("Components")]
        [SerializeField] private HandManager handManager;
        
        [Header("Prefabs")]
        [SerializeField] private CardView cardViewPrefab;

        private readonly List<Card> drawPile = new();
        private readonly List<Card> discardPile = new();
        private readonly List<Card> hand = new();

        private void OnEnable()
        {
            ActionSystem.AttachPerformer<GA_DrawCard>(DrawCardPerformer);
            ActionSystem.AttachPerformer<GA_PlayCard>(PlayCardPerformer);
            ActionSystem.SubscribeReaction<GA_PlayCard>(PlayCardReaction, ReactionTiming.POST);
        }

        private void OnDisable()
        {
            ActionSystem.DetachPerformer<GA_DrawCard>();
            ActionSystem.DetachPerformer<GA_PlayCard>();
            ActionSystem.UnsubscribeReaction<GA_PlayCard>(PlayCardReaction, ReactionTiming.POST);
        }
        
        // Performer
        public IEnumerator DrawCardPerformer(GA_DrawCard gaDrawCard)
        {
            int actualAmount = Mathf.Min(gaDrawCard.Amount, drawPile.Count);
            int notDrawnAmount = gaDrawCard.Amount - actualAmount;

            for (int i = 0; i < actualAmount; i++)
            {
                yield return DrawCard();
            }

            if (notDrawnAmount > 0)
            {
                RefillDeck();
                for (int i = 0; i < notDrawnAmount; i++)
                {
                    yield return DrawCard();
                }
            }
        }

        public IEnumerator PlayCardPerformer(GA_PlayCard gaPlayCard)
        {
            hand.Remove(gaPlayCard.Card);
            CardView cardView = handManager.RemoveCard(gaPlayCard.Card);
            yield return DiscardCard(cardView);
            
            //TODO: Perform Effects.
        }
        
        // Reactions
        private void PlayCardReaction(GA_PlayCard gaPlayCard)
        {
            GA_DrawCard gaDrawCard = new(handManager.maxHandSize);
            ActionSystem.Instance.AddReaction(gaDrawCard);
        }
        
        // Publics
        public void SetUp(List<SO_CardData> deckData)
        {
            foreach (var cardData in deckData)
            {
                Card card = new(cardData);
                drawPile.Add(card);
            }
        }

        // Helpers
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
        
        private void RefillDeck()
        {
            drawPile.AddRange(discardPile);
            discardPile.Clear();
        }

        public IEnumerator DrawCard()
        {
            /*if (handManager.IsHandFull)
                yield break;
            
            CardView cardView = CreateCardView(null, handManager.cardParent);
            StartCoroutine(handManager.AddCard(cardView));*/
            
            Card card = drawPile.Draw();
            hand.Add(card);
            CardView cardView = CreateCardView(card, handManager.cardParent);
            yield return handManager.AddCard(cardView);
        }

        private IEnumerator DiscardCard(CardView cardView)
        {
            yield return null;
            Destroy(cardView.gameObject);
        }
    }
}