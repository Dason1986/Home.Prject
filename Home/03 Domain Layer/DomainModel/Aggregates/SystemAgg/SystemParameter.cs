﻿using Library;
using Library.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Aggregates.SystemAgg
{
    public class SystemParameter : AuditedEntity, IParameter
    {
        public string Description { get; set; }
        public string Group { get; set; }

        public bool IsReadOnly { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        void IParameter.SetValue(string value)
        {
            Value = value;
        }
    }
}