﻿namespace Jobby.Application.Contracts.Auth;

public class RegisterRequest
{
  public string Username { get; set; }
  public string Password { get; set; }
  public string Email { get; set; }
  public string FirstName { get; set; }
  public string LastName { get; set; }
}
