using PROG_WEB_APP.Models;

namespace PROG_WEB_APP.ViewModels
{
    public class FarmerProductView
    {

        public class ProductFarmerViewModel
        {
            public Product Product { get; set; }
            public Farmer Farmer { get; set; }
        }
    }
}
