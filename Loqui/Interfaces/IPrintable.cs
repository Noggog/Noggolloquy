using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loqui
{
    public interface IPrintable
    {
        void ToString(FileGeneration fg, string? name = null);
    }
}
