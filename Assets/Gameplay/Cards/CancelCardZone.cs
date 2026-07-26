using UnityEngine;
using UnityEngine.EventSystems;
using Zlipacket.CoreZlipacket.Tools;
using Zlipacket.CoreZlipacket.UI.Canvas_Management;

namespace Gameplay.Cards
{
    public class CancelCardZone : Singleton<CancelCardZone>, IDropHandler
    {
        public CanvasGroup cg;
        public CanvasGroupController cgController;

        public override void Awake()
        {
            base.Awake();
            cgController = new CanvasGroupController(this, cg);
        }

        public void Show()
        {
            cg.alpha = 1f;
            cgController.SetInteractableState(true);
        }

        public void Hide()
        {
            cg.alpha = 0f;
            cgController.SetInteractableState(false);
        }
        
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null) return;
            
            if (eventData.pointerDrag.TryGetComponent(out CardView cardView))
            {
                cardView.cancelPlay = true;
            }
        }
    }
}