﻿namespace Bazaro.Web.Services.ViewModels
{
    public class CalenderEntryModel
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public EntryModel Entry { get; set; }
    }
}
