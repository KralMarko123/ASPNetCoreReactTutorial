﻿using aspnetserver.Data.Models;
using aspnetserver.Data.Models.Dtos;
using aspnetserver.Helper;
using aspnetserver.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Controllers;

[Route("")]
[AllowAnonymous]
public class AuthController : BaseController
{
    private readonly IUsersService usersService;
    private readonly IEmailHelper emailHelper;
    private readonly ILogger<AuthController> logger;

    public AuthController(IUsersService usersService, IEmailHelper emailHelper, ILogger<AuthController> logger, IMapper mapper) : base(mapper)
    {
        this.logger = logger;
        this.usersService = usersService;
        this.emailHelper = emailHelper;
    }

    [HttpPost]
    [Route("/register")]
    [Tags("Auth Endpoint")]
    public async Task<RequestResult> RegisterUser([FromBody] UserRegistrationDto userRegistration)
    {
        var result = await usersService.MapAndCreateUserAsync(userRegistration);

        if (result.StatusCode.Equals(HttpStatusCode.Created)) await SendEmailConfirmationLinkToUser(userRegistration.UserName);
        return result;
    }

    [HttpPost]
    [Route("/login")]
    [Tags("Auth Endpoint")]
    public async Task<RequestResult> AuthenticateUser([FromBody] UserLoginDto userLogin)
    {
        logger.LogInformation($"Logging in user: {userLogin.UserName}");

        var result = await usersService.ValidateUserAsync(userLogin);

        if (result.StatusCode.Equals(HttpStatusCode.Forbidden)) await SendEmailConfirmationLinkToUser(userLogin.UserName);
        return result;
    }

    private async Task SendEmailConfirmationLinkToUser(string username)
    {
        var user = await usersService.GetUserByUsernameAsync(username);
        var token = await usersService.GenerateEmailConfirmationTokenForUserAsync(user);
        var confirmationLink = Url.Action("ConfirmEmail", "Email", new { token, email = user.Email }, Request.Scheme);
        var subject = $"Please confirm the registration for {user.Email}";
        var body = $"Your account has been successfully created. Please click on the following link to confirm your registration: {confirmationLink}";

        await emailHelper.SendEmail(user.FirstName, user.LastName, user.Email, subject, body);
    }
}
