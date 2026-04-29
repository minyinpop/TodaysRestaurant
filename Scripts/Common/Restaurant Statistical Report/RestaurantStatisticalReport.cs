using System;

namespace Common.Restaurant_Statistical_Report
{
    public class RestaurantStatisticalReport
    {
        private int _totalCustomerCount;
        public int TotalCustomerCount
        {
            get => _totalCustomerCount;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("累加的參數不能低於 1");
                }
                
                _totalCustomerCount = value;
            }
        }
        
        private int _happyCustomerCount;
        public int HappyCustomerCount
        {
            get => _happyCustomerCount;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("累加的參數不能低於 1");
                }
                
                _happyCustomerCount = value;
            }
        }
        
        private int _angryCustomerCount;
        public int AngryCustomerCount
        {
            get => _angryCustomerCount;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("累加的參數不能低於 1");
                }
                
                _angryCustomerCount = value;
            }
        }
        
        private int _earnedCount ;
        public int EarnedCount
        {
            get => _earnedCount ;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("累加的參數不能低於 1");
                }
                
                _earnedCount = value;
            }
        }
    }
}