using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class MultiEventTargetDinoEffects
{
    protected class MultiEventEffects
    {
        protected class EventEffects
        {
            private List<MultiEventTargetDinoEffectController> m_multiEventEffects;
            public List<MultiEventTargetDinoEffectController> multiEventEffects{get{return m_multiEventEffects;}}

            public EventEffects()
            {
                m_multiEventEffects = new List<MultiEventTargetDinoEffectController>();
            }

            public void MakeEffects(MultiEventTargetDinoEffectController multiEffect)
            {
                m_multiEventEffects.Add(multiEffect);
                int count = multiEffect.count - 1;
                if(count <= 0)
                    return;
                    
                GameObject effectObject = multiEffect.gameObject;
                Transform parent = multiEffect.transform.parent;
                // multiEffect.SetOriginParent();
                for(int i = 0; i < count; i++)
                {
                    GameObject o = MonoBehaviour.Instantiate(effectObject) as GameObject;
                    o.transform.SetParent(parent);
                    MultiEventTargetDinoEffectController effect = o.GetComponent<MultiEventTargetDinoEffectController>();
                    o.transform.localPosition = multiEffect.transform.localPosition;
                    o.transform.localScale = multiEffect.transform.localScale;
                    effect.SetOriginParent();
                    m_multiEventEffects.Add(effect);
                }
            }

            public void PlayEffects(int index, Transform targetTransform)
            {
                int effectCount = m_multiEventEffects.Count;
                int effectIndex = index % effectCount;
                m_multiEventEffects[effectIndex].PlayTargetEffect(targetTransform);
            }
        }

        private List<EventEffects> m_multiEffects;
        private int m_playCount = 0;
        public int playCount{get{return m_playCount;}}

        public MultiEventEffects()
        {
            m_multiEffects = new List<EventEffects>();
        }

        public void AddMultiEventTargetEffect(MultiEventTargetDinoEffectController multiEffect)
        {
            EventEffects multiEventEffects = new EventEffects();
            multiEventEffects.MakeEffects(multiEffect);
            m_multiEffects.Add(multiEventEffects);
        }

        public void PlayMultiEventTargetEffect(Transform targetTransform)
        {
            foreach(EventEffects eventEffects in m_multiEffects)
            {
                eventEffects.PlayEffects(m_playCount, targetTransform);
            }

            m_playCount++;
        }

        public void InitializeMultiEventTargetEffect()
        {
            m_playCount = 0;
        }

    }
    private Dictionary<string, MultiEventEffects> m_multiEvents;

    

    public MultiEventTargetDinoEffects()
    {
        m_multiEvents = new Dictionary<string, MultiEventEffects>();
    }

    public void AddMultiEventSkillEffect(MultiEventTargetDinoEffectController multiEffect)
    {
        string eventName = multiEffect.eventName;
        multiEffect.SetOriginParent();
        if(m_multiEvents.ContainsKey(eventName))
            m_multiEvents[eventName].AddMultiEventTargetEffect(multiEffect);
        else
        {
            MultiEventEffects multiEventEffects = new MultiEventEffects();
            multiEventEffects.AddMultiEventTargetEffect(multiEffect);
            m_multiEvents.Add(eventName, multiEventEffects);
        }
    }


    public void PlayMultiEventTargetSkillEffect(string eventName, Transform targetTransform)
    {
        if(!m_multiEvents.ContainsKey(eventName))
            return;
        
        m_multiEvents[eventName].PlayMultiEventTargetEffect(targetTransform);
    }

    public void InitializeMultiEventTargetDinoEffect()
    {
        foreach(KeyValuePair<string, MultiEventEffects> pair in m_multiEvents)
        {
            pair.Value.InitializeMultiEventTargetEffect();
        }
    }



}

public class TargetDinoEffects
{
    protected class EventEffects
    {
        private List<TargetDinoEffectController> m_effects;
        public List<TargetDinoEffectController> effects{get{return m_effects;}}

        public void AddEffect(TargetDinoEffectController _effect)
        {
            if(m_effects == null)
                m_effects = new List<TargetDinoEffectController>();

            m_effects.Add(_effect);
        }
    }
    private Dictionary<string, EventEffects> m_events;

    public TargetDinoEffects()
    {
        m_events = new Dictionary<string, EventEffects>();
    }


