public class CreateOrderHandler: IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IAppDbContext _context;

    public CreateOrderHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(
        CreateOrderCommand request,
        CancellationToken ct)
    {
        var order = new Order(request.CustomerId);

        _context.Orders.Add(order);

        await _context.SaveChangesAsync(ct);

        return order.Id;
    }
}