using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NHLCafe.Pages
{
    public class TableSelect : PageModel
    {
        public List<string> Tables { get; set; }
        
        [BindProperty]
        public int SelectedTable { get; set; }
        
        public void OnGet()
        {
            Tables = new List<string>(){"tafel-1", "tafel-2", "tafel-3", "tafel-4"};
        }
    }
}