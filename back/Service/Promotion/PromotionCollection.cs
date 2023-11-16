using System.Reflection;
using Service.Promotion;

public class PromotionCollection
{
    private List<IPromotionStrategy>? _promotions;

    private string _promotionsPath;

    public PromotionCollection()
    {
        _promotions = new List<IPromotionStrategy>();
        ResetPromotionsPath();
    }

    public void ResetPromotionsPath()
    {
        _promotions = new List<IPromotionStrategy>();
        LoadPromotions();
    }

    public List<IPromotionStrategy> GetPromotions()
    {
        if (_promotions == null)
        {
            _promotions = new List<IPromotionStrategy>();
            LoadPromotions();
        }

        return _promotions;
    }

    private void LoadPromotions()
    {
        var _promotionDllDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "promotions");
        var promotionFiles = Directory.GetFiles(_promotionDllDirectory, "*.dll");

        foreach (var file in promotionFiles)
        {
            var assembly = Assembly.LoadFrom(file);

            foreach (var type in assembly.GetTypes())
            {
                if (type.GetInterface(nameof(IPromotionStrategy)) != null)
                {
                    var instance = Activator.CreateInstance(type) as IPromotionStrategy;
                    if (instance != null)
                    {
                        _promotions.Add(instance);
                    }
                }
            }
        }
    }
}