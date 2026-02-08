using System;
using System.ComponentModel.DataAnnotations;

namespace PRH.Client.Wasm.ViewModels;

	public class LoginForm
	{
		public required string Email { get; set; }
		public required string Password { get; set; }
	}