    public void AddDinoSkillTargetEffect(TargetDinoEffectController effect)
    {
        string eventName = effect.eventName;

        EventEffects eventEffects = null;
        if(m_events.ContainsKey(eventName))
            m_events[eventName].AddEffect(effect);
        else
        {
            eventEffects = new EventEffects();
            eventEffects.AddEffect(effect);
            m_events.Add(eventName, eventEffects);
        }
    }

    public List<TargetDinoEffectController> GetTargetDinoEffects(string eventName)
    {
        List<TargetDinoEffectController> effects = null;
        if(m_events.ContainsKey(eventName))
            effects = m_events[eventName].effects;

        return effects;
    }
}

public class MultiEventSkillEffect
{
    protected class MultiEventEffects
    {
        protected class EventEffects
        {
            private List<MultiEventDinoEffectController> m_multiEventEffects;
            public List<MultiEventDinoEffectController> multiEventEffects{get{return m_multiEventEffects;}}

            public EventEffects()
            {
                m_multiEventEffects = new List<MultiEventDinoEffectController>();
            }

            public void MakeEffects(MultiEventDinoEffectController multiEffect)
            {
                m_multiEventEffects.Add(multiEffect);
                int count = multiEffect.count - 1;
                if(count <= 0)
                    return;
                    
                GameObject effectObject = multiEffect.gameObject;
                Transform parent = multiEffect.transform.parent;
                for(int i = 0; i < count; i++)
                {
                    GameObject o = MonoBehaviour.Instantiate(effectObject) as GameObject;
                    o.transform.SetParent(parent);
                    o.transform.localPosition = multiEffect.transform.localPosition;
                    o.transform.localEulerAngles = multiEffect.transform.localEulerAngles;
                    o.transform.localScale = multiEffect.transform.localScale;
                    MultiEventDinoEffectController effect = o.GetComponent<MultiEventDinoEffectController>();
                    m_multiEventEffects.Add(effect);
                }
            }

            public void PlayEffects(int index)
            {
                int effectCount = m_multiEventEffects.Count;
                int effectIndex = index % effectCount;
                m_multiEventEffects[effectIndex].PlayDinoEffect();
            }
        }

        private List<EventEffects> m_multiEffects;
        private int m_playCount = 0;
        public int playCount{get{return m_playCount;}}

        public MultiEventEffects()
        {
            m_multiEffects = new List<EventEffects>();
        }

        public void AddMultiEventEffect(MultiEventDinoEffectController multiEffect)
        {
            EventEffects multiEventEffects = new EventEffects();
            multiEventEffects.MakeEffects(multiEffect);
            m_multiEffects.Add(multiEventEffects);
        }

        public void PlayMultiEventEffect()
        {
            foreach(EventEffects eventEffects in m_multiEffects)
            {
                eventEffects.PlayEffects(m_playCount);
            }

            m_playCount++;
        }

        public void InitializeMultiEventEffect()
        {
            m_playCount = 0;
        }

    }
    private Dictionary<string, MultiEventEffects> m_multiEvents;

    

    public MultiEventSkillEffect()
    {
        m_multiEvents = new Dictionary<string, MultiEventEffects>();
    }

    public void AddMultiEventSkillEffect(MultiEventDinoEffectController multiEffect)
    {
        string eventName = multiEffect.eventName;
        if(m_multiEvents.ContainsKey(eventName))
            m_multiEvents[eventName].AddMultiEventEffect(multiEffect);
        else
        {
            MultiEventEffects multiEventEffects = new MultiEventEffects();
            multiEventEffects.AddMultiEventEffect(multiEffect);
            m_multiEvents.Add(eventName, multiEventEffects);
        }
    }


    public void PlayMultiEventSkillEffect(string eventName)
    {
        if(!m_multiEvents.ContainsKey(eventName))
            return;
        
        m_multiEvents[eventName].PlayMultiEventEffect();
    }

    public void InitializeMultiEventSkillEffect()
    {
        foreach(KeyValuePair<string, MultiEventEffects> pair in m_multiEvents)
        {
            pair.Value.InitializeMultiEventEffect();
        }
    }

}

public class EventSkillEffects
{
    protected class EventEffects
    {
        private List<DinoEffectController> m_effects;
        public List<DinoEffectController> effects{get{return m_effects;}}

