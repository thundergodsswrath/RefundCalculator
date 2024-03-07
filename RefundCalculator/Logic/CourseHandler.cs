using System.Collections.Immutable;
using RefundCalculator.Enums;

namespace RefundCalculator.Logic;

public class CourseHandler
{
    public CourseType CourseType { get; private set; }

    public bool IsIntensive { get; private set; }
    
    public CourseHandler(ref CourseType courseType, bool isIntensive)
    {
        CourseType = courseType;
        IsIntensive = isIntensive;
    }

    public List<DayOfWeek> GetClassesDays()
    {
        List<DayOfWeek> classesDays = new List<DayOfWeek>();
        switch (CourseType)
        {
            case CourseType.Hist:
            case CourseType.Bio:
                classesDays.AddRange([DayOfWeek.Monday, DayOfWeek.Thursday]);
                if (IsIntensive)
                {
                    classesDays.Add(DayOfWeek.Saturday);
                }
                break;
            case CourseType.Math:
                classesDays.AddRange([DayOfWeek.Tuesday, DayOfWeek.Friday]);
                if (IsIntensive)
                {
                    classesDays.Add(DayOfWeek.Sunday);
                }
                break;
            case CourseType.Eng:
                classesDays.AddRange([DayOfWeek.Wednesday, DayOfWeek.Sunday]);
                if (IsIntensive)
                {
                    classesDays.Add(DayOfWeek.Saturday);
                }
                break;
            case CourseType.Ukr:
                classesDays.AddRange([DayOfWeek.Wednesday, DayOfWeek.Friday]);
                if (IsIntensive)
                {
                    classesDays.Add(DayOfWeek.Sunday);
                }
                break;
            case CourseType.Lit:
                classesDays.Add(DayOfWeek.Saturday);
                break;
            case CourseType.Geo:
                classesDays.AddRange([DayOfWeek.Tuesday, DayOfWeek.Sunday]);
                if (IsIntensive)
                {
                    classesDays.Add(DayOfWeek.Saturday);
                }
                break;
        }

        return classesDays;
    }

    public int GetClassesPerMonthAmount(bool isIntensive)
    {
        int classesPerMonthAmount;
        if (isIntensive)
        {
            classesPerMonthAmount = CourseType switch
            {
                CourseType.Lit => 5,
                CourseType.Geo => 16,
                CourseType.Math or CourseType.Ukr => 15,
                _ => 14
            };
        }
        else
        {
            classesPerMonthAmount = CourseType == CourseType.Lit ? 5 : 9;
        }
        return classesPerMonthAmount;
    }
}