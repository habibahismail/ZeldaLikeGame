using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bebaSpace
{
    public class GameManager : MonoBehaviour
    {
        #region Instance declaration
        private static GameManager _instance;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent<GameManager>();
                    obj.name = typeof(GameManager).ToString();
                }

                return _instance;
            }
        }

        #endregion

        [Header("Game Properties")]
        public bool bgmPersist;

        [SerializeField] List<ScriptableObject> objects = new List<ScriptableObject>();

        #region Awake
        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }

        }
        #endregion

        private void OnEnable()
        {
            objects = SaveLoad.LoadScriptables(objects);
        }

        private void OnDisable()
        {
            SaveLoad.SaveScriptables(objects);
        }

    }
}
