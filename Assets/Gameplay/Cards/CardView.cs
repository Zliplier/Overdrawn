using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zlipacket.CoreZlipacket.UI.Canvas_Management;

namespace Gameplay.Cards
{
    public class CardView : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image cardImage;
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text description;
        [SerializeField] private TMP_Text cost;
        [SerializeField] private CanvasGroup canvasGroup;
        
        [field: SerializeField] public RectTransform rectTransform { get; private set; }

        public Card card { get; private set; }
        private CanvasGroupController cgController;
        
        private void Awake()
        {
            if (rectTransform == null)
                rectTransform = GetComponent<RectTransform>();
            
            cgController = new CanvasGroupController(this, canvasGroup);
        }
        
        public void Show()
        {
            cgController.alpha = 1f;
            //cgController.SetInteractableState(true);
        }

        public void Hide()
        {
            cgController.alpha = 0f;
            //cgController.SetInteractableState(false);
        }

        public void Setup(Card card)
        {
            if (card == null)
                return;
            
            this.card = card;
            cardImage.sprite = card.Sprite;
            title.text = card.Title;
            cost.text = card.Cost.ToString();
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            /*card?.PerformEffect();
            Destroy(gameObject);*/
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Hide();
            CardViewHover.Instance.Show(card, rectTransform.anchoredPosition);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Show();
            CardViewHover.Instance.Hide();
        }
    }
}