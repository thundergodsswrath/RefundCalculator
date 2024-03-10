using RefundCalculator.Enums;

namespace RefundCalculator.Logic;

public class DateHandler
{
    public DateOnly CourseStartDate { get; private set; }
    private readonly DateOnly _today;
    private readonly DateOnly _intensiveStartDate;
    private readonly CourseHandler _courseHandler;
    public List<DayOfWeek> ClassesDays { get; private set; }

    public DateHandler(ref DateOnly courseStartDate, CourseHandler courseHandler)
    {
        CourseStartDate = courseStartDate;
        _courseHandler = courseHandler;
        ClassesDays = _courseHandler.GetClassesDays();
        _today =  DateOnly.FromDateTime(DateTime.Today);
        _intensiveStartDate = new DateOnly(2024, 3, 2);
    }

    private int GetAmountOfClassesFromDateToDate(DateOnly startDate, DateOnly endDate)
    {
        int amountOfClasses = 0;

        foreach (var day in ClassesDays)
        {
            if (_courseHandler.IsIntensive && startDate < _intensiveStartDate && _courseHandler.CourseType != CourseType.Lit)
            {
                startDate = day == ClassesDays[2] ? _intensiveStartDate : startDate;
            }
            
            for (var date = startDate; date < endDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek == day)
                {
                    amountOfClasses++;
                }
            }
        }

        return amountOfClasses;
    }
    
    public int GetAmountOfClasses(bool isFullCourse=false)
    {
        int amountOfClasses = GetAmountOfClassesFromDateToDate(CourseStartDate, _today);
        
        if (!isFullCourse)
        {
            DateOnly endOfPreviousMonth;
            if (_today.Day>CourseStartDate.Day)
            {
                endOfPreviousMonth = new DateOnly(_today.Year, _today.Month, CourseStartDate.Day);
            }
            else
            {
                int previousMonthNumber = _today.Month - 1;
                int previousYearNumber = _today.Year;
                if (previousMonthNumber == 0)
                {
                    previousMonthNumber = 12;
                    previousYearNumber -= 1;
                }
                endOfPreviousMonth = new DateOnly(previousYearNumber, previousMonthNumber, CourseStartDate.Day);
            }
            amountOfClasses -= GetAmountOfClassesFromDateToDate(CourseStartDate,
                endOfPreviousMonth);
        }
        return amountOfClasses;
    }
}