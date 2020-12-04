using API.Dtos;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;

namespace API.Helpers
{

    //this class will attach the address of the localhost to the local address
    //ex: attaching https://localhost:5001   to  images/products/sb-ts1.png


    //source, destination object, destination property type
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _config;
        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;

        }

        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                //attach ApiUrl variable created in appsettings.development.json
                return _config["ApiUrl"] + source.PictureUrl;
            };

            //if string is empty
            return null;
        }
    }
}