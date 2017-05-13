namespace FIFTTOW.Interfaces
{
    public interface ILogService
    {
        void Log(string tag, string message);
        void Debug(string message);
    }
}