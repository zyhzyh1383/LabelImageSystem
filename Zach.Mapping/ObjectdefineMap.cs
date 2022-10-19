using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zach.Entity;

namespace Zach.Mapping
{
    public class ObjectdefineMap : EntityTypeConfiguration<Objectdefine>
    {
        public ObjectdefineMap()
        {
            this.ToTable("Objectdefine");
            this.HasKey(o => o.ObjID);
        }
    }
}
