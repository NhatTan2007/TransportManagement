using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Entities
{
    public class EditTransportInformation
    {
        private string _editId;
        private string _editContent;
        private string _timeZone;
        private double _dateEditUTC;
        private double _dateEditLocal;
        private string _userEditId;
        private string _transportId;
        [Key]
        public string EditId { get => _editId; set => _editId = value; }
        [Required]
        [MaxLength(2000)]
        public string EditContent { get => _editContent; set => _editContent = value; }
        [Required]
        [MaxLength(200)]
        public string TimeZone { get => _timeZone; set => _timeZone = value; }
        [Required]
        public double DateEditUTC { get => _dateEditUTC; set => _dateEditUTC = value; }
        [Required]
        public double DateEditLocal { get => _dateEditLocal; set => _dateEditLocal = value; }
        public string UserEditId { get => _userEditId; set => _userEditId = value; }
        [ForeignKey("UserEditId")]
        public AppIdentityUser UserEdit { get; set; }
        public string TransportId { get => _transportId; set => _transportId = value; }
        [ForeignKey("TransportId")]
        public TransportInformation TransportInfo { get; set; }
    }
}
