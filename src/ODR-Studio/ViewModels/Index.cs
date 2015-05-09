using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ODRStudio.ViewModels
{
    public class Index
    {
        public string Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
    }
}
