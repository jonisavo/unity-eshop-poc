using System;
using System.Collections;
using UnityEngine;

namespace EshopPOC
{
    [RequireComponent(typeof(CanvasGroup))]
    public class VisibilityChange : MonoBehaviour
    {
        [Range(0.1f, 3f)]
        public float fadeSpeed = 1f;

        [Serializable]
        public enum Action
        {
            FadeIn,
            FadeOut
        }

        public Action action;
        
        private CanvasGroup _canvasGroup;

        private void Awake() => _canvasGroup = GetComponent<CanvasGroup>();

        protected IEnumerator ShowCoroutine()
        {
            while (_canvasGroup.alpha < 1f)
            {
                _canvasGroup.alpha += fadeSpeed * Time.deltaTime;
                yield return null;
            }

            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        protected IEnumerator HideCoroutine()
        {
            while (_canvasGroup.alpha > 0)
            {
                _canvasGroup.alpha -= fadeSpeed * Time.deltaTime;
                yield return null;
            }
            
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}