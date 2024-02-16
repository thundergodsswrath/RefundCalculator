using System.Collections.Immutable;
using RefundCalculator.Enums;

namespace RefundCalculator.Logic;

public class CourseHandler
{
    public CourseType CourseType { get; private set; }

    public CourseHandler(ref CourseType courseType)
    {
        CourseType = courseType;
    }

    public List<DayOfWeek> GetClassesDays(bool isIntensive = false)
    {
        List<DayOfWeek> classesDays = new List<DayOfWeek>();
        switch (CourseType)
        {
            case CourseType.Hist:
            case CourseType.Bio:
                classesDays.AddRange([DayOfWeek.Monday, DayOfWeek.Thursday]);
                if (isIntensive)
                {
                    classesDays.Add(DayOfWeek.Saturday);
                }
                break;
            case CourseType.Math:
                classesDays.AddRange([DayOfWeek.Tuesday, DayOfWeek.Friday]);
                if (isIntensive)
                {
                    classesDays.Add(DayOfWeek.Sunday);
                }
                break;
            case CourseType.Eng:
                classesDays.AddRange([DayOfWeek.Wednesday, DayOfWeek.Sunday]);
                if (isIntensive)
                {
                    classesDays.Add(DayOfWeek.Saturday);
                }
                break;
            case CourseType.Ukr:
                classesDays.AddRange([DayOfWeek.Wednesday, DayOfWeek.Friday]);
                if (isIntensive)
                {
                    classesDays.Add(DayOfWeek.Sunday);
                }
                break;
            case CourseType.Lit:
                classesDays.Add(DayOfWeek.Saturday);
                break;
            case CourseType.Geo:
                classesDays.AddRange([DayOfWeek.Tuesday, DayOfWeek.Wednesday]);
                if (isIntensive)
                {
                    classesDays.Add(DayOfWeek.Saturday);
                }
                break;
        }

        return classesDays;
    }
}