using UnityEngine;
using ScriptableObjectArchitecture;

namespace HarvestCode.Systems {
    public class GameManager : MonoBehaviour {

        // This is a root of leveldata, form here data are send to every object who needs it.
        [Header("Level to be load"), SerializeField] public LevelDataSO levelData = default; 

        [Header("Broadcasting Events")]
        [SerializeField] private LevelDataSOGameEvent LoadLevelEvent = default(LevelDataSOGameEvent);
        [SerializeField] private GameEvent GrowthEvent = default(GameEvent);
        [SerializeField] private GameEvent GameStartEvent = default(GameEvent);
        [SerializeField] private GameEvent SeedQueueUpdateEvent = default(GameEvent);

        private void Start() {
            GameStartEvent.Raise();
            LoadLevel();
        }

        public void OnMovementEnd() {
            GrowthEvent.Raise();
        }

        private void LoadLevel() {
            if(levelData != null) {

                LoadLevelEvent.Raise(levelData);

                SeedQueueUpdateEvent.Raise();

            } else {
                Debug.LogError("There is no level to load. Make sure you set levelData variable in GameManager.");
            }
        }
    }
}