namespace Core.Entities
{
    //primary key Id is derived from BaseEntity
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PictureUrl { get; set; }

        //Entity ProductType - related entity
        public ProductType ProductType { get; set; }
        //foreign key
        public int ProductTypeId { get; set; }

        //Entity ProductBrand - related entity
        public ProductBrand ProductBrand { get; set; }

        //foreign key
        public int ProductBrandId { get; set; }
    }
}