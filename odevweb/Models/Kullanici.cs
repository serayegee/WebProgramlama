﻿using System.ComponentModel.DataAnnotations;

namespace odevweb.Models
{
    public class Kullanici
    {
        [Key]
        public int KullaniciId { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı gerekli.")]
        public string KullaniciAdi { get; set; }

        [Required(ErrorMessage = "Şifre gerekli.")]
        public string Sifre { get; set; }

        public bool IsAdmin { get; set; }
        public string Ad { get; set; }
    }
}
