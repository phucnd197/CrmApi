namespace PIM_Api.Infrastructure.Entities;

public class Price
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public decimal BasePrice { get; set; }
    public string Currency { get; set; }
    public DateOnly EffectiveDate { get; set; }
    public DateOnly ExpirationDate { get; set; }

    public virtual Product Product { get; set; }
}
