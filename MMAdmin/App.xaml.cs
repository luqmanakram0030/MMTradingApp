﻿using MMAdmin.Views.AdminUserMangement;

namespace MMAdmin;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		//MainPage = new AppShell();
		MainPage = new NavigationPage(new AdminLoginView());
	}
}
