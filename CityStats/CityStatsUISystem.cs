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
        private ValueBinding<int> _deliveryVehicleCountBinding;
        private ValueBinding<int> _taxisVehicleCountBinding;
        private ValueBinding<int> _trucksVehicleCountBinding;
        private ValueBinding<int> _bikesCountBinding;
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

            _deliveryVehicleCountBinding = new ValueBinding<int>("citystats", "deliveryVehicleCount", 0);
            AddBinding(_deliveryVehicleCountBinding);
            
            _taxisVehicleCountBinding = new ValueBinding<int>("citystats", "taxisVehicleCount", 0);
            AddBinding(_taxisVehicleCountBinding);
            
            _trucksVehicleCountBinding = new ValueBinding<int>("citystats", "trucksVehicleCount", 0);
            AddBinding(_trucksVehicleCountBinding);
            
            _bikesCountBinding = new ValueBinding<int>("citystats", "bikesCount", 0);
            AddBinding(_bikesCountBinding);
            
            _refreshTrigger = new TriggerBinding("citystats", "requestRefresh", Refresh);
            AddBinding(_refreshTrigger);
            
            
        }

        private void Refresh()
        {
            _citizenCountBinding.Update(VehicleCountSystem.CurrentCitizenCount);
            _serviceVehicleCountBinding.Update(VehicleCountSystem.CurrentServiceVehicleCount);
            _parkedCarCountBinding.Update(VehicleCountSystem.CurrentParkedCarCount);
            _movingCarCountBinding.Update(VehicleCountSystem.CurrentMovingCarCount);
            _deliveryVehicleCountBinding.Update(VehicleCountSystem.CurrentDeliveryVehicleCount);
            _taxisVehicleCountBinding.Update(VehicleCountSystem.CurrentTaxisVehicleCount);
            _trucksVehicleCountBinding.Update(VehicleCountSystem.CurrentTrucksVehicleCount);
            _bikesCountBinding.Update(VehicleCountSystem.CurrentBikesCount);
            
        }
    }
}