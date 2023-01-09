using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SirketUygulamasi.Models
{
    public class CrudContext : DbContext
    {
        public DbSet<Calisan> Calisanlar { get; set; }
        public DbSet<Sirket> Sirketler { get; set; }
    }

    public class Sirket
    {
        [Key]
        public int SIRKET_ID { get; set; }
        public string SIRKET_ADI { get; set; }
        public string VERGI_DAIRESI { get; set; }
        public string VERGI_NUMARASI { get; set; }
        public string ADRES { get; set; }
    }
    public class Calisan
    {
        [Key]
        public int CALISAN_ID { get; set; }
        public string ADI_SOYADI { get; set; }

        public string TC_NO { get; set; }
        public Sirket Sirket { get; set; }
        [ForeignKey("Sirket")]
        public int CALISAN_SIRKET_ID { get; set; }

    }
    public class DTO_CALISAN
    {
        public int CALISAN_ID { get; set; }
        public string ADI_SOYADI { get; set; }
        public string TC_NO { get; set; }
        public int CALISAN_SIRKET_ID { get; set; }
        public string SIRKET_ADI { get; set; }
    }


}