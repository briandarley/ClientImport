﻿using System;

namespace Archived.Core.Conversion
{
    public class EntityNameAttribute: Attribute
    {
        public EntityNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
