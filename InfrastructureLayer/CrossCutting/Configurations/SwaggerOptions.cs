﻿namespace CrossCutting.Configurations
{
    public class SwaggerOptions
    {
        public const string Key = "SwaggerOptions";

        public string JsonRoute { get; set; }

        public string EndpointName { get; set; }

        public string Title { get; set; }

        public string UIEndpoint { get; set; }

        public string Version { get; set; }

        public string RoutePrefix { get; set; }
    }
}