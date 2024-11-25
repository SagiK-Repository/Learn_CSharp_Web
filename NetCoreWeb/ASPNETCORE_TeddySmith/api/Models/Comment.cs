namespace api.Models;

public class Comment
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Conent { get; set; } = string.Empty;
    public DateTime CreateOn { get; set; } = DateTime.Now;
    public int? StockId { get; set; }
    // Navigation Prop (탐색속성으로 점근, 모델 내에서 탐색 가능)
    public Stock? Stock { get; set; }
}
