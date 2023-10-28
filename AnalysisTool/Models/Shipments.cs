using System;
using System.Collections.Generic;

namespace AnalysisTool.Models
{
    public class Shipments : ICloneable
    {
        public Root shipment1 = new Root()
        {
            shipment = new Shipment()
            {
                to_location = new ToLocation()
                {
                    first_name = "test",
                    last_name = "test",
                    company = "test",
                    address1 = "399-443 Springfield Ave",
                    address2 = "",
                    city = "Newark",
                    state = "NJ",
                    postal_code = "07103",
                    country = "US",
                    phone = "000-000-0001"
                },
                from_location = new FromLocation()
                {
                    first_name = "test",
                    last_name = "test",
                    company = "test",
                    address1 = "399-443 Springfield Ave",
                    city = "Newark",
                    state = "NJ",
                    postal_code = "07103",
                    country = "US",
                    phone = "000-000-0000"
                },
                parcels = new List<Parcel>()
                    {
                        new Parcel()
                        {
                            weight = 1.0,
                            length = 6.0,
                            width = 6.0,
                            height = 6.0,
                            package_type = "parcel"
                        }
                    },
                label_format = "pdf",
                label_size = "4x6",
                include_services = new List<int> { 1041, 1042, 1043 },
            }
        };

        public Root shipment2 = new Root()
        {
            shipment = new Shipment()
            {
                to_location = new ToLocation()
                {
                    first_name = "test",
                    last_name = "test",
                    company = "test",
                    address1 = "1324 Arch St",
                    address2 = "",
                    city = "Philadelphia",
                    state = "PA",
                    postal_code = "19107",
                    country = "US",
                    phone = "000-000-0002"
                },
                from_location = new FromLocation()
                {
                    first_name = "test",
                    last_name = "test",
                    company = "test",
                    address1 = "399-443 Springfield Ave",
                    city = "Newark",
                    state = "NJ",
                    postal_code = "07103",
                    country = "US",
                    phone = "000-000-0000"
                },
                parcels = new List<Parcel>()
                    {
                        new Parcel()
                        {
                            weight = 1.0,
                            length = 6.0,
                            width = 6.0,
                            height = 6.0,
                            package_type = "parcel"
                        }
                    },
                label_format = "pdf",
                label_size = "4x6",
                include_services = new List<int> { 1041, 1042, 1043 },
            }
        };

        public Root shipment3 = new Root()
        {
            shipment = new Shipment()
            {
                to_location = new ToLocation()
                {
                    first_name = "test",
                    last_name = "test",
                    company = "test",
                    address1 = "2626 Delaware Ave",
                    address2 = "",
                    city = "Buffalo",
                    state = "NY",
                    postal_code = "14216",
                    country = "US",
                    phone = "000-000-0003"
                },
                from_location = new FromLocation()
                {
                    first_name = "test",
                    last_name = "test",
                    company = "test",
                    address1 = "399-443 Springfield Ave",
                    city = "Newark",
                    state = "NJ",
                    postal_code = "07103",
                    country = "US",
                    phone = "000-000-0000"
                },
                parcels = new List<Parcel>()
                    {
                        new Parcel()
                        {
                            weight = 1.0,
                            length = 6.0,
                            width = 6.0,
                            height = 6.0,
                            package_type = "parcel"
                        }
                    },
                label_format = "pdf",
                label_size = "4x6",
                include_services = new List<int> { 1041, 1042, 1043 },
            }
        };

        public Root shipment4 = new Root()
        {
            shipment = new Shipment()
            {
                to_location = new ToLocation()
                {
                    first_name = "test",
                    last_name = "test",
                    company = "test",
                    address1 = "900 Metropolitan Ave",
                    address2 = "Ste 2",
                    city = "Charlotte",
                    state = "NC",
                    postal_code = "28204",
                    country = "US",
                    phone = "000-000-0004"
                },
                from_location = new FromLocation()
                {
                    first_name = "test",
                    last_name = "test",
                    company = "test",
                    address1 = "399-443 Springfield Ave",
                    city = "Newark",
                    state = "NJ",
                    postal_code = "07103",
                    country = "US",
                    phone = "000-000-0000"
                },
                parcels = new List<Parcel>()
                    {
                        new Parcel()
                        {
                            weight = 1.0,
                            length = 6.0,
                            width = 6.0,
                            height = 6.0,
                            package_type = "parcel"
                        }
                    },
                label_format = "pdf",
                label_size = "4x6",
                include_services = new List<int> { 1041, 1042, 1043 },
            }
        };