        public void AddEffect(DinoEffectController _effect)
        {
            if(m_effects == null)
                m_effects = new List<DinoEffectController>();

            m_effects.Add(_effect);
        }
    }
    private Dictionary<string, EventEffects> m_events;

    public EventSkillEffects()
    {
        m_events = new Dictionary<string, EventEffects>();
    }

    public void AddDinoSkillEffect(DinoEffectController effect)
    {
        string eventName = effect.eventName;

        EventEffects eventEffects = null;
        if(m_events.ContainsKey(eventName))
            m_events[eventName].AddEffect(effect);
        else
        {
            eventEffects = new EventEffects();
            eventEffects.AddEffect(effect);
            m_events.Add(eventName, eventEffects);
        }

    }

    public List<DinoEffectController> GetDinoSkillEffects(string eventName)
    {
        List<DinoEffectController> effects = null;
        if(m_events.ContainsKey(eventName))
            effects = m_events[eventName].effects;

        return effects;
    }
}

public class DinoObject : MonoBehaviour
{
    private SkeletonAnimation m_skeletonAnimation;

    //private List<SpecialSkillStartEffectController> m_specialSkillStartEffects;

    //private List<AlwaysDinoEffectController> m_alwaysEffects;

    //중요 스킬 시작 이펙트 key - 애니메이션 이름, value - 발동되어야 할 시작 이펙트
    private Dictionary<string, EventSkillEffects> m_normalSkillEffects;

    private Dictionary<string, MultiEventSkillEffect> m_multiEventSkillEffects;

    //기본 피격 이펙트들 - 공격 애니메이션 시작후 9프레임에 터지는 이펙트가 기본 이펙트
    private Dictionary<string, TargetDinoEffects> m_normalTargetDinoEffects;

    //multi와 같이 한 애니메이션에서 여러 개의 이벤트가 발동될때 그에 맞춘 이펙트를 여러개 생성하여 띄워주는 경우
    private Dictionary<string, MultiEventTargetDinoEffects> m_multiEventTargetDinoEffects;
    

    private Transform m_targetTransform;

    private string m_playedEffectAnimation = string.Empty;

    private int m_dinoRenderOrder = 0;

    private DinoObject m_targetDino;

    private bool m_flip = false;
    private bool m_initialized = false;
    void Awake()
    {
        m_skeletonAnimation = GetComponent<SkeletonAnimation>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(m_initialized)
            return;

        m_dinoRenderOrder = m_skeletonAnimation.GetComponent<MeshRenderer>().sortingOrder;
        EnrollEvents();
        EnrollDinoEffects();
        //PlayAlwaysEffect();
        m_initialized = true;
    }

    public void SetDinoObject(bool _isEnemy = false)
    {
        if(m_initialized)
            return;

        m_flip = _isEnemy;
        m_dinoRenderOrder = m_skeletonAnimation.GetComponent<MeshRenderer>().sortingOrder;
        EnrollEvents();
        EnrollDinoEffects();
        //PlayAlwaysEffect();
        m_initialized = true;
    }

    void OnDestroy () {
        if (m_skeletonAnimation == null)
            return;
        
        m_skeletonAnimation.AnimationState.Start -= HandleAnimationStateStartEvent;
        m_skeletonAnimation.AnimationState.Event -= HandleAnimationStateEvents;
        m_skeletonAnimation.AnimationState.End -= HandleAnimationStateEndEvent;
    }

    private void EnrollEvents()
    {
        m_skeletonAnimation.AnimationState.Start += HandleAnimationStateStartEvent;
        m_skeletonAnimation.AnimationState.Event += HandleAnimationStateEvents;
        m_skeletonAnimation.AnimationState.End += HandleAnimationStateEndEvent;
    }

