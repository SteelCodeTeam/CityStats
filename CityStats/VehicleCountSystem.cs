using Colossal.Logging;
using Game;
using Game.Common;
using Game.Tools;
using Game.Vehicles;
using Unity.Entities;
using UnityEngine;

namespace CityStats
{
    public partial class VehicleCountSystem : GameSystemBase
    {
        private static readonly ILog Log = LogManager.GetLogger($"{nameof(CityStats)}.{nameof(VehicleCountSystem)}").SetShowsErrorsInUI(false);

        private EntityQuery _citizenQuery;
        private EntityQuery _serviceVehicleQuery;
        private EntityQuery _parkedCarQuery;
        private EntityQuery _movingCarQuery;
        private EntityQuery _deliveryVehicleQuery;
        private EntityQuery _bikesQuery;
        private EntityQuery _taxisVehicleQuery;
        private EntityQuery _trucksVehicleQuery;

        private int _frameSkip;

        public static int CurrentCitizenCount { get; private set; }
        public static int CurrentServiceVehicleCount { get; private set; }
        public static int CurrentParkedCarCount { get; private set; }
        public static int CurrentMovingCarCount { get; private set; }
        public static int CurrentDeliveryVehicleCount { get; private set; }
        public static int CurrentTaxisVehicleCount { get; private set; }
        public static int CurrentTrucksVehicleCount { get; private set; }
        
        public static int CurrentBikesCount { get; private set; }