        public Root shipment5 = new Root()
        {
            shipment = new Shipment()
            {
                to_location = new ToLocation()
                {
                    first_name = "test",
                    last_name = "test",
                    company = "test",
                    address1 = "1300 S Clinton St",
                    address2 = "",
                    city = "Chicago",
                    state = "IL",
                    postal_code = "60607",
                    country = "US",
                    phone = "000-000-0005"
                },
                from_location = new FromLocation()
                {
                    first_name = "test",
                    last_name = "test",
                    company = "test",
                    address1 = "399-443 Springfield Ave",
                    city = "Newark",
                    state = "NJ",
                    postal_code = "07103",
                    country = "US",
                    phone = "000-000-0000"
                },
                parcels = new List<Parcel>()
                    {
                        new Parcel()
                        {
                            weight = 1.0,
                            length = 6.0,
                            width = 6.0,
                            height = 6.0,
                            package_type = "parcel"
                        }
                    },
                label_format = "pdf",
                label_size = "4x6",
                include_services = new List<int> { 1041, 1042, 1043 },
            }
        };

        public Root shipment6 = new Root()
        {
            shipment = new Shipment()
            {
                to_location = new ToLocation()
                {
                    first_name = "test",
                    last_name = "test",
                    company = "test",
                    address1 = "2300 Metropolitan Ave",
                    address2 = "",
                    city = "Kansas City",
                    state = "KS",
                    postal_code = "66106",
                    country = "US",
                    phone = "000-000-0006"
                },
                from_location = new FromLocation()
                {
                    first_name = "test",
                    last_name = "test",
                    company = "test",
                    address1 = "399-443 Springfield Ave",
                    city = "Newark",
                    state = "NJ",
                    postal_code = "07103",
                    country = "US",
                    phone = "000-000-0000"
                },
                parcels = new List<Parcel>()
                    {
                        new Parcel()
                        {
                            weight = 1.0,
                            length = 6.0,
                            width = 6.0,
                            height = 6.0,
                            package_type = "parcel"
                        }
                    },
                label_format = "pdf",
                label_size = "4x6",
                include_services = new List<int> { 1041, 1042, 1043 },
            }
        };

        public Root shipment7 = new Root()
        {
            shipment = new Shipment()
            {
                to_location = new ToLocation()
                {
                    first_name = "test",
                    last_name = "test",
                    company = "test",
                    address1 = "757 E 20th Ave",
                    address2 = "",
                    city = "Denver",
                    state = "CO",
                    postal_code = "80205",
                    country = "US",
                    phone = "000-000-0007"
                },
                from_location = new FromLocation()
                {
                    first_name = "test",
                    last_name = "test",
                    company = "test",
                    address1 = "399-443 Springfield Ave",
                    city = "Newark",
                    state = "NJ",
                    postal_code = "07103",
                    country = "US",
                    phone = "000-000-0000"
                },
                parcels = new List<Parcel>()
                    {
                        new Parcel()
                        {
                            weight = 1.0,
                            length = 6.0,
                            width = 6.0,
                            height = 6.0,
                            package_type = "parcel"
                        }
                    },
                label_format = "pdf",
                label_size = "4x6",
                include_services = new List<int> { 1041, 1042, 1043 },
            }
        };

        public Root shipment8 = new Root()
        {
            shipment = new Shipment()
            {
                to_location = new ToLocation()
                {
                    first_name = "test",
                    last_name = "test",
                    company = "test",
                    address1 = "101 Towne Center Dr",
                    address2 = "",
                    city = "Compton",
                    state = "CA",
                    postal_code = "90220",
                    country = "US",
                    phone = "000-000-0008"
                },
                from_location = new FromLocation()
                {
                    first_name = "test",
                    last_name = "test",
                    company = "test",
                    address1 = "399-443 Springfield Ave",
                    city = "Newark",
                    state = "NJ",
                    postal_code = "07103",
                    country = "US",
                    phone = "000-000-0000"
                },
                parcels = new List<Parcel>()
                    {
                        new Parcel()
                        {
                            weight = 1.0,
                            length = 6.0,
                            width = 6.0,
                            height = 6.0,
                            package_type = "parcel"
                        }
                    },
                label_format = "pdf",
                label_size = "4x6",
                include_services = new List<int> { 1041, 1042, 1043 },
            }
        };

        public object Clone()
        {
            return new Shipments.Root();
        }

        public object CloneParcel(Parcel o)
        {
            return new Parcel();
        }

        public class FromLocation
        {
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string company { get; set; }
            public string address1 { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string postal_code { get; set; }
            public string country { get; set; }
            public string phone { get; set; }
        }

        public class Parcel
        {
            public double weight { get; set; }
            public double length { get; set; }
            public double width { get; set; }
            public double height { get; set; }
            public string package_type { get; set; }
        }

        public class Root
        {
            public Shipment shipment { get; set; }
        }

        public class Shipment
        {
            public ToLocation to_location { get; set; }
            public FromLocation from_location { get; set; }
            public List<Parcel> parcels { get; set; }
            public string label_format { get; set; }
            public string label_size { get; set; }
            public List<int> include_services { get; set; }
        }

        public class ToLocation
        {
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string company { get; set; }
            public string address1 { get; set; }
            public string address2 { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string postal_code { get; set; }
            public string country { get; set; }
            public string phone { get; set; }
        }
    }
}
