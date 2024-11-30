using BoostMyTool.Application.Interfaces;
using BoostMyTool.Application.UseCase;
using BoostMyTool.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace BoostMyTool.Pages.Clients
{
    public class EditModel : PageModel
    {
        private readonly IMediator _mediator;
        public ClientInfo clientInfo = new();
        public string ErrorMessage = string.Empty;
        public string SuccessMessage = string.Empty;

        public EditModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task OnGet()
        {
            string id = Request.Query["Id"]!;

            try
            {
                var response = await _mediator.Send(new GetClientFromIdRequest(id));
                clientInfo = response.ClientInfo;
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                return;
            }
        }

        public async void OnPost()
        {
            clientInfo.Id = Convert.ToInt32(Request.Form["Id"]!);
            clientInfo.Name = Request.Form["Name"]!;
            clientInfo.Email = Request.Form["Email"]!;
            clientInfo.Phone = Request.Form["Phone"]!;
            clientInfo.Address = Request.Form["Address"]!;

            if (clientInfo.Name.Length == 0 || clientInfo.Email.Length == 0 || clientInfo.Phone.Length == 0 || clientInfo.Address.Length == 0)
            {
                ErrorMessage = "All the fields are required";
                return;
            }

            UpdateClientResponse? response = null;
            try
            {
                response = await _mediator.Send(new UpdateClientRequest(clientInfo));
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                return;
            }

            Response.Redirect(response.GetRedirect());
        }
    }
}