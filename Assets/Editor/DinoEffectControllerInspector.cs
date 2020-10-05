using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Spine;
using Spine.Unity;
using Spine.Unity.Editor;

// namespace Spine.Unity.Editor {

// 	using Editor = UnityEditor.Editor;
// 	using Event = UnityEngine.Event;

    [CustomEditor(typeof(DinoEffectController)), CanEditMultipleObjects]
    public class DinoEffectControllerInspector : Editor
    {
        SerializedProperty animationName, eventName, effectDepthType;
        protected SkeletonAnimation skeletonAnimation;
        protected SkeletonData animationData;
        protected List<string> dinoAnimations;

        private List<string> m_eventList;

        DinoEffectController targetEffectController;

        private const string noneName = "none";
        // bool needsReset;

        protected virtual void OnEnable () {

            // Debug.Log("DinoEffectControllerInspector :: OnEnable");
            animationName = serializedObject.FindProperty("animationName");
            eventName = serializedObject.FindProperty("eventName");
            effectDepthType = serializedObject.FindProperty("effectDepthType");

            targetEffectController = (DinoEffectController)target;
            skeletonAnimation = targetEffectController.GetComponentInParent<SkeletonAnimation>();
			if(skeletonAnimation != null)
            {
                // Debug.Log(skeletonAnimation.gameObject.name);
                animationData = skeletonAnimation.skeletonDataAsset.GetSkeletonData(true);
                MakeAnimationList();
                MakeEventList();
            }
            else
            {
                dinoAnimations = new List<string>();
                dinoAnimations.Add(animationName.stringValue);
                m_eventList = new List<string>();
                m_eventList.Add(eventName.stringValue);

            }
		}

        private void MakeAnimationList()
        {
            if(animationData == null)
                return;

            dinoAnimations = new List<string>();
            dinoAnimations.Add(noneName);
            Spine.Animation[] animations = animationData.Animations.Items;
            for(int i = 0; i < animations.Length; i++)
            {
                dinoAnimations.Add(animations[i].Name);
            }
        }

        private void MakeEventList()
        {
            m_eventList = new List<string>();
            if(animationName.stringValue == noneName)
            {
                eventName.stringValue = noneName;
                targetEffectController.eventName = eventName.stringValue;
                m_eventList.Add(noneName);
                serializedObject.ApplyModifiedProperties();
                return;
            }    
            Spine.Animation anim = animationData.FindAnimation(animationName.stringValue);
            Timeline[] timelines = anim.Timelines.Items;
            m_eventList.Add(noneName);
            for(int i = 0; i < timelines.Length; i++)
            {
                EventTimeline eventTimeline = timelines[i] as EventTimeline;

                if(eventTimeline != null)
                {
                    Spine.Event[] events = eventTimeline.Events;
                    for(int k = 0; k < events.Length; k++)
                    {
                        string eventName = events[k].ToString();
                        if(m_eventList.Contains(eventName))
                            continue;
                        else
                            m_eventList.Add(eventName);
                        // Debug.Log(events[k].ToString());
                    }
                }
            }
            if(m_eventList.Contains(eventName.stringValue))
                return;

            eventName.stringValue = noneName;
            serializedObject.ApplyModifiedProperties();
        }

        override public void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            int selectedAnimationIndex = dinoAnimations.IndexOf(animationName.stringValue);
            selectedAnimationIndex = EditorGUILayout.Popup("Animation Name ", selectedAnimationIndex, dinoAnimations.ToArray());
            animationName.stringValue = dinoAnimations[selectedAnimationIndex];
            bool changedAnimation = EditorGUI.EndChangeCheck();
            if(changedAnimation)
            {
                serializedObject.ApplyModifiedProperties();
                MakeEventList();
            }

            EditorGUI.BeginChangeCheck();
            int selectedEventIndex = m_eventList.IndexOf(eventName.stringValue);
            selectedEventIndex = EditorGUILayout.Popup("Event Name ", selectedEventIndex, m_eventList.ToArray());
            eventName.stringValue = m_eventList[selectedEventIndex];
            bool changedEvent = EditorGUI.EndChangeCheck();
            if(changedEvent)
                serializedObject.ApplyModifiedProperties();

            EditorGUI.BeginChangeCheck();
            DinoEffectDepthType depthType = (DinoEffectDepthType)effectDepthType.enumValueIndex;
            effectDepthType.enumValueIndex = (int)(DinoEffectDepthType)EditorGUILayout.EnumPopup("Effect Depth ", depthType);
            bool changedDepth = EditorGUI.EndChangeCheck();
            if(changedDepth)
                serializedObject.ApplyModifiedProperties();

            // serializedObject.Update();
            // serializedObject.ApplyModifiedProperties();
            // serializedObject.Update();
            // bool changedDepth = EditorGUI.EndChangeCheck();
            // if(changedDepth)
            //     targetEffectController.effectDepthType = effectDepth
            // Debug.Log(changedAnimation);


            // Debug.Log(selectedIndex);
			// EditorGUILayout.PropertyField(animationName);

        }



    }
// }

