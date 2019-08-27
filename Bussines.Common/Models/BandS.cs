


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
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public DateTime PublishOn { get; set; }

        public override string ToString()
        {
            return this.Description;
        }



        #endregion
    }
}
