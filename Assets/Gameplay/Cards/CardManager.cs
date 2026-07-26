using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Gameplay.GameActions;
using Player.Script;
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
        [SerializeField] public PlayerStats playerStats;
        
        [Header("Prefabs")]
        [SerializeField] private CardView cardViewPrefab;
        
        [Header("Settings")]
        public LayerMask playLayer;
        [SerializeField] private SO_DeckData deckData;

        private readonly List<Card> drawPile = new();
        private readonly List<Card> discardPile = new();
        private readonly List<Card> hand = new();

        [HideInInspector] public Vector3 aimPosition = Vector3.zero;
        
        private void OnEnable()
        {
            ActionSystem.AttachPerformer<GA_DrawCard>(DrawCardPerformer);
            ActionSystem.AttachPerformer<GA_PlayCard>(PlayCardPerformer);

            ActionSystem.AttachPerformer<GA_AddEnergy>(AddEnergyPerformer);
            
            ActionSystem.SubscribeReaction<GA_PlayCard>(PlayCardReaction, ReactionTiming.POST);
        }

        private void OnDisable()
        {
            ActionSystem.DetachPerformer<GA_DrawCard>();
            ActionSystem.DetachPerformer<GA_PlayCard>();

            ActionSystem.DetachPerformer<GA_AddEnergy>();
            
            ActionSystem.UnsubscribeReaction<GA_PlayCard>(PlayCardReaction, ReactionTiming.POST);
        }

        private void Start()
        {
            SetUp(deckData.deckList);
            DrawToFull();
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

            if (notDrawnAmount > 0 && discardPile.Count > 0)
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
            CardViewHover.Instance.Hide();
            
            GA_AddEnergy gaAddEnergy = new(gaPlayCard.Card.Cost);
            ActionSystem.Instance.AddReaction(gaAddEnergy);

            foreach (var effect in gaPlayCard.Card.Effects)
            {
                GA_PerformEffect gaPerformEffect = new(effect);
                ActionSystem.Instance.AddReaction(gaPerformEffect);
            }
        }


        public IEnumerator AddEnergyPerformer(GA_AddEnergy gaAddEnergy)
        {
            playerStats.Energy += gaAddEnergy.amount;
            
            yield return null;
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

        public void DrawCard(int amount)
        {
            GA_DrawCard gaDrawCard = new(amount);
            ActionSystem.Instance.Perform(gaDrawCard);
        }

        public void DrawToFull()
        {
            GA_DrawCard gaDrawCard = new(handManager.maxHandSize);
            ActionSystem.Instance.Perform(gaDrawCard);
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
            if (handManager.IsHandFull)
                yield break;
            
            Card card = drawPile.Draw();
            hand.Add(card);
            CardView cardView = CreateCardView(card, handManager.cardParent);
            yield return handManager.AddCard(cardView);
        }

        private IEnumerator DiscardCard(CardView cardView)
        {
            discardPile.Add(cardView.card);
            
            yield return null;
            Destroy(cardView.gameObject);
        }
    }
}