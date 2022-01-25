namespace Ferret.Common.Data.DataStore
{
    public sealed class UserRecord
    {
        public string userName;
        public int playCount;
        public RecordData highRecord;
        public RecordData totalRecord;

        public UserRecord()
        {
            userName = "";
            playCount = 0;
            highRecord = new RecordData
            {
                score = 0,
                victimCount = 0,
            };
            totalRecord = new RecordData
            {
                score = 0,
                victimCount = 0,
            };
        }
    }

    public sealed class RecordData
    {
        public float score;
        public int victimCount;
    }
}