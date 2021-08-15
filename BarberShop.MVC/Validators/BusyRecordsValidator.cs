using System;
using BarberShop.BLL.Models;
using FluentValidation;

namespace BarberShop.MVC.Validators
{
    public class BusyRecordsValidator: AbstractValidator<BusyRecord>
    {
        public BusyRecordsValidator()
        {
            RuleFor(x => x.RecordTime).NotEmpty().Must(NotLessThanNow).WithMessage("" +
                "Please record to future date");
        }

        private bool NotLessThanNow(DateTime date)
        {
            return date > DateTime.Now;
        }
    }
}
