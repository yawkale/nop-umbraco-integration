namespace Nop.Api.Adapter.SettingsProvider
{
    public interface  ISettingsProvider
    {
        string ClientId { get;  }
        string ClientSecret { get;  }
        string ServerUrl { get;  }
        string RedirectUrl { get;  }

    }
}
