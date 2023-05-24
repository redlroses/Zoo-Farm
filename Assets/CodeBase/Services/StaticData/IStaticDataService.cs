using CodeBase.StaticData;

namespace CodeBase.Services.StaticData
{
  public interface IStaticDataService : IService
  {
    void Load();
    MoneyPackConfig MoneyPackConfig { get; }
  }
}