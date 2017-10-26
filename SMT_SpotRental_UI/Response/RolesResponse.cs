using System.Collections.Generic;
using SMT.SpotRental.Shared.Entities;

namespace SMT.SpotRental.UI.Response
{
    public class RolesResponse : ResponseBase
    {
        public IList<Roles> listRoles { get; set; }
    }
}