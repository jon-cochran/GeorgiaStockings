using System;
using System.ComponentModel.DataAnnotations;

namespace GeorgiaStockings.Models
{
    public class StockingMetadata
    {
        [StringLength(50)]
        [Display(Name = "Water Body")]
        public string WaterBody;

        [StringLength(50)]
        [Display(Name = "County")]
        public string County;
        
    }

    public class NoteMetadata
    {
        [StringLength(100)]
        [Display(Name = "Note Description")]
        public string Description;
    }

    public class ScheduleMetadata
    {
        [StringLength(50)]
        [Display(Name = "Schedule Name")]
        public string Name;
    }

}