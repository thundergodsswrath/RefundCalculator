using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RefundCalculator.Enums;
using RefundCalculator.Logic;

namespace RefundCalculator.Pages;
[BindProperties]
public class IndexModel : PageModel
{
    
    public class InputModel
    {
        public CourseType CourseType { get; set; }
        [Required(ErrorMessage = "Введіть ціну!")]
        [Range(0, 19999, ErrorMessage = "Значення повинно бути від 0 до 19999!")]
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
        var priceState = Input.PriceState;

        var priceHandler = new PriceHandler(coursePrice,priceType, priceState, courseStartDate, courseType);
        RefundPrice = Convert.ToInt32(priceHandler.CalculateRefund());
        
        _logger.LogInformation($"Радиобатон: {priceState}");
        _logger.LogInformation($"Цены курсу: {coursePrice}");
        _logger.LogInformation($"Предмет: {courseType}");
        _logger.LogInformation($"Дата начала курса: {courseStartDate}");
        _logger.LogInformation($"Тип цены: {priceType}");
        return Page();
    }
}