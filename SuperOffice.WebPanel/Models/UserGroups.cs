using System;
using System.Collections.Generic;

namespace SuperOffice.WebPanel.Models
{
    public class UserGroups
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ToolTip { get; set; }
        public bool Deleted { get; set; }
        public int Rank { get; set; }
        public string Type { get; set; }
        public int ColorBlock { get; set; }
        public string IconHint { get; set; }
        public bool Selected { get; set; }
        public DateTime LastChanged { get; set; }
        public IList<ChildItem> ChildItems { get; set; }
        public string ExtraInfo { get; set; }
        public string StyleHint { get; set; }
        public bool Hidden { get; set; }
        public string FullName { get; set; }
    }
}