        protected override void OnCreate()
        {
            base.OnCreate();

            _citizenQuery = GetEntityQuery(new EntityQueryDesc
            {
                All = new ComponentType[]
                {
                    ComponentType.ReadOnly<Game.Creatures.Human>(),
                    ComponentType.ReadOnly<Game.Objects.Moving>(),
                },
                Any = new ComponentType[]
                {
                    ComponentType.ReadOnly<Game.Creatures.HumanNavigation>(),
                    ComponentType.ReadOnly<Game.Creatures.HumanCurrentLane>(),
                },
                None = new ComponentType[]
                {
                    ComponentType.ReadOnly<Game.Creatures.Animal>(),
                    ComponentType.ReadOnly<Game.Creatures.Pet>(),
                    ComponentType.ReadOnly<Game.Creatures.Wildlife>(),
                    ComponentType.ReadOnly<Game.Creatures.CurrentVehicle>(),
                    ComponentType.ReadOnly<Deleted>(),
                    ComponentType.ReadOnly<Temp>(),
                    ComponentType.ReadOnly<Game.Objects.Unspawned>(),
                    ComponentType.ReadOnly<Game.Objects.Placeholder>(),
                }
            });

            _serviceVehicleQuery = GetEntityQuery(new EntityQueryDesc
            {
                All = new ComponentType[]
                {
                    ComponentType.ReadOnly<Game.Vehicles.Car>(),
                    ComponentType.ReadOnly<Game.Objects.Moving>(),
                },
                Any = new ComponentType[]
                {
                    ComponentType.ReadOnly<Game.Vehicles.Ambulance>(),
                    ComponentType.ReadOnly<Game.Vehicles.FireEngine>(),
                    ComponentType.ReadOnly<Game.Vehicles.GarbageTruck>(),
                    ComponentType.ReadOnly<Game.Vehicles.PoliceCar>(),
                    ComponentType.ReadOnly<Game.Vehicles.PrisonerTransport>(),
                    ComponentType.ReadOnly<Game.Vehicles.ParkMaintenanceVehicle>(),
                    ComponentType.ReadOnly<Game.Vehicles.MaintenanceVehicle>(),
                    ComponentType.ReadOnly<Game.Vehicles.Hearse>(),
                },
                None = new ComponentType[]
                {
                    ComponentType.ReadOnly<Game.Vehicles.ParkedCar>(),
                    ComponentType.ReadOnly<Deleted>(),
                    ComponentType.ReadOnly<Temp>(),
                    ComponentType.ReadOnly<Game.Objects.Unspawned>(),
                    ComponentType.ReadOnly<Game.Objects.Placeholder>(),
                }
            });

            _parkedCarQuery = GetEntityQuery(new EntityQueryDesc
            {
                All = new ComponentType[]
                {
                    ComponentType.ReadOnly<Game.Vehicles.ParkedCar>(),
                },
                None = new ComponentType[]
                {
                    ComponentType.ReadOnly<Deleted>(),
                    ComponentType.ReadOnly<Temp>(),
                    ComponentType.ReadOnly<Game.Objects.Unspawned>(),
                    ComponentType.ReadOnly<Game.Objects.Placeholder>(),
                    ComponentType.ReadOnly<Game.Objects.Moving>(),
                }
            });

            _movingCarQuery = GetEntityQuery(new EntityQueryDesc
            {
                All = new ComponentType[]
                {
                    ComponentType.ReadOnly<Game.Vehicles.Vehicle>(),
                    ComponentType.ReadOnly<Game.Vehicles.Car>(),
                    ComponentType.ReadOnly<Game.Vehicles.PersonalCar>(),
                    ComponentType.ReadOnly<Game.Objects.Moving>(),
                },
                Any = new ComponentType[]
                {
                    ComponentType.ReadOnly<Game.Vehicles.CarNavigation>(),
                    ComponentType.ReadOnly<Game.Vehicles.CarCurrentLane>(),
                },
                None = new ComponentType[]
                {
                    ComponentType.ReadOnly<Game.Vehicles.WorkVehicle>(),
                    ComponentType.ReadOnly<Game.Vehicles.ParkedCar>(),
                    ComponentType.ReadOnly<Deleted>(),
                    ComponentType.ReadOnly<Temp>(),
                    ComponentType.ReadOnly<Game.Objects.Unspawned>(),
                    ComponentType.ReadOnly<Game.Objects.Placeholder>(),
                }
            });
            
            _taxisVehicleQuery = GetEntityQuery(new EntityQueryDesc
            {
                All = new ComponentType[]
                {
                    ComponentType.ReadOnly<Game.Vehicles.Vehicle>(),
                    ComponentType.ReadOnly<Game.Vehicles.Car>(),
                    ComponentType.ReadOnly<Game.Vehicles.Taxi>(),
                    ComponentType.ReadOnly<Game.Objects.Moving>(),
                },
                Any = new ComponentType[]
                {
                    ComponentType.ReadOnly<Game.Vehicles.CarNavigation>(),
                    ComponentType.ReadOnly<Game.Vehicles.CarCurrentLane>(),
                },
                None = new ComponentType[]
                {
                    ComponentType.ReadOnly<Game.Vehicles.WorkVehicle>(),
                    ComponentType.ReadOnly<Game.Vehicles.ParkedCar>(),
                    ComponentType.ReadOnly<Deleted>(),
                    ComponentType.ReadOnly<Temp>(),
                    ComponentType.ReadOnly<Game.Objects.Unspawned>(),
                    ComponentType.ReadOnly<Game.Objects.Placeholder>(),
                }
            });
            

            _trucksVehicleQuery = GetEntityQuery(new EntityQueryDesc
            {
                All = new ComponentType[]
                {
                    ComponentType.ReadOnly<Game.Vehicles.Vehicle>(),
                    ComponentType.ReadOnly<Game.Vehicles.Car>(),
                    ComponentType.ReadOnly<Game.Objects.Moving>(),
                    ComponentType.ReadOnly<Game.Vehicles.DeliveryTruck>(),
                },
                Any = new ComponentType[]
                {
                    ComponentType.ReadOnly<Game.Vehicles.CarNavigation>(),
                    ComponentType.ReadOnly<Game.Vehicles.CarCurrentLane>(),
                },
                None = new ComponentType[]
                {
                    ComponentType.ReadOnly<Game.Vehicles.WorkVehicle>(),
                    ComponentType.ReadOnly<Game.Vehicles.ParkedCar>(),
                    ComponentType.ReadOnly<Deleted>(),
                    ComponentType.ReadOnly<Temp>(),
                    ComponentType.ReadOnly<Game.Objects.Unspawned>(),
                    ComponentType.ReadOnly<Game.Objects.Placeholder>(),
                }
            });

            _deliveryVehicleQuery = GetEntityQuery(new EntityQueryDesc
            {
                All = new ComponentType[]
                {
                    ComponentType.ReadOnly<Game.Vehicles.Vehicle>(),
                    ComponentType.ReadOnly<Game.Vehicles.Car>(),
                    ComponentType.ReadOnly<Game.Objects.Moving>(),
                    ComponentType.ReadOnly<Game.Vehicles.GoodsDeliveryVehicle>(),
                },
                Any = new ComponentType[]
                {
                    ComponentType.ReadOnly<Game.Vehicles.CarNavigation>(),
                    ComponentType.ReadOnly<Game.Vehicles.CarCurrentLane>(),
                },
                None = new ComponentType[]
                {
                    ComponentType.ReadOnly<Game.Vehicles.WorkVehicle>(),
                    ComponentType.ReadOnly<Game.Vehicles.ParkedCar>(),
                    ComponentType.ReadOnly<Deleted>(),
                    ComponentType.ReadOnly<Temp>(),
                    ComponentType.ReadOnly<Game.Objects.Unspawned>(),
                    ComponentType.ReadOnly<Game.Objects.Placeholder>(),
                }
            });
            
            _bikesQuery = GetEntityQuery(new EntityQueryDesc
            {
                All = new ComponentType[]
                {
                    ComponentType.ReadOnly<Game.Vehicles.Bicycle>(),
                    ComponentType.ReadOnly<Game.Objects.Moving>(),
                },
                None = new ComponentType[]
                {
                    ComponentType.ReadOnly<Game.Vehicles.ParkedCar>(),
                    ComponentType.ReadOnly<Deleted>(),
                    ComponentType.ReadOnly<Temp>(),
                    ComponentType.ReadOnly<Game.Objects.Unspawned>(),
                    ComponentType.ReadOnly<Game.Objects.Placeholder>(),
                }
            });

            Log.Info("VehicleCountSystem created");
        }

        protected override void OnUpdate()
        {
            _frameSkip++;
            if (_frameSkip < 30)
            {
                return;
            }

            _frameSkip = 0;

            CurrentCitizenCount = _citizenQuery.CalculateEntityCount();
            CurrentServiceVehicleCount = _serviceVehicleQuery.CalculateEntityCount();
            CurrentParkedCarCount = _parkedCarQuery.CalculateEntityCount();
            CurrentMovingCarCount = _movingCarQuery.CalculateEntityCount();
            CurrentDeliveryVehicleCount = _deliveryVehicleQuery.CalculateEntityCount();
            CurrentTaxisVehicleCount = _taxisVehicleQuery.CalculateEntityCount();
            CurrentTrucksVehicleCount = _trucksVehicleQuery.CalculateEntityCount();
            CurrentBikesCount = _bikesQuery.CalculateEntityCount();
        }

        protected override void OnDestroy()
        {
            Log.Info("VehicleCountSystem destroyed");
            base.OnDestroy();
        }
    }
}