namespace Dtos
{
    public class CategoryFieldValueDto
    {
        public int FieldId { get; set; }
        public int  CategoryId { get; set; }
        public string FieldName { get; set; }
        public string Value { get; set; }
    }

    public class CategoryFieldValueCreateDto
    {
        public int FieldId { get; set; }
        public int CategoryId { get; set; }
        public string Value { get; set; }
    }
}