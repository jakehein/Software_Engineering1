﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    class CategoryDTO
    {
        public long CategoryID { get; set; }
        public string Name { get; set; }
        public List<Item> Items { get; set; }

        override
        public string ToString()
        {
            return Name;
        }

        override
            public bool Equals(Object obj)
        {
            CategoryDTO c = obj as CategoryDTO;
            return c != null && c.CategoryID == CategoryID;
        }
    }
}
