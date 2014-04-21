using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Data.Linq;

namespace SupCarLocator.Model
{
    public class GeoDataContext : System.Data.Linq.DataContext
    
    {

            public GeoDataContext()
                : base("isostore:/geo.sdf",
                    new System.Data.Linq.Mapping.AttributeMappingSource())
            {
                if (!DatabaseExists())
                {
                    CreateDatabase();
                }
            }

            public Table<Geolocalisation> Geolocalisations
            {
                get { return base.GetTable<Geolocalisation>(); }
            }
        }    

}
