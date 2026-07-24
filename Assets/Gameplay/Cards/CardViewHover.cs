using DG.Tweening;
using UnityEngine;
using Zlipacket.CoreZlipacket.Tools;

namespace Gameplay.Cards
{
    public class CardViewHover : Singleton<CardViewHover>
    {
        [SerializeField] private CardView cardViewHover;

        public void Show(Card card, Vector3 anchoredPosition)
        {
            cardViewHover.rectTransform.localScale = Vector3.one;
            cardViewHover.rectTransform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.25f);
            cardViewHover.Setup(card);
            cardViewHover.Show();
            cardViewHover.rectTransform.anchoredPosition = new Vector2(anchoredPosition.x, 0f);
        }

        public void Hide()
        {
            cardViewHover.Hide();
        }
    }
}