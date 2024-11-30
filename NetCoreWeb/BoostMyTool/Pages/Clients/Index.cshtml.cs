using BoostMyTool.Application.UseCase;
using BoostMyTool.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoostMyTool.Pages.Clients
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;
        public List<ClientInfo> listClients = new();

        public IndexModel(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task OnGet() =>
            listClients = (await _mediator.Send(new GetClientRequest())).ClientInfos.ToList();
    }
}