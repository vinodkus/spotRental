using SMT.SpotRental.Shared.Entities;
using System.Collections.Generic;


namespace SMT.SpotRental.UI.Response
{
    public class UserResponse: ResponseBase
    { 
        public User userEntity { get; set; }
    }
    public class EmployeeResponse : ResponseBase
    {
        public IList<User> userList { get; set; }
        
    }
}