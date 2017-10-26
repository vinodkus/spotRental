using SMT.SpotRental.Data.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.SpotRental.Data.Factory
{
    public class DataRepositoryFactory
    {
        public static IItemsRepository CreateItemRepository()
        {
            return new ItemsRepository();
        }
        public static IUserRepository CreateUserRepository()
        {
            return new UserRepository();
        }

        public static IVehicleBookingRepository CreateVehicleBookingRepository()
        {
            return new VehicleBookingRepository();
        }
    }
}
