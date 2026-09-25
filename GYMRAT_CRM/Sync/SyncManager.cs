using System;
using System.Threading;
using System.Threading.Tasks;
using CRM.winforms.LocalData;
using Microsoft.EntityFrameworkCore;

namespace CRM.winforms.Sync
{
    public class SyncManager
    {
        private static readonly Lazy<SyncManager> _instance = new(() => new SyncManager());
        public static SyncManager Instance => _instance.Value;

        private System.Threading.Timer? _timer;
        private int _companyId;
        private string? _dbPath;
        private CRM.winforms.ApiClient? _api;
        private volatile bool _running;

        public event Action<string>? StatusChanged;

        private SyncManager() { }

        public void Start(int companyId, CRM.winforms.ApiClient apiClient, TimeSpan? interval = null)
        {
            Stop();

            _companyId = companyId;
            _api = apiClient;

            _dbPath = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "GymRat", $"tenant_{_companyId}.sqlite");
            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(_dbPath) ?? ".");

            var period = interval ?? TimeSpan.FromSeconds(30);
            _timer = new System.Threading.Timer(_ => _ = TimerTickAsync(), null, TimeSpan.Zero, period);
        }

        public void Stop()
        {
            _timer?.Dispose();
            _timer = null;
        }

        private async Task TimerTickAsync()
        {
            if (_running) return; // avoid overlapping runs
            if (_api == null || string.IsNullOrWhiteSpace(_dbPath)) return;

            _running = true;
            try
            {
                StatusChanged?.Invoke("Syncing...");

                var options = new DbContextOptionsBuilder<LocalDbContext>()
                    .UseSqlite($"Data Source={_dbPath}")
                    .Options;

                using var localDb = new LocalDbContext(options);
                var svc = new SyncService(localDb, _api);
                await svc.SyncAsync();

                StatusChanged?.Invoke("Idle");
            }
            catch (Exception ex)
            {
                StatusChanged?.Invoke($"Sync failed: {ex.Message}");
            }
            finally
            {
                _running = false;
            }
        }

        public void QueueImmediateSync()
        {
            // trigger an immediate run on threadpool
            _ = TimerTickAsync();
        }
    }
}
