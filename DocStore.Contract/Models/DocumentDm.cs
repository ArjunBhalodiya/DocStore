﻿using System;

namespace DocStore.Contract.Models
{
    public class DocumentDm
    {
        public string DocumentId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }
        public string DocumentOwnerId { get; set; }
        public int DocumentVersion { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
