﻿using CleanCodeTemplate.Business.Ports.Account.Output;

namespace CleanCodeTemplate.Infrastructure.Adapters.Presenters.Account;

public class TwoFactorLoginAccountPresenter : ITwoFactorLoginAccountOutput, IPresenter<string>
{
    public Task HandleAsync(string response, CancellationToken ct)
    {
        Response = response;

        return Task.CompletedTask;
    }

    public string Response { get; private set; }
}