﻿using System.Net;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using Otium.Domain.Models;
using Otium.Domain.Response;
using Otium.Repositories.Abstractions;
using Otium.Services.Abstractions;

namespace Otium.Services.Implementations;

public class EmailService : IEmailService
{
    private readonly IEmailRepository _repository;
    private readonly IConfiguration _configuration;

    public EmailService(IEmailRepository repository, IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }

    public async Task<BaseResponse<string>> SendEmailAsync(Email request)
    {
        var email = new MimeMessage();
        email.Subject = request.Subject;
        email.Body = new TextPart(TextFormat.Html) {Text = request.Text};
        email.From.Add(MailboxAddress.Parse(_configuration.GetSection("Email:From").Value));

        var isToAddressCorrect = MailboxAddress.TryParse(request.To, out var to);
        if (!isToAddressCorrect)
            return new BaseResponse<string>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Некорректный адрес получателя"
            };
        email.To.Add(to);

        try
        {
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_configuration.GetSection("Email:Host").Value, 
                int.Parse(_configuration.GetSection("Email:Port").Value));
            await smtp.AuthenticateAsync(_configuration.GetSection("Email:Username").Value,
                _configuration.GetSection("Email:Password").Value);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);

            var mailId = await _repository.AddAsync(request);
            return new BaseResponse<string>
            {
                StatusCode = HttpStatusCode.OK,
                Data = mailId.Id
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<string>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = e.Message
            };
        }
    }

    public async Task<BaseResponse<List<Email>>> GetLastEmailsByIpAsync(string ip, int minutes)
    {
        var response = await _repository
            .FindByAsync(email => email.Ip == ip && email.DateTime < DateTime.Now.AddMinutes(-minutes));
        
        if (response.Count == 0)
            return new BaseResponse<List<Email>>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "Message not found"
            };

        return new BaseResponse<List<Email>>
        {
            StatusCode = HttpStatusCode.OK,
            Data = response
        };
    }

    #region Dispose

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private bool _disposed;

    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _repository.Dispose();
            }
        }

        _disposed = true;
    }

    #endregion
}