using System;
using System.Collections.Generic;

namespace AspenCreditUnion.api.Models
{
    /// <summary>
    /// Extension methods for payment-related functionality
    /// </summary>
    public static class PaymentExtensions
    {
        /// <summary>
        /// Calculates future payment dates based on loan payment frequency and payment day
        /// </summary>
        /// <param name="loan">The loan to calculate payment dates for</param>
        /// <param name="numberOfPayments">Number of future payments to calculate</param>
        /// <returns>List of projected payment dates</returns>
        public static List<DateTime> CalculatePaymentDates(this Loan loan, int numberOfPayments)
        {
            if (loan.NextPaymentDue == null)
            {
                throw new InvalidOperationException("Cannot calculate payment dates without NextPaymentDue date");
            }

            var paymentDates = new List<DateTime>();
            var currentDate = loan.NextPaymentDue.Value;
            paymentDates.Add(currentDate);

            for (int i = 1; i < numberOfPayments; i++)
            {
                currentDate = loan.PaymentFrequencyType switch
                {
                    PaymentFrequency.Daily => currentDate.AddDays(1),
                    PaymentFrequency.Weekly => currentDate.AddDays(7),
                    PaymentFrequency.BiWeekly => currentDate.AddDays(14),
                    PaymentFrequency.Monthly => AddMonths(currentDate, 1, loan.PaymentDay),
                    PaymentFrequency.Quarterly => AddMonths(currentDate, 3, loan.PaymentDay),
                    PaymentFrequency.SemiAnnually => AddMonths(currentDate, 6, loan.PaymentDay),
                    PaymentFrequency.Annually => AddMonths(currentDate, 12, loan.PaymentDay),
                    _ => throw new ArgumentOutOfRangeException(nameof(loan.PaymentFrequencyType))
                };
                paymentDates.Add(currentDate);
            }

            return paymentDates;
        }

        /// <summary>
        /// Add months while preserving the payment day if possible
        /// </summary>
        private static DateTime AddMonths(DateTime date, int months, int preferredDay)
        {
            var newDate = date.AddMonths(months);
            
            // Try to maintain the preferred payment day if possible
            if (preferredDay > 0 && preferredDay <= 28)
            {
                // Always safe to set a day between 1-28
                return new DateTime(newDate.Year, newDate.Month, preferredDay);
            }
            else if (preferredDay > 28)
            {
                // For 29, 30, 31 - get last day of month if needed
                var daysInMonth = DateTime.DaysInMonth(newDate.Year, newDate.Month);
                return new DateTime(newDate.Year, newDate.Month, Math.Min(preferredDay, daysInMonth));
            }
            
            // Default to just adding months if PaymentDay is not properly set
            return newDate;
        }
        
        /// <summary>
        /// Calculates payment schedule with principal and interest breakdown
        /// </summary>
        /// <param name="loan">The loan to calculate amortization for</param>
        /// <param name="numberOfPayments">Number of payments to project</param>
        /// <returns>List of payment projections with amount breakdowns</returns>
        public static List<PaymentProjection> CalculateAmortizationSchedule(this Loan loan, int numberOfPayments)
        {
            var schedule = new List<PaymentProjection>();
            var remainingPrincipal = loan.Principal;
            var paymentDates = CalculatePaymentDates(loan, numberOfPayments);
            
            // Convert annual rate to period rate based on payment frequency
            decimal periodRate = GetPeriodInterestRate(loan.InterestRate, loan.PaymentFrequencyType);
            
            for (int i = 0; i < paymentDates.Count; i++)
            {
                var interestPayment = remainingPrincipal * periodRate;
                var principalPayment = loan.MinimumPayment - interestPayment;
                
                // Handle final payment or balloon payment situations
                if (principalPayment > remainingPrincipal)
                {
                    principalPayment = remainingPrincipal;
                }
                
                var projection = new PaymentProjection
                {
                    PaymentNumber = i + 1,
                    PaymentDate = paymentDates[i],
                    TotalPayment = principalPayment + interestPayment,
                    PrincipalAmount = principalPayment,
                    InterestAmount = interestPayment,
                    RemainingPrincipal = remainingPrincipal - principalPayment
                };
                
                schedule.Add(projection);
                remainingPrincipal -= principalPayment;
                
                // Stop if loan is paid off
                if (remainingPrincipal <= 0)
                {
                    break;
                }
            }
            
            return schedule;
        }
        
        /// <summary>
        /// Converts annual interest rate to the period rate based on payment frequency
        /// </summary>
        private static decimal GetPeriodInterestRate(decimal annualRate, PaymentFrequency frequency)
        {
            // Convert from percentage to decimal
            decimal rate = annualRate / 100;
            
            return frequency switch
            {
                PaymentFrequency.Daily => rate / 365,
                PaymentFrequency.Weekly => rate / 52,
                PaymentFrequency.BiWeekly => rate / 26,
                PaymentFrequency.Monthly => rate / 12,
                PaymentFrequency.Quarterly => rate / 4,
                PaymentFrequency.SemiAnnually => rate / 2,
                PaymentFrequency.Annually => rate,
                _ => throw new ArgumentOutOfRangeException(nameof(frequency))
            };
        }
    }
    
    /// <summary>
    /// Represents a projected payment for amortization schedule
    /// </summary>
    public class PaymentProjection
    {
        public int PaymentNumber { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal TotalPayment { get; set; }
        public decimal PrincipalAmount { get; set; }
        public decimal InterestAmount { get; set; }
        public decimal RemainingPrincipal { get; set; }
    }
}
