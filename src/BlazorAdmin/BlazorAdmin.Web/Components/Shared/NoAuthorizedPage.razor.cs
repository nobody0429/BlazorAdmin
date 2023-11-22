﻿using BlazorAdmin.Core.Constants;

namespace BlazorAdmin.Web.Components.Shared
{
	public partial class NoAuthorizedPage
	{
		private async Task LogoutClick()
		{
			await _localStorage.DeleteAsync(CommonConstant.UserToken);
			await _authService.SetCurrentUser();
			_navManager.NavigateTo("/login", true);
		}
	}
}