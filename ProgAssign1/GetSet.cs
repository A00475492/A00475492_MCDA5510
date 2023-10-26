//getter setter class to get the data from the CSV
using CsvHelper.Configuration.Attributes;

namespace ProgAssign1
{
    public class GetSet
    {
        [Name("First Name")]
        public string firstname { get; set; }
        [Name("Last Name")]
        public string lastname { get; set; }
        [Name("Street Number")]
        public int streetNumber { get; set; }
        [Name("Street")]
        public string street { get; set; }

        [Name("City")]
        public string city { get; set; }
        [Name("Province")]
        public string province { get; set; }
        [Name("Postal Code")]
        public string postalCode { get; set; }
        [Name("Country")]
        public string country { get; set; }
        [Name("Phone Number")]
        public int phoneNumber { get; set; }
        [Name("email Address")]
        public string email { get; set; }


    }
}
