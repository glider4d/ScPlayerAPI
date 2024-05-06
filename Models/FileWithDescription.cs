using Microsoft.AspNetCore.Routing.Constraints;

namespace ScPlayerAPI.Models
{
    public class FileWithDescription
    {
        private string fileName = string.Empty;
        public string FileName { get { return fileName; } 
                                 set { 
                                        fileName = value;
            
                                 } 
        }
        public string Description { get; set; } = string.Empty;
        public long FileSize { get; set; } = 0;
    }
}
