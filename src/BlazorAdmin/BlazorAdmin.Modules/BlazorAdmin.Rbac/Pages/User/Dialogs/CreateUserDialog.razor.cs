﻿using BlazorAdmin.Core.Data;
using BlazorAdmin.Core.Helper;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace BlazorAdmin.Rbac.Pages.User.Dialogs
{
	public partial class CreateUserDialog
	{
		private Dictionary<string, object> InputAttributes { get; set; } =
			new Dictionary<string, object>()
				{
				   { "autocomplete", "off2" },
				};

		[CascadingParameter] MudDialogInstance? MudDialog { get; set; }

		private UserCreateModel UserModel = new();

		private async Task CreateSubmit()
		{
			using var context = _dbFactory.CreateDbContext();
			if (context.Users.Any(u => u.Name == UserModel.UserName))
			{
				_snackbarService.Add("用户名重复！", Severity.Error);
				return;
			}

			context.Users.Add(new Data.Entities.User
			{
				IsEnabled = true,
				Name = UserModel.UserName!,
				RealName = UserModel.RealName!,
				PasswordHash = HashHelper.HashPassword(UserModel.Password!)
			});
			await context.AuditSaveChangesAsync();
			_snackbarService.Add("创建成功！", Severity.Success);
			MudDialog?.Close(DialogResult.Ok(true));
		}

		private record UserCreateModel
		{
			[Required(ErrorMessage = "请输入用户名")]
			[MaxLength(200, ErrorMessage = "用户名位数过长")]
			public string? UserName { get; set; }

			[Required(ErrorMessage = "请输入姓名")]
			[MaxLength(200, ErrorMessage = "姓名位数过长")]
			public string? RealName { get; set; }

			[Required(ErrorMessage = "请输入密码")]
			[MinLength(4, ErrorMessage = "密码位数过短")]
			[MaxLength(100, ErrorMessage = "密码位数过长")]
			[RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).+$", ErrorMessage = "密码中必须包含大小写字母以及数字")]
			public string? Password { get; set; }
		}
	}
}
