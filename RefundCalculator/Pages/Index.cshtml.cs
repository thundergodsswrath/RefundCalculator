using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RefundCalculator.Enums;

namespace RefundCalculator.Pages;
[BindProperties]
public class IndexModel : PageModel
{
    
    public class InputModel
    {
        public CourseType CourseType { get; set; }
        public int CoursePrice { get; set; }
        public DateOnly CourseStartDate { get; set; }
        public PriceType PriceType { get; set; }

        public PriceState PriceState { get; set; }
    }

    public InputModel Input { get; set; }
    
    public int RefundPrice { get; set; }
    
    private readonly ILogger<IndexModel> _logger;

    
    
    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }
    
    public void OnGet()
    {
    }
    
    
    public IActionResult OnPost()
    {
        var courseType = Input.CourseType;
        var coursePrice = Input.CoursePrice;
        var courseStartDate = Input.CourseStartDate;
        var priceType = Input.PriceType;
        var selectOption = Input.PriceState;
        
        _logger.LogInformation($"Радиобатон: {selectOption}");
        _logger.LogInformation($"Цены курсу: {coursePrice}");
        _logger.LogInformation($"Предмет: {courseType}");
        _logger.LogInformation($"Дата начала курса: {courseStartDate}");
        _logger.LogInformation($"Тип цены: {priceType}");
        return Page();
    }
}