﻿

namespace SMT.SpotRental.Business.Entities
{
    public class MenuEntity
    {
        public int UserID { get; set; }
        public int ActionID { get; set; }
        public string ActionName { get; set; }
        public string ActionText { get; set; }
        public int RootID { get; set; }
        public bool Active { get; set; }
        public string ControllerName { get; set; }
        public int MenuOrder { get; set; }
        public int IsMenuItems { get; set; }
        public string Icon { get; set; }
    }
}
