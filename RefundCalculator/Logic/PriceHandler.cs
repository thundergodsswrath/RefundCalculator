using RefundCalculator.Enums;

namespace RefundCalculator.Logic;

public class PriceHandler
{
    private int CoursePrice { get; set; }

    private CourseType CourseType { get; set; }
    private PriceState PriceState { get; set; }

    private CourseHandler CourseHandler { get; set; }

    private DateHandler DateHandler { get; set; }

    private PriceType PriceType { get; set; }

    public PriceHandler(int coursePrice, PriceType priceType, PriceState priceState, DateOnly courseStartDate,
        CourseType courseType)
    {
        CoursePrice = coursePrice;
        PriceState = priceState;
        PriceType = priceType;
        CourseType = courseType;
        CourseHandler = new CourseHandler(ref courseType, isIntensive: PriceState == PriceState.IntensivePrice);
        DateHandler = new DateHandler(ref courseStartDate, classesDays: CourseHandler.GetClassesDays());
    }

    private float GetPricePerClass()
    {
        int classesPerMonthAmount = CourseHandler.GetClassesPerMonthAmount();
        float pricePerClass;
        

        if (IsFullCourse())
        {
            float priceForMonth = 0;
            switch (CourseType)
        {
            case CourseType.Lit:
                switch (PriceState)
                {
                    case PriceState.Old:
                        switch (PriceType)
                        {
                            case PriceType.Lowest:
                                priceForMonth = 290f;
                                break;
                            case PriceType.Mid:
                                priceForMonth = 340f;
                                break;
                            case PriceType.Highest:
                                priceForMonth = 390f;
                                break;
                        }

                        break;
                    case PriceState.FirstIncrease:
                    case PriceState.IntensivePrice:
                        switch (PriceType)
                        {
                            case PriceType.Lowest:
                                priceForMonth = 290f;
                                break;
                            case PriceType.Mid:
                                priceForMonth = 390f;
                                break;
                            case PriceType.Highest:
                                priceForMonth = 490f;
                                break;
                        }
                        break;
                }
                break;
            default:
                switch (PriceState)
                {
                    case PriceState.Old:
                        switch (PriceType)
                        {
                            case PriceType.Lowest:
                                priceForMonth = 690f;
                                break;
                            case PriceType.Mid:
                                priceForMonth = 740f;
                                break;
                            case PriceType.Highest:
                                priceForMonth = 790f;
                                break;
                        }

                        break;
                    case PriceState.FirstIncrease:
                        switch (PriceType)
                        {
                            case PriceType.Lowest:
                                priceForMonth = 690f;
                                break;
                            case PriceType.Mid:
                                priceForMonth = 790f;
                                break;
                            case PriceType.Highest:
                                priceForMonth = 890f;
                                break;
                        }

                        break;

                    case PriceState.IntensivePrice:
                        switch (PriceType)
                        {
                            case PriceType.Lowest:
                                priceForMonth = 790f;
                                break;
                            case PriceType.Mid:
                                priceForMonth = 890f;
                                break;
                            case PriceType.Highest:
                                priceForMonth = 990f;
                                break;
                        }

                        break;
                }
                break;
        }
            pricePerClass = priceForMonth / classesPerMonthAmount;
        }
        else
        {
            pricePerClass = (float)CoursePrice / classesPerMonthAmount;
        }
        return pricePerClass;
    }

    private bool IsFullCourse()
    {
        bool fullLit = CourseType == CourseType.Lit && CoursePrice > 490;
        bool fullOther = CoursePrice > 990;
        return fullLit || fullOther;
    }

    public float CalculateRefund()
    {
        float pricePerClass = GetPricePerClass();
        int amountOfClasses = DateHandler.GetAmountOfClasses(CourseHandler.GetClassesPerMonthAmount(), IsFullCourse());
        float refund = CoursePrice - pricePerClass * amountOfClasses;
        return refund;
    }
}