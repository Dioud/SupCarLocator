using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace SupCarLocator.Model
{
   public  class GeolocalisationServices
    {
        public List<Geolocalisation> GetGeolocalisations()
        {
            using (var geolocalisationsContext = new GeoDataContext())
            {
                var geolocalisations = geolocalisationsContext.Geolocalisations.ToList();
                return geolocalisations;
            }
        }

        public Geolocalisation GetGeoById(int geoId)
        {
            using (var geolocalisationsContext = new GeoDataContext())
            {
                var geoQuery =
                            from b in geolocalisationsContext.Geolocalisations
                            where b.Id == geoId
                            select b;

                var geo = geoQuery.First();
                return geo;
            }
        }
        public Geolocalisation GetLastGeo()
        {
            using (var geolocalisationsContext = new GeoDataContext())
            {
                var geoQuery =
                            from b in geolocalisationsContext.Geolocalisations
                            orderby b.Id descending
                            select b;
                var geo = geoQuery.First();
                return geo;
            }
        }

        public void AddGeo(Geolocalisation geo)
        {
            using (var geolocalisationsContext = new GeoDataContext())
            {
                geolocalisationsContext.Geolocalisations.InsertOnSubmit(geo);
                geolocalisationsContext.SubmitChanges();
            }
        }

        public void DeleteGeo()
        {
            using (var geolocalisationsContext = new GeoDataContext())
            {
                var geoQuery =
                            from b in geolocalisationsContext.Geolocalisations
                            select b;

                var geo = geoQuery.First();
                geolocalisationsContext.Geolocalisations.DeleteAllOnSubmit(geoQuery);
                geolocalisationsContext.SubmitChanges();              
            }
        }
    }
}
 
      