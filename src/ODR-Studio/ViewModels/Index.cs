﻿using System.Reflection;

namespace ODRStudio.ViewModels
{
    public class Index
    {
        public string Version { get; } = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public string[] Configs { get; set; }
        public string CurrentConfig { get; set; }
    }
}
