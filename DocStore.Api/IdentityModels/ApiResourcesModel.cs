﻿using System.Collections.Generic;

namespace DocStore.Api.IdentityModels
{
    public class ApiResourcesModel
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public List<string> ClaimTypes { get; set; }
    }
}
