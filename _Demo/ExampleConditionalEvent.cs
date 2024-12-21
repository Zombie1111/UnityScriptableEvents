//UnityConditionalEvents by David Westberg https://github.com/Zombie1111/UnityConditionalEvents
using UnityEngine;

namespace zombCondEventsDemo
{
    public class ExampleConditionalEvent : MonoBehaviour
    {
        [Tooltip("Updates as positive on trigger enter and negative on trigger exit, press R at runtime to reset (Old input sys)")]
        [SerializeField] private zombCondEvents.ConditionalEvent condEvent = new();

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            //Should be called in editor in edit mode. Calling condEvent.EditorTick is not required but recommended
            //The condEvent.EditorTick function will only exist in editor so #if UNITY_EDITOR is required
            condEvent.EditorTick(gameObject);      
        }
#endif

        private void Awake()
        {
            //Tells all Events + Conditions to init + sets up ConditionalEvent transform/gameobject referemce
            condEvent.Init(gameObject);
        }

#if ENABLE_LEGACY_INPUT_MANAGER//I think this is for the old input system
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R) == false) return;

            condEvent.Reset(zombCondEvents.ConditionalEvent.ResetType.everything);
        }
#endif

        private void OnDestroy()
        {
            //Tells all Events + Conditions to destroy
            condEvent.WillDestroy();
        }

        private void OnTriggerEnter(Collider other)
        {
            //Checks conditions + trigger events if met
            condEvent.UpdateConditionalEvents(other.gameObject, true);
        }

        private void OnTriggerExit(Collider other)
        {
            //Checks conditions + trigger events if met
            condEvent.UpdateConditionalEvents(other.gameObject, false);
        }
    }
}

