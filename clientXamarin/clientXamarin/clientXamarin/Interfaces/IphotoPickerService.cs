using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace clientXamarin.Interfaces
{
    public interface IphotoPickerService
    {
        Task<Stream> GetImageStreamAsync();
    }
}
