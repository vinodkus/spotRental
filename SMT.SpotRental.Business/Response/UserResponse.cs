using System;
using System.Collections.Generic;
using SMT.SpotRental.Business.Entities;

namespace SMT.SpotRental.Business.Response
{
    public class UserResponse: ResponseBase
    {
        public UserEntity userEntity { get; set; }
    }
    public class EmployeeResponse : ResponseBase
    {
        public IList<UserEntity> userList { get; set; }
    }
}