    private void EnrollDinoEffects()
    {
        //m_specialSkillStartEffects = new List<SpecialSkillStartEffectController>();
        //m_alwaysEffects = new List<AlwaysDinoEffectController>();
        m_normalSkillEffects = new Dictionary<string, EventSkillEffects>();
        m_multiEventSkillEffects = new Dictionary<string, MultiEventSkillEffect>();
        m_normalTargetDinoEffects = new Dictionary<string, TargetDinoEffects>();
        m_multiEventTargetDinoEffects = new Dictionary<string, MultiEventTargetDinoEffects>();
        DinoEffectController[] effects = GetComponentsInChildren<DinoEffectController>(true);
        foreach(DinoEffectController effect in effects)
        {
            effect.SetDepth(m_dinoRenderOrder);
            //if(effect is SpecialSkillStartEffectController)
            //{
            //    m_specialSkillStartEffects.Add(effect as SpecialSkillStartEffectController);
            //    continue;
            //}
            //MultiEventTargetDinoEffectController는 TargetDinoEffectController를 상속 받았기 때문에 순서상 제일 먼저 체크해줘야 한다.
            if(effect is MultiEventTargetDinoEffectController)
            {
                EnrollMultiEventTargetEffect(effect);
                continue;
            }
            if(effect is TargetDinoEffectController)
            {
                EnrollNormalTargetEffect(effect);
                continue;
            }

            if(effect is MultiEventDinoEffectController)
            {
                EnrollMultiEventSkillEffect(effect);
                continue;
            }

            //if(effect is AlwaysDinoEffectController)
            //{
            //    m_alwaysEffects.Add(effect as AlwaysDinoEffectController);
            //    continue;
            //}

            string animationName = effect.animationName;
            // Debug.Log(animationName);
            // Debug.Log(effect.eventName);
            if(m_normalSkillEffects.ContainsKey(animationName))
                m_normalSkillEffects[animationName].AddDinoSkillEffect(effect);
            else
            {
                EventSkillEffects skillEffects = new EventSkillEffects();
                skillEffects.AddDinoSkillEffect(effect);
                m_normalSkillEffects.Add(animationName, skillEffects);
            }
        }
    }

    private void EnrollMultiEventSkillEffect(DinoEffectController effect)
    {
        string animationName = effect.animationName;
        MultiEventDinoEffectController effectController = effect as MultiEventDinoEffectController;
        if(m_multiEventSkillEffects.ContainsKey(animationName))
            m_multiEventSkillEffects[animationName].AddMultiEventSkillEffect(effectController);
        else
        {
            MultiEventSkillEffect multiEventSkillEffect = new MultiEventSkillEffect();
            multiEventSkillEffect.AddMultiEventSkillEffect(effectController);
            m_multiEventSkillEffects.Add(animationName, multiEventSkillEffect);
        }        
    }

    private void EnrollNormalTargetEffect(DinoEffectController effect)
    {
        string animationName = effect.animationName;
        TargetDinoEffectController targetDinoEffect = effect as TargetDinoEffectController;
        targetDinoEffect.SetOriginParent();

        if(m_flip)
            targetDinoEffect.SetFlip();

        if(m_normalTargetDinoEffects.ContainsKey(animationName))
            m_normalTargetDinoEffects[animationName].AddDinoSkillTargetEffect(targetDinoEffect);
        else
        {
            TargetDinoEffects targetEffects = new TargetDinoEffects();
            targetEffects.AddDinoSkillTargetEffect(targetDinoEffect);
            m_normalTargetDinoEffects.Add(animationName, targetEffects);
        }
    }

    private void EnrollMultiEventTargetEffect(DinoEffectController effect)
    {
        string animationName = effect.animationName;
        MultiEventTargetDinoEffectController multiEventTargetEffect = effect as MultiEventTargetDinoEffectController;
        multiEventTargetEffect.SetOriginParent();

        if(m_flip)
            multiEventTargetEffect.SetFlip();

        if(m_multiEventTargetDinoEffects.ContainsKey(animationName))
            m_multiEventTargetDinoEffects[animationName].AddMultiEventSkillEffect(multiEventTargetEffect);
        else
        {
            MultiEventTargetDinoEffects multiEventTargetDinoEffects = new MultiEventTargetDinoEffects();
            multiEventTargetDinoEffects.AddMultiEventSkillEffect(multiEventTargetEffect);
            m_multiEventTargetDinoEffects.Add(animationName, multiEventTargetDinoEffects);
        }
    }



