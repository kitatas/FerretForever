using Ferret.Common.Data.DataStore;
using UnityEngine;

namespace Ferret.Common.Domain.Repository
{
    public sealed class LanguageRepository
    {
        private readonly LanguageTable _languageTable;

        public LanguageRepository(LanguageTable languageTable)
        {
            _languageTable = languageTable;
            _languageTable.Init();
        }

        public LanguageJsonData FindJsonData(LanguageType type)
        {
            return _languageTable.jsonData
                .Find(x => x.type == type);
        }

        public Sprite GetHintImage(LanguageType type)
        {
            return _languageTable.dataList
                .Find(x => x.languageType == type)
                .hintImage;
        }
    }
}