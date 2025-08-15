using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Net.Http.Json;
using Guldaan.Common.RazorUI.Components;
using Guldaan.Common.RazorUI.Config;
using Guldaan.Common.RazorUI.Errors;
using Guldaan.Common.RazorUI.Tweaks;
using Guldaan.Security.Contracts.Authorizations.Results;
using Guldaan.Security.UI.Client.Components.Shared;
using Guldaan.Security.UI.Shared.Httpclients;
using Guldaan.Security.UI.Shared.State;

namespace Guldaan.Security.UI.Client.Components.Authorizations
{
    public partial class AddAuthorization(NavigationManager navigationManager,
        AuthorizationsState state,
        IHttpSecurityClient securityClient,
        ResponseManagerService responseManager)
    {
        private readonly NavigationManager _navigationManager = navigationManager;
        private readonly AuthorizationsState _state = state;
        private readonly IHttpSecurityClient _securityClient = securityClient;
        private readonly ResponseManagerService _responseManager = responseManager;

        [SupplyParameterFromForm]
        private AuthorizationUiObj Add { get; set; } = default!;

        private bool _isLoading = false;

        protected override Task OnInitializedAsync()
        {
            Add = new AuthorizationUiObj();
            return Task.CompletedTask;
        }

        private async Task HandleValidSubmitAsync()
        {
            _isLoading = true;

            var command = Add.MapToAddAuthorizationCommand();

            var response = await _securityClient.AddAuthorizationForAdminAsync(command);
            var result = await _responseManager.ManageWithSuccessMsgAsync<AuthorizationStandardResult>(
                response,
                "Save success.",
                "Authorization {x} added.",
                "Id",
                "Cannot saving data.",
                "An error occurred while saving authorization.");

            if (result != null)
            {
                _state.SetSelectedId(result?.Id);
                _navigationManager.NavigateTo($"/authorizations?state=true");
            }

            _isLoading = false;
        }

        private void Close()
        {
            _navigationManager.NavigateTo($"/authorizations?state=true");
        }
    }
}
