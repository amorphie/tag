using amorphie.core.IBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amorphie.core.Base
{
    public class Error : IResponse
    {
        public List<string> Errors { get; set; }
        public bool IsSuccessful { get; set; }
    }
    public class Error<T> : IResponse<T>
    {
        public List<string> Errors { get; set; }
        public bool IsSuccessful { get; set; }
        public T Data { get; set; } = default;
    }
}
