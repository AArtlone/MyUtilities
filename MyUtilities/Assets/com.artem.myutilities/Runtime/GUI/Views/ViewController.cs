using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyUtilities.GUI
{
    public abstract class ViewController : MonoBehaviour
    {
        [SerializeField] private bool hasChildNavController = default;
        [ShowIf(nameof(hasChildNavController), true, ComparisonType.Equals)]
        [SerializeField] private NavigationController childNavController = default;


        [Space(10f)]
        [SerializeField] private List<EffectBase> inTransitions = default;

        [Space(10f)]
        [SerializeField] private List<EffectBase> outTransitions = default;

        protected bool IsShowing { get; private set; }

        public WaitUntil TransitionIn()
        {
            bool done = false;

            StartCoroutine(TransitionInCo(() => { done = true; }));

            return new WaitUntil(() =>
            {
                return done;
            });
        }
        public WaitUntil TransitionOut()
        {
            bool done = false;

            StartCoroutine(TransitionOutCo(() => { done = true; }));

            return new WaitUntil(() =>
            {
                return done;
            });
        }

        private IEnumerator TransitionInCo(Action callback)
        {
            if (!HasInTransition())
            {
                //print("View controller does not have any IN transitions");
                callback.Invoke();
                yield break;
            }

            inTransitions.ForEach(t => t.PlayEffect());

            float duration = GetInTransitionDuration();

            //print($"In transitions durations is {duration}");

            yield return new WaitForSeconds(duration);

            callback.Invoke();
        }

        private IEnumerator TransitionOutCo(Action callback)
        {
            if (!HasOutTransition())
            {
                //print("View controller does not have any OUT transitions");
                callback.Invoke();
                yield break;
            }

            outTransitions.ForEach(t => t.PlayEffect());

            float duration = GetOutTransitionDuration();

            yield return new WaitForSeconds(duration);

            callback.Invoke();
        }

        private float GetInTransitionDuration()
        {
            float longestTransition = 0;
            foreach (var transition in inTransitions)
            {
                float totalDuration = transition.effectSO.tween.delay + transition.effectSO.tween.targetTime;

                if (totalDuration > longestTransition)
                    longestTransition = totalDuration;
            }

            return longestTransition;
        }

        private float GetOutTransitionDuration()
        {
            float longestTransition = 0;
            foreach (var transition in outTransitions)
            {
                float totalDuration = transition.effectSO.tween.delay + transition.effectSO.tween.targetTime;

                if (totalDuration > longestTransition)
                    longestTransition = totalDuration;
            }

            return longestTransition;
        }

        private bool HasInTransition()
        {
            return inTransitions.Count != 0;
        }

        private bool HasOutTransition()
        {
            return outTransitions.Count != 0;
        }

        public virtual void ViewWillDisappear()
        {

        }

        public virtual void ViewDisappeared()
        {

        }

        public virtual void ViewWillAppear()
        {

        }

        public virtual void ViewAppeared()
        {

        }

        public virtual void ViewWillBeFocused()
        {
        }

        public virtual void ViewFocused()
        {
            IsShowing = true;

            // This must be done in ViewFocused because it is called after the gameobject becomes active,
            // which is needed to start the coroutine inside FocusTopController()
            if (hasChildNavController)
                childNavController.FocusTopController();
        }

        public virtual void ViewWillBeUnfocused()
        {
            // This must be done in ViewWillBeUnfocused because it is called before the gameobject becomes inactive,
            // which is needed to start the coroutine inside UnfocusTopController()
            if (hasChildNavController)
                childNavController.UnfocusTopController();
        }

        public virtual void ViewUnfocused()
        {
            IsShowing = false;
        }
    }
}
