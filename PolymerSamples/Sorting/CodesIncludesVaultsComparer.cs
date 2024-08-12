using PolymerSamples.DTO;
using PolymerSamples.Models;

namespace PolymerSamples.Sorting;

public class CodesIncludesVaultsComparer : IComparer<CodeIncludesVaultsDTO>
{
    public int Compare(CodeIncludesVaultsDTO? x, CodeIncludesVaultsDTO? y)
    {
        if (x is null || y is null)
            throw new ArgumentNullException();

        int typeComparison = x.type.CompareTo(y.type);
        if (typeComparison != 0)
        {
            return typeComparison;
        }

        switch (x.type)
        {
            case "Лента":
                {
                    return OnlyInts(x.short_code_name, y.short_code_name);
                }
            case "Ziplink":
                {
                    return OnlyInts(x.short_code_name, y.short_code_name);
                }
            case "Плоский ремень":
                {
                    var xSplitted = x.short_code_name.Split('.');
                    var ySplitted = y.short_code_name.Split('.');

                    // Сравниваем первые две секции лексикографически
                    int firstPartComparison = string.Compare(xSplitted[0], ySplitted[0], StringComparison.Ordinal);
                    if (firstPartComparison != 0)
                    {
                        return firstPartComparison;
                    }

                    // Сравниваем две вторые секции в смешанном режиме
                    int secondPartComparison = CompareDigitLetterPart(xSplitted[1], ySplitted[1]);
                    if (secondPartComparison != 0)
                    {
                        return secondPartComparison;
                    }

                    // Сравниваем последние две секции как числа
                    int thirdPartComparison = int.Parse(xSplitted[2]).CompareTo(int.Parse(ySplitted[2]));
                    if (thirdPartComparison != 0)
                    {
                        return thirdPartComparison;
                    }

                    return int.Parse(xSplitted[3]).CompareTo(int.Parse(ySplitted[3]));
                }
            default: return 0;
        }


    }

    private static int OnlyInts(string x, string y)
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

    private static int CompareDigitLetterPart(string x, string y)
    {
        var xNumber = new string(x.TakeWhile(char.IsDigit).ToArray());
        var yNumber = new string(y.TakeWhile(char.IsDigit).ToArray());

        var xLetter = new string(x.SkipWhile(char.IsDigit).ToArray());
        var yLetter = new string(y.SkipWhile(char.IsDigit).ToArray());

        // Сравниваем числовые части
        int numberComparison = int.Parse(xNumber).CompareTo(int.Parse(yNumber));
        if (numberComparison != 0)
        {
            return numberComparison;
        }

        // Если числовые части равны, сравниваем буквенные
        return string.Compare(xLetter, yLetter, StringComparison.Ordinal);
    }
}
