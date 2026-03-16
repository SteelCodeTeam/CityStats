using Colossal.Logging;
using Game;
using Game.Modding;
using Game.SceneFlow;

namespace CityStats
{
    public class Mod : IMod
    {
        public static ILog log = LogManager.GetLogger($"{nameof(CityStats)}.{nameof(Mod)}").SetShowsErrorsInUI(false);

        public void OnLoad(UpdateSystem updateSystem)
        {
            log.Info(nameof(OnLoad));

            if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
            {
                log.Info($"Current mod asset at {asset.path}");
            }

            updateSystem.UpdateAt<VehicleCountSystem>(SystemUpdatePhase.GameSimulation);
            updateSystem.UpdateAt<CityStatsUISystem>(SystemUpdatePhase.UIUpdate);

            log.Info("VehicleCountSystem registered");
            log.Info("CityStatsUISystem registered");
        }

        public void OnDispose()
        {
            log.Info(nameof(OnDispose));
        }
    }
}