using System;
using System.ComponentModel.DataAnnotations;

namespace GeorgiaStockings.Models
{
    [MetadataType(typeof(ScheduleMetadata))]
    public partial class Schedule
    {
    }

    [MetadataType(typeof(NoteMetadata))]
    public partial class Note
    {
    }

    [MetadataType(typeof(StockingMetadata))]
    public partial class Stocking
    {
    }
}