using System;
using DG.Tweening;
using Gameplay.GameActions;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zlipacket.CoreZlipacket.ActionSystem;
using Zlipacket.CoreZlipacket.UI.Canvas_Management;

namespace Gameplay.Cards
{
    public class CardView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Image cardImage;
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text description;
        [SerializeField] private TMP_Text cost;
        [SerializeField] private CanvasGroup canvasGroup;
        
        [field: SerializeField] public RectTransform rectTransform { get; private set; }
        public RectTransform parentRectTransform { get; private set; }

        public Card card { get; private set; }
        private CanvasGroupController cgController;
        
        private Vector2 mouseScreenPosition => CorePlayer.Instance.inputReader.playerInputMap.mousePosition;
        
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
            if (cardImage != null)
                cardImage.sprite = card.Sprite;
            title.text = card.Title;
            description.text = card.Description;
            cost.text = card.Cost.ToString();
            parentRectTransform = rectTransform.parent.GetComponent<RectTransform>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (IsDragging) return;
            Hide();
            CardViewHover.Instance.Show(card, rectTransform.anchoredPosition);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (IsDragging) return;
            Show();
            CardViewHover.Instance.Hide();
        }

        public bool IsDragging = false;
        public bool cancelPlay = false;
        private Vector3 dragStartAnchoredPosition = Vector3.zero;
        private Quaternion dragStartRotation = Quaternion.identity;

        public void OnBeginDrag(PointerEventData eventData)
        {
            Show();
            CardViewHover.Instance.Hide();
            
            dragStartAnchoredPosition = rectTransform.anchoredPosition;
            dragStartRotation = rectTransform.rotation;
            
            rectTransform.rotation = Quaternion.Euler(0, 0, 0);
            rectTransform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            
            CancelCardZone.Instance.Show();
            
            cgController.SetInteractableState(false);
            IsDragging = true;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            IsDragging = false;
            cgController.SetInteractableState(true);
            
            CancelCardZone.Instance.Hide();
            
            Camera cam = Camera.main;
            Ray ray = cam.ScreenPointToRay(mouseScreenPosition);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction, Color.red);
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, CardManager.Instance.playLayer) && 
                !cancelPlay && 
                CardManager.Instance.playerStats.HasEnoughEnergy())
            {
                // Play this Card
                CardManager.Instance.aimPosition = hit.point;
                
                GA_PlayCard gaPlayCard = new(card);
                ActionSystem.Instance.Perform(gaPlayCard);
                
                Debug.Log("Play Card: " + card.Title);
            }
            else 
            {
                // Return to hand
                
                rectTransform.DOAnchorPos3D(dragStartAnchoredPosition, 0.25f);
                rectTransform.DORotateQuaternion(dragStartRotation, 0.25f);
                rectTransform.DOScale(1, 0.25f);
                
                Show();
            }

            CardViewHover.Instance.Hide();
            cancelPlay = false;
        }
        
        public void OnDrag(PointerEventData eventData)
         {
             // Convert the screen coordinates into the parent's local space
             if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                     parentRectTransform, 
                     mouseScreenPosition, 
                     null, 
                     out Vector2 localPoint))
             {
                 // Set the positioned logic
                 rectTransform.anchoredPosition = localPoint;
             }
         }
    }
}