using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Splines;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Gameplay.Cards
{
    public class HandManager : MonoBehaviour
    {
        [SerializeField] private int maxHandSize;
        [SerializeField] private SplineContainer splineContainer;
        [field: SerializeField] public RectTransform cardParent { get; private set; }

        private List<CardView> handCards = new();
        
        public bool IsHandFull => handCards.Count >= maxHandSize;

        public IEnumerator AddCard(CardView cardView)
        {
            if (IsHandFull)
                yield break;
            
            handCards.Add(cardView);
            yield return UpdateCardPosition();
        }
        
        private IEnumerator UpdateCardPosition(float duration = 0.25f)
        {
            if (handCards.Count == 0)
                yield break;

            float cardSpacing = (1f / maxHandSize);
            float firstCardPosition = 0.5f - (handCards.Count - 1) * cardSpacing / 2f;

            Spline spline = splineContainer.Spline;
            for (int i = 0; i < handCards.Count; i++)
            {
                float p = firstCardPosition + i * cardSpacing;

                Vector3 splineLocalPos = spline.EvaluatePosition(p);
                //Debug.Log(splineLocalPos);
                Vector3 tangent = spline.EvaluateTangent(p);

                // Spline's local X/Y becomes the UI anchored position directly
                Vector2 anchoredPos = new Vector2(splineLocalPos.x, splineLocalPos.y);

                // 2D tilt based on tangent angle, projected onto the canvas plane
                float angle = Mathf.Atan2(tangent.y, tangent.x) * Mathf.Rad2Deg/* - 90f*/;
                Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

                handCards[i].rectTransform.DOAnchorPos(anchoredPos, duration);
                handCards[i].rectTransform.DOLocalRotateQuaternion(rotation, duration);
            }

            yield return new WaitForSeconds(duration);
        }
    }
}