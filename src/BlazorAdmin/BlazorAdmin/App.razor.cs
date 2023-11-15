﻿using BlazorAdmin.Log.Pages.AuditLog;
using BlazorAdmin.Rbac.Pages.User;
using BlazorAdmin.Setting.Pages.Setting;
using System.Reflection;

namespace BlazorAdmin
{
	public partial class App
	{
		private List<Assembly> AdditionalAssemblies = new List<Assembly>()
		{
			typeof(User).Assembly,
			typeof(AuditLog).Assembly,
			typeof(SettingPage).Assembly
		};
	}
}