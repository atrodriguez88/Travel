using ModelTravel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebTravel.Models;

namespace WebTravel.ViewModels
{
    public class IndexViewModel : BaseModelo
    {
        public List<Territory> Territories { get; set; }
        public List<Supplier> Suppliers { get; set; }
    }
}