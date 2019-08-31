


namespace Bussines.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class BandS
    {
        #region attributes
        [Key]
        public int BandSId { get; set; }
        [Required]
        [StringLength(100)]
        //[Index("BansSIndex",IsUnique=true)]
        [Display(Name = "Nombre del servicio o negocio")]
        public string Description { get; set; }
        [Display(Name="Imagen")]
        public string ImagePath { get; set; }
        [Required]
        [Display(Name = "Direccion")]
        public string Address { get; set; }
        
        [Display(Name = "Telefono")]
        public string Phone { get; set; }

        [Display(Name = "Acerca del servicio o negocio")]
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        [Required]
        [Display(Name = "Fecha de Publicacion")]
        [DataType(DataType.Date)]     
        public DateTime PublishOn { get; set; }

        [NotMapped]
        public byte[] ImageArray { get; set; }

        public string ImageFullPath
        {
            get
            {

                if (string.IsNullOrEmpty(this.ImagePath))
                {
                    return "business";

                }
                return $"https://bussinesapi20190826063612.azurewebsites.net/{ this.ImagePath.Substring(1)}";




            }
        }

        public override string ToString()
        {
            return this.Description;
        }



        #endregion
    }
}
