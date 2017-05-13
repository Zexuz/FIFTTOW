using Android.Content;
using FIFTTOW.Interfaces;

namespace FIFTTOW.Servicies
{
    public class DebugLogService:ILogService
    {
        private readonly Context _context;

        public DebugLogService(Context context)
        {
            _context = context;
        }

        public void Log(string tag, string message)
        {
            System.Diagnostics.Debug.WriteLine($"{tag}: {message}");
        }

        public void Debug(string message)
        {
            System.Diagnostics.Debug.WriteLine($"DEBUG, {_context.Class}: {message}");
        }
    }
}