using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;


namespace SupCarLocator.Model
{
    [Table(Name = "Geolocalisations")]
    public class Geolocalisation
    {
        [Column(Name = "Id", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column(Name = "latitude")]
        public string latitude { get; set; }

        [Column(Name = "longitude")]
        public string longitude { get; set; }
        [Column(Name = "etage")]
        public string etage { get; set; }
    }
}
