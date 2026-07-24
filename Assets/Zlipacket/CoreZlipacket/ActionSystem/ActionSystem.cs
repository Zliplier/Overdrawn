using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using Zlipacket.CoreZlipacket.Tools;

namespace Zlipacket.CoreZlipacket.ActionSystem
{
    public class ActionSystem : Singleton<ActionSystem>
    {
        private List<GameAction> reactions = new();
        public bool IsPerforming { get; private set; } = false;
        public static Dictionary<Type, List<Action<GameAction>>> preSubs { get; private set; } = new();
        public static Dictionary<Type, List<Action<GameAction>>> postSubs { get; private set; } = new();
        public static Dictionary<Type, Func<GameAction, IEnumerator>> performers { get; private set; } = new();

        public void Perform(GameAction action, Action callback = null)
        {
            if (IsPerforming)
                return;
            
            IsPerforming = true;
            StartCoroutine(Flow(action, () =>
            {
                IsPerforming = false;
                callback?.Invoke(); 
            }));
        }

        public void AddReaction(GameAction gameAction)
        {
            reactions?.Add(gameAction);
        }

        public IEnumerator Flow(GameAction action, Action callback = null)
        {
            reactions = action.PreReactions;
            PerformSubscriber(action, preSubs);
            yield return PerformReactions();
            
            reactions = action.PerformReactions;
            yield return PerformPerformer(action);
            yield return PerformReactions();
            
            reactions = action.PostReactions;
            PerformSubscriber(action, postSubs);
            yield return PerformReactions();
            
            callback?.Invoke();
        }

        private IEnumerator PerformPerformer(GameAction action)
        {
            Type type = action.GetType();
            if (performers.ContainsKey(type))
                yield return performers[type](action);
        }
        
        private void PerformSubscriber(GameAction action, Dictionary<Type, List<Action<GameAction>>> subs)
        {
            Type type = action.GetType();
            if (subs.ContainsKey(type))
            {
                foreach (var sub in subs[type])
                {
                    sub(action);
                }
            }
        }

        private IEnumerator PerformReactions()
        {
            foreach (var reaction in reactions)
            {
                yield return Flow(reaction);
            }
        }

        public static void AttachPerformer<T>(Func<T, IEnumerator> performer) where T : GameAction
        {
            Type type = typeof(T);
            IEnumerator wrappedPerformer(GameAction action) => performer((T)action);
            if (performers.ContainsKey(type))
                performers[type] = wrappedPerformer;
            else
                performers.Add(type, wrappedPerformer);
        }

        public static void DetachPerformer<T>() where T : GameAction
        {
            Type type = typeof(T);
            if (performers.ContainsKey(type))
                performers.Remove(type);
        }

        public static void SubscribeReaction<T>(Action<T> reaction, ReactionTiming timing) where T : GameAction
        {
            Dictionary<Type, List<Action<GameAction>>> subs = timing == ReactionTiming.PRE ? preSubs : postSubs;
            void wrappedReaction(GameAction action) => reaction((T)action);
            if (subs.ContainsKey(typeof(T)))
                subs[typeof(T)].Add(wrappedReaction);
            else
            {
                subs.Add(typeof(T), new());
                subs[typeof(T)].Add(wrappedReaction);
            }
            
            
        }

        public static void UnsubscribeReaction<T>(Action<T> reaction, ReactionTiming timing) where T : GameAction
        {
            Dictionary<Type, List<Action<GameAction>>> subs = timing == ReactionTiming.PRE ? preSubs : postSubs;
            if (subs.ContainsKey(typeof(T)))
            {
                void wrappedReaction(GameAction action) => reaction((T)action);
                subs[typeof(T)].Remove(wrappedReaction);
            }
        }
    }

    public abstract class GameAction
    {
        public List<GameAction> PreReactions { get; private set; } = new();
        public List<GameAction> PerformReactions { get; private set; } = new();
        public List<GameAction> PostReactions { get; private set; } = new();
    }

    public enum ReactionTiming
    {
        PRE, POST
    }
}