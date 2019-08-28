﻿


namespace Bussines.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class BandS
    {
        #region attributes
        [Key]
        public int BandSId { get; set; }
        [Required]
        [StringLength(30)]
        //[Index("BansSIndex",IsUnique=true)]
        [Display(Name = "Nombre del servicio o negocio")]
        public string Description { get; set; }
        [Display(Name="Imagen")]
        public string ImagePath { get; set; }
        [Required]
        [Display(Name = "Direccion")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Telefono")]
        public string Phone { get; set; }

        [Display(Name = "Acerca del servicio o negocio")]
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        [Required]
        [Display(Name = "Fecha de Publicacion")]
        [DataType(DataType.Date)]     
        public DateTime PublishOn { get; set; }

        //[NotMapped]
        //public byte[] ImageArray { get; set; }

        //public string ImageFullPath
        //{
        //    get
        //    {

        //        if (string.IsNullOrEmpty(this.ImagePath))
        //        {
        //            return "productDefault";

        //        }
        //        return $"https://foodapi20190807091922.azurewebsites.net/{ this.ImagePath.Substring(1)}";




        //    }
        //}

        public override string ToString()
        {
            return this.Description;
        }



        #endregion
    }
}
