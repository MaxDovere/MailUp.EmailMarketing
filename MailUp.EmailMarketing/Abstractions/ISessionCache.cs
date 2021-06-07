namespace MailUp.EmailMarketing.Abstractions
{
    public interface ISessionCache
    {
        void LoadCache();
        void SaveCache();
        void DeleteCache();
    }
}