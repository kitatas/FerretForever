using Ferret.Common.Data.DataStore;
using Ferret.Common.Data.Entity;

namespace Ferret.OutGame.Domain.UseCase
{
    public sealed class UserRecordUseCase
    {
        private readonly UserRecordEntity _userRecordEntity;

        public UserRecordUseCase(UserRecordEntity userRecordEntity)
        {
            _userRecordEntity = userRecordEntity;
        }

        public string GetUid()
        {
            return _userRecordEntity.Get().uid;
        }

        public RecordData GetCurrentRecord()
        {
            return _userRecordEntity.Get().currentRecord;
        }

        public RecordData GetHighRecord()
        {
            return _userRecordEntity.Get().highRecord;
        }
    }
}