namespace pos_simple.DTO
{
    public class SalesReportDto
    {
        public decimal Today { get; set; }
        public decimal Yesterday { get; set; }
        public decimal Week { get; set; }
        public decimal LastWeek { get; set; }
        public decimal Month { get; set; }
        public decimal LastMonth { get; set; }
        public decimal TodayCash { get; set; }
        public decimal TodayDebit { get; set; }

        public int TotalItemsSoldToday { get; set; }
        public int TotalItemsSoldThisMonth { get; set; }
    }
}
