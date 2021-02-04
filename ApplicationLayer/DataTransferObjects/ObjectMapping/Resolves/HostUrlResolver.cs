//using AutoMapper;
//using Business.Entities;
//using Business.LogicObjects.Extensions;
//using CrossCutting.Configurations;
//using Microsoft.Extensions.Options;

//namespace Data.TransferObjects.ObjectMapping.Resolves
//{
//    public class HostUrlResolver : IValueResolver<UserProfile, UserProfileDto, string>
//    {
//        private UploadedConfig _uploadedConfig;

//        public HostUrlResolver(IOptions<UploadedConfig> uploadedConfig)
//        {
//            _uploadedConfig = uploadedConfig.Value;
//        }

//        public string Resolve(UserProfile source, UserProfileDto destination, string destMember, ResolutionContext context)
//        {
//            return source.File?.GetHostUrl(_uploadedConfig);
//        }
//    }
//}
