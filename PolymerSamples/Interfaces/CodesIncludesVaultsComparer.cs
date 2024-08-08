using PolymerSamples.DTO;

namespace PolymerSamples.Interfaces;

public class CodesIncludesVaultsComparer : IComparer<string>
{
    public int Compare(string x, string y)
    {
        var xSplitted = x.Split('.').Select(int.Parse).ToArray();
        var ySplitted = y.Split('.').Select(int.Parse).ToArray();

        int length = Math.Min(xSplitted.Length, ySplitted.Length);

        for (int i = 0; i < length; i++)
        {
            int partComparison = xSplitted[i].CompareTo(ySplitted[i]);
            if (partComparison != 0)
                return partComparison;
        }

        return xSplitted.Length.CompareTo(ySplitted.Length);
    }
}
