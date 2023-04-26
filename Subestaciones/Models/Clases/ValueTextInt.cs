using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class ValueTextInt
    {
        public int Value { get; set; }
        public string Text { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is ValueTextInt))
            {
                return false;
            }
            return (this.Value == ((ValueTextInt)obj).Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}