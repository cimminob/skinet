using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }

        //initialize the basket to an empty basket
        public List<BasketItemDto> Items { get; set; }
    }
}