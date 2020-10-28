using System;
using System.ComponentModel.DataAnnotations;
using DatabaseLayer.Entities;

namespace BusinessLayer.Models
{
    // Why don't we use the ConferenceViewModel for represent the new instance of the Conference?
    public class NewConferenceViewModel
    {
        public string UniqueAddress { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }
        public string Description { get; set; }

        public User Organizer { get; set; }
        
        [DataType(DataType.DateTime)] public DateTime DateStart { get; set; }
        [DataType(DataType.DateTime)] public DateTime DateFinish { get; set; }
    }
}