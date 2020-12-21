using System.Collections.Generic;

namespace Core.Entities
{
    public class CustomerBasket
    {

        public CustomerBasket()
        {
            
        }
        public CustomerBasket(string id)
        {
            Id = id;
        }
        public string Id { get; set; }

        //initialize the basket to an empty basket
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
    }
}