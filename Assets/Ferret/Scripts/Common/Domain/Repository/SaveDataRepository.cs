using Ferret.Common.Data.DataStore;
using UnityEngine;

namespace Ferret.Common.Domain.Repository
{
    public sealed class SaveDataRepository
    {
        public SaveData Load()
        {
            var data = ES3.Load(MasterConfig.SAVE_KEY, defaultValue: "");

            if (string.IsNullOrEmpty(data))
            {
                return Create();
            }

            return JsonUtility.FromJson<SaveData>(data);
        }

        private SaveData Create()
        {
            var newData = new SaveData
            {
                uid = "",
                bgmVolume = 0.5f,
                seVolume = 0.5f,
            };
            Save(newData);

            return newData;
        }

        public void Save(SaveData saveData)
        {
            var data = JsonUtility.ToJson(saveData);
            ES3.Save(MasterConfig.SAVE_KEY, data);
        }
    }
}