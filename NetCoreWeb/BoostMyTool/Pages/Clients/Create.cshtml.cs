using BoostMyTool.Application.Interfaces;
using BoostMyTool.Application.UseCase;
using BoostMyTool.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace BoostMyTool.Pages.Clients
{
    public class CreateModel : PageModel
    {
        private readonly IMediator _mediator;
        public ClientInfo clientInfo = new();
        public string ErrorMessage = string.Empty;
        public string SuccessMessage = string.Empty;

        public CreateModel(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task OnPost()
        {
            clientInfo.Name = Request.Form["Name"]!;
            clientInfo.Email = Request.Form["Email"]!;
            clientInfo.Phone = Request.Form["Phone"]!;
            clientInfo.Address = Request.Form["Address"]!;

            if (clientInfo.Name.Length == 0 || clientInfo.Email.Length == 0 || clientInfo.Phone.Length == 0 || clientInfo.Address.Length == 0)
            {
                ErrorMessage = "All the fields are required";
                return;
            }

            CreateClientResponse? response = null;
            try
            {
                response = await _mediator.Send(new CreateClientRequest(clientInfo.Name, clientInfo.Email, clientInfo.Phone, clientInfo.Address));
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                return;
            }

            clientInfo.Name = response.Name;
            clientInfo.Email = response.Email;
            clientInfo.Phone = response.Phone;
            clientInfo.Address = response.Address;

            SuccessMessage = "New Client Added Correctly";

            Response.Redirect(response.RedirectPath());
        }
    }
}
