using Application.DTOs.Cart;
using MediatR;


namespace Application.Cart.Queries.GetCartId
{
    public class GetCartIdQuery : IRequest<ICollection<GetCartServiceChargeDto>>
    {
    }
}
