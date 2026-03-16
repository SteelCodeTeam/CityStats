using Game;
using Game.UI;
using Colossal.UI;
using Colossal.UI.Binding;

namespace CityStats
{
    public partial class CityStatsUISystem : UISystemBase
    {
        private ValueBinding<int> _citizenCountBinding;
        private ValueBinding<int> _serviceVehicleCountBinding;
        private ValueBinding<int> _parkedCarCountBinding;
        private ValueBinding<int> _movingCarCountBinding;
        private TriggerBinding _refreshTrigger;

        protected override void OnCreate()
        {
            base.OnCreate();

            _citizenCountBinding = new ValueBinding<int>("citystats", "citizenCount", 0);
            AddBinding(_citizenCountBinding);

            _serviceVehicleCountBinding = new ValueBinding<int>("citystats", "serviceVehicleCount", 0);
            AddBinding(_serviceVehicleCountBinding);

            _parkedCarCountBinding = new ValueBinding<int>("citystats", "parkedCarCount", 0);
            AddBinding(_parkedCarCountBinding);

            _movingCarCountBinding = new ValueBinding<int>("citystats", "movingCarCount", 0);
            AddBinding(_movingCarCountBinding);

            _refreshTrigger = new TriggerBinding("citystats", "requestRefresh", Refresh);
            AddBinding(_refreshTrigger);
        }

        private void Refresh()
        {
            _citizenCountBinding.Update(VehicleCountSystem.CurrentCitizenCount);
            _serviceVehicleCountBinding.Update(VehicleCountSystem.CurrentServiceVehicleCount);
            _parkedCarCountBinding.Update(VehicleCountSystem.CurrentParkedCarCount);
            _movingCarCountBinding.Update(VehicleCountSystem.CurrentMovingCarCount);
        }
    }
}