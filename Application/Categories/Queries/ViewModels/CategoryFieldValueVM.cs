namespace Application.Categories.Queries.ViewModels
{
    public class CategoryFieldValueVM
    {
        public long FieldId { get; set; }
        public long  CategoryId { get; set; }
        public string FieldName { get; set; }
        public string Value { get; set; }
    }
}