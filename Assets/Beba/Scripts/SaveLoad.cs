using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace bebaSpace
{
    public class SaveLoad : MonoBehaviour
    {
       public static void SaveScriptables(List<ScriptableObject> so)
        {
            for (int i = 0; i < so.Count; i++)
            {
                FileStream file = File.Create(Application.persistentDataPath +
                    string.Format("/{0}.dat", i));
                BinaryFormatter binary = new BinaryFormatter();
                var json = JsonUtility.ToJson(so[i]);
                binary.Serialize(file, json);
                file.Close();
            }
        }

        public static List<ScriptableObject> LoadScriptables(List<ScriptableObject> so)
        {
            for (int i = 0; i < so.Count; i++)
            {
                if(File.Exists(Application.persistentDataPath +
                    string.Format("/{0}.dat", i)))
                {
                    FileStream file = File.Open(Application.persistentDataPath +
                    string.Format("/{0}.dat", i), FileMode.Open);

                    BinaryFormatter binary = new BinaryFormatter();
                    JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file),
                        so[i]);
                    file.Close();
                        
                }
            }

            return so;
        }
    }
}
