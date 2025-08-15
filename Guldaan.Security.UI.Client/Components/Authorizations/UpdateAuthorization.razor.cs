using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
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
    public partial class UpdateAuthorization(
        NavigationManager navigationManager,
        IHttpSecurityClient securityClient,
        AuthorizationsState state,
        ResponseManagerService responseManager)
    {
        private readonly NavigationManager _navigationManager = navigationManager;
        private readonly IHttpSecurityClient _securityClient = securityClient;
        private readonly AuthorizationsState _state = state;
        private readonly ResponseManagerService _responseManager = responseManager;

        [Parameter]
        public Guid Id { get; set; }

        [SupplyParameterFromForm]
        private AuthorizationUiObj Edit { get; set; } = default!;

        private bool _isSaving = false;

        private bool _isLoaded = false;

        protected override async Task OnInitializedAsync()
        {
            Edit = new AuthorizationUiObj();

            if (RendererInfo.IsInteractive)
            {
                await LoadAuthorizationAsync();
            }
        }

        private async Task LoadAuthorizationAsync()
        {
            _isLoaded = false;

            var response = await _securityClient.GetAuthorizationForAdminByIdAsync(Id);

            var result = await _responseManager.ManageAsync<AuthorizationStandardResult>(
                response,
                "Cannot retrieve data.",
                "An error occurred while loading authorization.");

            if(result !=null)
            {
                Edit = result.MapToAuthorizationUiObj();
            }

            _isLoaded = true;
        }

        private async Task HandleValidSubmitAsync()
        {
            _isSaving = true;

            var command = Edit.MapToUpdateAuthorizationCommand();

            var response = await _securityClient.UpdateAuthorizationForAdminAsync(Id, command);
            
            var result = await _responseManager.ManageWithSuccessMsgAsync<AuthorizationStandardResult>(
                response,
                "Save success.",
                "Authorization {x} updated.",
                "Id",
                $"Cannot saving data.",
                "An error occurred while saving authorization.");

            if(result != null)
            {
                _state.SetSelectedId(result!.Id);
                _navigationManager.NavigateTo($"/authorizations?state=true");
            }

            _isSaving = false;
        }

        private void Close()
        {
            _navigationManager.NavigateTo($"/authorizations?state=true");
        }
    }
}
