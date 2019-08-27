


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
        public string Description { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Remarks { get; set; }
        [Required]
        public DateTime PublishOn { get; set; }

        public override string ToString()
        {
            return this.Description;
        }



        #endregion
    }
}
