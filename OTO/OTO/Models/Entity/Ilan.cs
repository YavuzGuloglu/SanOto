//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OTO.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ilan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ilan()
        {
            this.Kullanici1 = new HashSet<Kullanici>();
        }
    
        public int ilan_id { get; set; }
        public string ilan_baslik { get; set; }
        public Nullable<int> ilan_kategori { get; set; }
        public string ilan_sehir { get; set; }
        public string ilan_ilce { get; set; }
        public string ilan_mahalle { get; set; }
        public string ilan_detay { get; set; }
        public string ilan_tarih { get; set; }
        public Nullable<decimal> ilan_fiyat { get; set; }
        public string ilan_verenAd { get; set; }
        public string ilan_verenSoyad { get; set; }
        public string ilan_verenTel { get; set; }
        public string ilan_verenEposta { get; set; }
        public string ilan_adres { get; set; }
        public string ilan_foto { get; set; }
        public Nullable<int> ilan_gelenMesaj { get; set; }
        public Nullable<int> ilan_kullanici { get; set; }
    
        public virtual Kategori Kategori { get; set; }
        public virtual Kullanici Kullanici { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kullanici> Kullanici1 { get; set; }
    }
}
