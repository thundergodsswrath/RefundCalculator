namespace RefundCalculator.Logic;

public class DateHandler
{
    public DateOnly CourseStartDate { get; private set; }
    private readonly DateOnly _today = DateOnly.FromDateTime(DateTime.Today);
    public List<DayOfWeek> ClassesDays { get; private set; }

    public DateHandler(ref DateOnly courseStartDate, List<DayOfWeek> classesDays)
    {
        CourseStartDate = courseStartDate;
        ClassesDays = classesDays;
    }

    private int GetFullMonthAmount()
    {
        int monthsAmount = (_today.Year - CourseStartDate.Year) * 12 + _today.Month - CourseStartDate.Month;
        if (_today.Day < CourseStartDate.Day)
        {
            monthsAmount--;
        }
        if (monthsAmount < 0)
        {
            monthsAmount = 0;
        }
        return monthsAmount;
    }

    public int GetAmountOfClasses(int classesPerMonth, bool isFullCourse=false)
    {
        int amountOfClasses = 0;

        foreach (var day in ClassesDays)
        {
            for (var date = CourseStartDate; date < _today; date = date.AddDays(1))
            {
                if (date.DayOfWeek == day)
                {
                    amountOfClasses++;
                }
            }
        }

        if (!isFullCourse)
        {
            amountOfClasses -= GetFullMonthAmount() * classesPerMonth;
        }
        return amountOfClasses;
    }
}