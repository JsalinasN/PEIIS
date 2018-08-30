﻿using PEIIS.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEIIS.Models
{
    public class ControllerInfo
    {
        public string FullName { get; set; }
        public string DisplayName { get; set; }
        public string DisplayImage { get; set; }
        public string ControllerName { get; set; }
        public List<ActionInfo> ControllerActions { get; set; }
        public TreeViewAttribute TreeViewSettings { get; set; }
        public TreeViewSettingsAttribute TreeViewSettings2 { get; set; }
        public bool HasAuthorize { get; internal set; }
        public string[] AuthorizedRoles { get; internal set; }
    }

}
