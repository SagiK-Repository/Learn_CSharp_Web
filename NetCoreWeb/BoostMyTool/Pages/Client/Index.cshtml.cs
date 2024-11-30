using BoostMyTool.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoostMyTool.Pages.Client
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClients = new();

        public void OnGet()
        {
        }
    }
}
