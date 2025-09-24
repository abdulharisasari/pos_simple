namespace pos_simple.DTO
{
    public class ApiResponse<T>
    {
        public int Code { get; set; }           
        public string Message { get; set; }     
        public T? Data { get; set; }            
    }
}


namespace pos_simple.DTO
{
    public class ApiResponseWithoutData
    {
        public int Code { get; set; }       
        public string Message { get; set; } 
    }
}