    /// <summary>
    /// 애니메이션이 시작할때의 이펙트가 나온다.
    /// 1. 상위 공룡의 스킬 스타트 이펙트 (엘그라시아 얼음)
    /// 2. 기본 피격 이펙트 - ex> attack_1의 9프레임에 나오는 피격 이펙트
    /// </summary>
    /// <param name="entry"></param>
    private void HandleAnimationStateStartEvent(TrackEntry entry)
    {
        string animationName = entry.Animation.Name;
        // if(m_normalSkillEffects.ContainsKey(animationName))
        // {
        //     string eventName = "start";
        //     List<DinoEffectController> effects = m_normalSkillEffects[animationName].GetDinoSkillEffects(eventName);

        //     if(effects != null)
        //     {
        //         foreach(DinoEffectController effect in effects)
        //             effect.PlayDinoEffect();
        //     }
        // }

        if(m_targetTransform == null)
            return;

        if(m_normalTargetDinoEffects.ContainsKey(animationName))
        {
            string eventName = "none";
            List<TargetDinoEffectController> effects = m_normalTargetDinoEffects[animationName].GetTargetDinoEffects(eventName);
            if(effects != null)
            {
                foreach(TargetDinoEffectController effect in effects)
                    effect.PlayTargetEffect(this, m_targetTransform);
            }
        }
    }

    /// <summary>
    /// 스킬에 관한 이벤트 이펙트
    /// 1. 애니메이션에서 어떠한 이벤트가 발동되면 그에 맞춘 스킬 이펙트를 보여준다.
    /// 2. 애니메이션에서 발동한 이벤트에 대하여 그에 맞춘 피격 이펙트가 있다면 보여준다.
    /// </summary>
    /// <param name="entry"></param>
    /// <param name="e"></param>
    private void HandleAnimationStateEvents(TrackEntry entry, Spine.Event e)
    {
        string animationName = entry.Animation.Name;
        string eventName = e.Data.Name;
        if(m_normalSkillEffects.ContainsKey(animationName))
        {
            
            List<DinoEffectController> effects = m_normalSkillEffects[animationName].GetDinoSkillEffects(eventName);
            if(effects != null)
            {
                foreach(DinoEffectController effect in effects)
                    effect.PlayDinoEffect();
            }
        }

        if(m_multiEventSkillEffects.ContainsKey(animationName))
        {
            MultiEventSkillEffect multiEventEffects = m_multiEventSkillEffects[animationName]; 
            multiEventEffects.PlayMultiEventSkillEffect(eventName);
        }

        if(m_targetTransform == null)
            return;

        if(m_normalTargetDinoEffects.ContainsKey(animationName))
        {
            List<TargetDinoEffectController> effects = m_normalTargetDinoEffects[animationName].GetTargetDinoEffects(eventName);
            if(effects != null)
            {
                foreach(TargetDinoEffectController effect in effects)
                    effect.PlayTargetEffect(m_targetTransform);
            }
        }

        if(m_multiEventTargetDinoEffects.ContainsKey(animationName))
        {
            MultiEventTargetDinoEffects multiEventTargetEffects = m_multiEventTargetDinoEffects[animationName];
            multiEventTargetEffects.PlayMultiEventTargetSkillEffect(eventName, m_targetTransform);
        }

    }

    private void HandleAnimationStateEndEvent(TrackEntry entry)
    {
        string animationName = entry.Animation.Name;
        if(m_playedEffectAnimation.Equals(animationName))
        {
            m_multiEventSkillEffects[animationName].InitializeMultiEventSkillEffect();
            m_playedEffectAnimation = string.Empty;
            // m_targetTransform = null;
        }
    }




    // public void PlayTargetEffect(Transform target, string animationName)
    // {
        // if(m_normalTargetDinoEffects.ContainsKey(animationName) == false)
        //     return;
            
        // m_targetTransform = target;

        // List<TargetDinoEffectController> effects = m_normalTargetDinoEffects[animationName];
        // foreach(TargetDinoEffectController effect in effects)
        //     effect.PlayTargetEffect(this, m_targetTransform);

        // m_playedEffectAnimation = animationName;
    // }

    public void SetTarget(Transform target)
    {
        m_targetTransform = target;
    }
    

    public void SetAnimation(string Name)
    {
        m_skeletonAnimation.AnimationName = Name;
    }

    public void SetAnimation(string Name, bool Loop)
    {
        //m_skeletonAnimation.AnimationName = Name;
        //m_skeletonAnimation.loop = Loop;

        m_skeletonAnimation.AnimationState.SetAnimation(0, Name, Loop);
    }

    public void AddAnimation(string Name, bool Loop)
    {
        m_skeletonAnimation.AnimationState.AddAnimation(0, Name, Loop, 0.0f);
    }
}
