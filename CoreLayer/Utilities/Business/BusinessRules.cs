using CoreLayer.Utilities.Results;

namespace CoreLayer.Utilities.Business;

public class BusinessRules
{
    public static IResult Run(params IResult[] logics)
    {
        foreach (var logic in logics)
        {
            if (!logic.Success)
            {
                return logic;
            }
        }
        return null;
    }
}
