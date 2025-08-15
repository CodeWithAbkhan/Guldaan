using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guldaan.Common.RazorUI.Components
{
    public partial class UbikContentGridTopPanel
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}
