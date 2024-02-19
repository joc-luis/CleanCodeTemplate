namespace CleanCodeTemplate.Infrastructure.Adapters.Presenters;

public interface IPresenter<TResponse>
{
    public TResponse Response { get; }
}