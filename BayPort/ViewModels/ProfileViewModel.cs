using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Entities;

namespace BayPortColombia.ViewModels
{
    public class ProfileViewModel
    {
        public OutAgreement agreements { get; set; }
        public OutPayable payables { get; set; }

    }
}